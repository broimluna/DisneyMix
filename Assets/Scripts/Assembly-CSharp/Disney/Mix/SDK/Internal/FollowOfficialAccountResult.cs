namespace Disney.Mix.SDK.Internal
{
	public class FollowOfficialAccountResult : IFollowOfficialAccountResult
	{
		public bool Success { get; private set; }

		public FollowOfficialAccountResult(bool success)
		{
			Success = success;
		}
	}
}
