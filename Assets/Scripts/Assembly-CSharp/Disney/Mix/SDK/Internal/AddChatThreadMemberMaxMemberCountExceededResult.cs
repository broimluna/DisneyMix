namespace Disney.Mix.SDK.Internal
{
	internal class AddChatThreadMemberMaxMemberCountExceededResult : IAddChatThreadMemberMaxMemberCountExceededResult, IAddChatThreadMemberResult
	{
		public bool Success { get; private set; }

		public AddChatThreadMemberMaxMemberCountExceededResult(bool success)
		{
			Success = success;
		}
	}
}
