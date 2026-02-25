using System;
using System.Collections;
using System.IO;

namespace Mix.Assets
{
	public class DiskSizer : ThreadPoolManager.IUnityThreadPoolInterface
	{
		protected IDiskSizer caller;

		private bool mCancelled;

		private long CalculatedDiskSize;

		private bool IsSynchrounos;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public DiskSizer(IDiskSizer aCaller, string aPath, bool aIsSynchrounos = false)
		{
			IsSynchrounos = aIsSynchrounos;
			caller = aCaller;
			Hashtable hashtable = new Hashtable();
			hashtable["path"] = aPath;
			long num = 0L;
			hashtable["size"] = num;
			if (IsSynchrounos)
			{
				ThreadedMethod(hashtable);
			}
			else
			{
				ThreadPoolManager.Instance.addToPool(this, hashtable);
			}
		}

		public void ThreadedMethod(object aObject)
		{
			Hashtable hashtable = (Hashtable)aObject;
			string path = (string)hashtable["path"];
			DateTime now = DateTime.Now;
			WalkDirectoryTree(new DirectoryInfo(path));
			hashtable["size"] = CalculatedDiskSize;
			if (IsSynchrounos)
			{
				ThreadComplete(hashtable);
			}
		}

		public void ThreadComplete(object aObject)
		{
			if (!mCancelled && caller != null)
			{
				Hashtable hashtable = (Hashtable)aObject;
				long aSize = 0L;
				if (hashtable != null && hashtable.ContainsKey("size"))
				{
					aSize = (long)hashtable["size"];
				}
				caller.OnDiskSizer(aSize);
			}
			Destroy();
		}

		public void Destroy()
		{
			caller = null;
		}

		private void WalkDirectoryTree(DirectoryInfo root)
		{
			FileInfo[] array = null;
			DirectoryInfo[] array2 = null;
			try
			{
				array = root.GetFiles("*.*");
			}
			catch (UnauthorizedAccessException exception)
			{
				Log.Exception("UnauthorizedAccessException", exception);
			}
			catch (DirectoryNotFoundException exception2)
			{
				Log.Exception("DirectoryNotFoundException", exception2);
			}
			if (array != null)
			{
				FileInfo[] array3 = array;
				foreach (FileInfo fileInfo in array3)
				{
					FileInfo fileInfo2 = new FileInfo(fileInfo.FullName);
					CalculatedDiskSize += fileInfo2.Length;
				}
				array2 = root.GetDirectories();
				DirectoryInfo[] array4 = array2;
				foreach (DirectoryInfo root2 in array4)
				{
					WalkDirectoryTree(root2);
				}
			}
		}
	}
}
