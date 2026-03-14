using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public interface IChatThreadFactory
	{
		IInternalOneOnOneChatThread CreateOneOnOneChatThread(IMixWebCallFactory mixWebCallFactory, long chatThreadId, IChatThreadTrustLevel trustLevel, string localUserId, long latestSequenceNumber, bool areSequenceNumbersIndexed, IList<IInternalRemoteChatMember> members, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler);

		IInternalGroupChatThread CreateGroupChatThread(IMixWebCallFactory mixWebCallFactory, long chatThreadId, IChatThreadTrustLevel trustLevel, string localUserId, long latestSequenceNumber, bool areSequenceNumbersIndexed, IList<IInternalRemoteChatMember> members, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler);

		IInternalOfficialAccountChatThread CreateOfficialAccountChatThread(IMixWebCallFactory mixWebCallFactory, long chatThreadId, IChatThreadTrustLevel trustLevel, string localUserId, string officialAccountId, long latestSequenceNumber, bool areSequenceNumbersIndexed, IList<IInternalRemoteChatMember> members, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler);
	}
}
