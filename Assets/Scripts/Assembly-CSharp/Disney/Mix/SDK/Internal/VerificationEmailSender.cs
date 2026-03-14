using System;
using Disney.Mix.SDK.Internal.GuestControllerDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class VerificationEmailSender
	{
		public static void SendVerificationEmail(AbstractLogger logger, IGuestControllerClient guestControllerClient, Action<ISendVerificationEmailResult> callback)
		{
			try
			{
				guestControllerClient.SendVerificationEmail(delegate(GuestControllerResult<NotificationResponse> r)
				{
					callback(new SendVerificationEmailResult(r.Success && r.Response.error == null && r.Response.data != null));
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(new SendVerificationEmailResult(false));
			}
		}
	}
}
