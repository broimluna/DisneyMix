namespace Disney.Mix.SDK.Internal
{
	public class OfficialAccount : IOfficialAccount
	{
		public IDisplayName DisplayName { get; private set; }

		public string AccountId { get; private set; }

		public bool IsAvailable { get; private set; }

		public bool CanUnfollow { get; private set; }

		public OfficialAccount(string accountId, IDisplayName displayName, bool isAvailable, bool canUnfollow)
		{
			DisplayName = displayName;
			AccountId = accountId;
			IsAvailable = isAvailable;
			CanUnfollow = canUnfollow;
		}
	}
}
