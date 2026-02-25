using System;

namespace Disney.Mix.SDK
{
	public interface IChatMessage
	{
		string SenderId { get; }

		bool Sent { get; }

		DateTime TimeSent { get; }

		long SequenceNumber { get; }

		string Id { get; }
	}
}
