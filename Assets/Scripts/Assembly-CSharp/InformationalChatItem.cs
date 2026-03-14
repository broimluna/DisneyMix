using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix;
using Mix.Data;
using Mix.Entitlements;
using Mix.Localization;
using Mix.Session.Extensions;
using Mix.Ui;
using UnityEngine;
using UnityEngine.UI;

public class InformationalChatItem : BaseChatItem, IScrollItem
{
	private GameObject prefabObject;

	public InformationalChatItem(GameObject item, IChatThread aThread, IChatMessage aChatMessage, ScrollView aScrollView)
		: base(aThread, aChatMessage, aScrollView)
	{
		prefabObject = item;
	}

	GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
	{
		GameObject gameObject = Object.Instantiate(prefabObject);
		Text component = gameObject.GetComponent<Text>();
		if (message is IChatMemberAddedMessage)
		{
			IEnumerable<string> memberIds = (message as IChatMemberAddedMessage).MemberIds;
			string newValue = MessageUtils.BuildDisplayableMemberList(thread, memberIds);
			string aToken = ((memberIds.Count() < 2) ? "customtokens.chat.group_join" : "customtokens.chat.group_join_plural");
			component.text = Singleton<Localizer>.Instance.getString(aToken).Replace("#DisplayName#", newValue);
		}
		else if (message is IChatMemberRemovedMessage)
		{
			string nickOrDisplayById = thread.GetNickOrDisplayById((message as IChatMemberRemovedMessage).MemberId);
			if (!string.IsNullOrEmpty(nickOrDisplayById))
			{
				component.text = Singleton<Localizer>.Instance.getString("customtokens.chat.group_leave").Replace("#DisplayName#", nickOrDisplayById);
			}
		}
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
}
