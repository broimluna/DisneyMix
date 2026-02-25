using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mix.Assets
{
	public class GoogleOAuthHelper
	{
		public delegate void OauthTokenCallback(AuthResponse aAuthResponse);

		private const int maxRetrys = 3;

		private int retrys;

		public OauthTokenCallback callback;

		public void Retry()
		{
			retrys++;
			if (retrys > 3)
			{
				callback(null);
			}
			else
			{
				MonoSingleton<AssetManager>.Instance.StartCoroutine(GetOAuthRequest(callback));
			}
		}

		public IEnumerator GetOAuthRequest(OauthTokenCallback aCallback, Dictionary<string, string> aHeaders = null)
		{
			yield return null;
		}

		public static long GetUnixEpochTimeSecs()
		{
			return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
		}

		public static string Base64Encode(string inputString)
		{
			if (inputString == null)
			{
				return null;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(inputString);
			return Convert.ToBase64String(bytes);
		}
	}
}
