using System;
using System.Collections;
using Mix.Assets;
using Mix.Connectivity;

namespace Mix
{
	public class CpipeFileManifestLoader : ITextAssetObject
	{
		private const int MAX_LOAD_ATTEMPTS = 3;

		private const float LOAD_ATTEMPT_INTERVAL = 0.5f;

		private CpipeCallback onComplete;

		private CpipeErrorCallback onError;

		private CpipeFileManifest manifest;

		private int loadAttempts;

		private string oldVersion;

		private string ContentVersion = string.Empty;

		void ITextAssetObject.TextAssetObjectComplete(string aText, object aUserData)
		{
			TextReady(aText, aUserData);
		}

		public void Load(CpipeCallback onComplete, CpipeErrorCallback onError, bool aIsFirstRun, CachePolicy aCachePolicy, string oldVersion)
		{
			this.onComplete = onComplete;
			this.onError = onError;
			this.oldVersion = oldVersion;

			// CPIPE backend retired: always use bundled preload manifest.
			PrepareManifest(null, true);
		}

		public void TextReady(string aText, object aUserData)
		{
			if (aUserData != null)
			{
				Hashtable hashtable = (Hashtable)aUserData;
				if ((string)hashtable["FLOW_HTTPSTATUS"] == "304")
				{
					onComplete(false);
					return;
				}
			}
			if (aText != string.Empty && aText != null)
			{
				PrepareManifest(aText, false);
				return;
			}
			if (!string.IsNullOrEmpty(ContentVersion) && MonoSingleton<ConnectionManager>.Instance.IsConnected && aUserData != null)
			{
				Hashtable hashtable2 = (Hashtable)aUserData;
				if ((string)hashtable2["FLOW_HTTPSTATUS"] == "204")
				{
					Log.Exception("No Cpipe Manifest for client version: " + ContentVersion);
				}
			}
			onError("no manifest for client version " + ContentVersion);
		}

		public bool IsLoaded()
		{
			return manifest != null;
		}

		public CpipeFileManifest GetManifest()
		{
			if (!IsLoaded())
			{
				throw new Exception("The CPIPE manifest has not been instantiated yet. Has CpipeFileManifestLoader.Load been called?");
			}
			return manifest;
		}

		private void PrepareManifest(string json, bool aFirstRun)
		{
			manifest = new CpipeFileManifest();
			if (!manifest.Prepare(json, aFirstRun))
			{
				onError("error preparing manifest");
			}
			else
			{
				onComplete(true);
			}
		}
	}
}
