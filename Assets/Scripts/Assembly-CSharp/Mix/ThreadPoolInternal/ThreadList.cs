using System.Collections;
using System.Threading;

namespace Mix.ThreadPoolInternal
{
	public class ThreadList
	{
		public const string RO = "M7s";

		protected ArrayList mThreads = new ArrayList();

		public bool IsCancelAll;

		public void CancelAllThreads()
		{
			lock (mThreads.SyncRoot)
			{
				for (int num = mThreads.Count - 1; num >= 0; num--)
				{
					ThreadPoolManager.IUnityThreadInterface unityThreadInterface = (ThreadPoolManager.IUnityThreadInterface)mThreads[num];
					if (unityThreadInterface == null)
					{
						mThreads.RemoveAt(num);
					}
					else if (unityThreadInterface != null)
					{
						mThreads[num] = null;
						mThreads.RemoveAt(num);
						unityThreadInterface.Cancel();
						unityThreadInterface.CompleteThread();
						unityThreadInterface = null;
					}
				}
			}
		}

		public void addToList(ThreadPoolManager.IUnityThreadInterface thread)
		{
			lock (mThreads.SyncRoot)
			{
				mThreads.Add(thread);
			}
			ThreadPool.QueueUserWorkItem(thread.Run);
		}

		public void RunUpdates()
		{
			lock (mThreads.SyncRoot)
			{
				for (int num = mThreads.Count - 1; num >= 0; num--)
				{
					ThreadPoolManager.IUnityThreadInterface unityThreadInterface = (ThreadPoolManager.IUnityThreadInterface)mThreads[num];
					if (unityThreadInterface == null)
					{
						mThreads.RemoveAt(num);
					}
					else if (unityThreadInterface != null && unityThreadInterface.Update())
					{
						mThreads[num] = null;
						mThreads.RemoveAt(num);
						unityThreadInterface.CompleteThread();
						unityThreadInterface = null;
					}
				}
			}
		}

		public int GetActiveThreadCount()
		{
			return mThreads.Count;
		}
	}
}
