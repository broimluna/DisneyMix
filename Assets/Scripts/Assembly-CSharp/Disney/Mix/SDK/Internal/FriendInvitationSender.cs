using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class FriendInvitationSender
	{
		public static void Send(AbstractLogger logger, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, string inviteeDisplayName, bool requestTrust, Action<long> successCallback, Action<ISendFriendInvitationResult> failureCallback)
		{
			try
			{
				AddFriendshipInvitationRequest addFriendshipInvitationRequest = new AddFriendshipInvitationRequest();
				addFriendshipInvitationRequest.InviteeDisplayName = inviteeDisplayName;
				addFriendshipInvitationRequest.IsTrusted = requestTrust;
				AddFriendshipInvitationRequest request = addFriendshipInvitationRequest;
				IWebCall<AddFriendshipInvitationRequest, AddFriendshipInvitationResponse> webCall = mixWebCallFactory.FriendshipInvitationPut(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<AddFriendshipInvitationResponse> e)
				{
					AddFriendshipInvitationResponse response = e.Response;
					if (NotificationValidator.Validate(response.Notification))
					{
						notificationQueue.Dispatch(response.Notification, delegate
						{
							successCallback(response.Notification.Invitation.FriendshipInvitationId.Value);
						}, delegate
						{
							failureCallback(new SendFriendInvitationResult(false, null));
						});
					}
					else
					{
						logger.Critical("Failed to validate invitation: " + JsonParser.ToJson(response));
						failureCallback(new SendFriendInvitationResult(false, null));
					}
				};
				webCall.OnError += delegate(object sender, WebCallErrorEventArgs e)
				{
					string status = e.Status;
					string message = e.Message;
					switch (status)
					{
					case "INVITATION_ALREADY_EXISTS":
						failureCallback(new SendFriendInvitationAlreadyExistsResult(false, null));
						break;
					default:
						failureCallback(new SendFriendInvitationResult(false, null));
						break;
					}
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback(new SendFriendInvitationResult(false, null));
			}
		}
	}
}
