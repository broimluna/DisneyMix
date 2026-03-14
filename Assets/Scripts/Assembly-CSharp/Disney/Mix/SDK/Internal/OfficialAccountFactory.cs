namespace Disney.Mix.SDK.Internal
{
	public static class OfficialAccountFactory
	{
		public static IOfficialAccount CreateOfficialAccount(string swid, string displayNameText, bool isAvailable, bool canUnfollow)
		{
			DisplayName displayName = new DisplayName(displayNameText);
			return new OfficialAccount(swid, displayName, isAvailable, canUnfollow);
		}
	}
}
