using UnityEngine;

namespace Mix.AssetBundles
{
	public interface IBundleObject
	{
		void OnBundleAssetObject(Object aGameObject, object aUserData);
	}
}
