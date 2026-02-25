namespace Disney.Mix.SDK.Internal
{
	internal class AddedToOneOnOneChatThreadEventArgs : AbstractAddedToOneOnOneChatThreadEventArgs
	{
		public AddedToOneOnOneChatThreadEventArgs(IOneOnOneChatThread chatThread)
		{
			base.ChatThread = chatThread;
		}
	}
}
