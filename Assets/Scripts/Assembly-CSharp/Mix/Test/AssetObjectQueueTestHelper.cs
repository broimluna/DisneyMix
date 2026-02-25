using System.Collections.Generic;
using Mix.Assets;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class AssetObjectQueueTestHelper : SequenceOperation, ITextAssetObject
	{
		public MonoBehaviour monoEngine;

		public int loads = 3;

		public int loaded;

		public AssetObjectQueueTestHelper(IOperationCompleteHandler aCaller, MonoBehaviour aMonoEngine)
			: base(aCaller)
		{
			monoEngine = aMonoEngine;
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			MonoSingleton<AssetManager>.Instance.maxConcurrentConnections = 0;
			textloadforqueuetest();
		}

		public void textloadforqueuetest()
		{
			MonoSingleton<AssetManager>.Instance.OnAssetObjectComplete += OnAssetObjectComplete;
			MonoSingleton<AssetManager>.Instance.AssetObjectQueue = new List<AssetObject>();
			MonoSingleton<AssetManager>.Instance.AssetObjectQueue.Clear();
			string text = "http://www.google.com?k=1";
			string shaString = AssetManager.GetShaString(text);
			LoadParams aLoadParams = new LoadParams(shaString, text);
			MonoSingleton<AssetManager>.Instance.AssetObjectQueue.Clear();
			MonoSingleton<AssetManager>.Instance.LoadText(this, aLoadParams);
			if (GetNumberOfGoogleCallsInQueue() != 1)
			{
				IntegrationTest.Fail("queue count wrong, expected 1 got " + MonoSingleton<AssetManager>.Instance.AssetObjectQueue.Count);
				return;
			}
			text = "http://www.google.com?k=2";
			shaString = AssetManager.GetShaString(text);
			aLoadParams = new LoadParams(shaString, text);
			MonoSingleton<AssetManager>.Instance.LoadText(this, aLoadParams);
			int count = MonoSingleton<AssetManager>.Instance.AssetObjectQueue.Count;
			MonoSingleton<AssetManager>.Instance.maxConcurrentConnections = 1;
			text = "http://www.google.com?k=3";
			shaString = AssetManager.GetShaString(text);
			aLoadParams = new LoadParams(shaString, text);
			MonoSingleton<AssetManager>.Instance.LoadText(this, aLoadParams);
			if (MonoSingleton<AssetManager>.Instance.AssetObjectQueue.Count == count - 1)
			{
			}
		}

		public int GetNumberOfGoogleCallsInQueue()
		{
			int num = 0;
			foreach (AssetObject item in MonoSingleton<AssetManager>.Instance.AssetObjectQueue)
			{
				if (item.LoadParams.Url.Contains("www.google.com"))
				{
					num++;
				}
			}
			return num;
		}

		public void OnAssetObjectComplete(AssetObject aAssetObject)
		{
			if (aAssetObject.LoadParams.Url.Contains("www.google.com"))
			{
				loaded++;
			}
			if (loaded == loads)
			{
				MonoSingleton<AssetManager>.Instance.OnAssetObjectComplete -= OnAssetObjectComplete;
				if (MonoSingleton<AssetManager>.Instance.AssetObjectQueue.Count != 0)
				{
					IntegrationTest.Fail("queue count wrong, expected 0 got " + MonoSingleton<AssetManager>.Instance.AssetObjectQueue.Count);
				}
				else
				{
					IntegrationTest.Pass(monoEngine.gameObject);
				}
				Application.TestingDirectory = string.Empty;
			}
		}

		public void TextAssetObjectComplete(string aText, object aUserData)
		{
		}
	}
}
