using System.Collections;
using Disney.MobileNetwork;
using Mix.Assets;
using UnityEngine;

namespace Mix
{
	public class SingletonCleaner : Singleton<SingletonCleaner>
	{
		private ArrayList singletons;

		public static int cleaningHouse;

		public SingletonCleaner()
		{
			singletons = new ArrayList();
		}

		public void AddSingleton(GameObject input)
		{
			singletons.Add(input);
		}

		public void CleanSingletons()
		{
			AssetManager.CancelAllBundles();
			ThreadPoolManager.Instance.CancelAllThreads();
			Service.ResetAll();
			cleaningHouse = singletons.Count;
			for (int i = 0; i < singletons.Count; i++)
			{
				Object.Destroy((GameObject)singletons[i]);
			}
			singletons.Clear();
		}
	}
}
