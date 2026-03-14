namespace Disney.Mix.SDK.Internal
{
	internal class CreateGroupChatThreadResult : ICreateGroupChatThreadResult
	{
		public bool Success { get; private set; }

		public IGroupChatThread ChatThread { get; private set; }

		public CreateGroupChatThreadResult(bool success, IGroupChatThread chatThread)
		{
			Success = success;
			ChatThread = chatThread;
		}
	}
}
