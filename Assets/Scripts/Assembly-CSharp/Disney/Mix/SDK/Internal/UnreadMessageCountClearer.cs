using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class UnreadMessageCountClearer
	{
		public static void ClearUnreadMessageCount(AbstractLogger logger, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, long chatThreadId, Action successCallback, Action failureCallback)
		{
			try
			{
				ClearUnreadMessageCountRequest clearUnreadMessageCountRequest = new ClearUnreadMessageCountRequest();
				clearUnreadMessageCountRequest.ChatThreadId = chatThreadId;
				ClearUnreadMessageCountRequest request = clearUnreadMessageCountRequest;
				IWebCall<ClearUnreadMessageCountRequest, ClearUnreadMessageCountResponse> webCall = mixWebCallFactory.ChatThreadMembershipUnreadMessageCountDeletePost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<ClearUnreadMessageCountResponse> e)
				{
					ClearUnreadMessageCountResponse response = e.Response;
					if (NotificationValidator.Validate(response.Notification))
					{
						notificationQueue.Dispatch(response.Notification, successCallback, failureCallback);
					}
					else
					{
						logger.Critical("Failed to validate clear unread message count response: " + JsonParser.ToJson(response));
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
	}
}
