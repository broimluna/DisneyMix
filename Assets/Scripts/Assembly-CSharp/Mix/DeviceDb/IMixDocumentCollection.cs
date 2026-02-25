using DeviceDB;

namespace Mix.DeviceDb
{
	public interface IMixDocumentCollection<TDocument> where TDocument : AbstractDocument, new()
	{
		void Clear();

		string GetUserId();

		TDocument GetDocumentByFieldAndKey(IDocumentCollection<TDocument> collection, string fieldName, string key);

		IDocumentCollection<TDocument> GetCollection(string collectionName, string aDirPath);
	}
}
