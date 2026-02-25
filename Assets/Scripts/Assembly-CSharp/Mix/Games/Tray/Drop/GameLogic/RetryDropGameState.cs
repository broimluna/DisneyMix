using Disney.LaunchPad.Packages.FiniteStateMachine;
using Mix.Games.Session;

namespace Mix.Games.Tray.Drop.GameLogic
{
	public class RetryDropGameState : DropGameState
	{
		private void Awake()
		{
			base.State.EventDispatcher.AddListener<StateEnterEvent>(OnStateEnter);
		}

		private bool OnStateEnter(StateEnterEvent stateChange)
		{
			string aGameMessage = string.Format("{0}|{1}", base.Game.Score, base.Game.BestScore);
			base.Game.GameController.LogEvent(GameLogEventType.ACTION, "winter_waltz_retry", aGameMessage);
			base.Game.Reset();
			base.Game.GameEventDispatcher.DispatchEvent(new GameInitializedEvent());
			return false;
		}
	}
}
