using Disney.LaunchPad.Packages.FiniteStateMachine;

namespace Mix.Games.Tray.TemplateGame
{
	public class SendState : BaseGameState
	{
		protected override void Awake()
		{
			base.Awake();
			mState.EventDispatcher.AddListener<StateEnterEvent>(OnStateEnter);
		}

		protected bool OnStateEnter(StateEnterEvent aEvent)
		{
			mGame.SendAndQuit();
			return false;
		}
	}
}
