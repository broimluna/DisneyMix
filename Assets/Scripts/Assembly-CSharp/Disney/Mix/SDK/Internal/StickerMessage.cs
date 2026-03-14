namespace Disney.Mix.SDK.Internal
{
	internal class StickerMessage : ChatMessage, IInternalChatMessage, IInternalStickerMessage, IChatMessage, IStickerMessage
	{
		public string ContentId { get; private set; }

		public StickerMessage(bool sent, long sequenceNumber, string senderId, string contentId, IEpochTime epochTime, long localChatMessageId)
			: base(sent, sequenceNumber, senderId, epochTime, localChatMessageId)
		{
			ContentId = contentId;
		}

		public void SendComplete(string contentId, long id, long timeSent, long sequenceNumber)
		{
			SendCompleteWithOffsetTime(id, timeSent, sequenceNumber);
			ContentId = contentId;
		}
	}
}
