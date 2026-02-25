using Mix.SingletonInternal;
using UnityEngine;

namespace Mix
{
	public class Singleton<T> where T : new()
	{
		private static SingletonService<T> service;

		private static TestFrameworkHelper testingFramework;

		private static Object _lock = new Object();

		public static bool applicationIsQuitting = false;

		public static T Instance
		{
			get
			{
				if (applicationIsQuitting)
				{
					return default(T);
				}
				lock (_lock)
				{
					if (service == null)
					{
						UnityGameSceneWrapper scene = new UnityGameSceneWrapper();
						service = new SingletonService<T>(scene);
						testingFramework = new TestFrameworkHelper(scene);
					}
					if (testingFramework == null)
					{
						UnityGameSceneWrapper scene2 = new UnityGameSceneWrapper();
						testingFramework = new TestFrameworkHelper(scene2);
					}
					if (testingFramework.IsTesting && testingFramework.HasTestChanged())
					{
						service.ClearInstance();
					}
					return service.GetInstance();
				}
			}
		}

		public void OnDestroy()
		{
			applicationIsQuitting = true;
			service.ClearInstance();
		}

		public static bool IsInstanceCreated()
		{
			if (service == null)
			{
				return false;
			}
			return true;
		}
	}
}
