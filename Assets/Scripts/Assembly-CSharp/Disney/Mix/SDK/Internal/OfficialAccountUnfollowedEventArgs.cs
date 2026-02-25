namespace Disney.Mix.SDK.Internal
{
	internal class OfficialAccountUnfollowedEventArgs : AbstractOfficialAccountUnfollowedEventArgs
	{
		public OfficialAccountUnfollowedEventArgs(IOfficialAccount officialAccount)
		{
			base.ExOfficialAccount = officialAccount;
		}
	}
}
