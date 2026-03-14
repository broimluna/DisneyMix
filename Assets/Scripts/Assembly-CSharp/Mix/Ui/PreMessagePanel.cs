using System;
using System.Collections.Generic;
using Disney.Native;
using Mix.Localization;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class PreMessagePanel : BasePanel
	{
		public Text TitleText;

		public Text MessageText;

		public Button ButtonOne;

		public Button ButtonTwo;

		private bool firstTime;

		public void Setup(bool aFirstTime = false)
		{
			firstTime = aFirstTime;
			string aToken = ((!MonoSingleton<PushNotifications>.Instance.IsDeviceNotificationEnabled()) ? "customtokens.panels.state_off" : "customtokens.panels.state_on");
			string aToken2 = ((!MonoSingleton<PushNotifications>.Instance.IsDeviceNotificationEnabled()) ? "customtokens.panels.state_on" : "customtokens.panels.state_off");
			TitleText.text = ReplaceToken(TitleText, "#currState#", Singleton<Localizer>.Instance.getString(aToken));
			MessageText.text = ReplaceToken(MessageText, "#changedState#", Singleton<Localizer>.Instance.getString(aToken2));
			if (!firstTime)
			{
				ButtonTwo.gameObject.SetActive(false);
			}
			else
			{
				ButtonTwo.GetComponentInChildren<Text>().text = ReplaceToken(ButtonTwo.GetComponentInChildren<Text>(), "#changedState#", Singleton<Localizer>.Instance.getString(aToken2));
			}
			base.Setup();
		}

		public void ButtonOneClicked()
		{
			ClosePanel();
		}

		public void ButtonTwoClicked()
		{
			if (firstTime)
			{
				GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
				Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string>
				{
					{ genericPanel.TitleText, "customtokens.panels.push_confirm_title" },
					{ genericPanel.MessageText, "customtokens.panels.push_confirm_message" },
					{ genericPanel.ButtonOneText, "customtokens.panels.push_confirm_settings" },
					{ genericPanel.ButtonTwoText, "customtokens.panels.push_confirm_nevermind" }
				});
				Singleton<PanelManager>.Instance.SetupButtons(new Dictionary<Button, Action>
				{
					{ genericPanel.ButtonOne, GotoDeviceSettings },
					{ genericPanel.ButtonTwo, genericPanel.ClosePanel }
				});
			}
			else
			{
				GotoDeviceSettings();
			}
			ClosePanel();
		}

		private void GotoDeviceSettings()
		{
			MonoSingleton<NativeUtilitiesManager>.Instance.Native.GotoApplicationSettings();
		}

		private string ReplaceToken(Text aText, string aToken, string aReplacement)
		{
			string result = Singleton<Localizer>.Instance.getString(aText.GetComponent<LocalizedText>().token).Replace(aToken, aReplacement);
			aText.GetComponent<LocalizedText>().doNotLocalize = true;
			return result;
		}
	}
}
