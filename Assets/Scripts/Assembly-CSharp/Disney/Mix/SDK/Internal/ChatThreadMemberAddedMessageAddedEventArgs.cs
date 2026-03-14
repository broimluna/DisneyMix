namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadMemberAddedMessageAddedEventArgs : AbstractChatThreadMemberAddedMessageAddedEventArgs
	{
		public override IChatMemberAddedMessage Message { get; protected set; }

		public ChatThreadMemberAddedMessageAddedEventArgs(IChatMemberAddedMessage message)
		{
			Message = message;
		}
	}
}
