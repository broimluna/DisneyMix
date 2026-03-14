using System;
using Mix.Games.Data;

namespace Mix.Games.Session
{
	public interface IGameMessageParameters
	{
		bool IsGameMessage { get; }

		bool IsGameResponseMessage { get; }

		bool IsMessageMine { get; }

		string ClientMessageId { get; }

		string ClientMessageSenderId { get; }

		string StateMessageId { get; }

		MixGameData GameStateData { get; }

		object Message { get; }

		string GameDataJson { get; }

		string GameStateJson { get; }

		DateTime TimeSent { get; }

		TimeSpan ResponseTimeDifference { get; }
	}
}
