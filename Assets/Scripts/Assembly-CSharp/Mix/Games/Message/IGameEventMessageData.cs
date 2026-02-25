using System.Collections.Generic;

namespace Mix.Games.Message
{
	public interface IGameEventMessageData : IGameMessageData
	{
		string GameName { get; }

		Dictionary<string, object> EventState { get; }

		Dictionary<string, object> State { get; }

		string StateMessageId { get; }

		string StateMessageSenderId { get; }
	}
}
