namespace Disney.Mix.SDK.Internal
{
	public class ChatMessageRetrieverFactory : IChatMessageRetrieverFactory
	{
		private const int InternalMaximumMessageCount = 50;

		private readonly AbstractLogger logger;

		private readonly IEpochTime epochTime;

		private readonly ISessionStatus sessionStatus;

		public ChatMessageRetrieverFactory(AbstractLogger logger, IEpochTime epochTime, ISessionStatus sessionStatus)
		{
			this.logger = logger;
			this.epochTime = epochTime;
			this.sessionStatus = sessionStatus;
		}

		public IInternalChatMessageRetriever CreateChatMessageRetriever(IMixWebCallFactory mixWebCallFactory, IInternalChatThread chatThread, int maximumMessageCount, IUserDatabase userDatabase, IChatMessageParser chatMessageParser)
		{
			if (maximumMessageCount > 50)
			{
				maximumMessageCount = 50;
			}
			if (maximumMessageCount < 1)
			{
				maximumMessageCount = 1;
			}
			return new ChatMessageRetriever(logger, mixWebCallFactory, chatThread, maximumMessageCount, sessionStatus, userDatabase, chatMessageParser, epochTime);
		}
	}
}
