namespace Disney.Mix.SDK.Internal
{
	internal class GagMessage : ChatMessage, IInternalGagMessage, IInternalChatMessage, IChatMessage, IGagMessage
	{
		public string ContentId { get; private set; }

		public string TargetUserId { get; private set; }

		public GagMessage(bool sent, long sequenceNumber, string senderId, string contentId, string targetUserId, IEpochTime epochTime, long localChatMessageId)
			: base(sent, sequenceNumber, senderId, epochTime, localChatMessageId)
		{
			ContentId = contentId;
			TargetUserId = targetUserId;
		}

		public void SendComplete(string contentId, string targetUserId, long id, long timeSent, long sequenceNumber)
		{
			SendCompleteWithOffsetTime(id, timeSent, sequenceNumber);
			ContentId = contentId;
			TargetUserId = targetUserId;
		}
	}
}
