using System.Collections.Generic;

namespace Mix.DeviceDb
{
	public interface IOfflineQueueDatabaseApi
	{
		void SaveOfflineQueue(string key, List<OfflineQueueItem> items);

		List<OfflineQueueDocument> GetOfflineQueue(string key);
	}
}
