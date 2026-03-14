namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadVideoMessageAddedEventArgs : AbstractChatThreadVideoMessageAddedEventArgs
	{
		public override IVideoMessage Message { get; protected set; }

		public ChatThreadVideoMessageAddedEventArgs(IVideoMessage message)
		{
			Message = message;
		}
	}
}
