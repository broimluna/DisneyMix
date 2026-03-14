namespace Disney.Mix.SDK.Internal
{
	internal class AddedToGroupChatThreadEventArgs : AbstractAddedToGroupChatThreadEventArgs
	{
		public AddedToGroupChatThreadEventArgs(IGroupChatThread chatThread)
		{
			base.ChatThread = chatThread;
		}
	}
}
