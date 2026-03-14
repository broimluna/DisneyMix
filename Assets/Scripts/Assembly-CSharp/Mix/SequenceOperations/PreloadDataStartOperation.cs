namespace Mix.SequenceOperations
{
	public class PreloadDataStartOperation : SequenceOperation
	{
		public PreloadDataStartOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			new PreloadData();
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}
	}
}
