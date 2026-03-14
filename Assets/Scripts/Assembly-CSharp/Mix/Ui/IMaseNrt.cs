namespace Mix.Ui
{
	public interface IMaseNrt : IErrorOverlay
	{
		void OnBack();

		void OnEmailSent(DisneyIdEmailType aType);
	}
}
