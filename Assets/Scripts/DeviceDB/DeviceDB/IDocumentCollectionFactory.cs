namespace DeviceDB
{
	public interface IDocumentCollectionFactory
	{
		IDocumentCollection<TDocument> CreateHighSecurityFileSystemCollection<TDocument>(string dirPath, byte[] key) where TDocument : AbstractDocument, new();
	}
}
