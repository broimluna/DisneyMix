using System;

namespace Disney.Mix.SDK.Internal
{
	internal class PhotoFlavor : IInternalPhotoFlavor, IPhotoFlavor
	{
		private readonly AbstractLogger logger;

		private readonly IPhotoStorage photoStorage;

		private readonly IAssetLoader assetLoader;

		public string MediaId { get; private set; }

		public string PhotoFlavorId { get; private set; }

		public PhotoEncoding Encoding { get; private set; }

		public string Url { get; private set; }

		public int Height { get; private set; }

		public int Width { get; private set; }

		public byte[] EncryptionKey { get; private set; }

		public PhotoFlavor(AbstractLogger logger, string mediaId, string photoFlavorId, string url, PhotoEncoding encoding, int width, int height, byte[] encryptionKey, IPhotoStorage photoStorage, IAssetLoader assetLoader)
		{
			MediaId = mediaId;
			PhotoFlavorId = photoFlavorId;
			Url = url;
			Encoding = encoding;
			Width = width;
			Height = height;
			EncryptionKey = encryptionKey;
			this.logger = logger;
			this.photoStorage = photoStorage;
			this.assetLoader = assetLoader;
		}

		public void GetFile(Action<IGetPhotoFlavorFileResult> callback)
		{
			try
			{
				byte[] array = photoStorage.Load(MediaId, PhotoFlavorId, EncryptionKey);
				if (array == null)
				{
					LoadAsset(callback);
				}
				else
				{
					callback(new GetPhotoFlavorFileResult(array, true));
				}
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(new GetPhotoFlavorFileResult(null, false));
			}
		}

		private void LoadAsset(Action<IGetPhotoFlavorFileResult> callback)
		{
			assetLoader.Load(Url, delegate(LoadAssetResult result)
			{
				if (result.Success)
				{
					try
					{
						photoStorage.Store(MediaId, PhotoFlavorId, result.Bytes, EncryptionKey);
						return;
					}
					finally
					{
						callback(new GetPhotoFlavorFileResult(result.Bytes, true));
					}
				}
				callback(new GetPhotoFlavorFileResult(null, false));
			});
		}
	}
}
