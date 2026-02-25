using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.Menu
{
	public class MenuRevealState : IState
	{
		private MenuSystem mMenuSystem;

		public MenuRevealState(MenuSystem inMenuSystem)
		{
			mMenuSystem = inMenuSystem;
		}

		void IState.Enter()
		{
			if (mMenuSystem.MenuReadyToBeOpened)
			{
				mMenuSystem.TurnOn();
				mMenuSystem.MenuFSM.ChangeToState(mMenuSystem.CategoryState);
			}
		}

		void IState.Update()
		{
			if (mMenuSystem.MenuReadyToBeOpened && mMenuSystem.GetCameraController().CheckState("QuizState"))
			{
				mMenuSystem.TurnOn();
				mMenuSystem.MenuFSM.ChangeToState(mMenuSystem.CategoryState);
			}
		}

		void IState.Exit()
		{
		}

		void IState.ReceiveMessage(IMessage eventMessage)
		{
		}
	}
}
