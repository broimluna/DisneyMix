namespace Disney.Mix.SDK.Internal
{
	public class UnfollowOfficialAccountResult : IUnfollowOfficialAccountResult
	{
		public bool Success { get; private set; }

		public UnfollowOfficialAccountResult(bool success)
		{
			Success = success;
		}
	}
}
