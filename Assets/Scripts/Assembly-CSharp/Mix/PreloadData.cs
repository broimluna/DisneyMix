using System;
using System.IO;
using Disney.MobileNetwork;
using LitJson;
using Mix.Assets;
using Mix.DeviceDb;
using Mix.Localization;

namespace Mix
{
	public class PreloadData
	{
		public static bool FirstRun;

		public static bool NewVersion;

		public static JsonData FirstRunCpipeData;

		public static JsonData CpipeData;

		public static JsonData LocData;

		public static JsonData ExternalConfigData;

		public static bool FirstRunCpipeDataComplete;

		public static bool CpipeDataComplete;

		public static bool LocDataComplete;

		public static bool ExternalConfigDataComplete;

		public PreloadData()
		{
			ConfigurationManager.InitStaticVars();
			string text = Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi.LoadDeviceValue("MixVersion");
			if (text == null)
			{
				FirstRun = true;
				Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi.SaveDeviceValue("MixVersion", EnvironmentManager.BundleVersion.ToString());
			}
			else if (!(text == EnvironmentManager.BundleVersion.ToString()))
			{
				NewVersion = true;
				Singleton<MixDocumentCollections>.Instance.assetCacheDocumentCollectionApi.DeleteRecordBySha(AssetManager.GetShaString(ConfigurationManager.RelativePathToConfigFile));
				Singleton<MixDocumentCollections>.Instance.assetCacheDocumentCollectionApi.DeleteRecordBySha(AssetManager.GetShaString("cpipeManfiest"));
				Singleton<MixDocumentCollections>.Instance.assetCacheDocumentCollectionApi.DeleteRecordBySha(AssetManager.GetShaString(Localizer.GetLocFileUrl()));
				Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi.SaveDeviceValue("MixVersion", EnvironmentManager.BundleVersion.ToString());
				string empty = string.Empty;
				empty = AssetManager.GetShaString("contentdata.joe.gz");
				Singleton<MixDocumentCollections>.Instance.assetCacheDocumentCollectionApi.DeleteRecordBySha(empty);
			}
			PreloadLoc();
			PreloadCpipeManifest();
			PreloadExternalConfig();
		}

		public void PreloadLoc()
		{
			string locFileUrl = Localizer.GetLocFileUrl();
			string streamingFilePath = AssetManager.GetStreamingFilePath(locFileUrl);
			string shaString = AssetManager.GetShaString(locFileUrl);
			string text = AssetManager.GetDiskCacheFilePath() + shaString + ".txt";
			string text2 = text;
			if (!File.Exists(text2))
			{
				text2 = streamingFilePath;
			}
			string aFileContents = AssetManager.LoadTextStatic(string.Empty, text2);
			new PreloadJsonFromDisk(aFileContents, false, ParseJsonGeneric, delegate(bool success, object aObject)
			{
				LocData = aObject as JsonData;
				LocDataComplete = true;
			});
		}

		public void PreloadCpipeManifest()
		{
			AssetManager.VerifyCacheFolder();
			string cpipeFileNameInStreamingAssets = AssetManager.GetCpipeFileNameInStreamingAssets();
			string streamingFilePath = AssetManager.GetStreamingFilePath(cpipeFileNameInStreamingAssets);
			string diskCacheFilePath = AssetManager.GetDiskCacheFilePath();
			string text = diskCacheFilePath + "FirstRunManifest.txt";
			if (!File.Exists(text))
			{
				string text2 = AssetManager.LoadTextStatic(string.Empty, streamingFilePath);
				if (!string.IsNullOrEmpty(text2))
				{
					File.WriteAllText(text, text2);
				}
			}
			new PreloadJsonFromDisk(text, true, ParseJsonGeneric, delegate(bool success, object aObject)
			{
				FirstRunCpipeData = aObject as JsonData;
				FirstRunCpipeDataComplete = true;
			});
			string shaString = AssetManager.GetShaString("cpipeManfiest");
			string text3 = AssetManager.GetDiskCacheFilePath() + shaString + ".txt";
			string text4 = text3;
			if (!File.Exists(text4))
			{
				text4 = streamingFilePath;
			}
			string aFileContents = AssetManager.LoadTextStatic(string.Empty, text4);
			new PreloadJsonFromDisk(aFileContents, false, ParseJsonGeneric, delegate(bool success, object aObject)
			{
				CpipeData = aObject as JsonData;
				CpipeDataComplete = true;
			});
		}

		public void PreloadExternalConfig()
		{
			string relativePathToConfigFile = ConfigurationManager.RelativePathToConfigFile;
			string streamingFilePath = AssetManager.GetStreamingFilePath(relativePathToConfigFile);
			string shaString = AssetManager.GetShaString(relativePathToConfigFile);
			string text = AssetManager.GetDiskCacheFilePath() + shaString + ".txt";
			string text2 = text;
			if (!File.Exists(text2))
			{
				text2 = streamingFilePath;
			}
			string aFileContents = AssetManager.LoadTextStatic(string.Empty, text2);
			new PreloadJsonFromDisk(aFileContents, false, ConfigurationManager.ParseData, delegate(bool success, object aObject)
			{
				if (success)
				{
					ExternalConfigData = aObject as JsonData;
				}
				ExternalConfigDataComplete = true;
			});
		}

		public object ParseJsonGeneric(string json)
		{
			object result = null;
			try
			{
				result = JsonMapper.ToObject(json);
			}
			catch (Exception exception)
			{
				Log.Exception(string.Empty, exception);
			}
			return result;
		}
	}
}
