using System;
using Mix.Data;

namespace Mix.Assets
{
	public class ZipJsonAssetObject : IZipAssetObject, IJsonSerializer
	{
		public IZipJsonAssetObject caller;

		public object userData;

		public string zipName;

		public string entryName;

		public Func<string, object> method;

		public ZipAssetObject zipAssetObject;

		public ZipJsonAssetObject(Func<string, object> aMethod, string aZipName, string aEntryName, IAssetManager aAssetManager, IZipJsonAssetObject aCaller, LoadParams aLoadParams, object aUserData)
		{
			caller = aCaller;
			userData = aUserData;
			method = aMethod;
			entryName = aEntryName;
			zipName = aZipName;
			zipAssetObject = new ZipAssetObject(aAssetManager, this, aLoadParams, aUserData);
		}

		void IZipAssetObject.OnZipAssetObject(string aPath, object aUserData)
		{
			if (string.IsNullOrEmpty(aPath))
			{
				caller.OnZipJsonAssetObject(null, userData);
				Destroy();
			}
			else
			{
				zipAssetObject.UnZip(aPath, entryName);
				new JsonSerializer(this, AssetManager.GetDiskCacheFilePath() + entryName, true, method);
			}
		}

		void IJsonSerializer.OnJsonSerializer(object data)
		{
			caller.OnZipJsonAssetObject(data, userData);
		}

		public void Destroy()
		{
			zipAssetObject = null;
			caller = null;
		}
	}
}
