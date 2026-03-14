using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.FSM
{
	public class FiniteStateMachine
	{
		public IState CurrentState;

		public FiniteStateMachine()
		{
			CurrentState = null;
		}

		public void Update()
		{
			CurrentState.Update();
		}

		public void ChangeToState(IState newState)
		{
			CurrentState.Exit();
			CurrentState = newState;
			CurrentState.Enter();
		}

		public void SendMessageToCurrentState(IMessage eventMessage)
		{
			CurrentState.ReceiveMessage(eventMessage);
		}
	}
}
