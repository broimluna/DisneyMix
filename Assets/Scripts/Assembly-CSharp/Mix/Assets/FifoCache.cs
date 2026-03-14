namespace Mix.Assets
{
	public class FifoCache : AssetCache
	{
		public FifoCache(long maxBytes = 10485760, long minBytes = 10485760)
			: base(maxBytes, minBytes)
		{
		}

		public override void Get(string aKey, ref object obj)
		{
			if (cache.ContainsKey(aKey))
			{
				obj = cache[aKey].Obj;
			}
		}

		public override object Get(string aKey)
		{
			if (cache.ContainsKey(aKey))
			{
				return cache[aKey].Obj;
			}
			return null;
		}
	}
}
