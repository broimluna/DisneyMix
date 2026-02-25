namespace Disney.Mix.SDK.Internal
{
	internal class SendPhotoMessageResult : ISendPhotoMessageResult
	{
		public bool Success { get; private set; }

		public SendPhotoMessageResult(bool success)
		{
			Success = success;
		}
	}
}
