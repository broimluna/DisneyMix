namespace Disney.Mix.SDK
{
	public interface ICreateOneOnOneChatThreadResult
	{
		bool Success { get; }

		IOneOnOneChatThread ChatThread { get; }
	}
}
