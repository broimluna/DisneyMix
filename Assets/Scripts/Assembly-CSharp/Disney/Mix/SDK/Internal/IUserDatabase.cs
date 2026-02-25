using System;
using System.Collections.Generic;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public interface IUserDatabase : IDisposable
	{
		IInternalAlert GetAlert(long alertId);

		IList<IInternalAlert> GetAlerts();

		void AddAlert(IInternalAlert alert);

		void RemoveAlert(long alertId);

		void InsertAvatar(Disney.Mix.SDK.Internal.MixDomain.Avatar avatar);

		AvatarDocument GetAvatar(long avatarId);

		void UpdateAvatar(Disney.Mix.SDK.Internal.MixDomain.Avatar avatar);

		void InsertOrUpdateAvatar(Disney.Mix.SDK.Internal.MixDomain.Avatar avatar);

		ChatMemberDocument[] GetMembersInThread(long chatThreadId);

		void InsertChatMember(long chatThreadId, string memberId);

		void ClearChatMembers();

		void DeleteChatMember(long chatThreadId, string memberId);

		void InsertChatMessage(long chatThreadId, ChatMessageDocument document);

		void UpdateChatMessage(long chatThreadId, long chatMessageId, string payload);

		void UpdateChatMessageSequenceNumbers(long chatThreadId, IList<long> sequenceNumberList);

		ChatMessageDocument GetChatMessage(long chatThreadId, long chatMessageId);

		void DeleteChatMessages(long chatThreadId);

		void DeleteChatMessageDocument(long chatThreadId, uint documentId);

		ChatMessageDocument[] GetMessagesCreatedBefore(long chatThreadId, long created);

		ChatMessageDocument[] GetMessagesBeforeMaxSequenceNumberInclusive(long chatThreadId, int maximumMessageCount);

		ChatMessageDocument[] GetMessagesBeforeSequenceNumberInclusive(long chatThreadId, long endSequenceNumber, int maximumMessageCount);

		void InsertAndUpdateChatMessages(long chatThreadId, IEnumerable<ChatMessageDocument> insertDocuments, IEnumerable<ChatMessageDocument> updateDocuments);

		bool ContainsZeroSequenceNumber(long chatThreadId);

		void IndexSequenceNumberField(long chatThreadId);

		ChatThreadDocument[] GetAllChatThreadDocuments();

		bool ContainsChatThread(long chatThreadId);

		void InsertChatThread(ChatThreadDocument doc);

		void UpdateChatThreadDocument(ChatThreadDocument doc);

		void DeleteChatThreadDocument(ChatThreadDocument doc);

		void SetChatThreadIsTrusted(long chatThreadId, bool isTrusted);

		void SetChatThreadNickname(long chatThreadId, string nickname);

		void SetChatThreadUnreadMessageCount(long chatThreadId, uint unreadMessageCount);

		void IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(long chatThreadId, long sequenceNumber);

		void UpdateLatestSequenceNumber(long chatThreadId, long sequenceNumber);

		void SetChatThreadSequenceNumbersIndexed(long chatThreadId);

		void DeleteChatThread(long chatThreadId);

		ChatThreadDocument GetChatThreadDocument(long chatThreadId);

		FriendDocument[] GetAllFriendDocuments();

		void DeleteFriend(string swid);

		void SetFriendIsTrusted(string swid, bool isTrusted);

		void SetFriendNickname(string swid, string nickname);

		void InsertFriend(FriendDocument doc);

		void ClearFriends();

		void DeleteFriendInvitation(long invitationId);

		void InsertFriendInvitation(FriendInvitationDocument doc);

		void InsertOrUpdateFriendInvitation(FriendInvitationDocument doc);

		void ClearFriendInvitations();

		FriendInvitationDocument[] GetFriendInvitationDocuments(bool isInviter);

		void InsertOfficialAccount(string accountId, string name, bool isFollowing, bool isAvailable, bool canUnfollow);

		bool ContainsOfficialAccount(string accountId);

		OfficialAccountDocument GetOfficialAccount(string accountId);

		OfficialAccountDocument[] GetAllOfficialAccounts();

		void UpdateOfficialAccount(string accountId, Action<OfficialAccountDocument> updateCallback);

		void DeleteOfficialAccount(string accountId);

		void InsertOrUpdateOfficialAccounts(IEnumerable<GuestOfficialAccount> officialAccounts);

		UserDocument GetUserBySwid(string swid);

		UserDocument GetUserByDisplayName(string displayName);

		void InsertUserDocument(UserDocument userDocument);

		void UpdateUserDocument(UserDocument userDocument);

		void PersistUser(string swid, string hashedSwid, string displayName, string firstName, Disney.Mix.SDK.Internal.MixDomain.Avatar avatar, string status);

		void SyncToGetStateResponse(GetStateResponse response, Action callback);

		void IndexSequenceNumberField(long chatThreadId, Action callback);
	}
}
