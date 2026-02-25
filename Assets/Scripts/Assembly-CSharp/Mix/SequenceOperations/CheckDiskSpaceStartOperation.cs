using Mix.Assets;

namespace Mix.SequenceOperations
{
	public class CheckDiskSpaceStartOperation : SequenceOperation
	{
		public CheckDiskSpaceStartOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			MonoSingleton<AssetManager>.Instance.CheckDiskSpace(Onclick);
		}

		public void Onclick()
		{
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}
	}
}
