using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class ForceUpdateTestHelper : SequenceOperation
	{
		public MonoBehaviour monoEngine;

		public ForceUpdateTestHelper(IOperationCompleteHandler aCaller, MonoBehaviour aMonoEngine)
			: base(aCaller)
		{
			monoEngine = aMonoEngine;
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			LoadManifest();
		}

		public void LoadManifest()
		{
			IntegrationTest.Fail("forceupdate should prevent startup from continuing");
		}
	}
}
