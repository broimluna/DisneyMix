using System.Collections;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class LoadAssetBundleHelper : SequenceOperation, IBundleObject
	{
		public MonoBehaviour monoEngine;

		private string firstEntitlement = "avatar_thumbs/en_US/avtr_blank_thumb.unity3d";

		public LoadAssetBundleHelper(IOperationCompleteHandler aCaller, MonoBehaviour aMonoEngine)
			: base(aCaller)
		{
			monoEngine = aMonoEngine;
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			MonoSingleton<AssetManager>.Instance.LoadABundle(this, firstEntitlement, "first_load", string.Empty, true, false, true);
		}

		public void OnBundleAssetObject(Object aGameObject, object aUserData)
		{
			if (!((string)aUserData).Equals("first_load"))
			{
				return;
			}
			string aKey = MonoSingleton<AssetManager>.Instance.ConvertEntitlementPathToCpipePath(firstEntitlement);
			aKey = AssetManager.RefCounter.FormatKey(aKey);
			if (MonoSingleton<AssetManager>.Instance.assetCache.Get(aKey) == null)
			{
				IntegrationTest.Fail("asset should exist in asset cache");
				return;
			}
			if (MonoSingleton<AssetManager>.Instance.GetReferenceCount(firstEntitlement) != 2)
			{
				IntegrationTest.Fail("asset should have ref count == 2. ref count was " + MonoSingleton<AssetManager>.Instance.GetReferenceCount(firstEntitlement));
				return;
			}
			Object bundleInstance = MonoSingleton<AssetManager>.Instance.GetBundleInstance(firstEntitlement);
			if (bundleInstance == null)
			{
				IntegrationTest.Fail("asset should exist in asset bundle cache");
				return;
			}
			if (MonoSingleton<AssetManager>.Instance.GetReferenceCount(firstEntitlement) != 3)
			{
				IntegrationTest.Fail("asset should have ref count == 3. ref count was " + MonoSingleton<AssetManager>.Instance.GetReferenceCount(firstEntitlement));
				return;
			}
			MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(firstEntitlement, bundleInstance);
			if (MonoSingleton<AssetManager>.Instance.GetReferenceCount(firstEntitlement) == 1)
			{
				IntegrationTest.Fail("asset should have ref count == 1. ref count was " + MonoSingleton<AssetManager>.Instance.GetReferenceCount(firstEntitlement));
			}
			else if (MonoSingleton<AssetManager>.Instance.assetCache.Get(aKey) == null)
			{
				IntegrationTest.Fail("asset should exist in asset cache");
			}
			else
			{
				monoEngine.StartCoroutine(WaitForAssetBundleControllerToFinish(firstEntitlement, aKey));
			}
		}

		private IEnumerator WaitForAssetBundleControllerToFinish(string aEntitlement, string aCacheKey)
		{
			yield return null;
			if (MonoSingleton<AssetManager>.Instance.assetCache.Get(aCacheKey) == null)
			{
				IntegrationTest.Fail("asset should exist in asset cache");
				yield break;
			}
			if (MonoSingleton<AssetManager>.Instance.GetReferenceCount(aEntitlement) != 1)
			{
				IntegrationTest.Fail("asset should have ref count == 1. ref count was " + MonoSingleton<AssetManager>.Instance.GetReferenceCount(aEntitlement));
				yield break;
			}
			Object obj = MonoSingleton<AssetManager>.Instance.GetBundleInstance(aEntitlement);
			if (obj == null)
			{
				IntegrationTest.Fail("asset should exist in asset bundle cache");
				yield break;
			}
			if (MonoSingleton<AssetManager>.Instance.GetReferenceCount(aEntitlement) != 2)
			{
				IntegrationTest.Fail("asset should have ref count == 2. ref count was " + MonoSingleton<AssetManager>.Instance.GetReferenceCount(aEntitlement));
				yield break;
			}
			MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(aEntitlement, obj);
			if (MonoSingleton<AssetManager>.Instance.GetReferenceCount(aEntitlement) != 1)
			{
				IntegrationTest.Fail("asset should have ref count == 1. ref count was " + MonoSingleton<AssetManager>.Instance.GetReferenceCount(aEntitlement));
				yield break;
			}
			if (MonoSingleton<AssetManager>.Instance.assetCache.Get(aCacheKey) == null)
			{
				IntegrationTest.Fail("asset should exist in asset cache");
				yield break;
			}
			MonoSingleton<AssetManager>.Instance.assetCache.ClearCache();
			if (MonoSingleton<AssetManager>.Instance.assetCache.Get(aCacheKey) != null)
			{
				IntegrationTest.Fail("asset should not exist in asset cache");
			}
			else if (MonoSingleton<AssetManager>.Instance.GetReferenceCount(aEntitlement) != -1)
			{
				IntegrationTest.Fail("asset should have ref count == -1. ref count was " + MonoSingleton<AssetManager>.Instance.GetReferenceCount(aEntitlement));
			}
			else
			{
				IntegrationTest.Pass();
			}
		}
	}
}
