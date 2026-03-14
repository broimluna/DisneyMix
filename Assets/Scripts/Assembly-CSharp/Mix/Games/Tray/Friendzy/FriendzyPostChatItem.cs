using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using Mix.Games.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy
{
	public class FriendzyPostChatItem : BaseGameChatController, IGameAsset
	{
		public Image ResultImage;

		public Text ResultText;

		public Text QuizTitle;

		private string URLToLoad;

		void IGameAsset.OnGameSessionAssetLoaded(object aUserData)
		{
			if (mSession != null && !this.IsNullOrDisposed())
			{
				GameObject gameObject = mSession.GetBundleInstance(URLToLoad) as GameObject;
				if (gameObject != null)
				{
					ResultImage.sprite = gameObject.GetComponent<Image>().sprite;
					Object.Destroy(gameObject);
				}
			}
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<FriendzyData>(aGameDataJson);
			mResponses = (mGameData as IMixGameData).GetResponses();
			FriendzyData friendzyData = mGameData as FriendzyData;
			FriendzyResponse myResponse = friendzyData.GetMyResponse(friendzyData.Responses, mSession.MessageData.SenderId);
			QuizTitle.text = friendzyData.QuizTitle;
			ResultText.text = myResponse.Result;
			URLToLoad = myResponse.ImageUrl;
			LoadAsset(this, URLToLoad);
		}
	}
}
