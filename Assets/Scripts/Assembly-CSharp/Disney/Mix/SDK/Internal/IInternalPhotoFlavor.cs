namespace Disney.Mix.SDK.Internal
{
	public interface IInternalPhotoFlavor : IPhotoFlavor
	{
		string MediaId { get; }

		string PhotoFlavorId { get; }

		string Url { get; }

		byte[] EncryptionKey { get; }
	}
}
