using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class PresenceSetter
	{
		public static void SetAway(AbstractLogger logger, IMixWebCallFactory mixWebCallFactory, Action<BaseResponse> successCallback, Action failureCallback)
		{
			try
			{
				SetPresenceRequest setPresenceRequest = new SetPresenceRequest();
				setPresenceRequest.State = "away";
				SetPresenceRequest request = setPresenceRequest;
				IWebCall<SetPresenceRequest, BaseResponse> webCall = mixWebCallFactory.PresencePut(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<BaseResponse> e)
				{
					BaseResponse response = e.Response;
					successCallback(response);
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
	}
}
