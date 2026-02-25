namespace Disney.Mix.SDK.Internal
{
	internal class RemoveChatThreadNicknameResult : IRemoveChatThreadNicknameResult
	{
		public bool Success { get; private set; }

		public RemoveChatThreadNicknameResult(bool success)
		{
			Success = success;
		}
	}
}
