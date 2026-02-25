using System;
using System.Collections.Generic;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class OfficialAccountFollower
	{
		public static void Follow(AbstractLogger logger, INotificationQueue notificationQueue, string officialAccountId, IMixWebCallFactory mixWebCallFactory, Action successCallback, Action failureCallback)
		{
			try
			{
				AddOfficialAccountFollowshipRequest addOfficialAccountFollowshipRequest = new AddOfficialAccountFollowshipRequest();
				addOfficialAccountFollowshipRequest.OfficialAccountId = officialAccountId;
				AddOfficialAccountFollowshipRequest request = addOfficialAccountFollowshipRequest;
				IWebCall<AddOfficialAccountFollowshipRequest, AddOfficialAccountFollowshipResponse> webCall = mixWebCallFactory.OfficialAccountAddPut(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<AddOfficialAccountFollowshipResponse> e)
				{
					AddOfficialAccountFollowshipResponse response = e.Response;
					if (NotificationValidator.Validate(response.AddFollowshipNotification))
					{
						bool flag = false;
						List<BaseNotification> list = new List<BaseNotification> { response.AddFollowshipNotification };
						if (response.AddChatThreadNotification != null && NotificationValidator.Validate(response.AddChatThreadNotification))
						{
							flag = true;
							list.Add(response.AddChatThreadNotification);
						}
						if (response.MembershipNotification != null && NotificationValidator.Validate(response.MembershipNotification))
						{
							flag = true;
							list.Add(response.MembershipNotification);
						}
						if (flag)
						{
							notificationQueue.Dispatch(list, successCallback, failureCallback);
						}
						else
						{
							logger.Critical("Failed to validate add followship response: " + JsonParser.ToJson(response));
							failureCallback();
						}
					}
					else
					{
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
