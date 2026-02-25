namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadGagMessageAddedEventArgs : AbstractChatThreadGagMessageAddedEventArgs
	{
		public override IGagMessage Message { get; protected set; }

		public ChatThreadGagMessageAddedEventArgs(IGagMessage message)
		{
			Message = message;
		}
	}
}
