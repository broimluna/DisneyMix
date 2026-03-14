using System;
using UnityEngine;

namespace Mix.AssetBundles
{
	public class AssetBundleData
	{
		public UnityEngine.Object LoadedAsset;

		public AssetBundle Bundle;

		public Texture2D UnpackedTexture;

		public bool ForceStreamingAssetsLoad;

		public int Retries;

		public string URL { get; private set; }

		public IBundleObject ContentCallback { get; private set; }

		public string CacheKey { get; private set; }

		public int Version { get; private set; }

		public string StreamingAssetsPath { get; private set; }

		public bool HashNew { get; private set; }

		public bool InStreamingAssets { get; private set; }

		public bool UnpackTexture { get; private set; }

		public object UserData { get; private set; }

		public bool UseMemCache { get; private set; }

		public Action<float> DownloadPercentCallback { get; private set; }

		public ThreadPriority LoadPriority { get; private set; }

		public string AssetName { get; private set; }

		public AssetBundleData(string aUrl, IBundleObject aContentCallback, string aCacheKey, int aVersion, string aStreamingAssetsPath, bool aHashNew, bool aInStreamingAssets, bool aUnpackTexture, object aUserData, bool aUseMemCache, Action<float> aDownloadPercentCallback, ThreadPriority aThreadPriority, string aAssetName)
		{
			URL = aUrl;
			ContentCallback = aContentCallback;
			CacheKey = aCacheKey;
			Version = aVersion;
			StreamingAssetsPath = aStreamingAssetsPath;
			HashNew = aHashNew;
			InStreamingAssets = aInStreamingAssets;
			UnpackTexture = aUnpackTexture;
			UserData = aUserData;
			UseMemCache = aUseMemCache;
			DownloadPercentCallback = aDownloadPercentCallback;
			LoadPriority = aThreadPriority;
			AssetName = aAssetName;
		}

		public void Callback()
		{
			if (ContentCallback != null)
			{
				ContentCallback.OnBundleAssetObject((UnityEngine.Object)GetAsset(), UserData);
			}
		}

		public object GetAsset()
		{
			if (UnpackTexture)
			{
				return UnpackedTexture;
			}
			return LoadedAsset;
		}
	}
}
