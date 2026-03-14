using Mix.Assets;

namespace Mix.Test
{
	public class AssetData
	{
		public int FileVersion = -1;

		public bool ShouldExist;

		public string EntitlementPath = string.Empty;

		public string ManifestPath = string.Empty;

		public AssetData(string aEntitlementPath, bool aShouldExist)
		{
			EntitlementPath = aEntitlementPath;
			ShouldExist = aShouldExist;
			UpdateData();
		}

		public void UpdateData()
		{
			if (EntitlementPath.ToLower().EndsWith(".unity3d"))
			{
				ManifestPath = MonoSingleton<AssetManager>.Instance.GetCpipePrefix(EntitlementPath);
			}
			else
			{
				ManifestPath = EntitlementPath;
			}
			FileVersion = MonoSingleton<AssetManager>.Instance.cpipeManager.cpipe.GetFileVersion(ManifestPath);
		}
	}
}
