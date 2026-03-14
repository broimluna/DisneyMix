using Mix.Threading;

namespace Mix.SequenceOperations
{
	public class StartThreadFramerateThrottlingOperation : SequenceOperation
	{
		public StartThreadFramerateThrottlingOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			Singleton<ThreadFramerateThrottler>.Instance.EnterThrottlingSection();
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}
	}
}
