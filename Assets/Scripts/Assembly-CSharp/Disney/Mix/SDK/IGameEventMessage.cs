using System.Collections.Generic;

namespace Disney.Mix.SDK
{
	public interface IGameEventMessage : IChatMessage
	{
		IGameStateMessage GameStateMessage { get; }

		string GameName { get; }

		Dictionary<string, object> EventState { get; }
	}
}
