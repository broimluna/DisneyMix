using System;
using System.Collections.Generic;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ChatThreadMemberRemover
	{
		public static void RemoveMember(AbstractLogger logger, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, string memberSwid, long chatThreadId, Action successCallback, Action failureCallback)
		{
			try
			{
				RemoveChatThreadMembershipRequest removeChatThreadMembershipRequest = new RemoveChatThreadMembershipRequest();
				removeChatThreadMembershipRequest.RemovedUserId = memberSwid;
				removeChatThreadMembershipRequest.ChatThreadId = chatThreadId;
				RemoveChatThreadMembershipRequest request = removeChatThreadMembershipRequest;
				IWebCall<RemoveChatThreadMembershipRequest, RemoveChatThreadMembershipResponse> webCall = mixWebCallFactory.ChatThreadMembershipDeletePost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<RemoveChatThreadMembershipResponse> e)
				{
					RemoveChatThreadMembershipResponse response = e.Response;
					RemoveChatThreadMembershipNotification membershipNotification = response.MembershipNotification;
					AddChatThreadMemberListChangedMessageNotification chatMessageNotification = response.ChatMessageNotification;
					if (NotificationValidator.Validate(membershipNotification) && (chatMessageNotification == null || NotificationValidator.Validate(chatMessageNotification)))
					{
						List<BaseNotification> list = new List<BaseNotification> { membershipNotification };
						if (chatMessageNotification != null)
						{
							list.Add(chatMessageNotification);
						}
						notificationQueue.Dispatch(list, successCallback, failureCallback);
					}
					else
					{
						logger.Critical("Failed to validate remove chat member response: " + JsonParser.ToJson(response));
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
