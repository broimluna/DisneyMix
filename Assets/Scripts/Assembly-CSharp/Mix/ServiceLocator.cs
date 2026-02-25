using System.Collections;

namespace Mix
{
	public class ServiceLocator : IServiceLocator
	{
		private Hashtable services;

		public ServiceLocator()
		{
			services = new Hashtable();
		}

		public T GetService<T>() where T : new()
		{
			if (services.ContainsKey(typeof(T)))
			{
				return (T)services[typeof(T)];
			}
			return default(T);
		}

		public void SetService<T>(T input) where T : new()
		{
			services[typeof(T)] = input;
		}
	}
}
