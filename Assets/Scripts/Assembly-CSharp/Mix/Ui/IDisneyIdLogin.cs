using Disney.Mix.SDK;

namespace Mix.Ui
{
	public interface IDisneyIdLogin : IErrorOverlay
	{
		void OnBackClicked();

		void OnInfoMissing(ILocalUser aLocalUser);

		void OnValidAccount(string aNavgationConstPath, bool aShowPopUp);

		void OnRecoveryClicked(bool aRecoverTrueIfUsernameFalseIfPassword);

		void HandleMaseAccount(string aEmail);
	}
}
