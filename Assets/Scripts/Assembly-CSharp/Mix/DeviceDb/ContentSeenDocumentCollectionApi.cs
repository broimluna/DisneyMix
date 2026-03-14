using DeviceDB;

namespace Mix.DeviceDb
{
	public class ContentSeenDocumentCollectionApi : MixDocumentCollectionApi<ContentSeenDocument>, IContentSeenDatabaseApi
	{
		protected override string CollectionName
		{
			get
			{
				return "ContentSeen";
			}
		}

		public IDocumentCollection<ContentSeenDocument> collection { get; private set; }

		public ContentSeenDocumentCollectionApi()
		{
			collection = GetCollection(GetUserId(), "/" + CollectionName + "/" + GetUserId());
		}

		public bool IsContentSeen(string aContentId)
		{
			if (collection == null)
			{
				return true;
			}
			ContentSeenDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "contentId", aContentId);
			return documentByFieldAndKey != null;
		}

		public void SetContentSeen(string aContentId)
		{
			if (collection != null)
			{
				ContentSeenDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "contentId", aContentId);
				if (documentByFieldAndKey == null)
				{
					ContentSeenDocument contentSeenDocument = new ContentSeenDocument();
					contentSeenDocument.contentId = aContentId;
					collection.Insert(contentSeenDocument);
				}
			}
		}

		public void ClearContentSeen(string aContentId)
		{
			if (collection != null)
			{
				ContentSeenDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "contentId", aContentId);
				if (documentByFieldAndKey != null)
				{
					collection.Delete(documentByFieldAndKey.Id);
				}
			}
		}
	}
}
