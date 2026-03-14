using System;
using System.Collections;
using System.Text;

namespace Mix.Assets.Worker
{
	public class StringToByte : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private object userData;

		protected IStringToByte caller;

		private bool mCancelled;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public StringToByte(IStringToByte aCaller, string s, object aUserData = null)
		{
			caller = aCaller;
			userData = aUserData;
			Hashtable hashtable = new Hashtable();
			hashtable["string"] = s;
			hashtable["bytes"] = null;
			ThreadPoolManager.Instance.addToPool(this, hashtable);
		}

		public void ThreadedMethod(object aObject)
		{
			if (!mCancelled)
			{
				try
				{
					Hashtable hashtable = (Hashtable)aObject;
					hashtable["bytes"] = Encoding.Unicode.GetBytes((string)hashtable["string"]);
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
				byte[] bytes = null;
				if (hashtable != null && hashtable.ContainsKey("bytes"))
				{
					bytes = (byte[])hashtable["bytes"];
				}
				caller.OnStringToByte(bytes, userData);
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
