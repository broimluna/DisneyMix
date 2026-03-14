using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.Menu;
using Mix.Games.Tray.Friendzy.Message;

public class CategoryIPState : IState
{
	private MenuSystem mMenuSystem;

	private Category[] mCategories { get; set; }

	public CategoryIPState(MenuSystem inMenuSystem)
	{
		mMenuSystem = inMenuSystem;
	}

	void IState.Enter()
	{
		if (mCategories == null)
		{
			mCategories = mMenuSystem.GetCategories();
		}
		mMenuSystem.DisplayCategories(mCategories);
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
