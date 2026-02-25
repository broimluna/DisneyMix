using System.Collections;

namespace Mix.Assets
{
	public class TestWorker : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private ITestWorker Caller;

		private object UserData;

		private bool mCancelled;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public TestWorker(ITestWorker aCaller, object aUserData)
		{
			Caller = aCaller;
			UserData = aUserData;
			Hashtable hashtable = new Hashtable();
			hashtable["looper"] = UserData;
			ThreadPoolManager.Instance.addToPool(this, hashtable);
		}

		public void ThreadedMethod(object aObject)
		{
			if (!mCancelled)
			{
				UnityToThreadTest.Queue.Add("l");
			}
		}

		public void ThreadComplete(object aObject)
		{
			if (!mCancelled)
			{
				Caller.OnTestWorker(UserData);
			}
			Destroy();
		}

		public void Destroy()
		{
		}
	}
}
