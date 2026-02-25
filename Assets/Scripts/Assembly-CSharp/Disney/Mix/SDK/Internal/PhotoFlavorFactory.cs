namespace Disney.Mix.SDK.Internal
{
	public class PhotoFlavorFactory : IPhotoFlavorFactory
	{
		private readonly AbstractLogger logger;

		private readonly IPhotoStorage photoStorage;

		private readonly IAssetLoader assetLoader;

		public PhotoFlavorFactory(AbstractLogger logger, IPhotoStorage photoStorage, IAssetLoader assetLoader)
		{
			this.logger = logger;
			this.photoStorage = photoStorage;
			this.assetLoader = assetLoader;
		}

		public IPhotoFlavor Create(string photoId, string photoFlavorId, string url, PhotoEncoding encoding, int width, int height, byte[] key)
		{
			return new PhotoFlavor(logger, photoId, photoFlavorId, url, encoding, width, height, key, photoStorage, assetLoader);
		}
	}
}
