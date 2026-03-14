using LitJson;
using Mix.Games.Data;

namespace Mix.Games.Tray.Runner
{
	public class RunnerGameController : BaseGameController
	{
		public override void SetupGameData()
		{
			mGameData = new RunnerData();
			mGameData.Type = MixGameDataType.INVITE;
		}

		public override void SetupGameData(MixGameData aData)
		{
			mGameData = aData as RunnerData;
			mGameData.Type = MixGameDataType.RESULT;
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<RunnerData>(aGameDataJson);
			mGameData.Type = MixGameDataType.RESULT;
		}
	}
}
