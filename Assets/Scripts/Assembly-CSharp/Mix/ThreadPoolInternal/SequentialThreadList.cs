using System.Collections;
using System.Threading;

namespace Mix.ThreadPoolInternal
{
	public class SequentialThreadList
	{
		private ArrayList mInactiveThreads = new ArrayList();

		private ThreadPoolManager.IUnityThreadInterface mActiveThread;

		public bool IsCancelAll;

		public void CancelAllThreads()
		{
			lock (mInactiveThreads.SyncRoot)
			{
				for (int num = mInactiveThreads.Count - 1; num >= 0; num--)
				{
					ThreadPoolManager.IUnityThreadInterface unityThreadInterface = (ThreadPoolManager.IUnityThreadInterface)mInactiveThreads[num];
					if (unityThreadInterface == null)
					{
						mInactiveThreads.RemoveAt(num);
					}
					else if (unityThreadInterface != null)
					{
						mInactiveThreads[num] = null;
						mInactiveThreads.RemoveAt(num);
						unityThreadInterface.CompleteThread();
						unityThreadInterface = null;
					}
				}
			}
		}

		public void addToList(ThreadPoolManager.IUnityThreadInterface thread)
		{
			lock (mInactiveThreads.SyncRoot)
			{
				mInactiveThreads.Add(thread);
				if (mActiveThread == null)
				{
					mActiveThread = (ThreadPoolManager.IUnityThreadInterface)mInactiveThreads[0];
					mInactiveThreads.Remove(mActiveThread);
					ThreadPool.QueueUserWorkItem(mActiveThread.Run);
				}
			}
		}

		public void RunUpdates()
		{
			lock (mInactiveThreads.SyncRoot)
			{
				if (mActiveThread != null)
				{
					if (mActiveThread.Update())
					{
						mActiveThread.CompleteThread();
						mActiveThread = null;
					}
				}
				else if (mInactiveThreads.Count > 0)
				{
					mActiveThread = (ThreadPoolManager.IUnityThreadInterface)mInactiveThreads[0];
					mInactiveThreads.Remove(mActiveThread);
					ThreadPool.QueueUserWorkItem(mActiveThread.Run);
				}
			}
		}
	}
}
