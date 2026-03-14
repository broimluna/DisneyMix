using System.Threading;

namespace Mix.Test
{
	public class TestOffThreadWorker : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private bool mCancelled;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public TestOffThreadWorker()
		{
			if (Thread.CurrentThread.IsBackground)
			{
			}
			ThreadPoolManager.Instance.addToPool(this, null);
		}

		public void ThreadedMethod(object aObject)
		{
			if (!Thread.CurrentThread.IsBackground)
			{
			}
			string aObject2 = "test string";
			new TestUIThreadWorker(aObject2);
		}

		public void ThreadComplete(object aObject)
		{
			if (!Thread.CurrentThread.IsBackground)
			{
			}
		}

		public void Destroy()
		{
		}
	}
}
