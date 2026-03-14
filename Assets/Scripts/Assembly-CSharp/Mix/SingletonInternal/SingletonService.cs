namespace Mix.SingletonInternal
{
	public class SingletonService<T> where T : new()
	{
		private IUnitySceneInterface scene;

		private IServiceLocator locator;

		public SingletonService(IUnitySceneInterface _scene)
		{
			scene = _scene;
		}

		public T GetInstance()
		{
			if (locator == null)
			{
				locator = scene.GetServiceLocator();
			}
			T val = locator.GetService<T>();
			if (val == null)
			{
				val = new T();
				locator.SetService(val);
			}
			return val;
		}

		public void ClearInstance()
		{
			if (locator == null)
			{
				locator = scene.GetServiceLocator();
			}
			T service = locator.GetService<T>();
			if (service != null)
			{
				locator.SetService(default(T));
			}
		}
	}
}
