using System;
using System.Collections.Generic;
using UnityEngine;

namespace Disney.MobileNetwork
{
	public class HockeyAppManagerAndroid : HockeyAppManager
	{
		public override void ForceCrash()
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.disney.mobilenetwork.plugins.HockeyAppPlugin"))
				{
					androidJavaClass.CallStatic("forceCrash");
				}
			}
			catch (Exception ex)
			{
				Debug.LogWarning("HockeyApp ForceCrash failed (plugin missing?): " + ex.Message);
			}
		}

		private string GetVersion()
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.hockeyapp.unity.HockeyUnityPlugin"))
				{
					return androidJavaClass.CallStatic<string>("getAppVersion", new object[0]);
				}
			}
			catch
			{
				return "0.0.0";
			}
		}

		protected override List<string> GetLogHeaders()
		{
			List<string> list = new List<string>();
			list.Add("Package: " + packageID);
			string version = GetVersion();
			list.Add("Version: " + version);
			string[] array = SystemInfo.operatingSystem.Split('/');
			string item = "Android: " + array[0].Replace("Android OS ", string.Empty);
			list.Add(item);
			list.Add("Model: " + SystemInfo.deviceModel);
			list.Add("Date: " + DateTime.UtcNow.ToString("ddd MMM dd HH:mm:ss {}zzzz yyyy").Replace("{}", "GMT"));
			return list;
		}
	}
}
