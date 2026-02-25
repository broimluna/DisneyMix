namespace Disney.Mix.SDK.Internal
{
	public interface IPhotoFlavorFactory
	{
		IPhotoFlavor Create(string photoId, string photoFlavorId, string url, PhotoEncoding encoding, int width, int height, byte[] key);
	}
}
