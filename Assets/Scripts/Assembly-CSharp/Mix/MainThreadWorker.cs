namespace Mix
{
	public abstract class MainThreadWorker
	{
		private object MyObject;

		protected MainThreadWorker()
		{
		}

		public MainThreadWorker(object aObject)
		{
			MyObject = aObject;
			ThreadPoolManager.UILooper.AddMainThreadWorker(this);
		}

		public object GetData()
		{
			object obj = null;
			lock (MyObject)
			{
				return MyObject;
			}
		}

		public virtual void DoWork()
		{
		}
	}
}
