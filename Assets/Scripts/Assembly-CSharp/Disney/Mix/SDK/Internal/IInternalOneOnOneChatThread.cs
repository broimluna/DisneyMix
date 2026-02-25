namespace Disney.Mix.SDK.Internal
{
	public interface IInternalOneOnOneChatThread : IInternalChatThread, IChatThread, IOneOnOneChatThread
	{
		void ClearChatHistory();
	}
}
