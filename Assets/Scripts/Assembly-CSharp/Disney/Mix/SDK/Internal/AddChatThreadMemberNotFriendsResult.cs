namespace Disney.Mix.SDK.Internal
{
	internal class AddChatThreadMemberNotFriendsResult : IAddChatThreadMemberNotFriendsResult, IAddChatThreadMemberResult
	{
		public bool Success { get; private set; }

		public AddChatThreadMemberNotFriendsResult(bool success)
		{
			Success = success;
		}
	}
}
