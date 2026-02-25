using Disney.Mix.SDK;

namespace Mix.FakeFriend.Results
{
	public class FakeSendPhotoMessageResult : ISendPhotoMessageResult
	{
		public bool Success { get; set; }

		public FakeSendPhotoMessageResult(bool success)
		{
			Success = success;
		}
	}
}
