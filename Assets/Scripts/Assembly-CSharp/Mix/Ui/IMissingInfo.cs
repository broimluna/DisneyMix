namespace Mix.Ui
{
	public interface IMissingInfo : IErrorOverlay, IRulesOverlay, IPrivacyPracticesOverlay
	{
		void OnInfoUpdated();
	}
}
