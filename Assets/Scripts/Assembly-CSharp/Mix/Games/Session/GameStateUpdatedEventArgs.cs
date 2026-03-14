using System;

namespace Mix.Games.Session
{
	public class GameStateUpdatedEventArgs : EventArgs
	{
		public string gameStateId;

		public string gameDataJson;

		public GameStateUpdatedEventArgs(string aGameStateId, string aGameDataJson)
		{
			gameStateId = aGameStateId;
			gameDataJson = aGameDataJson;
		}
	}
}
