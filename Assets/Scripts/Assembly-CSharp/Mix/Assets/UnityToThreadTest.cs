using System.Collections;
using UnityEngine;

namespace Mix.Assets
{
	public class UnityToThreadTest : MonoBehaviour, ITestWorker
	{
		public static ArrayList Queue = new ArrayList();

		void ITestWorker.OnTestWorker(object UserData)
		{
			Debug.Log("OnTestWorker ");
		}

		private void Update()
		{
			if (Queue.Count > 0)
			{
				Queue.RemoveAt(0);
				MakeGo();
			}
		}

		public void MakeGo()
		{
			new GameObject();
		}

		private void Start()
		{
			new TestWorker(this, this);
		}
	}
}
