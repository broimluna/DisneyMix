using System;
using Disney.MobileNetwork;
using Mix.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class AboutPanel : BasePanel
	{
		public Text CreditsText;

		public Text MixVersionText;

		public override void Setup()
		{
			Version bundleVersion = EnvironmentManager.BundleVersion;
			string newValue = bundleVersion.Major + "." + bundleVersion.Minor + "." + bundleVersion.Build;
			MixVersionText.text = Singleton<Localizer>.Instance.getString("aboutpanel.aboutcontent.versiontextpost1.0").Replace("#version#", newValue);
			MixVersionText.GetComponent<LocalizedText>().doNotLocalize = true;
			CreditsText.text = Singleton<Localizer>.Instance.getString("aboutpanel.aboutcontent.credits").Replace("#version#", UnityEngine.Application.unityVersion);
			CreditsText.GetComponent<LocalizedText>().doNotLocalize = true;
		}

		public void OnVisitDisneyClicked()
		{
			Application.OpenUrl(Singleton<Localizer>.Instance.getString("customtokens.panels.about_disney_url"));
		}

		public void OnHelpClicked()
		{
			Application.OpenUrl(Singleton<Localizer>.Instance.getString("customtokens.panels.about_help_url"));
		}
	}
}
