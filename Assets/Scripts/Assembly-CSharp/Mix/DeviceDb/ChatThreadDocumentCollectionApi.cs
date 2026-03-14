using System.Collections.Generic;
using DeviceDB;

namespace Mix.DeviceDb
{
	public class ChatThreadDocumentCollectionApi : MixDocumentCollectionApi<ChatThreadDocument>, IChatThreadDataDatabaseApi
	{
		protected override string CollectionName
		{
			get
			{
				return "ChatThreadData";
			}
		}

		public override void LogOut()
		{
			foreach (KeyValuePair<string, IDocumentCollection<ChatThreadDocument>> collection in base.collections)
			{
				if (collection.Value != null)
				{
					collection.Value.Dispose();
				}
			}
			base.collections = new Dictionary<string, IDocumentCollection<ChatThreadDocument>>();
		}

		public void AddMostRecentWhenAllDone(string key, long aTime, string aMessageId)
		{
			IDocumentCollection<ChatThreadDocument> collection = GetCollection(GetUserId(), "/" + CollectionName + "/" + GetUserId());
			if (collection != null)
			{
				ChatThreadDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "threadId", key);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new ChatThreadDocument();
					documentByFieldAndKey.threadId = key;
					documentByFieldAndKey.mostRecentWhenAllDone = aTime;
					documentByFieldAndKey.mostRecentMessageId = aMessageId;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.mostRecentWhenAllDone = aTime;
					documentByFieldAndKey.mostRecentMessageId = aMessageId;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		public ChatThreadDocument GetMostRecentWhenAllDone(string key)
		{
			IDocumentCollection<ChatThreadDocument> collection = GetCollection(GetUserId(), "/" + CollectionName + "/" + GetUserId());
			if (collection == null)
			{
				return null;
			}
			return GetDocumentByFieldAndKey(collection, "threadId", key);
		}
	}
}
