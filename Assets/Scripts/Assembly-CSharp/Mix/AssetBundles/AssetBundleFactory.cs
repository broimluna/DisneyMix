using System;
using Mix.Assets;
using UnityEngine;

namespace Mix.AssetBundles
{
	public class AssetBundleFactory
	{
		private AssetManager assetManager;

		public AssetBundleController assetBundleController;

		public AssetBundleFactory(AssetManager aAssetManager, MonoBehaviour aMonoBehaviour, ReferenceCounter aReferenceCounter, AssetCache aAssetCache)
		{
			assetManager = aAssetManager;
			assetBundleController = new AssetBundleController(aMonoBehaviour, aReferenceCounter, aAssetCache);
		}

		public void LoadAssetBundle(string aEntitlementUrl, IBundleObject aContentCallback, string aCacheKey, bool aUnpackTexture = false, object aUserData = null, bool aUseMemCache = true, Action<float> aDownloadPercentCallback = null, ThreadPriority aThreadPriority = ThreadPriority.Normal, string aAssetName = null)
		{
			string cpipePrefix = assetManager.GetCpipePrefix(aEntitlementUrl);
			string streamingFilePath = AssetManager.GetStreamingFilePath(cpipePrefix);
			string cpipeUrl = assetManager.GetCpipeUrl(cpipePrefix);
			int fileVersion = assetManager.cpipeManager.cpipe.GetFileVersion(cpipePrefix);
			bool aHashNew = !assetManager.cpipeManager.cpipe.DoLocalAndCurrentHashesMatch(cpipePrefix);
			bool aInStreamingAssets = assetManager.DoesExist(string.Empty, streamingFilePath);
			if (string.IsNullOrEmpty(aCacheKey))
			{
				aCacheKey = assetManager.ConvertEntitlementPathToCpipePath(aEntitlementUrl);
			}
			LoadAssetBundle(cpipeUrl, aContentCallback, aCacheKey, fileVersion, streamingFilePath, aHashNew, aInStreamingAssets, aUnpackTexture, aUserData, aUseMemCache, aDownloadPercentCallback, aThreadPriority, aAssetName);
		}

		public void LoadAssetBundle(string aUrl, IBundleObject aContentCallback, string aCacheKey, int aVersion, string aStreamingAssetsPath, bool aHashNew, bool aInStreamingAssets = false, bool aUnpackTexture = false, object aUserData = null, bool aUseMemCache = true, Action<float> aDownloadPercentCallback = null, ThreadPriority aThreadPriority = ThreadPriority.Normal, string aAssetName = null)
		{
			AssetBundleData aAssetBundleData = new AssetBundleData(aUrl, aContentCallback, aCacheKey, aVersion, aStreamingAssetsPath, aHashNew, aInStreamingAssets, aUnpackTexture, aUserData, aUseMemCache, aDownloadPercentCallback, aThreadPriority, aAssetName);
			assetBundleController.QueueAssetBundle(aAssetBundleData);
		}
	}
}
