using System;

namespace Mix.Games.Message
{
	public interface IGameMessageData
	{
		string SenderId { get; }

		bool Sent { get; }

		DateTime TimeSent { get; }

		string Id { get; }

		bool IsMine { get; }

		object MixMessageType { get; }

		string GetJson();
	}
}
