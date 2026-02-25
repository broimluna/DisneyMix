namespace Disney.Mix.SDK.Internal
{
	internal class GetPhotoFlavorFileResult : IGetPhotoFlavorFileResult
	{
		public bool Success { get; private set; }

		public byte[] File { get; private set; }

		public GetPhotoFlavorFileResult(byte[] file, bool success)
		{
			File = file;
			Success = success;
		}
	}
}
