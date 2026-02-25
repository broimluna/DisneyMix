using DeviceDB;

namespace Mix.DeviceDb
{
	public class AvatarTextureDocumentCollectionApi : MixDocumentCollectionApi<AvatarTextureDocument>, IAvatarTextureDocumentCollectionApi
	{
		protected override string CollectionName
		{
			get
			{
				return "AvatarTextureDb";
			}
		}

		public void AddAvatarTextureData(string index, string diffusePath, string normalPath, bool isHd, float loadPercentage)
		{
			IDocumentCollection<AvatarTextureDocument> collection = GetCollection(CollectionName, "/" + CollectionName);
			if (collection != null)
			{
				AvatarTextureDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "textureHash", index);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new AvatarTextureDocument();
					documentByFieldAndKey.textureHash = index;
					documentByFieldAndKey.diffusePath = diffusePath;
					documentByFieldAndKey.normalPath = normalPath;
					documentByFieldAndKey.isHd = isHd;
					documentByFieldAndKey.loadPercentage = loadPercentage;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.diffusePath = diffusePath;
					documentByFieldAndKey.normalPath = normalPath;
					documentByFieldAndKey.isHd = isHd;
					documentByFieldAndKey.loadPercentage = loadPercentage;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		public AvatarTextureDocument GetAvatarTextureData(string index)
		{
			IDocumentCollection<AvatarTextureDocument> collection = GetCollection(CollectionName, "/" + CollectionName);
			if (collection == null)
			{
				return null;
			}
			return GetDocumentByFieldAndKey(collection, "textureHash", index);
		}

		public void RemoveAvatarTextureData(string index)
		{
			IDocumentCollection<AvatarTextureDocument> collection = GetCollection(CollectionName, "/" + CollectionName);
			if (collection != null)
			{
				AvatarTextureDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "textureHash", index);
				if (documentByFieldAndKey != null)
				{
					collection.Delete(documentByFieldAndKey.Id);
				}
			}
		}
	}
}
