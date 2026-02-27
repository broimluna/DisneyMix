using UnityEngine;
using System;

namespace Disney.MobileNetwork
{
	public class SecurityUtilsAndroidManager : SecurityUtilsManager
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		private AndroidJavaObject androidPlugin;
#endif

		protected override void Init()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.disney.mobilenetwork.plugins.SecurityUtilsPlugin"))
				{
					if (androidJavaClass != null)
					{
						androidPlugin = androidJavaClass.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogWarning("[SecurityUtils] Plugin not found: " + ex.Message);
				androidPlugin = null;
			}
#endif
		}

#if UNITY_ANDROID && !UNITY_EDITOR
		public byte[] GetAndroidDeviceKey()
		{
			if (androidPlugin == null) return null;
			try
			{
				sbyte[] key = androidPlugin.Call<sbyte[]>("getDeviceKey");
				if (key == null) return null;
				byte[] result = new byte[key.Length];
				for (int i = 0; i < key.Length; i++) result[i] = (byte)key[i];
				return result;
			}
			catch { return null; }
		}

		public bool GetAndroidIsDeviceSecure()
		{
			if (androidPlugin == null) return false;
			try { return androidPlugin.Call<bool>("isDeviceSecure"); } catch { return false; }
		}
#endif
	}
}