using System;
using System.Collections;

namespace Mix.Assets.Worker
{
	public class GetAccessCount : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private object userData;

		protected IGetAccessCount caller;

		protected IAssetDatabaseApi databaseApi;

		private bool mCancelled;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public GetAccessCount(IGetAccessCount aCaller, string aSha, IAssetDatabaseApi aDatabaseApi, object aUserData = null)
		{
			caller = aCaller;
			databaseApi = aDatabaseApi;
			userData = aUserData;
			Hashtable hashtable = new Hashtable();
			hashtable["sha"] = aSha;
			hashtable["access_count"] = 0;
			ThreadPoolManager.Instance.addToPool(this, hashtable);
		}

		public void ThreadedMethod(object aObject)
		{
			if (!mCancelled)
			{
				try
				{
					Hashtable hashtable = (Hashtable)aObject;
					hashtable["access_count"] = databaseApi.GetAccessCount((string)hashtable["sha"]);
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
				if (AssetManager.IS_DEBUG)
				{
				}
				long aAccessCount = -1L;
				if (hashtable != null && hashtable.ContainsKey("access_count"))
				{
					aAccessCount = (long)hashtable["access_count"];
				}
				caller.OnGetAccessCount(aAccessCount, userData);
			}
			Destroy();
		}

		public void Destroy()
		{
			caller = null;
			userData = null;
			databaseApi = null;
		}
	}
}
