using Disney.LaunchPad.Packages.FiniteStateMachine;

namespace Mix.Games.Tray.TemplateGame
{
	public class ContinueGameState : BaseGameState
	{
		protected override void Awake()
		{
			base.Awake();
			mState.EventDispatcher.AddListener<StateEnterEvent>(OnStateEnter);
		}

		private bool OnStateEnter(StateEnterEvent evnt)
		{
			mGame.SetupGame();
			mGame.ContinueGame();
			return false;
		}
	}
}
