using Disney.LaunchPad.Packages.FiniteStateMachine;

namespace Mix.Games.Tray.TemplateGame
{
	public class PlayGameState : BaseGameState
	{
		protected override void Awake()
		{
			base.Awake();
			mState.EventDispatcher.AddListener<StateEnterEvent>(OnStateEnter);
		}

		private bool OnStateEnter(StateEnterEvent evnt)
		{
			mGame.SetupGame();
			mGame.PlayGame();
			return false;
		}
	}
}
