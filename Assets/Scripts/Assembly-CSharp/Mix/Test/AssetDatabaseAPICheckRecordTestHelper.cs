using Mix.Assets;
using Mix.Assets.Worker;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class AssetDatabaseAPICheckRecordTestHelper : SequenceOperation, IAddRecord, ICheckForRecord
	{
		private MonoBehaviour monoEngine;

		private Record threadedRecord;

		public AssetDatabaseAPICheckRecordTestHelper(IOperationCompleteHandler aCaller, MonoBehaviour aMonoEngine)
			: base(aCaller)
		{
			monoEngine = aMonoEngine;
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			Record aRecord = new Record("testsha");
			MonoSingleton<AssetManager>.Instance.AssetDatabaseApi.AddRecord(aRecord);
			if (!MonoSingleton<AssetManager>.Instance.AssetDatabaseApi.CheckForRecord("testsha"))
			{
				IntegrationTest.Fail(monoEngine.gameObject);
			}
			if (MonoSingleton<AssetManager>.Instance.AssetDatabaseApi.CheckForRecord("testsha_should_not_be_there"))
			{
				IntegrationTest.Fail(monoEngine.gameObject);
			}
			StartThreadedCheck();
		}

		public void StartThreadedCheck()
		{
			threadedRecord = new Record("test_threaded_sha");
			new AddRecord(this, threadedRecord, MonoSingleton<AssetManager>.Instance.AssetDatabaseApi);
		}

		public void OnAddRecord(object aUserData)
		{
			new CheckForRecord(this, threadedRecord.Sha, MonoSingleton<AssetManager>.Instance.AssetDatabaseApi);
		}

		public void OnCheckForRecord(bool aIsInDB, object aUserData)
		{
			if (aIsInDB)
			{
				IntegrationTest.Pass(monoEngine.gameObject);
				finish(OperationStatus.STATUS_SUCCESSFUL);
			}
			else
			{
				IntegrationTest.Fail(monoEngine.gameObject);
				finish(OperationStatus.STATUS_FAILED);
			}
			Application.TestingDirectory = string.Empty;
		}
	}
}
