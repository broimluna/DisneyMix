using UnityEngine;

namespace Mix.Assets
{
	public class BundleRef
	{
		public string Url { get; set; }

		public int Version { get; set; }

		public AssetBundle Bundle { get; set; }

		public BundleRef(string aUrl, int aVersion, AssetBundle aAssetBundle)
		{
			Url = aUrl;
			Version = aVersion;
			Bundle = aAssetBundle;
		}
	}
}
