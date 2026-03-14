using System.Threading;
using UnityEngine;

namespace Mix.Test
{
	public class TestUIThreadWorker : MainThreadWorker
	{
		public TestUIThreadWorker(object aObject)
			: base(aObject)
		{
			if (Thread.CurrentThread.IsBackground)
			{
			}
		}

		public override void DoWork()
		{
			if (Thread.CurrentThread.IsBackground)
			{
			}
			new GameObject();
			base.DoWork();
			MainThreadLooperTests.DoIncrement();
		}
	}
}
