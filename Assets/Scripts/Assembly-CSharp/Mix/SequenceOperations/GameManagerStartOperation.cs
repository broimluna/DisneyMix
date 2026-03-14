using Mix.Games;

namespace Mix.SequenceOperations
{
	public class GameManagerStartOperation : SequenceOperation
	{
		public GameManagerStartOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			MonoSingleton<GameManager>.Instance.Init();
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}
	}
}
