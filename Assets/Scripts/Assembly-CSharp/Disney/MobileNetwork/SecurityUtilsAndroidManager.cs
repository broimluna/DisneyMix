using UnityEngine;

namespace Disney.MobileNetwork
{
	public class SecurityUtilsAndroidManager : SecurityUtilsManager
	{
		private AndroidJavaObject androidPlugin;

		protected override void Init()
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.disney.mobilenetwork.plugins.SecurityUtilsPlugin"))
			{
				if (androidJavaClass != null)
				{
					androidPlugin = androidJavaClass.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
				}
			}
		}

		public override bool IsDebuggerAttached()
		{
			return androidPlugin.Call<bool>("IsDebuggerAttached", new object[0]);
		}
	}
}
