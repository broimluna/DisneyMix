using System.Collections.Generic;
using System.IO;

namespace Mix.Assets
{
	public class DiskManager : IDiskSpaceAlarm
	{
		public DiskMonitor diskMonitor = new DiskMonitor();

		public IAssetDatabaseApi assetDatabaseApi;

		public float DeletePercentage = 0.1f;

		public DiskManager()
		{
			diskMonitor.RegisterForAlarm(this);
		}

		public void OnDiskSpaceAlarm()
		{
		}

		public void ClearSomeAssetManagerAssets()
		{
			if (assetDatabaseApi == null)
			{
				return;
			}
			List<uint> idsByUseCountOrdered = assetDatabaseApi.GetIdsByUseCountOrdered(DeletePercentage);
			foreach (uint item in idsByUseCountOrdered)
			{
				DeleteAsset(item);
			}
		}

		public void DeleteUnpackFolderFiles()
		{
			string[] files = Directory.GetFiles(AssetManager.GetUnpackFolder());
			string[] array = files;
			foreach (string path in array)
			{
				File.Delete(path);
			}
		}

		public void DeleteAsset(uint id)
		{
			if (assetDatabaseApi != null)
			{
				Record recordByIndex = assetDatabaseApi.GetRecordByIndex(id);
				if (recordByIndex != null)
				{
					bool flag = MonoSingleton<AssetManager>.Instance.DeleteFile(string.Empty, recordByIndex.Path);
				}
				assetDatabaseApi.RemoveRecordByIndex(id);
			}
		}
	}
}
