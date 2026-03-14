using System;
using System.Collections.Generic;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class OfficialAccountUnfollower
	{
		public static void Unfollow(AbstractLogger logger, INotificationQueue notificationQueue, string officialAccountId, IMixWebCallFactory mixWebCallFactory, Action successCallback, Action failureCallback)
		{
			try
			{
				RemoveOfficialAccountFollowshipRequest removeOfficialAccountFollowshipRequest = new RemoveOfficialAccountFollowshipRequest();
				removeOfficialAccountFollowshipRequest.OfficialAccountId = officialAccountId;
				RemoveOfficialAccountFollowshipRequest request = removeOfficialAccountFollowshipRequest;
				IWebCall<RemoveOfficialAccountFollowshipRequest, RemoveOfficialAccountFollowshipResponse> webCall = mixWebCallFactory.OfficialAccountRemovePost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<RemoveOfficialAccountFollowshipResponse> e)
				{
					RemoveOfficialAccountFollowshipResponse response = e.Response;
					if (NotificationValidator.Validate(response.RemoveFollowshipNotification) && NotificationValidator.Validate(response.MembershipNotification))
					{
						List<BaseNotification> notifications = new List<BaseNotification> { response.RemoveFollowshipNotification, response.MembershipNotification };
						notificationQueue.Dispatch(notifications, successCallback, failureCallback);
					}
					else
					{
						logger.Critical("Failed to validate remove followship response: " + JsonParser.ToJson(response));
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
