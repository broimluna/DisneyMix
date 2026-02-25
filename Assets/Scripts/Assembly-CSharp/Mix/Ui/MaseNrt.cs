using Disney.Mix.SDK;
using Mix.Localization;
using Mix.Session;
using Mix.User;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class MaseNrt : MonoBehaviour
	{
		public Button SendEmailButton;

		public Button UseDifferentAccount;

		public Button GoBackButton;

		public Text DescriptionText;

		private IMaseNrt caller;

		private bool isTrueIfMaseOrFalseIfNrt;

		private string username;

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		private void Update()
		{
		}

		public void Show(string aUsername, bool aIsMase, IMaseNrt aCaller)
		{
			username = aUsername;
			isTrueIfMaseOrFalseIfNrt = aIsMase;
			caller = aCaller;
			if (isTrueIfMaseOrFalseIfNrt)
			{
				DescriptionText.text = Singleton<Localizer>.Instance.getString("customtokens.register.mase_description_text");
				Analytics.LogMaseEmailPageView();
			}
			else
			{
				DescriptionText.text = Singleton<Localizer>.Instance.getString("customtokens.register.nrt_description_text");
				Analytics.LogNrtEmailPageView();
			}
			base.gameObject.SetActive(true);
			setUiInteractable(true);
		}

		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		public void OnBack()
		{
			caller.OnBack();
		}

		public void OnSendEmail()
		{
			setUiInteractable(false);
			if (isTrueIfMaseOrFalseIfNrt)
			{
				MultipleAccountsResolutionSender multipleAccountsResolutionSender = new MultipleAccountsResolutionSender(MonoSingleton<LoginManager>.Instance.KeychainData, Log.MixLogger, MonoSingleton<LoginManager>.Instance.StorageDir, ExternalizedConstants.GuestControllerUrl, MixSession.GuestControllerSpoofedIP, ExternalizedConstants.GuestControllerClientId, MonoSingleton<LoginManager>.Instance.CoroutineManager);
				multipleAccountsResolutionSender.Send(username, OnMASEResponse);
			}
			else
			{
				NonRegisteredTransactorUpgradeSender nonRegisteredTransactorUpgradeSender = new NonRegisteredTransactorUpgradeSender(MonoSingleton<LoginManager>.Instance.KeychainData, Log.MixLogger, MonoSingleton<LoginManager>.Instance.StorageDir, ExternalizedConstants.GuestControllerUrl, MixSession.GuestControllerSpoofedIP, ExternalizedConstants.GuestControllerClientId, MonoSingleton<LoginManager>.Instance.CoroutineManager);
				nonRegisteredTransactorUpgradeSender.Send(username, OnNRTResponse);
			}
		}

		private void OnMASEResponse(ISendMultipleAccountsResolutionResult aResult)
		{
			OnResponse(aResult.Success);
		}

		private void OnNRTResponse(ISendNonRegisteredTransactorUpgradeResult aResult)
		{
			OnResponse(aResult.Success);
		}

		private void OnResponse(bool aResult)
		{
			if (aResult)
			{
				caller.OnEmailSent((!isTrueIfMaseOrFalseIfNrt) ? DisneyIdEmailType.UPGRADE_NRT : DisneyIdEmailType.RESOLVE_MASE);
				Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_2_SendDisney");
			}
			else
			{
				caller.ShowErrorOverlay("customtokens.register.error_send_email", string.Empty);
				Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_1_Error");
				setUiInteractable(true);
			}
		}

		private void setUiInteractable(bool aInteractable)
		{
			SendEmailButton.interactable = aInteractable;
			GoBackButton.interactable = aInteractable;
			UseDifferentAccount.interactable = aInteractable;
		}
	}
}
