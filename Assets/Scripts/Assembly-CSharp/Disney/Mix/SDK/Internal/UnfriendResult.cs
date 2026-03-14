namespace Disney.Mix.SDK.Internal
{
	internal class UnfriendResult : IUnfriendResult
	{
		public bool Success { get; private set; }

		public UnfriendResult(bool success)
		{
			Success = success;
		}
	}
}
