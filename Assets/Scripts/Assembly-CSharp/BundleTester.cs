using Mix;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.DeviceDb;
using Mix.Entitlements;
using UnityEngine;

public class BundleTester : MonoBehaviour, IBundleObject, ICpipeReady, IEntitlementsManager
{
	void IBundleObject.OnBundleAssetObject(Object go, object aUserData)
	{
		Debug.Log("on load");
		if (go != null)
		{
			Object.Instantiate(go);
		}
	}

	void IEntitlementsManager.OnEntitlementsManagerReady(bool IsSuccessful)
	{
		Debug.Log("all manifests loaded");
		MonoSingleton<AssetManager>.Instance.LoadABundle(this, "mega_avatarbundle.unity3d", null, "avtr_tAcc_0002_glassesWireRim", true);
	}

	private void Start()
	{
		IAssetDatabaseApi assetCacheDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.assetCacheDocumentCollectionApi;
		MonoSingleton<AssetManager>.Instance.AssetDatabaseApi = assetCacheDocumentCollectionApi;
		MonoSingleton<AssetManager>.Instance.Init(this);
	}

	public void OnCpipeReady(CpipeEvent aCpipeEvent)
	{
		Debug.Log("CPIPE Ready: ");
		Singleton<EntitlementsManager>.Instance.LoadNewContentData(this);
	}

	public void OnCpipeFail(CpipeEvent aCpipeEvent)
	{
		Debug.Log("CPIPE Fail");
		Singleton<EntitlementsManager>.Instance.LoadNewContentData(this);
	}
}
