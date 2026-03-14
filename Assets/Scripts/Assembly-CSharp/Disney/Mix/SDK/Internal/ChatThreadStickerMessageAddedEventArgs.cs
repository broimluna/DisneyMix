namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadStickerMessageAddedEventArgs : AbstractChatThreadStickerMessageAddedEventArgs
	{
		public override IStickerMessage Message { get; protected set; }

		public ChatThreadStickerMessageAddedEventArgs(IStickerMessage message)
		{
			Message = message;
		}
	}
}
