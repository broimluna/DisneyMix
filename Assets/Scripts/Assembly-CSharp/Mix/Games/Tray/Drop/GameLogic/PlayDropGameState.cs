using Disney.LaunchPad.Packages.FiniteStateMachine;

namespace Mix.Games.Tray.Drop.GameLogic
{
	public class PlayDropGameState : DropGameState
	{
		private void Awake()
		{
			base.State.EventDispatcher.AddListener<StateEnterEvent>(OnStateEnter);
		}

		private bool OnStateEnter(StateEnterEvent evnt)
		{
			if (!base.Game.UIManager.IsUILoaded(base.Game.PlayingUI))
			{
				base.Game.UIManager.LoadUI(base.Game.PlayingUI);
			}
			return false;
		}
	}
}
