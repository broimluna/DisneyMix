using System.Collections;
using System.IO;
using Mix.Assets;
using Mix.DeviceDb;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class LoadManifestTestHelper : SequenceOperation
	{
		public MonoBehaviour monoEngine;

		public LoadManifestTestHelper(IOperationCompleteHandler aCaller, MonoBehaviour aMonoEngine)
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
			string path = AssetManager.GetDiskCacheFilePath() + "FirstRunManifest.txt";
			if (!File.Exists(path) || string.IsNullOrEmpty(File.ReadAllText(path)))
			{
				IntegrationTest.Fail("FirstRunManifest.txt doesnt exist or is empty in cache database.");
				return;
			}
			CheckForManifest();
			Singleton<MixDocumentCollections>.Instance.assetCacheDocumentCollectionApi.DeleteRecordBySha(AssetManager.GetShaString("cpipeManfiest"));
			Record record = Singleton<MixDocumentCollections>.Instance.assetCacheDocumentCollectionApi.GetRecord(AssetManager.GetShaString("cpipeManfiest"));
			if (record != null)
			{
				IntegrationTest.Fail("Cpipe entry should not exist in asset cache database.");
				return;
			}
			path = AssetManager.GetDiskCacheFilePath() + AssetManager.GetShaString("cpipeManfiest") + ".txt";
			if (File.Exists(path))
			{
				IntegrationTest.Fail("Cpipe manifest should not exist in cache folder");
				return;
			}
			TestingUtils.SetClientVersion("2.1.0-183088+CI");
			monoEngine.StartCoroutine(WaitForManifestLoad());
		}

		private void CheckForManifest(bool CompleteRun = false)
		{
			Record record = Singleton<MixDocumentCollections>.Instance.assetCacheDocumentCollectionApi.GetRecord(AssetManager.GetShaString("cpipeManfiest"));
			if (record == null)
			{
				IntegrationTest.Fail("Cpipe entry should exist in asset cache database. CompleteRun = " + CompleteRun);
				return;
			}
			string path = AssetManager.GetDiskCacheFilePath() + AssetManager.GetShaString("cpipeManfiest") + ".txt";
			if (!File.Exists(path))
			{
				IntegrationTest.Fail("Cpipe manifest should exist in cache folder. CompleteRun = " + CompleteRun);
			}
			else if (CompleteRun)
			{
				IntegrationTest.Pass(monoEngine.gameObject);
			}
		}

		private IEnumerator WaitForManifestLoad()
		{
			yield return new WaitForSeconds(75f);
			CheckForManifest(true);
		}
	}
}
