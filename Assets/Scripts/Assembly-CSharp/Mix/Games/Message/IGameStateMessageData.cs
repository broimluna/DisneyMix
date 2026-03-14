using System.Collections.Generic;

namespace Mix.Games.Message
{
	public interface IGameStateMessageData : IGameMessageData
	{
		string GameName { get; }

		Dictionary<string, object> State { get; }
	}
}
