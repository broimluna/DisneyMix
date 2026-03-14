using LitJson;
using Mix.Games.Data;

namespace Mix.Games.Tray.WouldYouRather
{
	public class WouldYouRatherController : BaseGameController
	{
		public WouldYouRatherGame wouldYouRatherGame;

		public override void SetupGameData()
		{
			mGameData = new WouldYouRatherData();
			mGameData.Type = MixGameDataType.INVITE;
		}

		public override void SetupGameData(MixGameData aData)
		{
			mGameData = aData as WouldYouRatherData;
			mGameData.Type = MixGameDataType.RESULT;
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<WouldYouRatherData>(aGameDataJson);
			mGameData.Type = MixGameDataType.RESULT;
		}
	}
}
