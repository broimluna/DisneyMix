using System;
using Disney.Mix.SDK;

namespace Mix.Session.Local.Messages
{
	public abstract class LocalChatMessage<T> : IChatMessage
	{
		public string SenderId { get; set; }

		public bool Sent { get; set; }

		public DateTime TimeSent { get; set; }

		public long SequenceNumber { get; set; }

		public string Id { get; set; }

		public bool Read { get; set; }

		public abstract bool Equals(IChatMessage other);
	}
}
