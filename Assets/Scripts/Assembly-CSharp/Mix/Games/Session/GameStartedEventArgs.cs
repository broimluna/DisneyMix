using System;

namespace Mix.Games.Session
{
	public class GameStartedEventArgs : EventArgs
	{
		public string gameStateId;

		public GameStartedEventArgs(string aGameStateId)
		{
			gameStateId = aGameStateId;
		}
	}
}
