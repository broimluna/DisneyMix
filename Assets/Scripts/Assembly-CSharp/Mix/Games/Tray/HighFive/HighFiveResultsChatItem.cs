using System.Collections.Generic;
using System.Linq;
using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using Mix.Games.Message;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveResultsChatItem : BaseGameChatController
	{
		public Text ActionText;

		public Text ScoreText;

		public GameObject[] RatingIcons;

		public override void SetupGameData(string aGameDataJson)
		{
			mGameResponseData = JsonMapper.ToObject<HighFiveResponse>(aGameDataJson);
			Dictionary<string, object> state = (mSession.MessageData as IGameEventMessageData).State;
			string json = (string)state["GameData"];
			mGameData = JsonMapper.ToObject<HighFiveData>(json);
			mResponses = (mGameData as IMixGameData).GetResponses();
		}

		protected override void SetupView()
		{
			base.SetupView();
			HighFiveResponse highFiveResponse = (HighFiveResponse)mGameResponseData;
			int totalScore = highFiveResponse.TotalScore;
			ScoreText.text = totalScore.ToString();
			int rating = highFiveResponse.Rating;
			if (rating >= 0 && rating < RatingIcons.Length)
			{
				RatingIcons[rating].SetActive(true);
			}
			ActionText.text = GetCallToActionText(totalScore);
			GameOwnerText.text = GameOwnerText.text.Replace("#displayname#", mSession.SenderName);
		}

		protected string GetCallToActionText(int myScore)
		{
			string empty = string.Empty;
			List<HighFiveResponse> responses = (mGameData as HighFiveData).Responses;
			List<HighFiveResponse> list = responses.OrderByDescending((HighFiveResponse r) => r.TotalScore).ToList();
			HighFiveResponse highFiveResponse = list[0];
			HighFiveResponse highFiveResponse2 = list.FirstOrDefault((HighFiveResponse r) => string.Equals(r.PlayerSwid, mSession.SenderId));
			empty = ((highFiveResponse2 == highFiveResponse || highFiveResponse2.TotalScore > highFiveResponse.TotalScore) ? "customtokens.game.drop_bestsofar" : ((highFiveResponse2.TotalScore != highFiveResponse.TotalScore) ? "customtokens.game.drop_notbest" : "customtokens.game.drop_tiedbest"));
			return mSession.GetLocalizedString(empty);
		}
	}
}
