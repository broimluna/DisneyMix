using Mix.Games.Tray.Friendzy.Camera;
using Mix.Games.Tray.Friendzy.Data;

namespace Mix.Games.Tray.Friendzy.Menu
{
	public class MenuController
	{
		private FriendzyGame mFriendzyGame;

		private MenuSystem mFriendzyMenuSystem;

		public MenuController(FriendzyGame inFriendzyGame)
		{
			mFriendzyGame = inFriendzyGame;
			mFriendzyMenuSystem = mFriendzyGame.FriendzyMenuSystem;
			mFriendzyMenuSystem.gameObject.SetActive(true);
			mFriendzyMenuSystem.SetMenuController(this);
		}

		public void ReceiveMessage(ref MenuMessage message)
		{
			switch (message.Type)
			{
			case MenuMessageType.IP_QUIZ_PICKED:
			{
				FriendzyMessage message4 = new FriendzyMessage(FriendzyMessageType.IP_QUIZ_PICKED, message.Category, message.Quiz);
				FriendzyGame.PlaySound("HeavyApplause", FriendzyGame.SOUND_PREFIX);
				mFriendzyGame.ReceiveMessage(ref message4);
				break;
			}
			case MenuMessageType.IP_QUIZ_DATA_LOADED:
				mFriendzyMenuSystem.ReceiveMessage(ref message);
				break;
			case MenuMessageType.MENU_IMPLODE:
				mFriendzyMenuSystem.ReceiveMessage(ref message);
				break;
			case MenuMessageType.TURN_OFF_LEVEL_GEOMETRY:
			{
				FriendzyMessage message3 = new FriendzyMessage(FriendzyMessageType.TURN_OFF_LEVEL_GEOMETRY);
				mFriendzyGame.ReceiveMessage(ref message3);
				break;
			}
			case MenuMessageType.BACK_BUTTON_PRESSED:
			{
				FriendzyMessage message2 = new FriendzyMessage(FriendzyMessageType.BACK_BUTTON_PRESSED, message.Category);
				mFriendzyGame.ReceiveMessage(ref message2);
				break;
			}
			}
		}

		public Category[] GetCategories()
		{
			return mFriendzyGame.GetCategories();
		}

		public string[] GetQuizzesByIP(string inCategory)
		{
			return mFriendzyGame.GetQuizzesByIP(inCategory);
		}

		public void TurnOnMenu()
		{
			mFriendzyMenuSystem.TurnOn();
		}

		public void TurnOffMenu()
		{
			mFriendzyMenuSystem.TurnOff();
		}

		public CameraController GetCameraController()
		{
			return mFriendzyGame.GetCameraController();
		}
	}
}
