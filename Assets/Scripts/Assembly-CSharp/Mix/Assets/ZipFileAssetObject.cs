namespace Mix.Assets
{
	public class ZipFileAssetObject : IZipAssetObject
	{
		public IZipFileAssetObject caller;

		public object userData;

		public string zipName;

		public string entryName;

		public ZipAssetObject zipAssetObject;

		public ZipFileAssetObject(string aZipName, string aEntryName, IAssetManager aAssetManager, IZipFileAssetObject aCaller, LoadParams aLoadParams, object aUserData)
		{
			caller = aCaller;
			userData = aUserData;
			entryName = aEntryName;
			zipName = aZipName;
			zipAssetObject = new ZipAssetObject(aAssetManager, this, aLoadParams, aUserData);
		}

		void IZipAssetObject.OnZipAssetObject(string aPath, object aUserData)
		{
			if (string.IsNullOrEmpty(aPath))
			{
				caller.OnZipFileAssetObject(null, userData);
				Destroy();
			}
			else
			{
				zipAssetObject.UnZip(aPath, entryName);
				caller.OnZipFileAssetObject(AssetManager.GetDiskCacheFilePath(), userData);
			}
		}

		public void Destroy()
		{
			zipAssetObject = null;
			caller = null;
		}
	}
}
