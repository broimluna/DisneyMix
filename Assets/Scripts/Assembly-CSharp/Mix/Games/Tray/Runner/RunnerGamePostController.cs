using System;
using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using UnityEngine.UI;

namespace Mix.Games.Tray.Runner
{
	public class RunnerGamePostController : BaseGameChatController
	{
		public Text CallToActionText;

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<RunnerData>(aGameDataJson);
			mResponses = (mGameData as IMixGameData).GetResponses();
		}

		protected override void SetupView()
		{
			base.SetupView();
			RunnerData runnerData = (RunnerData)mGameData;
			int gameCreatorGemCount = runnerData.GameCreatorGemCount;
			string text = ((gameCreatorGemCount != 1) ? mSession.GetLocalizedString("customtokens.game.spikes_gameitem_gemcountplural") : mSession.GetLocalizedString("customtokens.game.spikes_gameitem_gemcountsingular"));
			text = text + Environment.NewLine + mSession.GetLocalizedString("customtokens.game.spikes_gameitem_calltoaction");
			CallToActionText.text = text.Replace("#gemcount#", gameCreatorGemCount.ToString());
		}
	}
}
