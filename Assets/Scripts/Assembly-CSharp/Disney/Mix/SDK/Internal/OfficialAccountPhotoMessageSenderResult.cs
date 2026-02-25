namespace Disney.Mix.SDK.Internal
{
	public class OfficialAccountPhotoMessageSenderResult : IOfficialAccountPhotoMessageSenderResult
	{
		public bool Success { get; private set; }

		public OfficialAccountPhotoMessageSenderResult(bool success)
		{
			Success = success;
		}
	}
}
