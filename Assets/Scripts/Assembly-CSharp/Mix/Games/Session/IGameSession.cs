using System;
using Mix.Games.Data;

namespace Mix.Games.Session
{
	public interface IGameSession
	{
		event EventHandler<EventArgs> OnUnfriended;

		event EventHandler<GameStartedEventArgs> OnGameStarted;

		event EventHandler<GameClosedEventArgs> OnGameClosed;

		event EventHandler<GameStateUpdatedEventArgs> OnGameStateUpdated;

		void HandleSessionState(GameSession aSession, GameSessionState aSessionState);

		void LoadGame(ChatThreadGameSession threadSession, IGameTray gameTray);

		void PostGameStateData(GameSession aSession, object aGameStateData);

		void PostGameEventData(GameSession aSession, MixGameData stateData, MixGameResponse eventData);

		void PostChat(IEntitlementGameData aEntitlement);
	}
}
