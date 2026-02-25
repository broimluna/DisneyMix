using System;
using Disney.Mix.SDK.Internal.GuestControllerDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ProfileGetter
	{
		public static void GetProfile(AbstractLogger logger, IGuestControllerClient guestControllerClient, Action<ProfileData> callback)
		{
			try
			{
				guestControllerClient.GetProfile(delegate(GuestControllerResult<ProfileResponse> r)
				{
					callback((!r.Success) ? null : r.Response.data);
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(null);
			}
		}
	}
}
