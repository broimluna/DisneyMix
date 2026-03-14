using System;
using System.Collections.Generic;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public interface IInternalLocalUser : ILocalUser
	{
		string Swid { get; }

		IInternalAvatar InternalAvatar { get; }

		IEnumerable<IInternalFriend> InternalFriends { get; }

		IEnumerable<IInternalOutgoingFriendInvitation> InternalOutgoingFriendInvitations { get; }

		IEnumerable<IInternalIncomingFriendInvitation> InternalIncomingFriendInvitations { get; }

		IEnumerable<IInternalChatThread> InternalChatThreads { get; }

		IEnumerable<IInternalOneOnOneChatThread> InternalOneOnOneChatThreads { get; }

		IEnumerable<IInternalGroupChatThread> InternalGroupChatThreads { get; }

		IEnumerable<IInternalOfficialAccountChatThread> InternalOfficialAccountChatThreads { get; }

		IInternalRegistrationProfile InternalRegistrationProfile { get; }

		event Action<bool> OnPushNotificationsToggled;

		event Action<bool> OnPushNotificationReceived;

		event Action<string> OnDisplayNameUpdated;

		void AddFriend(IInternalFriend friend);

		void RemoveFriend(IInternalFriend friend);

		void AddOneOnOneChatThread(IInternalOneOnOneChatThread chatThread);

		void AddGroupChatThread(IInternalGroupChatThread chatThread);

		void AddOfficialAccountChatThread(IInternalOfficialAccountChatThread chatThread);

		void RemoveOneOnOneChatThread(IInternalOneOnOneChatThread chatThread);

		void RemoveGroupChatThread(IInternalGroupChatThread chatThread);

		void UntrustFriend(IInternalFriend friend);

		void AddIncomingFriendInvitation(IInternalIncomingFriendInvitation invitation);

		void AddOutgoingFriendInvitation(IInternalOutgoingFriendInvitation invitation);

		void RemoveIncomingFriendInvitation(IInternalIncomingFriendInvitation invitation);

		void RemoveOutgoingFriendInvitation(IInternalOutgoingFriendInvitation invitation);

		void DispatchOnAvatarChanged();

		void UpdateFollowships(IEnumerable<IOfficialAccount> officialAccounts);

		void AddFriendshipInvitation(FriendshipInvitation invitation, User friend);

		void RemoveFriendshipInvitation(long invitationId);

		void AddFriend(User domainFriend, bool isTrusted, long invitationId);

		void RemoveFriend(string friendSwid, bool sendEvent);

		void UntrustFriend(string friendSwid);

		void AddChatThread(ChatThread chatThread);

		void AddChatThreadMembership(long chatThreadId, IEnumerable<User> members);

		void RemoveChatThreadMembership(long chatThreadId, string memberUserId);

		void AddChatTextMessage(TextChatMessage message);

		void AddChatStickerMessage(StickerChatMessage message);

		void AddChatGagMessage(GagChatMessage message);

		void AddChatPhotoMessage(PhotoChatMessage message);

		void AddChatVideoMessage(VideoChatMessage message);

		void AddChatMemberListChangedMessage(MemberListChangedChatMessage message);

		void AddChatGameStateMessage(GameStateChatMessage message);

		void UpdateChatGameStateMessage(GameStateChatMessage message, string result);

		void AddChatGameEventMessage(GameEventChatMessage message);

		void AddNickname(Disney.Mix.SDK.Internal.MixDomain.UserNickname userNickname);

		void ClearChatThreadHistory(IEnumerable<long> chatThreadIds);

		void ClearUnreadMessageCount(IEnumerable<long> chatThreadIds);

		void FollowOfficialAccount(GuestOfficialAccount domainOfficialAccount);

		void UnfollowOfficialAccount(string officialAccountId);

		void DispatchOnAlertsAdded(IAlert alert);

		void DispatchOnAlertsCleared(IEnumerable<IAlert> alerts);

		void UpdateChatTrustLevel(long chatThreadId, bool isTrusted);

		void AddChatThreadNickname(Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname nickname);

		void RemoveChatThreadNickname(long chatThreadId);

		void UpdateAvatar(Disney.Mix.SDK.Internal.MixDomain.Avatar avatar);

		void UpdateNickname(string userId, IUserNickname nickname);
	}
}
