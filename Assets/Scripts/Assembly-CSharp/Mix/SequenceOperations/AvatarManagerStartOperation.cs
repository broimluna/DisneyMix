using Mix.Avatar;

namespace Mix.SequenceOperations
{
	public class AvatarManagerStartOperation : SequenceOperation, IAvatarManagerInitListener
	{
		public AvatarManagerStartOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			MonoSingleton<AvatarManager>.Instance.Init(this);
		}

		public void OnShadersLoaded()
		{
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}
	}
}
