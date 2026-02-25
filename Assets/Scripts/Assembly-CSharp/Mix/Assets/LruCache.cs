namespace Mix.Assets
{
	public class LruCache : AssetCache
	{
		public LruCache(long maxBytes = 10485760, long minBytes = 10485760)
			: base(maxBytes, minBytes)
		{
		}

		public override void Get(string aKey, ref object obj)
		{
			if (cache.ContainsKey(aKey))
			{
				CacheObject cacheObject = cache[aKey];
				obj = cacheObject.Obj;
				if (cacheObject.index != 0)
				{
					priorityList.Remove(cacheObject);
					priorityList.Insert(0, cacheObject);
					UpdatePriorityList();
				}
			}
		}

		public override object Get(string aKey)
		{
			if (cache.ContainsKey(aKey))
			{
				CacheObject cacheObject = cache[aKey];
				object obj = cacheObject.Obj;
				if (cacheObject.index != 0)
				{
					priorityList.Remove(cacheObject);
					priorityList.Insert(0, cacheObject);
					UpdatePriorityList();
				}
				return obj;
			}
			return null;
		}
	}
}
