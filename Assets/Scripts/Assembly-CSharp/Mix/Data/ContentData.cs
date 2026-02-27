using System;
using System.Collections.Generic;
using System.IO;
using Core.MetaData;
using LitJson;
using Mix.Assets;
using UnityEngine;

namespace Mix.Data
{
	public class ContentData : IZipFileAssetObject, IZipJsonAssetObject
	{
		public const string CONTENT_DATA_JSON_ZIP = "contentdata";

		public const string CONTENT_DATA_JOE_ZIP = "contentdata.joe.zip";

		public const string CONTENT_DATA_PATH = "contentdata.txt";

		public const string CONTENT_DATA_JOE_PATH = "contentdata.joe";

		private IContentData Caller;

		private CachePolicy CachePolicy;

		public ContentObj Data;

		public Catalog catalog;

		public ContentData(IContentData aCaller, CachePolicy aPolicy = CachePolicy.DefaultCacheControlProtocol)
		{
			Debug.Log("[ContentData] Ctor: creating ContentData");
			Caller = aCaller;
			CachePolicy = aPolicy;
		}

		void IZipJsonAssetObject.OnZipJsonAssetObject(object aObject, object aUserData)
		{
			Debug.Log("[ContentData] OnZipJsonAssetObject called");

			if (aObject is ContentObj)
			{
				Debug.Log("[ContentData] OnZipJsonAssetObject: object is ContentObj");
				Data = (ContentObj)aObject;
				bool flag = false;
				try
				{
					if (Data.content.objects.Avatar_Multiplane.Count > 0 &&
						Data.content.objects.Gag.Count > 0 &&
						Data.content.objects.Game.Count > 0 &&
						Data.content.objects.Sticker.Count > 0 &&
						Data.content.objects.Sticker_Tag.Count > 0 &&
						Data.content.objects.Sticker_Pack.Count > 0 &&
						Data.content.objects.Official_Account.Count > 0 &&
						Data.content.objects.Official_Account_Bot.Count > 0)
					{
						flag = true;
					}
				}
				catch (Exception ex)
				{
					Debug.LogWarning("[ContentData] OnZipJsonAssetObject validation error: " + ex);
				}
				if (flag)
				{
					Debug.Log("[ContentData] OnZipJsonAssetObject: validation OK, notifying caller");
					Caller.OnContentDataDone(null);
					return;
				}
				Debug.LogWarning("[ContentData] OnZipJsonAssetObject: validation failed");
			}
			else
			{
				Debug.LogWarning("[ContentData] OnZipJsonAssetObject: unexpected object type " + (aObject == null ? "null" : aObject.GetType().FullName));
			}

			Caller.OnContentDataDone("Error loading json zip.");
		}

		public void Init()
		{
			Debug.Log("[ContentData] Init called with cache policy: " + CachePolicy);
			Refresh(CachePolicy);
		}

		public void Refresh(CachePolicy aPolicy = CachePolicy.DefaultCacheControlProtocol)
		{
			Debug.Log("[ContentData] Refresh called with policy: " + aPolicy);

			try
			{
#if UNITY_ANDROID && !UNITY_EDITOR
				// On Android StreamingAssets is inside the APK (jar:). We CANNOT use File.* directly.
				// For now, just log and fail fast here; the file must be copied out using UnityWebRequest before this is called.
				ParseJoeData(string.Empty + "contentdata.joe");
				Caller.OnContentDataDone(null);
#else
				string sourceJoePath;
				// Editor / Standalone: StreamingAssets is a real directory, safe for File IO.
				sourceJoePath = Path.Combine(Application.StreamingAssetsPath, CONTENT_DATA_JOE_PATH);
				string cacheDir = Path.Combine(Application.PersistentDataPath, "cache");
				string cacheJoePath = Path.Combine(cacheDir, "contentfile.joe");

				Debug.Log("[ContentData] Refresh: sourceJoePath=" + sourceJoePath);
				Debug.Log("[ContentData] Refresh: cacheDir=" + cacheDir);
				Debug.Log("[ContentData] Refresh: cacheJoePath=" + cacheJoePath);

				if (!Directory.Exists(cacheDir))
				{
					Debug.Log("[ContentData] Refresh: cacheDir does not exist, creating");
					Directory.CreateDirectory(cacheDir);
				}

				if (File.Exists(sourceJoePath))
				{
					Debug.Log("[ContentData] Refresh: source JOE exists, copying to cache");
					File.Copy(sourceJoePath, cacheJoePath, true);

					Debug.Log("[ContentData] Refresh: calling ParseJoeData with " + cacheJoePath);
					ParseJoeData(cacheJoePath);
				}
				else
				{
					Debug.LogError("[ContentData] Refresh: source JOE file not found at " + sourceJoePath);
					Caller.OnContentDataDone("Source JOE file not found at " + sourceJoePath);
				}
#endif
			}
			catch (Exception ex)
			{
				Debug.LogError("[ContentData] Refresh error: " + ex);
				Caller.OnContentDataDone("Error refreshing content data: " + ex.Message);
			}
		}

