using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class VideoFlavorUrlGetter : IVideoFlavorUrlGetter
	{
		private readonly AbstractLogger logger;

		public VideoFlavorUrlGetter(AbstractLogger logger)
		{
			this.logger = logger;
		}

		public void Get(string videoId, string videoFlavorId, IMixWebCallFactory mixWebCallFactory, Action<GetVideoUrlResponse> successCallback, Action failureCallback)
		{
			try
			{
				GetVideoUrlRequest getVideoUrlRequest = new GetVideoUrlRequest();
				getVideoUrlRequest.VideoId = videoId;
				getVideoUrlRequest.VideoFlavorId = videoFlavorId;
				GetVideoUrlRequest request = getVideoUrlRequest;
				IWebCall<GetVideoUrlRequest, GetVideoUrlResponse> webCall = mixWebCallFactory.VideoUrlPost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<GetVideoUrlResponse> e)
				{
					GetVideoUrlResponse response = e.Response;
					if (ValidateResponse(response))
					{
						successCallback(e.Response);
					}
					else
					{
						logger.Critical("Failed to validate add followship response: " + JsonParser.ToJson(response));
						failureCallback();
					}
				};
				webCall.OnError += delegate
				{
					failureCallback();
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback();
			}
		}

		private static bool ValidateResponse(GetVideoUrlResponse response)
		{
			return response.Url != null;
		}
	}
}
