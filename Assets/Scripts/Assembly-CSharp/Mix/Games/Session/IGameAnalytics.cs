using System.Collections;

namespace Mix.Games.Session
{
	public interface IGameAnalytics
	{
		void LogEvent(GameLogEventType aGameLogEventType, Hashtable aData);
	}
}
