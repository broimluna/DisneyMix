using System;
using System.Collections;

namespace Mix.Assets.Worker
{
	public class CheckForRecord : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private object userData;

		protected ICheckForRecord caller;

		protected IAssetDatabaseApi databaseApi;

		private bool mCancelled;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public CheckForRecord(ICheckForRecord aCaller, string aSha, IAssetDatabaseApi aDatabaseApi, object aUserData = null)
		{
			caller = aCaller;
			databaseApi = aDatabaseApi;
			userData = aUserData;
			Hashtable hashtable = new Hashtable();
			hashtable["sha"] = aSha;
			hashtable["isInDB"] = false;
			ThreadPoolManager.Instance.addToPool(this, hashtable);
		}

		public void ThreadedMethod(object aObject)
		{
			if (!mCancelled)
			{
				try
				{
					Hashtable hashtable = (Hashtable)aObject;
					hashtable["isInDB"] = databaseApi.CheckForRecord((string)hashtable["sha"]);
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
				bool aIsInDB = false;
				if (hashtable != null && hashtable.ContainsKey("isInDB"))
				{
					aIsInDB = (bool)hashtable["isInDB"];
				}
				caller.OnCheckForRecord(aIsInDB, userData);
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
