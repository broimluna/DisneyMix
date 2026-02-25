using System;

namespace Mix.Assets.Worker
{
	public class UpdateRecord : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private object userData;

		protected IUpdateRecord caller;

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

		public UpdateRecord(IUpdateRecord aCaller, Record aRecord, IAssetDatabaseApi aDatabaseApi, object aUserData = null, bool aSynchronous = true)
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
				Record record = null;
				try
				{
					record = (Record)aObject;
					databaseApi.UpdateRecord(record);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				if (synchronous)
				{
					ThreadComplete(record);
				}
			}
		}

		public void ThreadComplete(object aObject)
		{
			if (!mCancelled && caller != null)
			{
				caller.OnUpdateRecord(userData);
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
