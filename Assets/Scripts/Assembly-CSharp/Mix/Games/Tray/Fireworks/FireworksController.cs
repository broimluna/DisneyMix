using LitJson;
using Mix.Games.Data;
using Mix.Games.Session;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class FireworksController : BaseGameController
	{
		public override void SetupGameData()
		{
			mGameData = new FireworksData();
			mGameData.Type = MixGameDataType.INVITE;
		}

		public override void SetupGameData(MixGameData aData)
		{
			mGameData = aData as FireworksData;
			mGameData.Type = MixGameDataType.RESULT;
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<FireworksData>(aGameDataJson);
			mGameData.Type = MixGameDataType.RESULT;
		}

		private void Start()
		{
		}

		private void OnButtonClick()
		{
			Debug.Log("On Button Click");
		}

		public GameSession GetGameSession()
		{
			return mSession;
		}
	}
}
