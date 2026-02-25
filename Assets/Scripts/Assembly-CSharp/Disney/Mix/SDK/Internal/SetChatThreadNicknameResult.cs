namespace Disney.Mix.SDK.Internal
{
	internal class SetChatThreadNicknameResult : ISetChatThreadNicknameResult
	{
		public bool Success { get; private set; }

		public SetChatThreadNicknameResult(bool success)
		{
			Success = success;
		}
	}
}
