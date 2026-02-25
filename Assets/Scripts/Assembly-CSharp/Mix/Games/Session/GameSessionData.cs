using System;

namespace Mix.Games.Session
{
	public class GameSessionData
	{
		public string ThreadId { get; protected set; }

		public string EntitlementId { get; protected set; }

		public string PlayerId { get; protected set; }

		public int CompletedGames { get; set; }

		public DateTime LastPlayed { get; set; }

		public int StartedGames { get; set; }

		public GameSessionData()
		{
		}

		public GameSessionData(string entitlementId, string threadId, string playerId)
		{
			EntitlementId = entitlementId;
			ThreadId = threadId;
			PlayerId = playerId;
		}

		public bool IsMine(string entitlementId, string threadId, string playerId)
		{
			return string.Equals(EntitlementId, entitlementId) && string.Equals(ThreadId, threadId) && string.Equals(PlayerId, playerId);
		}
	}
}
