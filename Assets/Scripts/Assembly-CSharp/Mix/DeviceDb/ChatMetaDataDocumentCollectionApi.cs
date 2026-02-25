using System.Collections.Generic;
using DeviceDB;

namespace Mix.DeviceDb
{
	public class ChatMetaDataDocumentCollectionApi : MixDocumentCollectionApi<ChatMetaDataDocument>, IChatMetaDataDatabaseApi
	{
		protected override string CollectionName
		{
			get
			{
				return "ChatMetaData";
			}
		}

		public override void LogOut()
		{
			foreach (KeyValuePair<string, IDocumentCollection<ChatMetaDataDocument>> collection in base.collections)
			{
				if (collection.Value != null)
				{
					collection.Value.Dispose();
				}
			}
			base.collections = new Dictionary<string, IDocumentCollection<ChatMetaDataDocument>>();
		}

		public void AddHeight(string key, float height)
		{
			IDocumentCollection<ChatMetaDataDocument> collection = GetCollection(GetUserId(), "/" + CollectionName + "/" + GetUserId());
			if (collection != null)
			{
				ChatMetaDataDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "messageId", key);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new ChatMetaDataDocument();
					documentByFieldAndKey.messageId = key;
					documentByFieldAndKey.height = height;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.height = height;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		public float GetHeight(string key)
		{
			float result = 0f;
			IDocumentCollection<ChatMetaDataDocument> collection = GetCollection(GetUserId(), "/" + CollectionName + "/" + GetUserId());
			if (collection == null)
			{
				return result;
			}
			ChatMetaDataDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "messageId", key);
			if (documentByFieldAndKey != null)
			{
				result = documentByFieldAndKey.height;
			}
			return result;
		}
	}
}
