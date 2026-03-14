using Mix.Assets;
using Mix.Entitlements;

namespace Mix.SequenceOperations
{
	public class EntitlementsStartOperation : SequenceOperation, IEntitlementsManager
	{
		public EntitlementsStartOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		void IEntitlementsManager.OnEntitlementsManagerReady(bool IsSuccessful)
		{
			finish((!IsSuccessful) ? OperationStatus.STATUS_FAILED : OperationStatus.STATUS_SUCCESSFUL);
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			Singleton<EntitlementsManager>.Instance.LoadNewContentData(this, CachePolicy.CacheThenBundle);
		}
	}
}
