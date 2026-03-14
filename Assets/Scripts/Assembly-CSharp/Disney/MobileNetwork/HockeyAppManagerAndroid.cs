using System;
using System.Collections.Generic;

namespace Disney.MobileNetwork
{
	public class HockeyAppManagerAndroid : HockeyAppManager
	{
		public override void ForceCrash()
		{
			// Safe empty fallback. No Java plugin required.
		}

		private string GetVersion()
		{
			// Hardcoded string. Avoids calling Application.version on background threads.
			return "0.0.0";
		}

		protected override List<string> GetLogHeaders()
		{
			// Thread-safe generic responses. 
			// Do NOT use Unity's SystemInfo or Application classes here.
			List<string> list = new List<string>();
			list.Add("Package: " + packageID);
			list.Add("Version: " + GetVersion());
			list.Add("Android: Unknown");
			list.Add("Model: Unknown");
			
			try
			{
				list.Add("Date: " + DateTime.UtcNow.ToString("ddd MMM dd HH:mm:ss {}zzzz yyyy").Replace("{}", "GMT"));
			}
			catch
			{
				list.Add("Date: Unknown");
			}
			
			return list;
		}
	}
}
