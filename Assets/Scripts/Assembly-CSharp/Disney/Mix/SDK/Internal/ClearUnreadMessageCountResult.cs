namespace Disney.Mix.SDK.Internal
{
	internal class ClearUnreadMessageCountResult : IClearUnreadMessageCountResult
	{
		public bool Success { get; private set; }

		public ClearUnreadMessageCountResult(bool success)
		{
			Success = success;
		}
	}
}
