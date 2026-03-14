using DeviceDB;

namespace Mix.DeviceDb
{
	public class AvatarSnapshotDocumentCollectionApi : MixDocumentCollectionApi<AvatarSnapshotDocument>, IAvatarSnapshotDocumentCollectionApi
	{
		protected override string CollectionName
		{
			get
			{
				return "AvatarSnapshotDb";
			}
		}

		public void AddAvatarSnapshotData(string index, string path, bool isHd, bool hasNormals, int size, float loadPercentage)
		{
			IDocumentCollection<AvatarSnapshotDocument> collection = GetCollection(CollectionName, "/" + CollectionName);
			AvatarSnapshotDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "snapshotHash", index);
			if (documentByFieldAndKey == null)
			{
				documentByFieldAndKey = new AvatarSnapshotDocument();
				documentByFieldAndKey.snapshotHash = index;
				documentByFieldAndKey.path = path;
				documentByFieldAndKey.isHd = isHd;
				documentByFieldAndKey.hasNormals = hasNormals;
				documentByFieldAndKey.size = size;
				documentByFieldAndKey.loadPercentage = loadPercentage;
				collection.Insert(documentByFieldAndKey);
			}
			else
			{
				documentByFieldAndKey.path = path;
				documentByFieldAndKey.isHd = isHd;
				documentByFieldAndKey.hasNormals = hasNormals;
				documentByFieldAndKey.size = size;
				documentByFieldAndKey.loadPercentage = loadPercentage;
				collection.Update(documentByFieldAndKey);
			}
		}

		public AvatarSnapshotDocument GetAvatarSnapshotData(string index)
		{
			IDocumentCollection<AvatarSnapshotDocument> collection = GetCollection(CollectionName, "/" + CollectionName);
			if (collection == null)
			{
				return null;
			}
			return GetDocumentByFieldAndKey(collection, "snapshotHash", index);
		}

		public void RemoveAvatarSnapshotData(string index)
		{
			IDocumentCollection<AvatarSnapshotDocument> collection = GetCollection(CollectionName, "/" + CollectionName);
			if (collection != null)
			{
				AvatarSnapshotDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "snapshotHash", index);
				if (documentByFieldAndKey != null)
				{
					collection.Delete(documentByFieldAndKey.Id);
				}
			}
		}
	}
}
