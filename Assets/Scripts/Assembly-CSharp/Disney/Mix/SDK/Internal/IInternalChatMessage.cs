using System;

namespace Disney.Mix.SDK.Internal
{
	public interface IInternalChatMessage : IChatMessage
	{
		long Created { get; }

		new DateTime TimeSent { get; set; }

		uint? ChatMessageDocumentId { get; set; }

		long ChatMessageId { get; }

		long LocalChatMessageId { get; set; }

		void SendComplete(long id, long timeSent, long sequenceNumber);

		void SendCompleteWithOffsetTime(long id, long timeSent, long sequenceNumber);
	}
}
