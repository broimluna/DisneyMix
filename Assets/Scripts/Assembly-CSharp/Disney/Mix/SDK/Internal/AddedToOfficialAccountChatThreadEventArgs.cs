namespace Disney.Mix.SDK.Internal
{
	internal class AddedToOfficialAccountChatThreadEventArgs : AbstractAddedToOfficialAccountThreadEventArgs
	{
		public AddedToOfficialAccountChatThreadEventArgs(IOfficialAccountChatThread chatThread)
		{
			base.ChatThread = chatThread;
		}
	}
}
