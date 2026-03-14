using Mix;

namespace Tests
{
	public class ThreadTestImpl : ThreadPoolManager.IUnityThreadPoolInterface
	{
		public delegate void Callback();

		public bool isComplete;

		public object mCallback;

		private bool mCancelled;

		private Callback call;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public ThreadTestImpl(Callback mCallback)
		{
			call = mCallback;
		}

		public void ThreadedMethod(object o)
		{
		}

		public void ThreadComplete(object o)
		{
			if (!mCancelled)
			{
				call();
			}
			Destroy();
		}

		public void Destroy()
		{
		}
	}
}
