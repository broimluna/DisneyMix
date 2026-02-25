using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class UserNicknameSetter
	{
		public static void SetNickname(AbstractLogger logger, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, string friendSwid, string nickname, Action successCallback, Action failureCallback)
		{
			try
			{
				AddNicknameRequest addNicknameRequest = new AddNicknameRequest();
				addNicknameRequest.NicknamedUserId = friendSwid;
				addNicknameRequest.Nickname = nickname;
				AddNicknameRequest request = addNicknameRequest;
				IWebCall<AddNicknameRequest, AddNicknameResponse> webCall = mixWebCallFactory.NicknamePut(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<AddNicknameResponse> e)
				{
					AddNicknameResponse response = e.Response;
					if (NotificationValidator.Validate(response.Notification))
					{
						notificationQueue.Dispatch(response.Notification, successCallback, failureCallback);
					}
					else
					{
						logger.Critical("Failed to validate set nickname response: " + JsonParser.ToJson(response));
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
