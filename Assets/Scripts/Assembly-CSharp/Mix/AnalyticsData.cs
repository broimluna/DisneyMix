using System.Collections.Generic;
using Disney.MobileNetwork;

namespace Mix
{
	public class AnalyticsData
	{
		public string eventName;

		public Dictionary<string, object> gameData;

		public AnalyticsData(string aEventName, Dictionary<string, object> aGameData)
		{
			eventName = aEventName;
			gameData = aGameData;
		}

		public void LogAction()
		{
			if (string.IsNullOrEmpty(eventName))
			{
				Service.Get<AnalyticsManager>().LogGameAction(gameData);
			}
			else
			{
				Service.Get<AnalyticsManager>().LogAnalyticsEventWithContext(eventName, gameData);
			}
		}
	}
}
