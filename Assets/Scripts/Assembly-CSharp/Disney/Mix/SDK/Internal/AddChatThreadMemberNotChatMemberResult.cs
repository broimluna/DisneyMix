namespace Disney.Mix.SDK.Internal
{
	internal class AddChatThreadMemberNotChatMemberResult : IAddChatThreadMemberNotChatMemberResult, IAddChatThreadMemberResult
	{
		public bool Success { get; private set; }

		public AddChatThreadMemberNotChatMemberResult(bool success)
		{
			Success = success;
		}
	}
}
