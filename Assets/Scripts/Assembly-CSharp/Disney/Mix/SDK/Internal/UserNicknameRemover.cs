using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class UserNicknameRemover
	{
		public static void RemoveNickname(AbstractLogger logger, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, string friendSwid, Action successCallback, Action failureCallback)
		{
			try
			{
				RemoveNicknameRequest removeNicknameRequest = new RemoveNicknameRequest();
				removeNicknameRequest.NicknamedUserId = friendSwid;
				RemoveNicknameRequest request = removeNicknameRequest;
				IWebCall<RemoveNicknameRequest, RemoveNicknameResponse> webCall = mixWebCallFactory.NicknameDeletePost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<RemoveNicknameResponse> e)
				{
					RemoveNicknameResponse response = e.Response;
					if (NotificationValidator.Validate(response.Notification))
					{
						notificationQueue.Dispatch(response.Notification, successCallback, failureCallback);
					}
					else
					{
						logger.Critical("Failed to validate remove nickname response: " + JsonParser.ToJson(response));
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
