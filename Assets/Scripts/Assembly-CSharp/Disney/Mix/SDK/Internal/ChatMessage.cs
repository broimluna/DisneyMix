using System;

namespace Disney.Mix.SDK.Internal
{
	public class ChatMessage : IInternalChatMessage, IChatMessage
	{
		private readonly IEpochTime epochTime;

		private uint? chatMessageDocumentId;

		public long LocalChatMessageId { get; set; }

		public string Id { get; private set; }

		public string SenderId { get; private set; }

		public bool Sent { get; private set; }

		public long Created { get; private set; }

		public DateTime TimeSent { get; set; }

		public long ChatMessageId { get; private set; }

		public long SequenceNumber { get; private set; }

		public uint? ChatMessageDocumentId
		{
			get
			{
				return chatMessageDocumentId;
			}
			set
			{
				chatMessageDocumentId = value;
				uint? num = chatMessageDocumentId;
				Id = ((!num.HasValue) ? IdHasher.HashId(ChatMessageId.ToString()) : IdHasher.HashId("u" + chatMessageDocumentId));
			}
		}

		protected ChatMessage(bool sent, long sequenceNumber, string senderId, IEpochTime epochTime, long localChatMessageId)
		{
			SenderId = senderId;
			SequenceNumber = sequenceNumber;
			Sent = sent;
			this.epochTime = epochTime;
			Created = epochTime.Milliseconds;
			TimeSent = epochTime.UtcNow;
			LocalChatMessageId = localChatMessageId;
		}

		public void SendComplete(long id, long timeSent, long sequenceNumber)
		{
			Sent = true;
			Created = timeSent;
			TimeSent = epochTime.FromMilliseconds(timeSent);
			ChatMessageId = id;
			Id = IdHasher.HashId(ChatMessageId.ToString());
			SequenceNumber = sequenceNumber;
		}

		public void SendCompleteWithOffsetTime(long id, long timeSent, long sequenceNumber)
		{
			Sent = true;
			Created = timeSent;
			TimeSent = epochTime.FromMillisecondsAndOffset(timeSent);
			ChatMessageId = id;
			Id = IdHasher.HashId(ChatMessageId.ToString());
			SequenceNumber = sequenceNumber;
		}
	}
}
