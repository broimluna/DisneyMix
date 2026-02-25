using System;
using System.Collections;
using System.Collections.Generic;

namespace Mix.Assets.Worker
{
	public class GetHeader : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private object userData;

		protected IGetHeader caller;

		protected IAssetDatabaseApi databaseApi;

		private bool mCancelled;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public GetHeader(IGetHeader aCaller, string aSha, IAssetDatabaseApi aDatabaseApi, object aUserData = null)
		{
			if (string.IsNullOrEmpty(aSha))
			{
				aCaller.OnGetHeader(null, aUserData);
				return;
			}
			caller = aCaller;
			databaseApi = aDatabaseApi;
			userData = aUserData;
			Hashtable hashtable = new Hashtable();
			hashtable["sha"] = aSha;
			hashtable["header"] = new Dictionary<string, string>();
			ThreadPoolManager.Instance.addToPool(this, hashtable);
		}

		public void ThreadedMethod(object aObject)
		{
			if (!mCancelled)
			{
				try
				{
					Hashtable hashtable = (Hashtable)aObject;
					hashtable["header"] = databaseApi.GetHeader((string)hashtable["sha"]);
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
				Dictionary<string, string> aHeader = null;
				if (hashtable != null && hashtable.ContainsKey("header"))
				{
					aHeader = (Dictionary<string, string>)hashtable["header"];
				}
				caller.OnGetHeader(aHeader, userData);
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
