namespace Disney.Mix.SDK.Internal
{
	internal class ChatMemberRemovedMessage : ChatMessage, IInternalChatMemberRemovedMessage, IInternalChatMessage, IChatMemberRemovedMessage, IChatMessage
	{
		public string MemberId { get; private set; }

		public ChatMemberRemovedMessage(bool sent, long sequenceNumber, string senderId, string memberId, IEpochTime epochTime, long localChatMessageId)
			: base(sent, sequenceNumber, senderId, epochTime, localChatMessageId)
		{
			MemberId = memberId;
		}
	}
}
