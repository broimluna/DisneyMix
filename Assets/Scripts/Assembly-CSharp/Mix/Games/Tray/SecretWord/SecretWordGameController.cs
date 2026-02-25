using LitJson;
using Mix.Games.Data;
using Mix.Games.Session;

namespace Mix.Games.Tray.SecretWord
{
	public class SecretWordGameController : BaseGameController
	{
		public override void SetupGameData()
		{
			mGameData = new SecretWordData();
			mGameData.Type = MixGameDataType.INVITE;
		}

		public override void SetupGameData(MixGameData aData)
		{
			mGameData = aData as SecretWordData;
			mGameData.Type = MixGameDataType.RESULT;
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<SecretWordData>(aGameDataJson);
			mGameData.Type = MixGameDataType.RESULT;
		}

		public void UpdateSessionState(GameSessionState state)
		{
			if (mSession != null)
			{
				mSession.UpdateGameSessionState(state);
			}
		}
	}
}
