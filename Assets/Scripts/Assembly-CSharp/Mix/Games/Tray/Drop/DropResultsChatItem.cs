using System.Collections.Generic;
using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using Mix.Games.Message;
using UnityEngine.UI;

namespace Mix.Games.Tray.Drop
{
	public class DropResultsChatItem : BaseGameChatController
	{
		public Text ResultsMessageText;

		public Text ScoreCountText;

		public override void SetupGameData(string aGameDataJson)
		{
			mGameResponseData = JsonMapper.ToObject<DropResponse>(aGameDataJson);
			if (mSession.MessageData is IGameEventMessageData)
			{
				Dictionary<string, object> state = (mSession.MessageData as IGameEventMessageData).State;
				string json = (string)state["GameData"];
				mGameData = JsonMapper.ToObject<DropData>(json);
			}
			else
			{
				mGameData = JsonMapper.ToObject<DropData>(mSession.MessageData.GetJson());
			}
			mResponses = (mGameData as IMixGameData).GetResponses();
		}

		protected override void SetupView()
		{
			base.SetupView();
			DropResponse dropResponse = (DropResponse)mGameResponseData;
			ScoreCountText.text = dropResponse.Score.ToString();
			GameOwnerText.text = GameOwnerText.text.Replace("#displayname#", mSession.SenderName);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < mResponses.Count; i++)
			{
				DropResponse dropResponse2 = mResponses[i] as DropResponse;
				if (dropResponse2.Score > num)
				{
					num = dropResponse2.Score;
					num2 = 1;
				}
				else if (dropResponse2.Score == num)
				{
					num2++;
				}
			}
			if (dropResponse.Score == num)
			{
				if (num2 == 1)
				{
					ResultsMessageText.text = mSession.GetLocalizedString("customtokens.game.drop_bestsofar");
				}
				else
				{
					ResultsMessageText.text = mSession.GetLocalizedString("customtokens.game.drop_tiedbest");
				}
			}
			else
			{
				ResultsMessageText.text = mSession.GetLocalizedString("customtokens.game.drop_notbest");
			}
		}
	}
}
