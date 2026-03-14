namespace Disney.Mix.SDK.Internal
{
	internal class SendVideoMessageResult : ISendVideoMessageResult
	{
		public bool Success { get; private set; }

		public SendVideoMessageResult(bool success)
		{
			Success = success;
		}
	}
}
