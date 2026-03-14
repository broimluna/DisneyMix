using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ChatThreadNicknameRemover
	{
		public static void RemoveNickname(AbstractLogger logger, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, long chatThreadId, Action successCallback, Action failureCallback)
		{
			try
			{
				RemoveChatThreadNicknameRequest removeChatThreadNicknameRequest = new RemoveChatThreadNicknameRequest();
				removeChatThreadNicknameRequest.ChatThreadId = chatThreadId;
				RemoveChatThreadNicknameRequest request = removeChatThreadNicknameRequest;
				IWebCall<RemoveChatThreadNicknameRequest, RemoveChatThreadNicknameResponse> webCall = mixWebCallFactory.ChatThreadNicknameDeletePost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<RemoveChatThreadNicknameResponse> e)
				{
					RemoveChatThreadNicknameResponse response = e.Response;
					if (NotificationValidator.Validate(response.Notification))
					{
						notificationQueue.Dispatch(response.Notification, successCallback, failureCallback);
						successCallback();
					}
					else
					{
						logger.Critical("Failed to validate remove chat thread nickname response: " + JsonParser.ToJson(response));
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
