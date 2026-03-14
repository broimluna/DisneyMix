using System;

namespace Disney.Mix.SDK
{
	public interface IChatMessageRetriever
	{
		void RetrieveMessages(Action<IRetrieveChatThreadMessagesResult> localCallback, Action<IRetrieveChatThreadMessagesResult> remoteCallback);

		void RetrieveMessages(Action<IRetrieveChatThreadMessagesResult> callback);
	}
}
