using Mix.ThreadPoolInternal;
using UnityEngine;

namespace Mix
{
	public class ThreadPoolManager : MonoBehaviour
	{
		public interface IUnityThreadPoolInterface
		{
			bool Cancelled { set; }

			void ThreadComplete(object obj);

			void ThreadedMethod(object obj);

			void Destroy();
		}

		public interface IUnityThreadInterface
		{
			void Run(object obj);

			bool Update();

			void CompleteThread();

			void Cancel();
		}

		protected ThreadList mThreadList = new ThreadList();

		protected SequentialThreadList mDatabaseThreadList = new SequentialThreadList();

		public static MainThreadLooper UILooper = new MainThreadLooper();

		private static ThreadPoolManager inst;

		private static TestFrameworkHelper testingFramework;

		public ThreadList ThreadList
		{
			get
			{
				return mThreadList;
			}
		}

		public static ThreadPoolManager Instance
		{
			get
			{
				if (testingFramework == null)
				{
					UnityGameSceneWrapper scene = new UnityGameSceneWrapper();
					testingFramework = new TestFrameworkHelper(scene);
				}
				if (testingFramework.IsTesting && testingFramework.HasTestChanged())
				{
					Object.Destroy(inst);
					inst = null;
				}
				if (inst == null)
				{
					GameObject gameObject = new GameObject();
					inst = gameObject.AddComponent<ThreadPoolManager>();
				}
				return inst;
			}
		}

		private void Start()
		{
		}

		private void Update()
		{
			UILooper.UpdateLoop();
			mThreadList.RunUpdates();
			mDatabaseThreadList.RunUpdates();
		}

		public void CancelAllThreads()
		{
			mThreadList.CancelAllThreads();
			mDatabaseThreadList.CancelAllThreads();
		}

		public void addToPool(IUnityThreadPoolInterface caller, object obj)
		{
			UnityThreadDelegate unityThreadDelegate = new UnityThreadDelegate(caller, obj);
			if (caller != null && unityThreadDelegate != null)
			{
				mThreadList.addToList(unityThreadDelegate);
			}
			else if (caller != null)
			{
				caller.ThreadComplete(null);
			}
		}

		public void AddToDatabasePool(IUnityThreadPoolInterface caller, object obj)
		{
			UnityThreadDelegate unityThreadDelegate = new UnityThreadDelegate(caller, obj);
			if (caller != null && unityThreadDelegate != null)
			{
				mDatabaseThreadList.addToList(unityThreadDelegate);
			}
			else if (caller != null)
			{
				caller.ThreadComplete(null);
			}
		}
	}
}
