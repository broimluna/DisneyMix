namespace Mix.Ui
{
	public interface IChildAccount : IErrorOverlay, IRulesOverlay, IPrivacyPracticesOverlay, ICountrySelectOverlay
	{
		void OnAccountCreated();

		void OnBackClicked();

		void GoToDisneyIdLogin();
	}
}
