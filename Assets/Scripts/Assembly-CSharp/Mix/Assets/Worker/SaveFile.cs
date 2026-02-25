using System;
using System.Collections;
using System.IO;

namespace Mix.Assets.Worker
{
	public class SaveFile : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private object userData;

		protected ISaveFile caller;

		private bool mCancelled;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public SaveFile(ISaveFile aCaller, string path, long byteSize, byte[] bytes, bool asynchronous, object aUserData = null)
		{
			caller = aCaller;
			userData = aUserData;
			Hashtable hashtable = new Hashtable();
			hashtable["path"] = path;
			hashtable["byteSize"] = byteSize;
			hashtable["bytes"] = bytes;
			hashtable["success"] = false;
			if (asynchronous)
			{
				ThreadPoolManager.Instance.addToPool(this, hashtable);
				return;
			}
			bool success = false;
			try
			{
				if (byteSize > 0 && bytes.Length > 0)
				{
					File.WriteAllBytes(path, bytes);
					success = true;
				}
			}
			catch (Exception exception)
			{
				Log.Exception("Image saving failed", exception);
			}
			if (caller != null)
			{
				caller.OnSaveFile(success, path, aUserData);
			}
		}

		public void ThreadedMethod(object aObject)
		{
			if (mCancelled)
			{
				return;
			}
			Hashtable hashtable = (Hashtable)aObject;
			try
			{
				if ((byte[])hashtable["bytes"] != null && ((byte[])hashtable["bytes"]).Length == (long)hashtable["byteSize"])
				{
					File.WriteAllBytes((string)hashtable["path"], (byte[])hashtable["bytes"]);
					hashtable["success"] = true;
				}
			}
			catch (Exception ex)
			{
				hashtable["success"] = false;
				hashtable["exceptionStackTrace"] = ex.StackTrace;
				hashtable["exceptionMessage"] = ex.Message;
			}
		}

		public void ThreadComplete(object aObject)
		{
			if (!mCancelled && caller != null)
			{
				Hashtable hashtable = (Hashtable)aObject;
				bool flag = false;
				string path = string.Empty;
				if (hashtable != null)
				{
					if (hashtable.ContainsKey("success"))
					{
						flag = (bool)hashtable["success"];
						if (!flag && (string)hashtable["exceptionStackTrace"] != null && (string)hashtable["exceptionMessage"] != null)
						{
							Log.Exception((string)hashtable["exceptionMessage"] + ", stacktrace: " + (string)hashtable["exceptionStackTrace"]);
						}
					}
					if (hashtable.ContainsKey("path"))
					{
						path = (string)hashtable["path"];
					}
				}
				caller.OnSaveFile(flag, path, userData);
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
