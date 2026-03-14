using Disney.Mix.SDK;

namespace Mix.FakeFriend.Results
{
	public class FakeSendStickerMessageResult : ISendStickerMessageResult
	{
		public bool Success { get; set; }

		public FakeSendStickerMessageResult(bool success)
		{
			Success = success;
		}
	}
}
