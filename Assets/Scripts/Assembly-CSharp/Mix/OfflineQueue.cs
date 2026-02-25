using System.Collections.Generic;
using Mix.DeviceDb;

namespace Mix
{
	public class OfflineQueue
	{
		private IOfflineQueueDatabaseApi offlineDatabase;

		private List<OfflineQueueItem> items;

		public OfflineQueue()
		{
			offlineDatabase = Singleton<MixDocumentCollections>.Instance.offlineQueueDocumentCollectionApi;
			items = new List<OfflineQueueItem>();
		}

		public void Add(string id, OfflineQueueItem itemToQueue)
		{
			if (!string.IsNullOrEmpty(id))
			{
				RemoveDupeUniqeItems(itemToQueue);
				items.Add(itemToQueue);
				offlineDatabase.SaveOfflineQueue(id, items);
			}
		}

		public void Process(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return;
			}
			items.Clear();
			List<OfflineQueueDocument> offlineQueue = offlineDatabase.GetOfflineQueue(id);
			foreach (OfflineQueueDocument item in offlineQueue)
			{
				OfflineQueueItem offlineQueueItem = null;
				if (item.type == "avatar")
				{
					offlineQueueItem = new AvatarQueueItem(item.data);
				}
				else
				{
					if (!(item.type == "push"))
					{
						continue;
					}
					offlineQueueItem = new PushQueueItem(item.data);
				}
				offlineQueueItem.index = item.queueIndex;
				items.Add(offlineQueueItem);
			}
			items.Sort((OfflineQueueItem item1, OfflineQueueItem item2) => item1.index.CompareTo(item2.index));
			List<OfflineQueueItem> list = new List<OfflineQueueItem>();
			foreach (OfflineQueueItem item2 in items)
			{
				if (!item2.Process())
				{
					list.Add(item2);
				}
			}
			items.Clear();
			offlineDatabase.SaveOfflineQueue(id, list);
		}

		private void RemoveDupeUniqeItems(OfflineQueueItem itemToQueue)
		{
			List<OfflineQueueItem> list = new List<OfflineQueueItem>();
			foreach (OfflineQueueItem item in items)
			{
				if (item.type == itemToQueue.type)
				{
					list.Add(item);
				}
			}
			foreach (OfflineQueueItem item2 in list)
			{
				items.Remove(item2);
			}
		}
	}
}
