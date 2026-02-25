namespace Disney.Mix.SDK.Internal
{
	internal class TextMessage : ChatMessage, IInternalChatMessage, IInternalTextMessage, IChatMessage, ITextMessage
	{
		public string Text { get; set; }

		public TextMessage(bool sent, long sequenceNumber, string senderId, string text, IEpochTime epochTime, long localChatMessageId)
			: base(sent, sequenceNumber, senderId, epochTime, localChatMessageId)
		{
			Text = text;
		}
	}
}
