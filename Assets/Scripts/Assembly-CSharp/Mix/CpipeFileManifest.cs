using System;
using System.Collections.Generic;
using LitJson;
using Mix.Assets;

namespace Mix
{
	public class CpipeFileManifest
	{
		private const string AB_KEY = "ab";

		private const string MANIFEST_KEY = "manifest";

		private const string CDN_ROOT_KEY = "cdnRoot";

		private const string VERSION_KEY = "version";

		private const string UNIQUE_KEY = "unique";

		private const string PATHS_KEY = "paths";

		private const string FILE_UNIQUE_KEY = "v";

		private const string FILE_CRC_KEY = "crc";

		private string baseUrl = string.Empty;

		private bool isReady;

		private string version;

		private JsonData fileData;

		private JsonData data;

		private Dictionary<string, string> translatedUrls;

		private JsonData locallyCachedFileData;

		public CpipeFileManifest()
		{
			translatedUrls = new Dictionary<string, string>();
		}

		public bool Prepare(string json, bool aFirstRun)
		{
			data = null;
			try
			{
				if (aFirstRun)
				{
					data = PreloadData.CpipeData;
				}
				else
				{
					data = JsonMapper.ToObject(json);
				}
				if (!data.Contains("manifest"))
				{
				}
				JsonData jsonData = data["manifest"];
				fileData = jsonData["paths"];
				if (jsonData["version"] != null)
				{
					version = jsonData["version"].ToString();
				}
				if (jsonData["unique"] != null)
				{
				}
				if (jsonData["cdnRoot"] != null)
				{
					baseUrl = jsonData["cdnRoot"].ToString();
				}
			}
			catch (Exception exception)
			{
				Log.Exception("Exception parsing Cpipe Manifest", exception);
				string shaString = AssetManager.GetShaString("cpipeManfiest");
				MonoSingleton<AssetManager>.Instance.AssetDatabaseApi.DeleteRecordBySha(shaString);
				MonoSingleton<AssetManager>.Instance.FlagRefreshContent();
				return false;
			}
			isReady = true;
			return true;
		}

		public JsonData GetLocalHashes()
		{
			return locallyCachedFileData;
		}

		public JsonData GetHashes()
		{
			return fileData;
		}

		public JsonData GetData()
		{
			return data;
		}

		private string getCRC(JsonData fileNode)
		{
			if (fileNode["crc"] != null)
			{
				return fileNode["crc"].ToString();
			}
			return null;
		}

		public string GetLatestHash(string relPath)
		{
			try
			{
				return getCRC(fileData[relPath]);
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		public void SetLocalHashes(JsonData someFileData)
		{
			locallyCachedFileData = someFileData;
		}

		public bool DoesNewHashMatch(JsonData someFileData, string relativePath)
		{
			if (fileData == null)
			{
				return true;
			}
			try
			{
				if (fileData[relativePath]["v"].ToString() == someFileData["v"].ToString())
				{
					return true;
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		public bool DoLocalAndCurrentHashesMatch(string relativePath)
		{
			if (locallyCachedFileData == null)
			{
				return true;
			}
			try
			{
				return DoesNewHashMatch(locallyCachedFileData[relativePath], relativePath);
			}
			catch (Exception)
			{
			}
			return false;
		}

		public string TranslateFileUrl(string relativePath)
		{
			AssertReady();
			if (!translatedUrls.ContainsKey(relativePath))
			{
				if (!fileData.Contains(relativePath))
				{
					return string.Empty;
				}
				string text = baseUrl;
				if (!text.EndsWith("/") && !text.StartsWith("/"))
				{
					text += "/";
				}
				JsonData jsonData = fileData[relativePath];
				string text2 = jsonData["v"].ToString();
				text = text + text2 + "/" + relativePath;
				translatedUrls[relativePath] = text;
			}
			return translatedUrls[relativePath];
		}

		public int GetVersionFromFileUrl(string relativePath)
		{
			AssertReady();
			if (fileData.Contains(relativePath))
			{
				JsonData jsonData = fileData[relativePath];
				if (jsonData != null && jsonData.Contains("v") && jsonData["v"] != null)
				{
					return int.Parse(jsonData["v"].ToString());
				}
			}
			if (!relativePath.Contains("cpipe"))
			{
			}
			return 0;
		}

		public string GetManifestVersion()
		{
			AssertReady();
			return version;
		}

		private void AssertReady()
		{
			if (!isReady)
			{
				throw new Exception("Versioned manifest is not ready. Call VersionedFileManifest.Prepare first.");
			}
		}
	}
}
