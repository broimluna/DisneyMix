namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadTextMessageAddedEventArgs : AbstractChatThreadTextMessageAddedEventArgs
	{
		public override ITextMessage Message { get; protected set; }

		public ChatThreadTextMessageAddedEventArgs(ITextMessage message)
		{
			Message = message;
		}
	}
}
