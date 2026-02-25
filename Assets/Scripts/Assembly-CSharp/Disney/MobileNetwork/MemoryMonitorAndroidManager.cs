using UnityEngine;
using UnityEngine.Profiling;

namespace Disney.MobileNetwork
{
	public class MemoryMonitorAndroidManager : MemoryMonitorManager
	{
		private AndroidJavaObject androidPlugin;

		protected override void Init()
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.disney.mobilenetwork.plugins.MemoryMonitorPlugin"))
			{
				if (androidJavaClass != null)
				{
					androidPlugin = androidJavaClass.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
				}
			}
		}

		public override ulong GetHeapSize()
		{
            return (ulong)UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong();

        }

        public override ulong GetFreeBytes()
		{
            long total = Profiler.GetMonoHeapSizeLong();
            long used = Profiler.GetMonoUsedSizeLong();
            return (ulong)(total - used);
        }

		public override ulong GetTotalBytes()
		{
			return (ulong)UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong();
        }

        public float GetBatteryPercent()
        {
            // SystemInfo.batteryLevel returns a value from 0.0 to 1.0
            // If the value is -1.0, it means the status is unavailable (e.g., a Desktop PC without a battery)
            float batteryLevel = SystemInfo.batteryLevel;

            if (batteryLevel < 0)
            {
                // Optional: Return 100% or 0% if no battery is detected (like a Desktop PC)
                return 100f;
            }

            return batteryLevel * 100f;
        }
    }
}
