using System.Collections.Generic;
using Mix.Assets;
using Mix.Assets.Worker;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class AssetDatabaseManageHeadersTestHelper : SequenceOperation, IAddRecord, IGetHeader, IGetRecord, IUpdateRecord
	{
		private MonoBehaviour monoEngine;

		private Record threadedRecord;

		private Dictionary<string, string> threadedHeader;

		public AssetDatabaseManageHeadersTestHelper(IOperationCompleteHandler aCaller, MonoBehaviour aMonoEngine)
			: base(aCaller)
		{
			monoEngine = aMonoEngine;
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			AddUpdateHeader();
		}

		public void AddUpdateHeader()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("h1_key", "h1_value");
			dictionary.Add("h2_key", "h2_value");
			dictionary.Add("h3_key", "h3_value");
			Record record = new Record("header_test", dictionary, null, 0L, string.Empty);
			MonoSingleton<AssetManager>.Instance.AssetDatabaseApi.AddRecord(record);
			Dictionary<string, string> header = MonoSingleton<AssetManager>.Instance.AssetDatabaseApi.GetHeader("header_test");
			if (header == null)
			{
				RecordResult(false);
				return;
			}
			if (header["h1_key"] != dictionary["h1_key"] || header["h2_key"] != dictionary["h2_key"] || header["h3_key"] != dictionary["h3_key"])
			{
				RecordResult(false);
				return;
			}
			record.Header["h1_key"] = "new value for h1_key";
			MonoSingleton<AssetManager>.Instance.AssetDatabaseApi.UpdateRecord(record);
			header = MonoSingleton<AssetManager>.Instance.AssetDatabaseApi.GetHeader("header_test");
			if (header == null)
			{
				RecordResult(false);
			}
			else if (header["h1_key"] != "new value for h1_key" || header["h2_key"] != dictionary["h2_key"] || header["h3_key"] != dictionary["h3_key"])
			{
				RecordResult(false);
			}
			else
			{
				StartThreaded();
			}
		}

		public void StartThreaded()
		{
			threadedHeader = new Dictionary<string, string>();
			threadedHeader.Add("h1_key", "h1_value");
			threadedHeader.Add("h2_key", "h2_value");
			threadedHeader.Add("h3_key", "h3_value");
			threadedRecord = new Record("threaded_header", threadedHeader, null, 0L, string.Empty);
			new AddRecord(this, threadedRecord, MonoSingleton<AssetManager>.Instance.AssetDatabaseApi);
		}

		public void OnAddRecord(object aUserData)
		{
			new GetHeader(this, threadedRecord.Sha, MonoSingleton<AssetManager>.Instance.AssetDatabaseApi);
		}

		public void OnGetHeader(Dictionary<string, string> aHeader, object aUserData)
		{
			foreach (string key in aHeader.Keys)
			{
			}
			if (aHeader["h1_key"] != threadedHeader["h1_key"] || aHeader["h2_key"] != threadedHeader["h2_key"] || aHeader["h3_key"] != threadedHeader["h3_key"])
			{
				RecordResult(false);
				return;
			}
			threadedRecord.Header["h1_key"] = "h1_value_threaded!";
			new UpdateRecord(this, threadedRecord, MonoSingleton<AssetManager>.Instance.AssetDatabaseApi);
		}

		public void OnUpdateRecord(object aUserData)
		{
			new GetRecord(this, threadedRecord.Sha, MonoSingleton<AssetManager>.Instance.AssetDatabaseApi);
		}

		public void OnGetRecord(Record aRecord, object aUserData)
		{
			if (aRecord == null)
			{
				RecordResult(false);
			}
			else if (aRecord.Header["h1_key"] != "h1_value_threaded!" || aRecord.Header["h2_key"] != threadedHeader["h2_key"] || aRecord.Header["h3_key"] != threadedHeader["h3_key"])
			{
				RecordResult(false);
			}
			else
			{
				RecordResult(true);
			}
		}

		public void RecordResult(bool success)
		{
			Application.TestingDirectory = string.Empty;
			if (success)
			{
				IntegrationTest.Pass(monoEngine.gameObject);
			}
			else
			{
				IntegrationTest.Fail(monoEngine.gameObject);
			}
		}
	}
}
