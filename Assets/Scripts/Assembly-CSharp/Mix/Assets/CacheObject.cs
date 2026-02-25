namespace Mix.Assets
{
	public class CacheObject
	{
		public object Obj;

		public string key;

		public int index;

		public bool isRefCounting;

		public long size { get; private set; }

		public CacheObject(object aObj, string aKey, int aIndex, long aSize, bool aIsRefCounting = false)
		{
			Obj = aObj;
			key = aKey;
			index = aIndex;
			size = aSize;
			isRefCounting = aIsRefCounting;
		}
	}
}
