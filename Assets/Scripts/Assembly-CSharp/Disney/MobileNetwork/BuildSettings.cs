using System.Collections.Generic;
using System.Text;
using LitJson;

namespace Disney.MobileNetwork
{
	public static class BuildSettings
	{
		public static byte[] SaltBytes = new byte[32]
		{
			251, 239, 229, 223, 199, 193, 181, 173, 163, 151,
			139, 131, 113, 107, 101, 89, 79, 73, 67, 59,
			47, 41, 31, 23, 19, 17, 13, 11, 7, 5,
			3, 2
		};

		public static string SETTINGS_FILE = "BuildSettings.txt";

		private static IDictionary<string, object> m_keyValueStore = new Dictionary<string, object>();

		public static void LoadSettings()
		{
			string buildSettingsJson = EnvironmentManager.GetBuildSettingsJson();

			if (!string.IsNullOrEmpty(buildSettingsJson) && !buildSettingsJson.TrimStart().StartsWith("{"))
			{
				string key = Encoding.UTF8.GetString(SaltBytes);
				try
				{
					buildSettingsJson = AesCipher.Decrypt(buildSettingsJson, key);
				}
				catch
				{
					m_keyValueStore = new Dictionary<string, object>();
					return;
				}
			}

			if (string.IsNullOrEmpty(buildSettingsJson))
			{
				m_keyValueStore = new Dictionary<string, object>();
				return;
			}

			m_keyValueStore = JsonMapperSimple.ToObjectSimple(buildSettingsJson) as IDictionary<string, object>;
			if (m_keyValueStore == null)
			{
				m_keyValueStore = new Dictionary<string, object>();
			}
		}

		public static T Get<T>(string key)
		{
			// use safe accessor to avoid KeyNotFoundException
			return Get(key, default(T));
		}

		public static T Get<T>(string key, T defaultValue)
		{
			if (m_keyValueStore != null && m_keyValueStore.ContainsKey(key))
			{
				return (T)m_keyValueStore[key];
			}
			return defaultValue;
		}
	}
}
