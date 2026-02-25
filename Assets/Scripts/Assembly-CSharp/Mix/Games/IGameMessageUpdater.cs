using System;
using System.Collections.Generic;

namespace Mix.Games
{
	public interface IGameMessageUpdater
	{
		void UpdateGameStateMessage(object gameName, Dictionary<string, object> payload, Action<object> callback);
	}
}
