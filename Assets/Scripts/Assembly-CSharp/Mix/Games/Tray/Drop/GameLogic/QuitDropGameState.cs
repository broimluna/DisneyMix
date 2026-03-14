using Disney.LaunchPad.Packages.FiniteStateMachine;

namespace Mix.Games.Tray.Drop.GameLogic
{
	public class QuitDropGameState : DropGameState
	{
		private void Awake()
		{
			base.State.EventDispatcher.AddListener<StateEnterEvent>(OnStateEnter);
		}

		private bool OnStateEnter(StateEnterEvent stateChange)
		{
			base.Game.CanFinish = true;
			return false;
		}
	}
}
