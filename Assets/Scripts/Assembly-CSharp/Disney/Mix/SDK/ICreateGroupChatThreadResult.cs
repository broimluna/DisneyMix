namespace Disney.Mix.SDK
{
	public interface ICreateGroupChatThreadResult
	{
		bool Success { get; }

		IGroupChatThread ChatThread { get; }
	}
}
