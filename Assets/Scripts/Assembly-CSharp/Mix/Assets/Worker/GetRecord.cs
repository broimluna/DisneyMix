using System;
using System.Collections;

namespace Mix.Assets.Worker
{
	public class GetRecord : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private object userData;

		protected IGetRecord caller;

		protected IAssetDatabaseApi databaseApi;

		private bool mCancelled;

		private bool synchronous = true;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public GetRecord(IGetRecord aCaller, string aSha, IAssetDatabaseApi aDatabaseApi, object aUserData = null, bool aSynchronous = true)
		{
			synchronous = aSynchronous;
			caller = aCaller;
			databaseApi = aDatabaseApi;
			userData = aUserData;
			Hashtable hashtable = new Hashtable();
			hashtable["sha"] = aSha;
			hashtable["record"] = null;
			if (synchronous)
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
			if (!mCancelled)
			{
				Hashtable hashtable = null;
				try
				{
					hashtable = (Hashtable)aObject;
					hashtable["record"] = databaseApi.GetRecord((string)hashtable["sha"]);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				if (synchronous)
				{
					ThreadComplete(hashtable);
				}
			}
		}

		public void ThreadComplete(object aObject)
		{
			if (!mCancelled && caller != null)
			{
				Hashtable hashtable = (Hashtable)aObject;
				Record aRecord = null;
				if (hashtable != null && hashtable.ContainsKey("record"))
				{
					aRecord = (Record)hashtable["record"];
				}
				caller.OnGetRecord(aRecord, userData);
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
