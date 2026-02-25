namespace Mix.SequenceOperations
{
	public class ForceUpdateStartOperation : SequenceOperation
	{
		public ForceUpdateStartOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			Singleton<ForceUpdate>.Instance.Init();
			if (!Singleton<ForceUpdate>.Instance.CheckForForceUpdate())
			{
				finish(OperationStatus.STATUS_SUCCESSFUL);
			}
		}
	}
}
