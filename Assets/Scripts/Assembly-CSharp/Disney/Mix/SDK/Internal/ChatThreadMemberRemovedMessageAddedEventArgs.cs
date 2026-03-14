namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadMemberRemovedMessageAddedEventArgs : AbstractChatThreadMemberRemovedMessageAddedEventArgs
	{
		public override IChatMemberRemovedMessage Message { get; protected set; }

		public ChatThreadMemberRemovedMessageAddedEventArgs(IChatMemberRemovedMessage message)
		{
			Message = message;
		}
	}
}
