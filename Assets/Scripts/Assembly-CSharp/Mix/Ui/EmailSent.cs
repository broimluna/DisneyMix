using Mix.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class EmailSent : MonoBehaviour
	{
		public Text TitleText;

		public Text DescriptionText;

		private IEmailSent caller;

		private DisneyIdEmailType type;

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		private void Update()
		{
		}

		public void Show(DisneyIdEmailType aType, IEmailSent aCaller)
		{
			caller = aCaller;
			type = aType;
			TitleText.text = Singleton<Localizer>.Instance.getString("customtokens.register.title_email_sent");
			if (type == DisneyIdEmailType.UPGRADE_NRT)
			{
				DescriptionText.text = Singleton<Localizer>.Instance.getString("customtokens.register.text_description_email_sent_nrt");
			}
			else if (type == DisneyIdEmailType.RESOLVE_MASE)
			{
				DescriptionText.text = Singleton<Localizer>.Instance.getString("customtokens.register.text_description_email_sent_mase");
			}
			else if (type == DisneyIdEmailType.RECOVER_USERNAME)
			{
				DescriptionText.text = Singleton<Localizer>.Instance.getString("customtokens.register.text_description_email_sent_username");
			}
			else
			{
				DescriptionText.text = Singleton<Localizer>.Instance.getString("customtokens.register.text_description_email_sent_password");
			}
			base.gameObject.SetActive(true);
		}

		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		public DisneyIdEmailType GetEmailType()
		{
			return type;
		}

		public void onNextClicked()
		{
			caller.OnNext();
		}
	}
}
