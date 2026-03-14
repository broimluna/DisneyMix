using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;

namespace Mix.Games.Tray.WouldYouRather
{
	public class WouldYouRatherGamePostController : BaseGameChatController
	{
		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<WouldYouRatherData>(aGameDataJson);
			mResponses = (mGameData as IMixGameData).GetResponses();
		}
	}
}
