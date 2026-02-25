using System.Collections.Generic;
using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using Mix.Games.Message;
using Mix.Games.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy
{
	public class FriendzyResultsChatItem : BaseGameChatController, IGameAsset
	{
		public Text ResultText;

		public Text QuizTitle;

		public Image ResultPic;

		private string URLToLoad;

		void IGameAsset.OnGameSessionAssetLoaded(object aUserData)
		{
			if (mSession != null && !this.IsNullOrDisposed())
			{
				GameObject gameObject = mSession.GetBundleInstance(URLToLoad) as GameObject;
				if (gameObject != null)
				{
					ResultPic.sprite = gameObject.GetComponent<Image>().sprite;
					Object.Destroy(gameObject);
				}
			}
		}

		public override void SetupGameData(string aGameDataJson)
		{
			if (mSession.MessageData is IGameEventMessageData)
			{
				Dictionary<string, object> state = (mSession.MessageData as IGameEventMessageData).State;
				string json = (string)state["GameData"];
				mGameData = JsonMapper.ToObject<FriendzyData>(json);
			}
			else
			{
				mGameData = JsonMapper.ToObject<FriendzyData>(mSession.MessageData.GetJson());
			}
			mResponses = (mGameData as IMixGameData).GetResponses();
			mGameResponseData = JsonMapper.ToObject<FriendzyResponse>(aGameDataJson);
		}

		protected override void SetupView()
		{
			base.SetupView();
			FriendzyData friendzyData = (FriendzyData)mGameData;
			FriendzyResponse friendzyResponse = (FriendzyResponse)mGameResponseData;
			GameOwnerText.text = GameOwnerText.text.Replace("#displayname#", mSession.SenderName);
			string aGameAction = "respond_" + mEntitlement.GetName();
			mSession.LogAction(aGameAction);
			ResultText.text = friendzyResponse.Result;
			QuizTitle.text = friendzyData.QuizTitle;
			URLToLoad = friendzyResponse.ImageUrl;
			LoadAsset(this, URLToLoad);
		}
	}
}
