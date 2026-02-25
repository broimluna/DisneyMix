using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class GenericPanel : BasePanel
	{
		public Text TitleText;

		public Text MessageText;

		public Text ButtonOneText;

		public Text ButtonTwoText;

		public Button ButtonOne;

		public Button ButtonTwo;

		public override void Setup()
		{
			Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string> { { ButtonOneText, "customtokens.panels.button_ok" } });
			Singleton<PanelManager>.Instance.SetupButtons(new Dictionary<Button, Action>
			{
				{ ButtonOne, null },
				{ ButtonTwo, ClosePanel }
			});
		}

		public void ShowSimpleError(string aTitleToken, string aMessageToken = null)
		{
			Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string>
			{
				{ TitleText, aTitleToken },
				{ MessageText, aMessageToken },
				{ ButtonOneText, "customtokens.panels.button_ok" }
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
	}
}
