using System;
using Disney.Mix.SDK.Internal.GuestControllerDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ParentalApprovalEmailSender
	{
		public static void SendParentalApprovalEmail(AbstractLogger logger, IGuestControllerClient guestControllerClient, Action<ISendParentalApprovalEmailResult> callback)
		{
			try
			{
				guestControllerClient.SendParentalApprovalEmail(delegate(GuestControllerResult<NotificationResponse> r)
				{
					callback(new SendParentalApprovalEmailResult(r.Success && r.Response.data != null));
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(new SendParentalApprovalEmailResult(false));
			}
		}
	}
}
