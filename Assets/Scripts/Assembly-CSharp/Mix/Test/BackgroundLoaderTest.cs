using System.Collections.Generic;
using Mix.Assets;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class BackgroundLoaderTest : MonoBehaviour, IOperationCompleteHandler
	{
		private BackgroundLoader backgroundLoader;

		void IOperationCompleteHandler.OnOperationComplete(SequenceOperation aOperation)
		{
			BackgroundLoaderTestHelper();
		}

		private void Start()
		{
			TestingUtils.ClearAllCache();
			IntegrationTest.Pass(base.gameObject);
		}

		private void Update()
		{
			if (backgroundLoader != null)
			{
				List<string>[] files = backgroundLoader.GetFiles();
				List<string> list = files[0];
				List<string> list2 = files[1];
				List<string> list3 = files[2];
				if (list.Count == 0 && list2.Count == 0 && list3.Count == 0)
				{
					IntegrationTest.Pass(base.gameObject);
				}
			}
		}

		public void BackgroundLoaderTestHelper()
		{
			TestBackgroundLoader();
		}

		private void TestBackgroundLoader()
		{
			backgroundLoader = new BackgroundLoader(this);
			backgroundLoader.AddFile("avatar/accessory/en_US/avtr_tAcc_0000_empty_hd.unity3d", LoadPriority.High);
			backgroundLoader.AddFile("avatar/accessory/en_US/avtr_tAcc_0000_empty_hd.unity3d", LoadPriority.Medium);
			backgroundLoader.AddFile("avatar/accessory/en_US/avtr_tAcc_0000_empty_hd.unity3d", LoadPriority.Low);
			List<string>[] files = backgroundLoader.GetFiles();
			List<string> list = files[0];
			List<string> list2 = files[1];
			List<string> list3 = files[2];
			if (list.Count != 0 || (list2.Count != 1 && list3.Count != 1))
			{
				IntegrationTest.Fail(base.gameObject);
			}
		}
	}
}
