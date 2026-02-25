namespace Disney.Mix.SDK.Internal
{
	internal class SendGameStateMessageResult : ISendGameStateMessageResult
	{
		public bool Success { get; private set; }

		public SendGameStateMessageResult(bool success)
		{
			Success = success;
		}
	}
}
