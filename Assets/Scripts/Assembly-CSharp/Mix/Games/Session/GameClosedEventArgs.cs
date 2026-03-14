using System;

namespace Mix.Games.Session
{
	public class GameClosedEventArgs : EventArgs
	{
		public string gameStateId;

		public GameClosedEventArgs(string aGameStateId)
		{
			gameStateId = aGameStateId;
		}
	}
}
