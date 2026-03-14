namespace Disney.Mix.SDK.Internal
{
	public interface IChatMessageRetrieverFactory
	{
		IInternalChatMessageRetriever CreateChatMessageRetriever(IMixWebCallFactory mixWebCallFactory, IInternalChatThread chatThread, int maximumMessageCount, IUserDatabase userDatabase, IChatMessageParser chatMessageParser);
	}
}
