using System;
using System.Collections.Generic;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ChatHistoryClearer
	{
		public static void ClearChatHistory(AbstractLogger logger, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, long chatThreadId, Action successCallback, Action failureCallback)
		{
			try
			{
				ClearMemberChatHistoryRequest clearMemberChatHistoryRequest = new ClearMemberChatHistoryRequest();
				clearMemberChatHistoryRequest.ChatThreadIds = new List<long?> { chatThreadId };
				ClearMemberChatHistoryRequest request = clearMemberChatHistoryRequest;
				IWebCall<ClearMemberChatHistoryRequest, ClearMemberChatHistoryResponse> webCall = mixWebCallFactory.ChatThreadMembershipHistoryDeletePost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<ClearMemberChatHistoryResponse> e)
				{
					ClearMemberChatHistoryResponse response = e.Response;
					if (NotificationValidator.Validate(response.Notification))
					{
						notificationQueue.Dispatch(response.Notification, successCallback, failureCallback);
					}
					else
					{
						logger.Critical("Failed to validate clear chat history response: " + JsonParser.ToJson(response));
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
