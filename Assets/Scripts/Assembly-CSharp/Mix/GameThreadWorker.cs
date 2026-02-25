using System.Collections;

namespace Mix
{
	public class GameThreadWorker : MainThreadWorker
	{
		public GameThreadWorker(string aGameContext, string aGameAction, string aGameType, string aMessage, string aLocation)
			: base(GetGameDataHash(aGameContext, aGameAction, aGameType, aMessage, aLocation))
		{
		}

		public GameThreadWorker(Hashtable aAnalyticsData)
			: base(aAnalyticsData)
		{
		}

		public override void DoWork()
		{
			Hashtable hashtable = (Hashtable)GetData();
			Analytics.LogGameAction((string)hashtable["context"], (string)hashtable["action"], (string)hashtable["message"], (string)hashtable["type"], (string)hashtable["location"]);
		}

		protected static Hashtable GetGameDataHash(string aGameContext, string aGameAction, string aGameType, string aMessage, string aLocation)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("context", aGameContext);
			hashtable.Add("action", aGameAction);
			hashtable.Add("message", aMessage);
			hashtable.Add("type", aGameType);
			hashtable.Add("location", aLocation);
			return hashtable;
		}
	}
}
