using LitJson;
using Mix.Games.Data;
using Mix.Games.Session;

namespace Mix.Games.Tray.Drop
{
	public class DropGameController : BaseGameController
	{
		public override void SetupGameData()
		{
			mGameData = new DropData();
			mGameData.Type = MixGameDataType.INVITE;
		}

		public override void SetupGameData(MixGameData aData)
		{
			mGameData = aData as DropData;
			mGameData.Type = MixGameDataType.RESULT;
		}

		public override void SetupGameData(string aDataJson)
		{
			mGameData = JsonMapper.ToObject<DropData>(aDataJson);
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

		public string GetGhostName(string swid)
		{
			string result = "Ghost";
			if (mSession != null)
			{
				result = mSession.GetPlayerName(swid);
			}
			return result;
		}
	}
}
