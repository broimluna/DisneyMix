namespace Disney.Mix.SDK.Internal
{
	internal class SendGagMessageResult : ISendGagMessageResult
	{
		public bool Success { get; private set; }

		public SendGagMessageResult(bool success)
		{
			Success = success;
		}
	}
}
