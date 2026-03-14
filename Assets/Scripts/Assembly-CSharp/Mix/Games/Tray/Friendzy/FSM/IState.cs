using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.FSM
{
	public interface IState
	{
		void Enter();

		void Update();

		void Exit();

		void ReceiveMessage(IMessage aEventMessage);
	}
}
