using System.Threading;
using UnityEngine;

namespace Disney.Manimal.Async.Unity
{
	public sealed class UnitySynchronizationContext : SynchronizationContext
	{
		private readonly IConcurrentScheduler _scheduler;

		public UnitySynchronizationContext(MonoBehaviour behavior)
		{
			_scheduler = new UnityThreadScheduler(behavior);
		}

		public override void Post(SendOrPostCallback d, object state)
		{
			_scheduler.QueueAction(delegate
			{
				d(state);
			});
		}
	}
}
