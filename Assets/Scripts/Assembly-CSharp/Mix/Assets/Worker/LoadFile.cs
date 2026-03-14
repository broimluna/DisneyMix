using System;
using System.Collections;
using System.IO;

namespace Mix.Assets.Worker
{
	public class LoadFile : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private object userData;

		protected ILoadFile caller;

		private bool mCancelled;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public LoadFile(ILoadFile aCaller, string path, object aUserData = null)
		{
			caller = aCaller;
			userData = aUserData;
			Hashtable hashtable = new Hashtable();
			hashtable["path"] = path;
			hashtable["bytes"] = null;
			hashtable["success"] = false;
			ThreadPoolManager.Instance.addToPool(this, hashtable);
		}

		public void ThreadedMethod(object aObject)
		{
			if (!mCancelled)
			{
				try
				{
					Hashtable hashtable = (Hashtable)aObject;
					hashtable["bytes"] = File.ReadAllBytes((string)hashtable["path"]);
					hashtable["success"] = true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		public void ThreadComplete(object aObject)
		{
			if (!mCancelled && caller != null)
			{
				Hashtable hashtable = (Hashtable)aObject;
				bool success = false;
				string path = string.Empty;
				byte[] bytes = null;
				if (hashtable != null)
				{
					if (hashtable.ContainsKey("success"))
					{
						success = (bool)hashtable["success"];
					}
					if (hashtable.ContainsKey("path"))
					{
						path = (string)hashtable["path"];
					}
					if (hashtable.ContainsKey("bytes"))
					{
						bytes = (byte[])hashtable["bytes"];
					}
				}
				caller.OnLoadFile(success, path, bytes, userData);
			}
			Destroy();
		}

		public void Destroy()
		{
			caller = null;
			userData = null;
		}
	}
}
