namespace Mix.Ui
{
	public interface IForgotInfo : IErrorOverlay
	{
		void OnEmailSent(DisneyIdEmailType aType);

		void OnBack();
	}
}
