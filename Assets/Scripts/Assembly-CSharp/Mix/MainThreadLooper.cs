using System.Collections.Generic;
using UnityEngine;

namespace Mix
{
	public class MainThreadLooper
	{
		private object ThisLock = new Object();

		protected List<MainThreadWorker> Workers;

		private int count;

		public MainThreadLooper()
		{
			Workers = new List<MainThreadWorker>();
		}

		public void AddMainThreadWorker(MainThreadWorker aWorker)
		{
			if (aWorker != null)
			{
				lock (ThisLock)
				{
					Workers.Add(aWorker);
					count++;
				}
			}
		}

		public void UpdateLoop()
		{
			MainThreadWorker mainThreadWorker = null;
			if (count > 0)
			{
				lock (ThisLock)
				{
					mainThreadWorker = Workers[0];
					Workers.RemoveAt(0);
					count--;
				}
				if (mainThreadWorker != null)
				{
					mainThreadWorker.DoWork();
				}
			}
		}
	}
}
