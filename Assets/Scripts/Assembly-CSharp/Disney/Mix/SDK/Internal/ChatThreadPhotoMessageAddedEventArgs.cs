namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadPhotoMessageAddedEventArgs : AbstractChatThreadPhotoMessageAddedEventArgs
	{
		public override IPhotoMessage Message { get; protected set; }

		public ChatThreadPhotoMessageAddedEventArgs(IPhotoMessage message)
		{
			Message = message;
		}
	}
}
