using System;
using System.Collections.Generic;
using System.Text;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class MixWebCall<TRequest, TResponse> : IDisposable, IWebCall<TRequest, TResponse> where TRequest : BaseUserRequest where TResponse : BaseResponse, new()
	{
		private const int MaxTimeoutAttempts = 3;

		private const int MaxServerErrorAttempts = 1;

		private readonly AbstractLogger logger;

		private readonly Uri uri;

		private readonly string body;

		private readonly HttpMethod method;

		private readonly Dictionary<string, string> headers;

		private readonly IWwwCallFactory wwwCallFactory;

		private readonly long latencyWwwCallTimeout;

		private readonly long maxWwwCallTimeout;

		private readonly IDatabase database;

		private readonly string swid;

		private readonly StringBuilder timeoutLogs;

		private IWwwCall wwwCall;

		private int numAttempts;

		private byte[] bodyBytes;

		public WebCallRefreshStatus RefreshStatus { get; set; }

		public IWebCallEncryptor WebCallEncryptor { private get; set; }

		public event EventHandler<WebCallEventArgs<TResponse>> OnResponse = delegate
		{
		};

		public event EventHandler<WebCallErrorEventArgs> OnError = delegate
		{
		};

		public event EventHandler<WebCallUnauthorizedEventArgs> OnUnauthorized = delegate
		{
		};

		public event EventHandler<WebCallUnqueuedEventArgs> OnUnqueued = delegate
		{
		};

		public MixWebCall(AbstractLogger logger, Uri uri, string body, HttpMethod method, Dictionary<string, string> headers, IWwwCallFactory wwwCallFactory, IWebCallEncryptor webCallEncryptor, long latencyWwwCallTimeout, long maxWwwCallTimeout, IDatabase database, string swid)
		{
			this.logger = logger;
			this.uri = uri;
			this.body = body;
			this.method = method;
			this.headers = headers;
			this.wwwCallFactory = wwwCallFactory;
			this.latencyWwwCallTimeout = latencyWwwCallTimeout;
			this.maxWwwCallTimeout = maxWwwCallTimeout;
			this.database = database;
			this.swid = swid;
			WebCallEncryptor = webCallEncryptor;
			RefreshStatus = WebCallRefreshStatus.NotRefreshing;
			timeoutLogs = new StringBuilder();
		}

		public void Dispose()
		{
			if (wwwCall != null)
			{
				wwwCall.Dispose();
			}
		}

		public void SetHeader(string key, string val)
		{
			headers[key] = val;
		}

		public void Execute()
		{
			Execute(false);
		}

		public void Execute(bool force)
		{
			if (RefreshStatus == WebCallRefreshStatus.WaitingForRefreshCallback)
			{
				if (!force)
				{
					return;
				}
				RefreshStatus = WebCallRefreshStatus.NotRefreshing;
			}
			if (force)
			{
				this.OnUnqueued(this, new WebCallUnqueuedEventArgs());
			}
			SessionDocument sessionDocument = database.GetSessionDocument(swid);
			if (!force && sessionDocument != null && sessionDocument.ProtocolVersion != 3)
			{
				this.OnUnauthorized(this, new WebCallUnauthorizedEventArgs("UNAUTHORIZED_MIX_SESSION"));
				return;
			}
			numAttempts++;
			bodyBytes = Encoding.UTF8.GetBytes(body);
			bodyBytes = WebCallEncryptor.Encrypt(bodyBytes);
			wwwCall = wwwCallFactory.Create(uri, method, bodyBytes, headers, latencyWwwCallTimeout, maxWwwCallTimeout);
			wwwCall.OnDone += HandleWwwDone;
			wwwCall.Execute();
		}

		private void HandleWwwDone(object sender, WwwDoneEventArgs e)
		{
			wwwCall.OnDone -= HandleWwwDone;
			bool flag = false;
			string error = wwwCall.Error;
			if (!string.IsNullOrEmpty(error))
			{
				string text = error.ToLower();
				flag = text == "couldn't connect to host" || text.Contains("timedout") || text.Contains("timed out");
			}
			if (flag)
			{
				string responsePlaintext = GetResponsePlaintext(wwwCall.ResponseBody);
				string value = HttpLogBuilder.BuildTimeoutLog(wwwCall.RequestId, uri, method, wwwCall.ResponseHeaders, responsePlaintext, wwwCall.TimeToStartUpload, wwwCall.TimeToFinishUpload, wwwCall.PercentUploaded, wwwCall.TimeToStartDownload, wwwCall.TimeToFinishDownload, wwwCall.PercentDownloaded, wwwCall.TimeoutReason, wwwCall.TimeoutTime);
				timeoutLogs.Append(value);
				timeoutLogs.Append("\n\n");
				wwwCall.Dispose();
				if (numAttempts > 3)
				{
					logger.Critical("Too many timeouts: " + uri.AbsoluteUri + "\nPrevious logs:\n" + timeoutLogs);
					DispatchError("Too many timeouts");
				}
				else
				{
					Execute();
				}
				return;
			}
			uint statusCode = wwwCall.StatusCode;
			if (statusCode >= 500 && statusCode <= 599)
			{
				string responsePlaintext2 = GetResponsePlaintext(wwwCall.ResponseBody);
				string text2 = HttpLogBuilder.BuildResponseLog(wwwCall.RequestId, uri, method, wwwCall.ResponseHeaders, responsePlaintext2, statusCode);
				wwwCall.Dispose();
				if (numAttempts > 1)
				{
					logger.Critical("Too many server errors:\n" + text2);
					DispatchError(statusCode + " Server error: " + wwwCall.Error);
				}
				else
				{
					Execute();
				}
				return;
			}
			bool flag2 = statusCode >= 200 && statusCode <= 299;
			if (statusCode >= 400 && statusCode <= 499)
			{
				string responseBody;
				try
				{
					responseBody = GetResponseText(wwwCall.ResponseBody);
				}
				catch (Exception)
				{
					responseBody = GetResponsePlaintext(wwwCall.ResponseBody);
				}
				if (statusCode == 429)
				{
					this.OnError(this, new WebCallErrorEventArgs(statusCode + " Too many requests", "THROTTLED"));
					return;
				}
				string status = GetStatus(responseBody);
				if (statusCode == 401)
				{
					wwwCall.Dispose();
					this.OnUnauthorized(this, new WebCallUnauthorizedEventArgs(status));
				}
				else
				{
					this.OnError(this, new WebCallErrorEventArgs(statusCode + " Client error = " + wwwCall.Error, status));
					wwwCall.Dispose();
				}
			}
			else if (flag2)
			{
				string responseText;
				try
				{
					responseText = GetResponseText(wwwCall.ResponseBody);
				}
				catch (Exception ex2)
				{
					string responsePlaintext3 = GetResponsePlaintext(wwwCall.ResponseBody);
					logger.Critical(string.Concat("Error getting response body:\n", ex2, "\n", HttpLogBuilder.BuildResponseLog(wwwCall.RequestId, uri, method, wwwCall.ResponseHeaders, responsePlaintext3, statusCode)));
					DispatchError(statusCode + " Couldn't get response body text");
					wwwCall.Dispose();
					return;
				}
				TResponse val = (TResponse)null;
				try
				{
					val = JsonParser.FromJson<TResponse>(responseText);
				}
				catch (Exception ex3)
				{
					logger.Critical(string.Concat("Error parsing response body: ", ex3, "\n", responseText));
				}
				if (val != null && val.Status == "OK")
				{
					this.OnResponse(this, new WebCallEventArgs<TResponse>(val));
				}
				else
				{
					DispatchError(statusCode + " Response JSON is invalid or indicates an error: " + responseText);
				}
				wwwCall.Dispose();
			}
			else
			{
				bool flag3 = false;
				if (!string.IsNullOrEmpty(error))
				{
					string text3 = error.ToLower();
					flag3 = text3.Contains("connection appears to be offline") || text3.Contains("connection was lost") || text3.Contains("network is unreachable");
				}
				if (!flag3)
				{
					logger.Critical("Other web call error = " + wwwCall.Error);
				}
				DispatchError(statusCode + " Other web call error = " + wwwCall.Error);
				wwwCall.Dispose();
			}
		}

		private string GetStatus(string responseBody)
		{
			try
			{
				TResponse val = JsonParser.FromJson<TResponse>(responseBody);
				return val.Status;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public void DispatchError(string message)
		{
			this.OnError(this, new WebCallErrorEventArgs(message, null));
		}

		private string GetResponseText(byte[] responseBody)
		{
			if (responseBody == null)
			{
				return string.Empty;
			}
			byte[] bytes = WebCallEncryptor.Decrypt(responseBody);
			return Encoding.UTF8.GetString(bytes);
		}

		private static string GetResponsePlaintext(byte[] responseBody)
		{
			return (responseBody != null) ? Encoding.UTF8.GetString(responseBody) : string.Empty;
		}
	}
}
