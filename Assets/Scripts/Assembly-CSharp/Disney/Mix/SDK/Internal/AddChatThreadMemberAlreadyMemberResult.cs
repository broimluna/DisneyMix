namespace Disney.Mix.SDK.Internal
{
	internal class AddChatThreadMemberAlreadyMemberResult : IAddChatThreadMemberAlreadyMemberResult, IAddChatThreadMemberResult
	{
		public bool Success { get; private set; }

		public AddChatThreadMemberAlreadyMemberResult(bool success)
		{
			Success = success;
		}
	}
}
