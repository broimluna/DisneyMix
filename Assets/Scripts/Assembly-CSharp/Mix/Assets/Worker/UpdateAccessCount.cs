using System;
using System.Collections;

namespace Mix.Assets.Worker
{
	public class UpdateAccessCount : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private object userData;

		protected IUpdateAccessCount caller;

		protected IAssetDatabaseApi databaseApi;

		private bool mCancelled;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public UpdateAccessCount(IUpdateAccessCount aCaller, string aSha, IAssetDatabaseApi aDatabaseApi, object aUserData = null)
		{
			caller = aCaller;
			databaseApi = aDatabaseApi;
			userData = aUserData;
			Hashtable hashtable = new Hashtable();
			hashtable["sha"] = aSha;
			hashtable["rows_affected"] = -1;
			ThreadPoolManager.Instance.addToPool(this, hashtable);
		}

		public void ThreadedMethod(object aObject)
		{
			if (!mCancelled)
			{
				try
				{
					Hashtable hashtable = (Hashtable)aObject;
					hashtable["rows_affected"] = databaseApi.IncrementAccessCount((string)hashtable["sha"]);
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
				int aRowsAffected = -1;
				if (hashtable != null && hashtable.ContainsKey("rows_affected"))
				{
					aRowsAffected = (int)hashtable["rows_affected"];
				}
				caller.OnUpdateAccessCount(aRowsAffected, userData);
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
