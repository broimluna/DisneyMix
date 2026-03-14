using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;

namespace Mix.Games.Tray.Fireworks
{
	public class FireworksPostController : BaseGameChatController
	{
		protected FireworksData mFireworksData;

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<FireworksData>(aGameDataJson);
			mFireworksData = mGameData as FireworksData;
		}
	}
}
