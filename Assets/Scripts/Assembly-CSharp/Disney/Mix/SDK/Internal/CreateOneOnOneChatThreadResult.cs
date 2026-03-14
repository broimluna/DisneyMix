namespace Disney.Mix.SDK.Internal
{
	internal class CreateOneOnOneChatThreadResult : ICreateOneOnOneChatThreadResult
	{
		public bool Success { get; private set; }

		public IOneOnOneChatThread ChatThread { get; private set; }

		public CreateOneOnOneChatThreadResult(bool success, IOneOnOneChatThread chatThread)
		{
			Success = success;
			ChatThread = chatThread;
		}
	}
}
