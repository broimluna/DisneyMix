using System;
using Avatar.DataTypes;
using Avatar.Interfaces;
using Mix.DeviceDb;

namespace Mix.Avatar
{
	public class AvatarSnapshotDiskCache : ICache<AvatarSnapshotData>
	{
		private IAvatarSnapshotManagerDependencies manager;

		private IAvatarSnapshotDocumentCollectionApi dbApi;

		public AvatarSnapshotDiskCache(IAvatarSnapshotManagerDependencies aManager, IAvatarSnapshotDocumentCollectionApi aDbApi)
		{
			manager = aManager;
			dbApi = aDbApi;
		}

		public void GetCacheData(string id, CacheCallback<AvatarSnapshotData> callback)
		{
			AvatarSnapshotDocument avatarSnapshotData = dbApi.GetAvatarSnapshotData(id);
			if (avatarSnapshotData != null)
			{
				if (manager.DoesFileExist(avatarSnapshotData.path))
				{
					manager.LoadSnapshots(avatarSnapshotData, delegate(AvatarSnapshotData ret)
					{
						callback((ret != null) ? true : false, ret);
						if (ret == null)
						{
							dbApi.RemoveAvatarSnapshotData(id);
						}
					});
				}
				else
				{
					callback(false, null);
				}
			}
			else
			{
				callback(false, null);
			}
		}

		public void SetCacheData(string id, AvatarSnapshotData cacheData, Action<bool> callback)
		{
			AvatarSnapshotDocument info = new AvatarSnapshotDocument();
			string text = "/Avatar/" + id + "/";
			info.path = text + "snapshot.png";
			info.loadPercentage = cacheData.loadPercentage;
			info.isHd = cacheData.isHd;
			info.hasNormals = cacheData.hasNormals;
			info.size = cacheData.size;
			manager.GenerateFolder(text);
			manager.SaveSnapshots(info, cacheData, delegate(bool success)
			{
				if (success)
				{
					dbApi.AddAvatarSnapshotData(id, info.path, info.isHd, info.hasNormals, info.size, info.loadPercentage);
				}
				callback(success);
			});
		}
	}
}
