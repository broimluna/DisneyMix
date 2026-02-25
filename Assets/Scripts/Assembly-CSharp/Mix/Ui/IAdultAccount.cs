using Disney.Mix.SDK;

namespace Mix.Ui
{
	public interface IAdultAccount : IErrorOverlay, IRulesOverlay, IPrivacyPracticesOverlay, ICountrySelectOverlay
	{
		void OnAccountCreated(ILocalUser aParent);

		void OnBackClicked();

		void GoToDisneyIdLogin();

		void HandleMaseAccount(string aEmail);

		void HandleNrtAccount(string aEmail);
	}
}
