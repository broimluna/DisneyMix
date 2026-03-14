using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	internal class ChatMemberAddedMessage : ChatMessage, IInternalChatMemberAddedMessage, IInternalChatMessage, IChatMemberAddedMessage, IChatMessage
	{
		public IEnumerable<string> MemberIds { get; private set; }

		public ChatMemberAddedMessage(bool sent, long sequenceNumber, string senderId, IEnumerable<string> members, IEpochTime epochTime, long localChatMessageId)
			: base(sent, sequenceNumber, senderId, epochTime, localChatMessageId)
		{
			MemberIds = members;
		}
	}
}
