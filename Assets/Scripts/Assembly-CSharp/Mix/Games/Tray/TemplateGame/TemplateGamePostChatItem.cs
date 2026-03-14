using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;

namespace Mix.Games.Tray.TemplateGame
{
	public class TemplateGamePostChatItem : BaseGameChatController
	{
		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<TemplateGameData>(aGameDataJson);
		}
	}
}
