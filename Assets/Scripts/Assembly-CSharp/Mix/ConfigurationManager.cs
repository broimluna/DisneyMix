using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Mix.Assets;

namespace Mix
{
	public class ConfigurationManager : Singleton<ConfigurationManager>, ITextAssetObject
	{
		private const string LAST_UPDATED = "lastUpdated";

		private const int updateSecondsInterval = 600;

		public const string ED = "9g==";

		private static string key;

		private static byte[] iv;

		public static string MixPlatformServicesUrlOverride;

		private Dictionary<string, string> configuration;

		private int numTimesConfigLoaded;

		public static string RelativePathToConfigFile { get; private set; }

		public static string EnvironmentString { get; private set; }

		private JsonData jsonData { get; set; }

		public event EventHandler<ConfigurationInitEventArgs> OnConfigurationInited = delegate
		{
		};

		public event EventHandler<ConfigurationRefreshEventArgs> OnConfigurationRefreshed = delegate
		{
		};

		public ConfigurationManager()
		{
			InitStaticVars();
		}

		void ITextAssetObject.TextAssetObjectComplete(string aText, object aUserData)
		{
			numTimesConfigLoaded++;
			if (configuration == null)
			{
				configuration = new Dictionary<string, string>();
			}
			else
			{
				configuration.Clear();
			}
			if (!string.IsNullOrEmpty(aText))
			{
				object obj = ParseData(aText);
				if (obj != null && obj is JsonData)
				{
					jsonData = (JsonData)obj;
				}
				ProcessData();
				this.OnConfigurationRefreshed(this, new ConfigurationRefreshEventArgs(true, configuration));
			}
			else
			{
				Log.Exception("Unable to get external configuration file on " + Util.AddOrdinal(numTimesConfigLoaded) + " time loaded");
				this.OnConfigurationRefreshed(this, new ConfigurationRefreshEventArgs(false, configuration));
			}
		}

		public static byte[] GetKey()
		{
			if (string.IsNullOrEmpty(key))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("U8r");
				stringBuilder.Append("Yu9u");
				stringBuilder.Append("zHmu");
				stringBuilder.Append("jSv");
				stringBuilder.Append("AogN");
				stringBuilder.Append("fj4");
				stringBuilder.Append("Ifu");
				stringBuilder.Append("C9G");
				stringBuilder.Append("RJzN");
				stringBuilder.Append("Carb");
				stringBuilder.Append("+cf");
				stringBuilder.Append("aYN");
				stringBuilder.Append("5k=");
				key = stringBuilder.ToString();
			}
			return Convert.FromBase64String(key);
		}

		public static byte[] GetIV()
		{
			if (iv == null)
			{
				iv = new byte[16];
				iv[0] = 5;
				iv[1] = 236;
				iv[2] = 85;
				iv[3] = 198;
				iv[4] = 121;
				iv[5] = 95;
				iv[6] = 151;
				iv[7] = 44;
				iv[8] = 15;
				iv[9] = 56;
				iv[10] = 162;
				iv[11] = 32;
				iv[12] = 106;
				iv[13] = 129;
				iv[14] = 227;
				iv[15] = 105;
			}
			return iv;
		}

		public static string GetEnvironment()
		{
			return "prod";
		}

		public static void InitStaticVars()
		{
			if (EnvironmentString == null)
			{
				EnvironmentString = GetEnvironment();
				switch (EnvironmentString)
				{
				case "dev":
					RelativePathToConfigFile = "donotcopy/n7c4c656_dev";
					break;
				case "qa":
					RelativePathToConfigFile = "donotcopy/n7c4c656_qa";
					break;
				default:
					RelativePathToConfigFile = "donotcopy/n7c4c656";
					break;
				}
			}
		}

		private IEnumerator WaitForPreloadData()
		{
			while (!PreloadData.ExternalConfigDataComplete)
			{
				yield return null;
			}
			if (PreloadData.ExternalConfigData != null)
			{
				jsonData = PreloadData.ExternalConfigData;
				ProcessData();
				this.OnConfigurationRefreshed(this, new ConfigurationRefreshEventArgs(true, configuration));
			}
			else
			{
				string sha = AssetManager.GetShaString(RelativePathToConfigFile);
				MonoSingleton<AssetManager>.Instance.AssetDatabaseApi.DeleteRecordBySha(sha);
				MonoSingleton<AssetManager>.Instance.FlagRefreshContent();
				Refresh(true, "ConfigurationManager");
			}
		}

		public void Init()
		{
			ExternalizedConstants.OnExternalizedConstantsRefreshed += HandleOnExternalizedConstantsRefreshed;
			ExternalizedConstants.SetupEventHandlers();
			configuration = new Dictionary<string, string>();
			MonoSingleton<StartUpSequence>.Instance.StartCoroutine(WaitForPreloadData());
		}

		public void HandleOnExternalizedConstantsRefreshed(object sender, ExternalizedConstantsEventArgs args)
		{
			if (sender != null)
			{
			}
			ExternalizedConstants.OnExternalizedConstantsRefreshed -= HandleOnExternalizedConstantsRefreshed;
			ConfigurationInitEventArgs.InitStatus status = ConfigurationInitEventArgs.InitStatus.Success;
			if (args.Status == ExternalizedConstantsEventArgs.RefreshStatus.NonFatalError)
			{
				status = ConfigurationInitEventArgs.InitStatus.NonFatalError;
			}
			else if (args.Status == ExternalizedConstantsEventArgs.RefreshStatus.FatalError)
			{
				status = ConfigurationInitEventArgs.InitStatus.FatalError;
			}
			this.OnConfigurationInited(this, new ConfigurationInitEventArgs(status));
		}

		public void Refresh(bool duringStartup = false, string message = null)
		{
			if (MonoSingleton<AssetManager>.Instance.cpipeManager != null && MonoSingleton<AssetManager>.Instance.cpipeManager.CpipeIsReady)
			{
				LoadParams aLoadParams = new LoadParams(AssetManager.GetShaString(RelativePathToConfigFile), RelativePathToConfigFile, duringStartup ? CachePolicy.CacheThenBundle : CachePolicy.DefaultCacheControlProtocol);
				MonoSingleton<AssetManager>.Instance.LoadText(this, aLoadParams, message);
			}
		}

		public static object ParseData(string aText)
		{
			object result = null;
			try
			{
				string json = EncryptionUtil.Decrypt(aText, GetKey(), GetIV());
				result = JsonMapper.ToObject(json);
			}
			catch (Exception exception)
			{
				Log.Exception(exception);
			}
			return result;
		}

		private void ProcessData()
		{
			ICollection keys = ((IDictionary)jsonData).Keys;
			foreach (string item in keys)
			{
				configuration[item] = (string)jsonData[item];
			}
		}
	}
}
