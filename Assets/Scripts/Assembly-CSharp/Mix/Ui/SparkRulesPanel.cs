namespace Mix.Ui
{
	public class SparkRulesPanel : BasePanel
	{
		private void Start()
		{
			Analytics.LogAppRulesPageView();
		}
	}
}
