using System.Collections.Generic;
using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using Mix.Games.Message;
using Mix.Games.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.WouldYouRather
{
	public class WouldYouRatherGameResponseController : BaseGameChatController, IGameAsset
	{
		public Text CardLabelText;

		public Image CardImage;

		private WouldYouRatherData.WouldYouRatherOption choice;

		void IGameAsset.OnGameSessionAssetLoaded(object aUserData)
		{
			if (mSession != null && !this.IsNullOrDisposed())
			{
				GameObject gameObject = mSession.GetBundleInstance(choice.ImageUrl) as GameObject;
				if (!gameObject.IsNullOrDisposed())
				{
					CardImage.sprite = gameObject.GetComponent<Image>().sprite;
					Object.Destroy(gameObject);
					UpdateHeight();
				}
			}
		}

		public override void SetupGameData(string aGameDataJson)
		{
			if (mSession.MessageData is IGameEventMessageData)
			{
				Dictionary<string, object> state = (mSession.MessageData as IGameEventMessageData).State;
				string json = (string)state["GameData"];
				mGameData = JsonMapper.ToObject<WouldYouRatherData>(json);
			}
			else
			{
				mGameData = JsonMapper.ToObject<WouldYouRatherData>(mSession.MessageData.GetJson());
			}
			mResponses = (mGameData as IMixGameData).GetResponses();
			mGameResponseData = JsonMapper.ToObject<WouldYouRatherResponse>(aGameDataJson);
		}

		protected override void SetupView()
		{
			base.SetupView();
			WouldYouRatherData wouldYouRatherData = mGameData as WouldYouRatherData;
			WouldYouRatherResponse wouldYouRatherResponse = mGameResponseData as WouldYouRatherResponse;
			choice = ((wouldYouRatherResponse.Choice != WouldYouRatherResponse.WouldYouRatherResponseType.ChoiceA) ? wouldYouRatherData.Option2 : wouldYouRatherData.Option1);
			CardLabelText.text = choice.Caption;
			LoadAsset(this, choice.ImageUrl);
			GameOwnerText.text = GameOwnerText.text.Replace("#displayname#", mSession.SenderName);
		}
	}
}
