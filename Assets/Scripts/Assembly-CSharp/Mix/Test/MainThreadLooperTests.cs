using UnityEngine;

namespace Mix.Test
{
	public class MainThreadLooperTests : MonoBehaviour
	{
		public static int Counter;

		public static int CounterTarget = 10;

		private void Start()
		{
			ThreadPoolManager instance = ThreadPoolManager.Instance;
			for (int i = 0; i < CounterTarget; i++)
			{
				new TestOffThreadWorker();
			}
		}

		private void Update()
		{
			if (Counter == CounterTarget)
			{
				IntegrationTest.Pass(base.gameObject);
			}
		}

		public static void DoIncrement()
		{
			Counter++;
		}
	}
}
