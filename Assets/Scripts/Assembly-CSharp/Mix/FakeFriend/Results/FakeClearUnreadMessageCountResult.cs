using Disney.Mix.SDK;

namespace Mix.FakeFriend.Results
{
	public class FakeClearUnreadMessageCountResult : IClearUnreadMessageCountResult
	{
		public bool Success { get; set; }

		public FakeClearUnreadMessageCountResult(bool success)
		{
			Success = success;
		}
	}
}
