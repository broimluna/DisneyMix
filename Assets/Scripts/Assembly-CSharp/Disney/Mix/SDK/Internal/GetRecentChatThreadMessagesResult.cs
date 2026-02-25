namespace Disney.Mix.SDK.Internal
{
	internal class GetRecentChatThreadMessagesResult : IGetRecentChatThreadMessagesResult
	{
		public bool Success { get; private set; }

		public GetRecentChatThreadMessagesResult(bool success)
		{
			Success = success;
		}
	}
}
