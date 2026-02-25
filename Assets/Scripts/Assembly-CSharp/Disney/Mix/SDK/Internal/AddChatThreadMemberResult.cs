namespace Disney.Mix.SDK.Internal
{
	internal class AddChatThreadMemberResult : IAddChatThreadMemberResult
	{
		public bool Success { get; private set; }

		public AddChatThreadMemberResult(bool success)
		{
			Success = success;
		}
	}
}
