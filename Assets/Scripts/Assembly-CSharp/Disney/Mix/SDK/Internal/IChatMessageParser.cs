using System.Collections.Generic;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public interface IChatMessageParser
	{
		void ParseMessage(BaseChatMessage message, IInternalChatThread chatThread, IList<IInternalChatMessage> messageList);

		void ParseMessage(BaseChatMessage message, IInternalChatThread chatThread, IList<IInternalChatMessage> messageList, IList<IInternalChatMessage> existingMessages);

		void InsertParsedChatMessageDocuments();

		IEnumerable<IInternalChatMessage> RetrieveParsedChatMessages(IInternalChatThread chatThread, int maximumMessageCount, long? timestampOffset, IList<IInternalChatMessage> excludedMessages);

		IEnumerable<IInternalChatMessage> RetrieveParsedChatMessages(IInternalChatThread chatThread, long maxSequenceNumber, int maxMessageCount);

		IEnumerable<IInternalChatMessage> RetrieveParsedChatMessages(IInternalChatThread chatThread, int maxMessageCount);

		IInternalChatMessage RetrieveParsedChatMessage(long chatThreadId, long chatMessageId);
	}
}
