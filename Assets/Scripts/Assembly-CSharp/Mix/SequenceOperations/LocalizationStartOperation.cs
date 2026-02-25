using Mix.Localization;
using UnityEngine;

namespace Mix.SequenceOperations
{
	public class LocalizationStartOperation : SequenceOperation, ILocalization
	{
		private MonoBehaviour monoEngine;

		public LocalizationStartOperation(IOperationCompleteHandler aCaller, MonoBehaviour aMonoEngine)
			: base(aCaller)
		{
			monoEngine = aMonoEngine;
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			Singleton<Localizer>.Instance.SetLocData(this, monoEngine, true);
		}

		public void OnLocalizationReady()
		{
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}
	}
}
