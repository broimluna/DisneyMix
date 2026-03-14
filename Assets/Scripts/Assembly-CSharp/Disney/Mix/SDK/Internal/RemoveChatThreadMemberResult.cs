namespace Disney.Mix.SDK.Internal
{
	internal class RemoveChatThreadMemberResult : IRemoveChatThreadMemberResult
	{
		public bool Success { get; private set; }

		public RemoveChatThreadMemberResult(bool success)
		{
			Success = success;
		}
	}
}
