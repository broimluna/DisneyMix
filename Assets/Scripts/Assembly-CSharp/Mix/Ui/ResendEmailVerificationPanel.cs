using Disney.Mix.SDK;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ResendEmailVerificationPanel : BasePanel
	{
		public interface IResendEmailVerificationPanelListener
		{
			void AdultEmailVerified(ILocalUser aLocalUser);
		}

		public string ChildUsedEmailToken = string.Empty;

		public string CheckEmailToken = string.Empty;

		public string ResendEmailToken = string.Empty;

		public string EmailSentToken = string.Empty;

		public Button Continue;

		public Button Resend;

		public Text Email;

		private ILocalUser parent;

		private bool sendEmail;

		private IResendEmailVerificationPanelListener currentListener;

		private void Start()
		{
		}

		public void Init(ILocalUser aParent, bool aSendEmail, IResendEmailVerificationPanelListener aListener)
		{
			parent = aParent;
			sendEmail = aSendEmail;
			currentListener = aListener;
			Email.text = parent.RegistrationProfile.Email;
			if (!sendEmail)
			{
				return;
			}
			parent.SendVerificationEmail(delegate(ISendVerificationEmailResult sendVerificationEmailResult)
			{
				if (!sendVerificationEmailResult.Success)
				{
					Debug.Log("request failed");
				}
			});
		}

		public void OnContinueClicked()
		{
			if (!sendEmail)
			{
				return;
			}
			parent.RefreshProfile(delegate(IRefreshProfileResult refreshProfileResult)
			{
				if (!refreshProfileResult.Success)
				{
					GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
					genericPanel.ShowSimpleError("customtokens.global.generic_error");
				}
				else if (parent.RegistrationProfile.EmailVerified)
				{
					currentListener.AdultEmailVerified(parent);
				}
				else
				{
					GenericPanel genericPanel2 = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
					genericPanel2.TitleText.text = "Temp Text";
					genericPanel2.MessageText.text = "Still waiting for email verification";
				}
			});
		}
	}
}
