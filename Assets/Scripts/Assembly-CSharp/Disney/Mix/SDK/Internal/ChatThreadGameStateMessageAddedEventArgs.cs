namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadGameStateMessageAddedEventArgs : AbstractChatThreadGameStateMessageAddedEventArgs
	{
		public override IGameStateMessage Message { get; protected set; }

		public ChatThreadGameStateMessageAddedEventArgs(IGameStateMessage message)
		{
			Message = message;
		}
	}
}
