namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadGameEventMessageAddedEventArgs : AbstractChatThreadGameEventMessageAddedEventArgs
	{
		public override IGameEventMessage Message { get; protected set; }

		public ChatThreadGameEventMessageAddedEventArgs(IGameEventMessage message)
		{
			Message = message;
		}
	}
}
