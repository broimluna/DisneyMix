using System;
using Avatar.DataTypes;
using Avatar.Interfaces;
using Mix.Assets;
using Mix.DeviceDb;

namespace Mix.Avatar
{
	public class AvatarSnapshotCache : ICache<AvatarSnapshotData>
	{
		private IAvatarSnapshotManagerDependencies manager;

		private AvatarSnapshotDiskCache diskCache;

		public AvatarSnapshotCache(IAvatarSnapshotManagerDependencies aManager, IAvatarSnapshotDocumentCollectionApi aDbApi)
		{
			manager = aManager;
			diskCache = new AvatarSnapshotDiskCache(manager, aDbApi);
		}

		public void SetCacheData(string id, AvatarSnapshotData cacheData, Action<bool> callback)
		{
			LoadIntoCache(id, cacheData);
			diskCache.SetCacheData(id, cacheData, callback);
		}

		public void GetCacheData(string id, CacheCallback<AvatarSnapshotData> callback)
		{
			AvatarSnapshotData avatarSnapshotData = (AvatarSnapshotData)MonoSingleton<AssetManager>.Instance.assetCache.Get(id);
			if (avatarSnapshotData != null)
			{
				callback(true, avatarSnapshotData);
				return;
			}
			diskCache.GetCacheData(id, delegate(bool success, AvatarSnapshotData data)
			{
				if (success)
				{
					LoadIntoCache(id, data);
				}
				callback(success, data);
			});
		}

		private void LoadIntoCache(string id, AvatarSnapshotData cacheData)
		{
			MonoSingleton<AssetManager>.Instance.assetCache.Add(id, cacheData, cacheData.size * cacheData.size * 4);
		}
	}
}
