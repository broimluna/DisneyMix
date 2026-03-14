using System;
using Mix.AssetBundles;
using UnityEngine;

namespace Mix.Assets
{
	public interface IAssetManager
	{
		IAssetDatabaseApi GetDatabaseApiInterface();

		void Updates();

		string GetCpipeUrl(string aUrl);

		CpipeEnvironment GetEnvironment();

		string GetBuildNumber();

		void Init(ICpipeReady aCaller);

		bool IsPathLocalForIsDemoMode(string path);

		void UnloadAssetsWhenPossible();

		bool DoesExist(string relPath, string FullPath = null);

		bool SaveText(string relPath, string text, string FullPath = null);

		string LoadText(string relPath, string FullPath = null);

		bool DeleteFile(string relPath, string FullPath = null);

		bool SaveBytes(string relPath, byte[] bytes, string FullPath = null);

		void SaveImage(string relPath, Texture2D image, Action<bool> callback, string FullPath = null);

		bool IsPathAndroidStreamingAssets(string path);

		bool GetImage(string relPath, ref Texture2D image, string FullPath = null);

		void LoadImage(string relPath, ref Texture2D image, OnImageLoaded aCallback, string FullPath = null);

		AssetObject LoadAssetStoreImage(IPNGAssetObject aCaller, string aSwid, string aId, bool aIsThumb = false, object aUserData = null);

		AssetObject LoadAssetStoreVideo(IVideoAssetObject aCaller, string aSwid, string aId, object aUserData = null);

		void LoadZip(IZipAssetObject aCaller, LoadParams aLoadParams, object aUserData = null);

		void LoadText(ITextAssetObject aCaller, LoadParams aLoadParams, object aUserData = null);

		AssetObject LoadPng(IPNGAssetObject aCaller, LoadParams aLoadParams, object aUserData = null);

		void LoadABundle(IBundleObject aCaller, string aUrl, object aUserData = null, string aAssetName = "", bool aMemCache = false, bool aIsBackgroundLoad = false, bool aUseCache = false);

		void CancelBundles(IBundleObject aCaller);

		void CancelBundles(string aUrl);

		void AssetObjectDone(AssetObject aAssetObject);
	}
}
