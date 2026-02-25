using System;
using Disney.Mix.SDK.Internal.GuestControllerDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class AdultVerificationStatusGetter
	{
		public static void GetAdultVerificationStatus(AbstractLogger logger, IGuestControllerClient guestControllerClient, Action<IGetAdultVerificationStatusResult> callback)
		{
			try
			{
				guestControllerClient.GetAdultVerificationStatus(delegate(GuestControllerResult<AdultVerificationStatusResponse> r)
				{
					if (!r.Success || r.Response.error != null || r.Response.data == null)
					{
						callback(new GetAdultVerificationStatusResult(false, false, false));
					}
					else
					{
						callback(new GetAdultVerificationStatusResult(true, r.Response.data.verified, r.Response.data.maxAttempts));
					}
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(new GetAdultVerificationStatusResult(false, false, false));
			}
		}
	}
}
