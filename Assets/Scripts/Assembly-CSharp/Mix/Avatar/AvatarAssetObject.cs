using Avatar.Interfaces;
using Mix.AssetBundles;
using UnityEngine;

namespace Mix.Avatar
{
	public class AvatarAssetObject : IBundleObject
	{
		private BundleCallback callback;

		public AvatarAssetObject(BundleCallback aCallback)
		{
			callback = aCallback;
		}

		public void OnBundleAssetObject(Object aGameObject, object aUserData)
		{
			callback(aGameObject, aUserData);
		}
	}
}
