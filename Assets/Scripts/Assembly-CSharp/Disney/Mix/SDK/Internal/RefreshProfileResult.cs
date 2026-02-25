namespace Disney.Mix.SDK.Internal
{
	internal class RefreshProfileResult : IRefreshProfileResult
	{
		public bool Success { get; private set; }

		public RefreshProfileResult(bool success)
		{
			Success = success;
		}
	}
}
