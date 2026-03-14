namespace Mix.SequenceOperations
{
	public class ExternalConfigurationStartOperation : SequenceOperation
	{
		public const string SA = "Ifu";

		public ExternalConfigurationStartOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			Singleton<ConfigurationManager>.Instance.OnConfigurationInited += HandleOnConfigurationInited;
			Singleton<ConfigurationManager>.Instance.Init();
		}

		private void HandleOnConfigurationInited(object sender, ConfigurationInitEventArgs e)
		{
			if (Singleton<ConfigurationManager>.Instance == null || e == null)
			{
				finish(OperationStatus.STATUS_SUCCESSFUL);
				return;
			}
			Singleton<ConfigurationManager>.Instance.OnConfigurationInited -= HandleOnConfigurationInited;
			status = ((e.Status != ConfigurationInitEventArgs.InitStatus.Success && e.Status != ConfigurationInitEventArgs.InitStatus.NonFatalError) ? OperationStatus.STATUS_FAILED : OperationStatus.STATUS_SUCCESSFUL);
			if (status == OperationStatus.STATUS_FAILED)
			{
			}
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}
	}
}
