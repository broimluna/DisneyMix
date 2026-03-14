using LitJson;
using Mix.Games.Data;
using Mix.Games.Session;

namespace Mix.Games.Tray.FortuneCookie
{
	public class FortuneCookieGameController : BaseGameController
	{
		public override void SetupGameData()
		{
			mGameData = new FortuneCookieData();
			mGameData.Type = MixGameDataType.INVITE;
		}

		public override void SetupGameData(MixGameData aData)
		{
			mGameData = aData as FortuneCookieData;
			mGameData.Type = MixGameDataType.RESULT;
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<FortuneCookieData>(aGameDataJson);
			mGameData.Type = MixGameDataType.RESULT;
		}

		private void Start()
		{
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
