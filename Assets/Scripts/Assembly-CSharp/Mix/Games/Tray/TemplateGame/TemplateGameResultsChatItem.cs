using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using UnityEngine.UI;

namespace Mix.Games.Tray.TemplateGame
{
	public class TemplateGameResultsChatItem : BaseGameChatController
	{
		public Text ResultText;

		public override void SetupGameData(string aGameDataJson)
		{
			mGameResponseData = JsonMapper.ToObject<TemplateGameResponse>(aGameDataJson);
		}

		protected override void SetupView()
		{
			base.SetupView();
			if (ResultText != null)
			{
				ResultText.text = ResultText.text.Replace("#displayname#", mSession.SenderName);
			}
		}
	}
}
