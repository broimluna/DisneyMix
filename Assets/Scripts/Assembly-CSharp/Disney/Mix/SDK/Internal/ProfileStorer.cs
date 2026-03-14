using System;
using Disney.Mix.SDK.Internal.GuestControllerDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ProfileStorer
	{
		public static bool StoreProfile(AbstractLogger logger, IDatabase database, IEpochTime epochTime, string swid, ProfileData profileData)
		{
			Disney.Mix.SDK.Internal.GuestControllerDomain.DisplayName displayNameData = profileData.displayName;
			if (displayNameData != null)
			{
				try
				{
					database.UpdateSessionDocument(swid, delegate(SessionDocument doc)
					{
						doc.DisplayNameText = displayNameData.displayName;
						doc.ProposedDisplayName = displayNameData.proposedDisplayName;
						doc.ProposedDisplayNameStatus = displayNameData.proposedStatus;
						doc.FirstName = profileData.profile.firstName;
						doc.LastProfileRefreshTime = epochTime.Seconds;
						doc.AccountStatus = profileData.profile.status;
					});
				}
				catch (Exception ex)
				{
					logger.Critical("Unhandled exception: " + ex);
					return false;
				}
			}
			return true;
		}
	}
}
