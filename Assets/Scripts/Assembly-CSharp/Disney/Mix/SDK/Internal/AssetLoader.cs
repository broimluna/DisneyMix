using System;
using System.Collections.Generic;
using System.Text;

namespace Disney.Mix.SDK.Internal
{
	public class AssetLoader : IAssetLoader
	{
		private enum WwwCallResult
		{
			Success = 0,
			TooManyTimeoutsError = 1,
			TooManyServerErrors = 2,
			ClientError = 3,
			OfflineError = 4,
			RetryTimeout = 5,
			RetryServerError = 6
		}

		private const long DefaultLatencyWwwCallTimeout = 10000L;

		private const long DefaultMaxWwwCallTimeout = 30000L;

		private const int MaxTimeoutAttempts = 3;

		private const int MaxServerErrorAttempts = 1;

		private static readonly Dictionary<string, string> emptyHeaders = new Dictionary<string, string>();

		private readonly AbstractLogger logger;

		private readonly IWwwCallFactory wwwCallFactory;

		public AssetLoader(AbstractLogger logger, IWwwCallFactory wwwCallFactory)
		{
			this.logger = logger;
			this.wwwCallFactory = wwwCallFactory;
		}

		public void Load(string url, Action<LoadAssetResult> callback)
		{
			Uri uri = new Uri(url);
			StringBuilder timeoutLogs = new StringBuilder();
			Load(uri, callback, timeoutLogs, 1);
		}

		private void Load(Uri uri, Action<LoadAssetResult> callback, StringBuilder timeoutLogs, int numAttempts)
		{
			IWwwCall wwwCall = wwwCallFactory.Create(uri, HttpMethod.GET, null, emptyHeaders, 10000L, 30000L);
			EventHandler<WwwDoneEventArgs> handleOnDone = null;
			handleOnDone = delegate
			{
				wwwCall.OnDone -= handleOnDone;
				Dictionary<string, string> responseHeaders = wwwCall.ResponseHeaders;
				uint statusCode = wwwCall.StatusCode;
				WwwCallResult result = GetResult(wwwCall.Error, statusCode, numAttempts);
				string log = BuildResponseLog(uri, result, responseHeaders, statusCode, wwwCall);
				byte[] responseBody = wwwCall.ResponseBody;
				HandleResult(result, log, responseBody, uri, timeoutLogs, numAttempts, callback, Load, logger);
				wwwCall.Dispose();
			};
			wwwCall.OnDone += handleOnDone;
			wwwCall.Execute();
		}

		private static string BuildResponseLog(Uri uri, WwwCallResult result, Dictionary<string, string> headers, uint statusCode, IWwwCall wwwCall)
		{
			return (result != WwwCallResult.RetryTimeout) ? HttpLogBuilder.BuildResponseLog(wwwCall.RequestId, uri, HttpMethod.GET, headers, string.Empty, statusCode) : HttpLogBuilder.BuildTimeoutLog(wwwCall.RequestId, uri, HttpMethod.GET, headers, string.Empty, wwwCall.TimeToStartUpload, wwwCall.TimeToFinishUpload, wwwCall.PercentUploaded, wwwCall.TimeToStartDownload, wwwCall.TimeToFinishDownload, wwwCall.PercentDownloaded, wwwCall.TimeoutReason, wwwCall.TimeoutTime);
		}

		private static string BuildRequestLog(Uri uri, int requestId)
		{
			return HttpLogBuilder.BuildRequestLog(requestId, uri, HttpMethod.GET, emptyHeaders, string.Empty);
		}

		private static WwwCallResult GetResult(string error, uint statusCode, int numAttempts)
		{
			return IsTimeout(error) ? ((numAttempts > 3) ? WwwCallResult.TooManyTimeoutsError : WwwCallResult.RetryTimeout) : (IsServerError(statusCode) ? ((numAttempts <= 1) ? WwwCallResult.RetryServerError : WwwCallResult.TooManyServerErrors) : (IsClientError(statusCode) ? WwwCallResult.ClientError : ((!IsSuccess(statusCode)) ? ((!IsOfflineError(error)) ? WwwCallResult.ClientError : WwwCallResult.OfflineError) : WwwCallResult.Success)));
		}

		private static void HandleResult(WwwCallResult result, string log, byte[] bytes, Uri uri, StringBuilder timeoutLogs, int numAttempts, Action<LoadAssetResult> callback, Action<Uri, Action<LoadAssetResult>, StringBuilder, int> retry, AbstractLogger logger)
		{
			switch (result)
			{
			case WwwCallResult.Success:
				callback(new LoadAssetResult(true, bytes));
				break;
			case WwwCallResult.TooManyTimeoutsError:
				logger.Critical("Too many timeouts: " + uri.AbsoluteUri + "\nPrevious logs:\n" + timeoutLogs);
				callback(new LoadAssetResult(false, null));
				break;
			case WwwCallResult.TooManyServerErrors:
				logger.Critical("Too many server errors:\n" + log);
				callback(new LoadAssetResult(false, null));
				break;
			case WwwCallResult.RetryTimeout:
				timeoutLogs.Append(log);
				timeoutLogs.Append("\n\n");
				retry(uri, callback, timeoutLogs, numAttempts + 1);
				break;
			case WwwCallResult.RetryServerError:
				retry(uri, callback, timeoutLogs, numAttempts + 1);
				break;
			case WwwCallResult.OfflineError:
				callback(new LoadAssetResult(false, null));
				break;
			case WwwCallResult.ClientError:
				logger.Critical(log);
				callback(new LoadAssetResult(false, null));
				break;
			}
		}

		private static bool IsTimeout(string error)
		{
			if (!string.IsNullOrEmpty(error))
			{
				string text = error.ToLower();
				return text == "couldn't connect to host" || text.Contains("timedout") || text.Contains("timed out");
			}
			return false;
		}

		private static bool IsServerError(uint statusCode)
		{
			return statusCode >= 500 && statusCode <= 599;
		}

		private static bool IsClientError(uint statusCode)
		{
			return statusCode >= 400 && statusCode <= 499;
		}

		private static bool IsOfflineError(string error)
		{
			if (!string.IsNullOrEmpty(error))
			{
				string text = error.ToLower();
				return text.Contains("connection appears to be offline") || text.Contains("connection was lost") || text.Contains("network is unreachable");
			}
			return false;
		}

		private static bool IsSuccess(uint statusCode)
		{
			return statusCode >= 200 && statusCode <= 299;
		}
	}
}
