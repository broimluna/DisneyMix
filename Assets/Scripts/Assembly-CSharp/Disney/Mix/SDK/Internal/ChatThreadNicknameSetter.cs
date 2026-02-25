using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ChatThreadNicknameSetter
	{
		public static void SetNickname(AbstractLogger logger, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, string nickname, long chatThreadId, Action successCallback, Action failureCallback)
		{
			try
			{
				AddChatThreadNicknameRequest addChatThreadNicknameRequest = new AddChatThreadNicknameRequest();
				addChatThreadNicknameRequest.Nickname = nickname;
				addChatThreadNicknameRequest.ChatThreadId = chatThreadId;
				AddChatThreadNicknameRequest request = addChatThreadNicknameRequest;
				IWebCall<AddChatThreadNicknameRequest, AddChatThreadNicknameResponse> webCall = mixWebCallFactory.ChatThreadNicknamePut(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<AddChatThreadNicknameResponse> e)
				{
					AddChatThreadNicknameResponse response = e.Response;
					if (NotificationValidator.Validate(response.Notification))
					{
						notificationQueue.Dispatch(response.Notification, successCallback, failureCallback);
					}
					else
					{
						logger.Critical("Failed to validate set chat thread nickname response: " + JsonParser.ToJson(response));
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
