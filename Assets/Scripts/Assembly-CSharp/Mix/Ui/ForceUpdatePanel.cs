using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ForceUpdatePanel : BasePanel
	{
		public Text TitleText;

		public Text MessageText;

		public Text ButtonOneText;

		public Button ButtonOne;

		public override void Setup()
		{
			Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string> { { ButtonOneText, "customtokens.forceupdate.okbutton" } });
			Singleton<PanelManager>.Instance.SetupButtons(new Dictionary<Button, Action> { { ButtonOne, null } });
		}

		public void ShowSimpleError(string aTitleToken, string aMessageToken = null)
		{
			Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string>
			{
				{ TitleText, aTitleToken },
				{ MessageText, aMessageToken },
				{ ButtonOneText, "customtokens.forceupdate.okbutton" }
			});
			SetTitleColor(new Color(73f / 85f, 0f, 0f));
			Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_1_Error");
		}

		public void SetTitleColor(Color aTitleColor)
		{
			if (TitleText != null)
			{
				TitleText.color = aTitleColor;
			}
		}

		public override bool OnAndroidBackButton()
		{
			GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
			Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string>
			{
				{ genericPanel.TitleText, "customtokens.panels.leaving_app" },
				{ genericPanel.MessageText, null },
				{ genericPanel.ButtonOneText, "customtokens.login.logout_yes" },
				{ genericPanel.ButtonTwoText, "customtokens.login.logout_no" }
			});
			Singleton<PanelManager>.Instance.SetupButtons(new Dictionary<Button, Action> { 
			{
				genericPanel.ButtonOne,
				Application.Quit
			} });
			return true;
		}
	}
}
