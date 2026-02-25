using System.Collections.Generic;
using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using Mix.Games.Message;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.SecretWord
{
	public class SecretWordGameResponseController : BaseGameChatController
	{
		public GameObject SuccessIcon;

		public GameObject FailIcon;

		public Text SuccessText;

		public Text FailText;

		public override void SetupGameData(string aGameDataJson)
		{
			if (mSession.MessageData is IGameEventMessageData)
			{
				Dictionary<string, object> state = (mSession.MessageData as IGameEventMessageData).State;
				string json = (string)state["GameData"];
				mGameData = JsonMapper.ToObject<SecretWordData>(json);
			}
			else
			{
				mGameData = JsonMapper.ToObject<SecretWordData>(mSession.MessageData.GetJson());
			}
			mResponses = (mGameData as IMixGameData).GetResponses();
			mGameResponseData = JsonMapper.ToObject<SecretWordResponse>(aGameDataJson);
		}

		protected override void SetupView()
		{
			base.SetupView();
			SecretWordResponse secretWordResponse = (SecretWordResponse)mGameResponseData;
			SuccessText.text = mSession.GetLocalizedString("customtokens.game.writersblock_gameitem_success");
			FailText.text = mSession.GetLocalizedString("customtokens.game.writersblock_gameitem_failure");
			SuccessText.text = SuccessText.text.Replace("#displayname#", mSession.SenderName);
			FailText.text = FailText.text.Replace("#displayname#", mSession.SenderName);
			SuccessIcon.SetActive(secretWordResponse.Success);
			FailIcon.SetActive(!secretWordResponse.Success);
			GameOwnerText.text = GameOwnerText.text.Replace("#displayname#", mSession.SenderName);
		}
	}
}
