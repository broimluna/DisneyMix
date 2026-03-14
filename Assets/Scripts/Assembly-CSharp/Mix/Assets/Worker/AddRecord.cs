using System;

namespace Mix.Assets.Worker
{
	public class AddRecord : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private object userData;

		protected IAddRecord caller;

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

		public AddRecord(IAddRecord aCaller, Record aRecord, IAssetDatabaseApi aDatabaseApi, object aUserData = null, bool aSynchronous = true)
		{
			synchronous = aSynchronous;
			caller = aCaller;
			databaseApi = aDatabaseApi;
			userData = aUserData;
			if (synchronous)
			{
				ThreadedMethod(aRecord);
			}
			else
			{
				ThreadPoolManager.Instance.addToPool(this, aRecord);
			}
		}

		public void ThreadedMethod(object aObject)
		{
			if (!mCancelled)
			{
				try
				{
					Record aRecord = (Record)aObject;
					databaseApi.AddRecord(aRecord);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				if (synchronous)
				{
					ThreadComplete(aObject);
				}
			}
		}

		public void ThreadComplete(object aObject)
		{
			if (!mCancelled && caller != null)
			{
				caller.OnAddRecord(userData);
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
