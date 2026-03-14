namespace Disney.Mix.SDK.Internal
{
	internal class ResendChatMessageResult : IResendChatMessageResult
	{
		public bool Success { get; private set; }

		public bool IsModerated { get; private set; }

		public ResendChatMessageResult(bool success, bool isModerated)
		{
			Success = success;
			IsModerated = isModerated;
		}
	}
}
