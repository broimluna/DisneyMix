using UnityEngine;
using System;

namespace Disney.MobileNetwork
{
	public class MemoryMonitorAndroidManager : MemoryMonitorManager
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		private AndroidJavaObject androidPlugin;

		protected override void Init()
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.disney.mobilenetwork.plugins.MemoryMonitorPlugin"))
				{
					if (androidJavaClass != null)
					{
						androidPlugin = androidJavaClass.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogWarning("[MemoryMonitor] Plugin not found: " + ex.Message);
				androidPlugin = null;
			}
		}

		public override ulong GetHeapSize()
		{
			if (androidPlugin == null) return 0UL;
			try {
				int num = androidPlugin.Call<int>("GetHeapSize", new object[0]);
				return (ulong)((long)num * 1024L * 1024L);
			} catch { return 0UL; }
		}

		public override ulong GetFreeBytes()
		{
			if (androidPlugin == null) return 0UL;
			try { return (ulong)androidPlugin.Call<long>("GetFreeBytes", new object[0]); } catch { return 0UL; }
		}

		public override ulong GetTotalBytes()
		{
			if (androidPlugin == null) return 0UL;
			try { return (ulong)androidPlugin.Call<long>("GetTotalBytes", new object[0]); } catch { return 0UL; }
		}

		public override float GetBatteryPercent()
		{
			if (androidPlugin == null) return 0f;
			try { return androidPlugin.Call<float>("GetBatteryPercent", new object[0]); } catch { return 0f; }
		}
#else
		protected override void Init() {}
		public override ulong GetHeapSize() { return 0UL; }
		public override ulong GetFreeBytes() { return 0UL; }
		public override ulong GetTotalBytes() { return 0UL; }
		public override float GetBatteryPercent() { return 0f; }
#endif
	}
}
