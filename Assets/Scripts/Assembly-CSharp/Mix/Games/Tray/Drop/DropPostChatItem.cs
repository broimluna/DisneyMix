using LitJson;
using Mix.Games.Chat;
using UnityEngine.UI;

namespace Mix.Games.Tray.Drop
{
	public class DropPostChatItem : BaseGameChatController
	{
		public Text ScoreCount;

		protected DropData dropData;

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<DropData>(aGameDataJson);
		}

		protected override void SetupView()
		{
			base.SetupView();
			dropData = mGameData as DropData;
			DropResponse myResponse = dropData.GetMyResponse(dropData.Responses, mSession.MessageData.SenderId);
			ScoreCount.text = myResponse.Score.ToString();
		}
	}
}
