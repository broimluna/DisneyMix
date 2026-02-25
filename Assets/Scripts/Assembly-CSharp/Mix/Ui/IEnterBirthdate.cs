using Disney.Mix.SDK;

namespace Mix.Ui
{
	public interface IEnterBirthdate : IErrorOverlay
	{
		void OnBirthdateEntered(IAgeBand aAgeBand, int year, int month, int day);

		void OnNavigateBack();

		string GetCountryCode();

		void SetCountryCode(string aCountryCode);
	}
}
