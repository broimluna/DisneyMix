using LitJson;
using Mix.Assets;

namespace Mix
{
	public class Cpipe
	{
		private CpipeFileManifestLoader manifestLoader;

		public void Init(CpipeCallback onReady, CpipeErrorCallback onFailed, bool IsFirstRun, CachePolicy aCachePolicy, string oldVersion)
		{
			manifestLoader = new CpipeFileManifestLoader();
			manifestLoader.Load(onReady, onFailed, IsFirstRun, aCachePolicy, oldVersion);
		}

		public JsonData GetLocalHashes()
		{
			return manifestLoader.GetManifest().GetLocalHashes();
		}

		public JsonData GetHashes()
		{
			return manifestLoader.GetManifest().GetHashes();
		}

		public JsonData GetData()
		{
			return manifestLoader.GetManifest().GetData();
		}

		public string GetLatestHash(string relPath)
		{
			return manifestLoader.GetManifest().GetLatestHash(relPath);
		}

		public void SetLocalHashes(JsonData data)
		{
			manifestLoader.GetManifest().SetLocalHashes(data);
		}

		public bool DoesNewHashMatch(JsonData oldHash, string relPath)
		{
			return manifestLoader.GetManifest().DoesNewHashMatch(oldHash, relPath);
		}

		public bool DoLocalAndCurrentHashesMatch(string relativePath)
		{
			return manifestLoader.GetManifest().DoLocalAndCurrentHashesMatch(relativePath);
		}

		public string GetFileUrl(string relativePath)
		{
			if (manifestLoader == null)
			{
				return string.Empty;
			}
			if (manifestLoader.IsLoaded())
			{
				return manifestLoader.GetManifest().TranslateFileUrl(relativePath);
			}
			return string.Empty;
		}

		public int GetFileVersion(string relativePath)
		{
			return manifestLoader.GetManifest().GetVersionFromFileUrl(relativePath);
		}

		public string GetManifestVersion()
		{
			return manifestLoader.GetManifest().GetManifestVersion();
		}
	}
}
