using System;
using Disney.Mix.SDK;
using Mix;
using Mix.Data;
using Mix.Entitlements;
using Mix.Localization;
using Mix.Ui;
using UnityEngine;
using UnityEngine.UI;

public class DateTimeChatItem : BaseChatItem, IScrollItem
{
	private GameObject prefabObject;

	public DateTimeChatItem(GameObject item, IChatThread aThread, IChatMessage aMessage, ScrollView aScrollView)
		: base(aThread, aMessage, aScrollView)
	{
		prefabObject = item;
		DateTime = aMessage.TimeSent - new TimeSpan(1L);
	}

	GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(prefabObject);
		Text component = gameObject.GetComponent<Text>();
		component.text = GetTimestampFromNow();
		TextGenerationSettings generationSettings = component.GetGenerationSettings(ScrollView.gameObject.GetComponent<RectTransform>().rect.size);
		TextGenerator textGenerator = new TextGenerator();
		textGenerator.Populate(component.text, generationSettings);
		float preferredHeight = textGenerator.GetPreferredHeight(component.text, generationSettings);
		gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, preferredHeight);
		if (thread is IOfficialAccountChatThread)
		{
			Official_Account officialAccount = Singleton<EntitlementsManager>.Instance.GetOfficialAccount(((IOfficialAccountChatThread)thread).OfficialAccount.AccountId);
			if (officialAccount != null)
			{
				Util.UpdateTintablesForOfficialAccount(officialAccount, gameObject);
			}
		}
		return gameObject;
	}

	void IScrollItem.Destroy()
	{
	}

	public override void UpdateClientMessage(IChatMessage aMessage)
	{
	}

	public string GetTimestampFromNow()
	{
		DateTime dateTime = message.TimeSent.ToLocalTime();
		TimeSpan timeSpan = DateTime.Now - dateTime;
		if (timeSpan.Days < 1)
		{
			return dateTime.ToString("h:mm tt");
		}
		if (timeSpan.Days == 1)
		{
			return Singleton<Localizer>.Instance.getString("customtokens.chat.time_stamp_1day").Replace("#time#", dateTime.ToString("h:mm tt"));
		}
		string text;
		if (timeSpan.Days < 7)
		{
			text = Singleton<Localizer>.Instance.getString("customtokens.chat.time_stamp_1week").Replace("#numdays#", timeSpan.Days.ToString());
			return text.Replace("#time#", dateTime.ToString("h:mm tt"));
		}
		text = Singleton<Localizer>.Instance.getString("customtokens.chat.time_stamp_more_than_a_week").Replace("#date#", dateTime.ToString("MMMM dd, yyyy"));
		return text.Replace("#time#", dateTime.ToString("h:mm tt"));
	}
}
