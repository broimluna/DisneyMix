namespace Disney.Mix.SDK.Internal
{
	public interface IPhotoStorage
	{
		void Store(string mediaId, string photoFlavorId, byte[] contents, byte[] encryptionKey);

		byte[] Load(string mediaId, string photoFlavorId, byte[] encryptionKey);
	}
}
