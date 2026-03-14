using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.Menu;
using Mix.Games.Tray.Friendzy.Message;

public class QuizIPState : IState
{
	private MenuSystem mMenuSystem;

	public QuizIPState(MenuSystem inMenuSystem)
	{
		mMenuSystem = inMenuSystem;
	}

	void IState.Enter()
	{
		mMenuSystem.DisplayQuizzes();
	}

	void IState.Update()
	{
	}

	void IState.Exit()
	{
	}

	void IState.ReceiveMessage(IMessage eventMessage)
	{
	}
}
