namespace Disney.Mix.SDK.Internal
{
	internal class ClearChatHistoryResult : IClearChatHistoryResult
	{
		public bool Success { get; private set; }

		public ClearChatHistoryResult(bool success)
		{
			Success = success;
		}
	}
}
