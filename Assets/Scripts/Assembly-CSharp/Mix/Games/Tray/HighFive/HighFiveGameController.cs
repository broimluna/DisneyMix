using LitJson;
using Mix.Games.Data;
using Mix.Games.Session;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveGameController : BaseGameController
	{
		public override void SetupGameData()
		{
			mGameData = new HighFiveData();
			mGameData.Type = MixGameDataType.INVITE;
		}

		public override void SetupGameData(MixGameData aData)
		{
			mGameData = aData as HighFiveData;
			mGameData.Type = MixGameDataType.RESULT;
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<HighFiveData>(aGameDataJson);
			mGameData.Type = MixGameDataType.RESULT;
		}

		public void UpdateSessionState(GameSessionState state)
		{
			if (mSession != null)
			{
				mSession.UpdateGameSessionState(state);
			}
		}

		public void GameOverPost(HighFiveStats aStats, int aSeed)
		{
			if (DebugSceneIndicator.IsMainScene)
			{
				HighFiveData gameData = GetGameData<HighFiveData>();
				gameData.RandomSeed = aSeed;
				HighFiveResponse myResponse = gameData.GetMyResponse(gameData.Responses, base.PlayerId);
				InitResponseFromStats(myResponse, aStats);
				gameData.Status = MixGameSessionStatus.COMPLETE;
				GameOver(gameData);
			}
		}

		public void GameOverResponse(HighFiveStats aStats)
		{
			if (DebugSceneIndicator.IsMainScene)
			{
				HighFiveData gameData = GetGameData<HighFiveData>();
				HighFiveResponse myResponse = gameData.GetMyResponse(gameData.Responses, base.PlayerId);
				InitResponseFromStats(myResponse, aStats);
				GameOver(myResponse);
			}
		}

		protected void InitResponseFromStats(HighFiveResponse aResponse, HighFiveStats aStats)
		{
			aResponse.TotalHits = aStats.TotalHits;
			aResponse.MaxStreak = aStats.MaxStreak;
			aResponse.TotalScore = aStats.CurScore;
			aResponse.Rating = aStats.GetRating();
			aResponse.Attempts++;
		}
	}
}
