using System;

namespace Mix.Games.Session
{
	public interface IGameStatistics
	{
		DateTime LastPlayed { get; set; }

		int SessionsCompleted { get; set; }

		int SessionsStarted { get; set; }

		DateTime GetLastPlayed(string id);
	}
}
