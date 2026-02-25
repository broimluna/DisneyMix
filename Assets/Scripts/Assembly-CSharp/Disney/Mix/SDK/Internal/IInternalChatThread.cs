using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public interface IInternalChatThread
	{
		long ChatThreadId { get; }

		IEnumerable<IInternalRemoteChatMember> InternalMembers { get; }

		IEnumerable<IInternalRemoteChatMember> InternalFormerMembers { get; }

		IEnumerable<IInternalChatMessage> InternalChatMessages { get; }

		IChatThreadTrustLevel InternalTrustLevel { get; }

		IChatThreadNickname InternalNickname { get; }

		bool AreSequenceNumbersIndexed { get; set; }

		long LatestSequenceNumber { get; set; }

		long NextChatMessageSequenceNumber { get; }

		void AddRemoteMember(IInternalRemoteChatMember member, bool triggerEvent);

		void RemoveRemoteMember(IInternalRemoteChatMember member, bool addToFormerList, bool triggerEvent);

		void AddFormerRemoteMember(IInternalRemoteChatMember member);

		void DispatchLocalUserRemovedEvent();

		void AddTextMessage(IInternalTextMessage message, bool addMessage, bool triggerEvent);

		void AddStickerMessage(IInternalStickerMessage message, bool addMessage, bool triggerEvent);

		void AddGagMessage(IInternalGagMessage message, bool addMessage, bool triggerEvent);

		void AddPhotoMessage(IInternalPhotoMessage message, bool addMessage, bool triggerEvent);

		void AddVideoMessage(IInternalVideoMessage message, bool addMessage, bool triggerEvent);

		void AddGameStateMessage(IInternalGameStateMessage message, bool addMessage, bool triggerEvent);

		void AddGameEventMessage(IInternalGameEventMessage message, bool triggerEvent);

		void AddMemberAddedMessage(IInternalChatMemberAddedMessage message, bool triggerEvent);

		void AddMemberRemovedMessage(IInternalChatMemberRemovedMessage message, bool triggerEvent);

		void UpdateChatTrustLevel(bool trustLevel);

		void UpdateNickname(string nickname);

		void UpdateUnreadMessageCount(uint unreadMessageCount);
	}
}
