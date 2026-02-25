using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class MassPushNotificationSender
	{
		public static void SendMassPushNotification(AbstractLogger logger, IMixWebCallFactory mixWebCallFactory, Action<bool> callback)
		{
			try
			{
				BaseUserRequest request = new BaseUserRequest();
				IWebCall<BaseUserRequest, BaseResponse> webCall = mixWebCallFactory.IntegrationTestSupportPushNotificationMassSendPost(request);
				webCall.OnResponse += delegate
				{
					callback(true);
				};
				webCall.OnError += delegate
				{
					callback(false);
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(false);
			}
		}
	}
}
