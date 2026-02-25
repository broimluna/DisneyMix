namespace Mix.Ui
{
	public interface IChatBar
	{
		void OnKeyboardShown();

		void OnKeyboardHidden();

		void OnSendTextMessage(string aTextMessage);

		void OnHidePreviewPanel();
	}
}
