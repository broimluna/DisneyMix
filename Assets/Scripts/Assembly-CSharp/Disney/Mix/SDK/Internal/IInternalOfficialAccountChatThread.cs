namespace Disney.Mix.SDK.Internal
{
	public interface IInternalOfficialAccountChatThread : IInternalChatThread, IChatThread, IOfficialAccountChatThread
	{
		void ClearChatHistory();
	}
}
