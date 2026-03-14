namespace Disney.Mix.SDK.Internal
{
	internal class OfficialAccountFollowedEventArgs : AbstractOfficialAccountFollowedEventArgs
	{
		public OfficialAccountFollowedEventArgs(IOfficialAccount officialAccount)
		{
			base.OfficialAccount = officialAccount;
		}
	}
}
