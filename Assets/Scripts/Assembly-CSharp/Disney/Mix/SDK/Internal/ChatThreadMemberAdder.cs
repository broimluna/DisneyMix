using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ChatThreadMemberAdder
	{
		private const string MemberCountExceeded = "MAX_MEMBER_COUNT_EXCEEDED";

		private const string ForbiddenNotFriends = "FORBIDDEN_NOT_FRIENDS";

		private const string NotFound = "NOT_FOUND";

		private const string UserAlreadyThreadMember = "USER_ALREADY_THREAD_MEMBER";

		private const string UserNotThreadMember = "USER_NOT_THREAD_MEMBER";

		public static void AddMembers(AbstractLogger logger, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, IEnumerable<string> memberSwids, long chatThreadId, Action<MemberListChangedChatMessage> successCallback, Action<IAddChatThreadMemberResult> failureCallback)
		{
			try
			{
				AddChatThreadMembershipRequest addChatThreadMembershipRequest = new AddChatThreadMembershipRequest();
				addChatThreadMembershipRequest.AddedUserIds = memberSwids.ToList();
				addChatThreadMembershipRequest.ChatThreadId = chatThreadId;
				AddChatThreadMembershipRequest request = addChatThreadMembershipRequest;
				IWebCall<AddChatThreadMembershipRequest, AddChatThreadMembershipResponse> webCall = mixWebCallFactory.ChatThreadMembershipPut(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<AddChatThreadMembershipResponse> e)
				{
					AddChatThreadMembershipResponse response = e.Response;
					if (NotificationValidator.Validate(response.MembershipNotification) && NotificationValidator.Validate(response.ChatMessageNotification))
					{
						int remain = 2;
						Action successCallback2 = delegate
						{
							remain--;
							if (remain == 0)
							{
								successCallback(response.ChatMessageNotification.Message);
							}
						};
						notificationQueue.Dispatch(response.MembershipNotification, successCallback2, delegate
						{
							failureCallback(new AddChatThreadMemberResult(false));
						});
						notificationQueue.Dispatch(response.ChatMessageNotification, successCallback2, delegate
						{
							failureCallback(new AddChatThreadMemberResult(false));
						});
					}
					else
					{
						logger.Critical("Failed to validate add chat member response: " + JsonParser.ToJson(response));
						failureCallback(new AddChatThreadMemberResult(false));
					}
				};
				webCall.OnError += delegate(object sender, WebCallErrorEventArgs e)
				{
					switch (e.Status)
					{
					case "MAX_MEMBER_COUNT_EXCEEDED":
						failureCallback(new AddChatThreadMemberMaxMemberCountExceededResult(false));
						break;
					case "NOT_FOUND":
						failureCallback(new AddChatThreadMemberThreadNotFoundResult(false));
						break;
					case "FORBIDDEN_NOT_FRIENDS":
						failureCallback(new AddChatThreadMemberNotFriendsResult(false));
						break;
					case "USER_ALREADY_THREAD_MEMBER":
						failureCallback(new AddChatThreadMemberAlreadyMemberResult(false));
						break;
					case "USER_NOT_THREAD_MEMBER":
						failureCallback(new AddChatThreadMemberNotChatMemberResult(false));
						break;
					default:
						failureCallback(new AddChatThreadMemberResult(false));
						break;
					}
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback(new AddChatThreadMemberResult(false));
			}
		}
	}
}
