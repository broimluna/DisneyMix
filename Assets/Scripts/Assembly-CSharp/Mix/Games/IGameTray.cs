using System;
using System.Collections.Generic;
using Mix.Games.Data;
using Mix.Games.Message;
using UnityEngine;

namespace Mix.Games
{
	public interface IGameTray
	{
		string GetCurrentThreadId();

		void SetGamePanel(GameTrayState aState);

		void OnGamePause(string aLogo);

		void OnGameError(string aLogo);

		void UpdateGameHeight(int aHeight);

		void UpdateGameLoader(string aLogo);

		void UpdateGameExit(Texture2D aGameScreenshot);

		void SendGameStateMessage(string gameName, Dictionary<string, object> payload, Action<object> callback);

		void UpdateGameStateMessage(IGameMessageData message, Dictionary<string, object> payload, Action<object> callback);

		void SendEntitlement(IEntitlementGameData aEntitlement);
	}
}
