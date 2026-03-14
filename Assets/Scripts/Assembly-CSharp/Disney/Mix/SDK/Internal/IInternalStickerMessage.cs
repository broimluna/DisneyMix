namespace Disney.Mix.SDK.Internal
{
	public interface IInternalStickerMessage : IInternalChatMessage, IChatMessage, IStickerMessage
	{
		void SendComplete(string contentId, long id, long timeSent, long sequenceNumber);
	}
}
