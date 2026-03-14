namespace Disney.Mix.SDK.Internal
{
	internal class SendStickerMessageResult : ISendStickerMessageResult
	{
		public bool Success { get; private set; }

		public SendStickerMessageResult(bool success)
		{
			Success = success;
		}
	}
}
