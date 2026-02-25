using System.Collections.Generic;
using DeviceDB;

namespace Mix.DeviceDb
{
	public class OfflineQueueDocumentCollectionApi : MixDocumentCollectionApi<OfflineQueueDocument>, IOfflineQueueDatabaseApi
	{
		protected override string CollectionName
		{
			get
			{
				return "OfflineQueue";
			}
		}

		public void SaveOfflineQueue(string key, List<OfflineQueueItem> items)
		{
			IDocumentCollection<OfflineQueueDocument> collection = GetCollection(GetUserId(), "/" + CollectionName + "/" + GetUserId());
			if (collection == null)
			{
				return;
			}
			collection.Drop();
			if (items == null)
			{
				return;
			}
			int num = 0;
			foreach (OfflineQueueItem item in items)
			{
				OfflineQueueDocument offlineQueueDocument = new OfflineQueueDocument();
				offlineQueueDocument.queueIndex = num;
				offlineQueueDocument.type = item.type;
				offlineQueueDocument.data = item.data;
				collection.Insert(offlineQueueDocument);
			}
		}

		public List<OfflineQueueDocument> GetOfflineQueue(string key)
		{
			List<OfflineQueueDocument> list = new List<OfflineQueueDocument>();
			IDocumentCollection<OfflineQueueDocument> collection = GetCollection(GetUserId(), "/" + CollectionName + "/" + GetUserId());
			if (collection == null)
			{
				return list;
			}
			foreach (OfflineQueueDocument item in collection)
			{
				list.Add(item);
			}
			return list;
		}
	}
}
