namespace Disney.Mix.SDK.Internal
{
	internal class AddChatThreadMemberThreadNotFoundResult : IAddChatThreadMemberResult, IAddChatThreadMemberThreadNotFoundResult
	{
		public bool Success { get; private set; }

		public AddChatThreadMemberThreadNotFoundResult(bool success)
		{
			Success = success;
		}
	}
}
