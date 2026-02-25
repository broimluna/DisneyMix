using System;
using System.Collections.Generic;
using Avatar.DataTypes;
using UnityEngine;

namespace Avatar.Interfaces
{
	public interface IAssetManager
	{
		void UnloadAssetsWhenPossible();

		void LoadABundle(BundleCallback aCaller, string aUrl, object aUserData = null, string aAssetName = "", bool aMemCache = false, bool aIsBackgroundLoad = false, bool aUseCache = false);

		string GetShaString(string input);

		void LoadCachedImage(string path, Action<bool, Texture2D> callback);

		void SaveCachedImage(Texture2D texture, string path, Action<bool> callback);

		bool IsAvatarDataLoaded();

		List<AvatarElement> GetAllAvatarDataByCategory(string categoryName);

		List<AvatarElement> GetMyAvatarDataByCategory(string categoryName);

		AvatarElement GetAvatarData(string entitlementId);

		string GetAvatarElementVersionInfo(AvatarElement element);
	}
}
