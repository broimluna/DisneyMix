namespace Disney.Mix.SDK.Internal
{
	internal class SendTextMessageResult : ISendTextMessageResult
	{
		public bool Success { get; private set; }

		public bool IsModerated { get; private set; }

		public SendTextMessageResult(bool success, bool isModerated)
		{
			Success = success;
			IsModerated = isModerated;
		}
	}
}
