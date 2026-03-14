namespace Mix.ThreadPoolInternal
{
	public class UnityThreadDelegate : ThreadPoolManager.IUnityThreadInterface
	{
		protected ThreadPoolManager.IUnityThreadPoolInterface mCaller;

		protected object mObject;

		private object MyLocker = new object();

		private bool m_IsDone;

		private object m_Handle = new object();

		public bool IsDone
		{
			get
			{
				lock (m_Handle)
				{
					return m_IsDone;
				}
			}
			set
			{
				lock (m_Handle)
				{
					m_IsDone = value;
				}
			}
		}

		public UnityThreadDelegate(ThreadPoolManager.IUnityThreadPoolInterface aCaller, object aObject)
		{
			mCaller = aCaller;
			mObject = aObject;
		}

		public void Run(object obj)
		{
			object obj2 = new object();
			lock (MyLocker)
			{
				obj2 = mObject;
			}
			mCaller.ThreadedMethod(obj2);
			IsDone = true;
		}

		public void Cancel()
		{
			mCaller.Cancelled = true;
		}

		public virtual bool Update()
		{
			if (IsDone)
			{
				return true;
			}
			return false;
		}

		public void CompleteThread()
		{
			if (mCaller != null)
			{
				mCaller.ThreadComplete(mObject);
			}
		}
	}
}
