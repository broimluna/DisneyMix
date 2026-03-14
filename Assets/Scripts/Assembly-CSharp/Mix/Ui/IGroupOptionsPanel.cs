namespace Mix.Ui
{
	public interface IGroupOptionsPanel
	{
		void OnNicknameUpdated(string aNickname);

		void OnShowNotification(string aString);

		void OnClosing();
	}
}
