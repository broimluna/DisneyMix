using System.Collections.Generic;
using System.IO;

namespace Mix.Assets
{
	public class DiskMonitor : IDiskSizer
	{
		protected bool IsGettingDiskSize;

		protected List<IDiskSpaceAlarm> Registers = new List<IDiskSpaceAlarm>();

		public long MaxCacheSize;

		public string RootFolder;

		public long CalculatedDiskSize { get; protected set; }

		public long RuntimeSize { get; protected set; }

		public DiskMonitor(long aMaxCacheSize = 1000000000, string aRootFolder = null)
		{
			if (aRootFolder == null)
			{
				RootFolder = Application.PersistentDataPath;
			}
			else
			{
				RootFolder = aRootFolder;
			}
			MaxCacheSize = aMaxCacheSize;
		}

		public void CheckForMaxSizeReached()
		{
			if (GetSize() <= MaxCacheSize)
			{
				return;
			}
			foreach (IDiskSpaceAlarm register in Registers)
			{
				if (register != null)
				{
					register.OnDiskSpaceAlarm();
				}
			}
		}

		public void RegisterForAlarm(IDiskSpaceAlarm aAlarm)
		{
			Registers.Add(aAlarm);
		}

		public void RemoveAlarm(IDiskSpaceAlarm aAlarm)
		{
			Registers.Remove(aAlarm);
		}

		public void OnDiskSizer(long aSize)
		{
			if (aSize > 0)
			{
				RuntimeSize = 0L;
				CalculatedDiskSize = 0L;
				IsGettingDiskSize = false;
				CalculatedDiskSize = aSize;
				CheckForMaxSizeReached();
			}
		}

		public long GetSize()
		{
			return CalculatedDiskSize + RuntimeSize;
		}

		public void AddFile(string aPath)
		{
			FileInfo fileInfo = new FileInfo(aPath);
			if (fileInfo.Exists)
			{
				RuntimeSize += fileInfo.Length;
				CheckForMaxSizeReached();
			}
		}

		public void AddFile(long aSize)
		{
			RuntimeSize += aSize;
			CheckForMaxSizeReached();
		}

		public void GetFolderSize(bool IsSynchrounos = false)
		{
			if (!IsGettingDiskSize)
			{
				new DiskSizer(this, RootFolder, IsSynchrounos);
			}
		}
	}
}