		public void OnZipFileAssetObject(string zipFileBasePath, object userData)
		{
			Debug.Log("[ContentData] OnZipFileAssetObject called. zipFileBasePath=" + zipFileBasePath + ", userData=" + (userData ?? (object)"<null>"));

            // In this project zipFileBasePath is always empty, so we ignore it and use a known source.
            // On non-Android platforms we can read StreamingAssets with System.IO.

#if UNITY_ANDROID && !UNITY_EDITOR
			// On Android StreamingAssets is inside the APK (jar:). We CANNOT use File.* directly.
			// For now, just log and fail fast here; the file must be copied out using UnityWebRequest before this is called.
			try {
			ParseJoeData(zipFileBasePath + "contentdata.joe");
			Caller.OnContentDataDone(null);
			}
			            catch (Exception ex)
            {
                Debug.LogError("[ContentData] OnZipFileAssetObject error: " + ex);
                Caller.OnContentDataDone("Error loading/copying JOE data: " + ex.Message);
            }
#else
            string sourceJoePath;
            // Editor / Standalone: StreamingAssets is a real directory, safe for File IO.
            sourceJoePath = Path.Combine(Application.StreamingAssetsPath, CONTENT_DATA_JOE_PATH);
            string cacheDir = Path.Combine(Application.PersistentDataPath, "cache");
            string cacheJoePath = Path.Combine(cacheDir, "contentfile.joe");

            Debug.Log("[ContentData] OnZipFileAssetObject: sourceJoePath=" + sourceJoePath);
            Debug.Log("[ContentData] OnZipFileAssetObject: cacheDir=" + cacheDir);
            Debug.Log("[ContentData] OnZipFileAssetObject: cacheJoePath=" + cacheJoePath);

            try
            {
                if (!Directory.Exists(cacheDir))
                {
                    Debug.Log("[ContentData] OnZipFileAssetObject: cacheDir does not exist, creating");
                    Directory.CreateDirectory(cacheDir);
                }

                if (File.Exists(sourceJoePath))
                {
                    Debug.Log("[ContentData] OnZipFileAssetObject: source JOE exists, copying to cache");
                    File.Copy(sourceJoePath, cacheJoePath, true);

                    Debug.Log("[ContentData] OnZipFileAssetObject: calling ParseJoeData with " + cacheJoePath);
                    ParseJoeData(cacheJoePath);
                }
                else
                {
                    Debug.LogError("[ContentData] OnZipFileAssetObject: source JOE file not found at " + sourceJoePath);
                    Caller.OnContentDataDone("Source JOE file not found at " + sourceJoePath);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("[ContentData] OnZipFileAssetObject error: " + ex);
                Caller.OnContentDataDone("Error loading/copying JOE data: " + ex.Message);
            }
#endif
        }

		private void ParseJoeData(string joeFilePath)
		{
			Debug.Log("[ContentData] ParseJoeData called. joeFilePath=" + joeFilePath);

			try
			{
				if (!File.Exists(joeFilePath))
				{
					Debug.LogError("[ContentData] ParseJoeData: file does not exist at " + joeFilePath);
				}

				catalog = new Catalog();
				Debug.Log("[ContentData] ParseJoeData: calling catalog.PatchData");
				catalog.PatchData(joeFilePath, null);

				Data = new ContentObj();
				Data.content = new Content();
				Data.content.objects = new Objects();

				Sheet sheet = catalog.GetSheet("Avatar_Multiplane");
				Data.content.objects.Avatar_Multiplane = new List<Avatar_Multiplane>();
				if (sheet != null)
				{
					Debug.Log("[ContentData] ParseJoeData: populating Avatar_Multiplane");
					foreach (KeyValuePair<string, Row> allRow in sheet.GetAllRows())
					{
						Data.content.objects.Avatar_Multiplane.Add(new Avatar_Multiplane(sheet, allRow.Value));
					}
				}
				else
				{
					Debug.LogWarning("[ContentData] ParseJoeData: sheet Avatar_Multiplane is null");
				}

				sheet = catalog.GetSheet("Gag");
				Data.content.objects.Gag = new List<Gag>();
				if (sheet != null)
				{
					Debug.Log("[ContentData] ParseJoeData: populating Gag");
					foreach (KeyValuePair<string, Row> allRow2 in sheet.GetAllRows())
					{
						Data.content.objects.Gag.Add(new Gag(sheet, allRow2.Value));
					}
				}
				else
				{
					Debug.LogWarning("[ContentData] ParseJoeData: sheet Gag is null");
				}

				sheet = catalog.GetSheet("Sticker");
				Data.content.objects.Sticker = new List<Sticker>();
				if (sheet != null)
				{
					Debug.Log("[ContentData] ParseJoeData: populating Sticker");
					foreach (KeyValuePair<string, Row> allRow3 in sheet.GetAllRows())
					{
						Data.content.objects.Sticker.Add(new Sticker(sheet, allRow3.Value));
					}
				}
				else
				{
					Debug.LogWarning("[ContentData] ParseJoeData: sheet Sticker is null");
				}

				sheet = catalog.GetSheet("Sticker_Tag");
				Data.content.objects.Sticker_Tag = new List<Sticker_Tag>();
				if (sheet != null)
				{
					Debug.Log("[ContentData] ParseJoeData: populating Sticker_Tag");
					foreach (KeyValuePair<string, Row> allRow4 in sheet.GetAllRows())
					{
						Data.content.objects.Sticker_Tag.Add(new Sticker_Tag(sheet, allRow4.Value));
					}
				}
				else
				{
					Debug.LogWarning("[ContentData] ParseJoeData: sheet Sticker_Tag is null");
				}

				sheet = catalog.GetSheet("Sticker_Pack");
				Data.content.objects.Sticker_Pack = new List<Sticker_Pack>();
				if (sheet != null)
				{
					Debug.Log("[ContentData] ParseJoeData: populating Sticker_Pack");
					foreach (KeyValuePair<string, Row> allRow5 in sheet.GetAllRows())
					{
						Data.content.objects.Sticker_Pack.Add(new Sticker_Pack(sheet, allRow5.Value));
					}
				}
				else
				{
					Debug.LogWarning("[ContentData] ParseJoeData: sheet Sticker_Pack is null");
				}

				sheet = catalog.GetSheet("Game");
				Data.content.objects.Game = new List<Game>();
				if (sheet != null)
				{
					Debug.Log("[ContentData] ParseJoeData: populating Game");
					foreach (KeyValuePair<string, Row> allRow6 in sheet.GetAllRows())
					{
						Data.content.objects.Game.Add(new Game(sheet, allRow6.Value));
					}
				}
				else
				{
					Debug.LogWarning("[ContentData] ParseJoeData: sheet Game is null");
				}

				sheet = catalog.GetSheet("Official_Account");
				Data.content.objects.Official_Account = new List<Official_Account>();
				if (sheet != null)
				{
					Debug.Log("[ContentData] ParseJoeData: populating Official_Account");
					foreach (KeyValuePair<string, Row> allRow7 in sheet.GetAllRows())
					{
						Data.content.objects.Official_Account.Add(new Official_Account(sheet, allRow7.Value));
					}
				}
				else
				{
					Debug.LogWarning("[ContentData] ParseJoeData: sheet Official_Account is null");
				}

				sheet = catalog.GetSheet("Official_Account_Bot");
				Data.content.objects.Official_Account_Bot = new List<Official_Account_Bot>();
				if (sheet != null)
				{
					Debug.Log("[ContentData] ParseJoeData: populating Official_Account_Bot");
					foreach (KeyValuePair<string, Row> allRow8 in sheet.GetAllRows())
					{
						Data.content.objects.Official_Account_Bot.Add(new Official_Account_Bot(sheet, allRow8.Value));
					}
				}
				else
				{
					Debug.LogWarning("[ContentData] ParseJoeData: sheet Official_Account_Bot is null");
				}

				Debug.Log("[ContentData] ParseJoeData: finished parsing, notifying caller");
				Caller.OnContentDataDone(null);
			}
			catch (Exception ex)
			{
				Debug.LogError("[ContentData] ParseJoeData error: " + ex);
				Caller.OnContentDataDone("Error parsing JOE data." + ex.Message);
				DeleteZipCache();
			}
		}

		public object ParseJsonString(string json)
		{
			Debug.Log("[ContentData] ParseJsonString called");

			object result = null;
			try
			{
				DateTime now = DateTime.Now;
				Debug.Log("[ContentData] ParseJsonString: starting parse at " + now);
				result = JsonMapper.ToObject<ContentObj>(json);
				Debug.Log("[ContentData] ParseJsonString: parse success");
			}
			catch (Exception exception)
			{
				Debug.LogError("[ContentData] ParseJsonString error: " + exception);
				Log.Exception(string.Empty, exception);
				DeleteZipCache();
			}
			return result;
		}

		private void DeleteZipCache()
		{
			Debug.Log("[ContentData] DeleteZipCache called");

			try
			{
				string sha = AssetManager.GetShaString("contentdata.joe.gz");
				Debug.Log("[ContentData] DeleteZipCache: SHA=" + sha);

				MonoSingleton<AssetManager>.Instance.AssetDatabaseApi.DeleteRecordBySha(sha);
				MonoSingleton<AssetManager>.Instance.FlagRefreshContent();
				Debug.Log("[ContentData] DeleteZipCache: cache deleted and content refresh flagged");
			}
			catch (Exception ex)
			{
				Debug.LogError("[ContentData] DeleteZipCache error: " + ex);
			}
		}
	}
}
