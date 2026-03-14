using System.Text;
using Mix.Games.Session;

namespace Mix.Games.Tray.Friendzy.BILog
{
	public class LogFriendzyBILog
	{
		public const string FRIENDZY_CHOOSE_IP = "friendzy_choose_ip";

		public const string FRIENDZY_CHOOSE_QUIZ = "friendzy_choose_quiz";

		public const string FRIENDZY_BACK_BUTTON_CLICKED = "friendzy_back_button_clicked";

		public static void FriendzyLogIPChosen(FriendzyGameController aGameController, string aIPChosen)
		{
			if (DebugSceneIndicator.IsMainScene)
			{
				string aGameMessage = string.Format("{0}", aIPChosen);
				aGameController.LogEvent(GameLogEventType.ACTION, "friendzy_choose_ip", aGameMessage);
			}
		}

		public static void FriendzyLogQuizChosen(FriendzyGameController aGameController, string aQuizChosen)
		{
			if (DebugSceneIndicator.IsMainScene)
			{
				string aGameMessage = string.Format("{0}", aQuizChosen);
				aGameController.LogEvent(GameLogEventType.ACTION, "friendzy_choose_quiz", aGameMessage);
			}
		}

		public static void FriendzyLogBackButton(FriendzyGameController aGameController, string aIPBackedFrom)
		{
			if (DebugSceneIndicator.IsMainScene)
			{
				StringBuilder stringBuilder = new StringBuilder(64, 256);
				stringBuilder.Append("{");
				stringBuilder.Append(aIPBackedFrom);
				stringBuilder.Append("_to_category}");
				aGameController.LogEvent(GameLogEventType.ACTION, stringBuilder.ToString());
			}
		}
	}
}
