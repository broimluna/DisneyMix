namespace Disney.Mix.SDK.Internal
{
	internal class SendGameStateMessageTooLargeResult : ISendGameStateMessageResult, ISendGameStateMessageTooLargeResult
	{
		public bool Success
		{
			get
			{
				return false;
			}
		}
	}
}
