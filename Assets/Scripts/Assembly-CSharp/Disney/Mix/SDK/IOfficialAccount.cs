namespace Disney.Mix.SDK
{
	public interface IOfficialAccount
	{
		string AccountId { get; }

		IDisplayName DisplayName { get; }

		bool IsAvailable { get; }

		bool CanUnfollow { get; }
	}
}
