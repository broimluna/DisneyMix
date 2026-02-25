using System;
using System.Collections.Generic;
using Mix.Games.Data;

namespace Mix.Games
{
	public interface IGameMessageSender
	{
		void SendGameStateMessage(string gameName, Dictionary<string, object> payload, Action<object> callback);

		void SendEntitlement(IEntitlementGameData aEntitlement);
	}
}
