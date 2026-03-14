using System.Collections.Generic;
using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.HighFive
{
	public class HighFivePostChatItem : BaseGameChatController
	{
		public Text ScoreText;

		public GameObject[] RatingIcons;

		public List<Image> Images;

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<HighFiveData>(aGameDataJson);
			mResponses = (mGameData as IMixGameData).GetResponses();
		}

		protected override void SetupView()
		{
			base.SetupView();
			HighFiveData highFiveData = mGameData as HighFiveData;
			HighFiveResponse myResponse = mGameData.GetMyResponse(highFiveData.Responses, mSession.MessageData.SenderId);
			ScoreText.text = myResponse.TotalScore.ToString();
			int rating = myResponse.Rating;
			if (rating >= 0 && rating < RatingIcons.Length)
			{
				RatingIcons[rating].SetActive(true);
			}
		}
	}
}
