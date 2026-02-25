using LitJson;
using Mix.Games.Data;
using Mix.Games.Session;

namespace Mix.Games.Tray.Friendzy
{
	public class FriendzyGameController : BaseGameController
	{
		public override void SetupGameData()
		{
			mGameData = new FriendzyData();
			mGameData.Type = MixGameDataType.INVITE;
		}

		public override void SetupGameData(MixGameData aData)
		{
			mGameData = aData as FriendzyData;
			mGameData.Type = MixGameDataType.RESULT;
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<FriendzyData>(aGameDataJson);
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
