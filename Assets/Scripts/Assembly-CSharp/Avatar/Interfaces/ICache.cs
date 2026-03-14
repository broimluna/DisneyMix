using System;

namespace Avatar.Interfaces
{
	public interface ICache<T>
	{
		void GetCacheData(string id, CacheCallback<T> callback);

		void SetCacheData(string id, T cacheData, Action<bool> callback);
	}
}
