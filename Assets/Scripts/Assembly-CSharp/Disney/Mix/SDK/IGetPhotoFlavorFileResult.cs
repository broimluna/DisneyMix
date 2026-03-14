namespace Disney.Mix.SDK
{
	public interface IGetPhotoFlavorFileResult
	{
		bool Success { get; }

		byte[] File { get; }
	}
}
