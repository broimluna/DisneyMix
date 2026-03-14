using System.Collections.Generic;

namespace Fabric
{
	internal class VirtualizationEventInstance
	{
		private static Queue<VirtualizationEventInstance> _eventInstancePool = new Queue<VirtualizationEventInstance>();

		public Event _event;

		public ComponentInstance _componentInstance;

		public bool _isPlaying;

		public double _dspTime;

		public float _time;

		public static void Initialise(int size)
		{
			for (int i = 0; i < size; i++)
			{
				_eventInstancePool.Enqueue(new VirtualizationEventInstance());
			}
		}

		public static VirtualizationEventInstance Alloc(Event e)
		{
			if (_eventInstancePool.Count == 0)
			{
				DebugLog.Print("VirtualizationEventInstance: Failed to allocate event instance", DebugLevel.Error);
				return null;
			}
			VirtualizationEventInstance virtualizationEventInstance = _eventInstancePool.Dequeue();
			Event obj = new Event();
			obj.Copy(e);
			virtualizationEventInstance._event = obj;
			return virtualizationEventInstance;
		}

		public static void Free(VirtualizationEventInstance instance)
		{
			instance._componentInstance = null;
			instance._event = null;
			instance._isPlaying = false;
			instance._dspTime = 0.0;
			instance._time = 0f;
			_eventInstancePool.Enqueue(instance);
		}
	}
}
