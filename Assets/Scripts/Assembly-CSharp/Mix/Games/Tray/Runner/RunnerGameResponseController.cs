using System;
using System.Collections.Generic;
using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using Mix.Games.Message;
using UnityEngine.UI;

namespace Mix.Games.Tray.Runner
{
	public class RunnerGameResponseController : BaseGameChatController
	{
		public Text GemCountText;

		public override void SetupGameData(string aGameDataJson)
		{
			if (mSession.MessageData is IGameEventMessageData)
			{
				Dictionary<string, object> state = (mSession.MessageData as IGameEventMessageData).State;
				string json = (string)state["GameData"];
				mGameData = JsonMapper.ToObject<RunnerData>(json);
			}
			else
			{
				mGameData = JsonMapper.ToObject<RunnerData>(mSession.MessageData.GetJson());
			}
			mResponses = (mGameData as IMixGameData).GetResponses();
			mGameResponseData = JsonMapper.ToObject<RunnerResponse>(aGameDataJson);
		}

		protected override void SetupView()
		{
			base.SetupView();
			RunnerResponse runnerResponse = mGameResponseData as RunnerResponse;
			RunnerData runnerData = mGameData as RunnerData;
			int checkpoints = runnerResponse.Checkpoints;
			string text = ((checkpoints != 1) ? mSession.GetLocalizedString("customtokens.game.spikes_gameitem_gemcountplural") : mSession.GetLocalizedString("customtokens.game.spikes_gameitem_gemcountsingular"));
			if (runnerData != null)
			{
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < runnerData.Responses.Count; i++)
				{
					if (runnerData.Responses[i].Checkpoints > num)
					{
						num = runnerData.Responses[i].Checkpoints;
						num2 = 1;
					}
					else if (runnerData.Responses[i].Checkpoints == num)
					{
						num2++;
					}
				}
				if (checkpoints == 8)
				{
					text = text + Environment.NewLine + mSession.GetLocalizedString("customtokens.game.spikes_gameitem_perfectscore");
				}
				else if (checkpoints == num && num2 == 1)
				{
					text = text + Environment.NewLine + mSession.GetLocalizedString("customtokens.game.spikes_gameitem_bestsofar");
				}
			}
			GemCountText.text = text.Replace("#gemcount#", checkpoints.ToString());
			GameOwnerText.text = GameOwnerText.text.Replace("#displayname#", mSession.SenderName);
		}
	}
}
