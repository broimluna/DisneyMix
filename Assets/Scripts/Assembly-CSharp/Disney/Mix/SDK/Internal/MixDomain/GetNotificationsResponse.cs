using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class GetNotificationsResponse : BaseResponse
	{
		public long? LastNotificationTimestamp;

		public long? NotificationSequenceCounter;

		public List<AddChatThreadNotification> AddChatThread;

		public List<AddChatThreadMembershipNotification> AddChatThreadMembership;

		public List<AddChatThreadGagMessageNotification> AddChatThreadGagMessage;

		public List<AddChatThreadMemberListChangedMessageNotification> AddChatThreadMemberListChangedMessage;

		public List<AddChatThreadPhotoMessageNotification> AddChatThreadPhotoMessage;

		public List<AddChatThreadStickerMessageNotification> AddChatThreadStickerMessage;

		public List<AddChatThreadTextMessageNotification> AddChatThreadTextMessage;

		public List<AddChatThreadVideoMessageNotification> AddChatThreadVideoMessage;

		public List<AddChatThreadNicknameNotification> AddChatThreadNickname;

		public List<AddChatThreadGameStateMessageNotification> AddChatThreadGameStateMessage;

		public List<UpdateChatThreadGameStateMessageNotification> UpdateChatThreadGameStateMessage;

		public List<AddChatThreadGameEventMessageNotification> AddChatThreadGameEventMessage;

		public List<AddFriendshipNotification> AddFriendship;

		public List<AddFollowshipNotification> AddFollowship;

		public List<AddFriendshipInvitationNotification> AddFriendshipInvitation;

		public List<AddNicknameNotification> AddNickname;

		public List<AddAlertNotification> AddAlert;

		public List<ClearAlertNotification> ClearAlert;

		public List<ClearMemberChatHistoryNotification> ClearMemberChatHistory;

		public List<ClearUnreadMessageCountNotification> ClearUnreadMessageCount;

		public List<RemoveChatThreadMembershipNotification> RemoveChatThreadMembership;

		public List<RemoveChatThreadNicknameNotification> RemoveChatThreadNickname;

		public List<RemoveFriendshipInvitationNotification> RemoveFriendshipInvitation;

		public List<RemoveFriendshipNotification> RemoveFriendship;

		public List<RemoveFollowshipNotification> RemoveFollowship;

		public List<RemoveFriendshipTrustNotification> RemoveFriendshipTrust;

		public List<RemoveNicknameNotification> RemoveNickname;

		public List<SetAvatarNotification> SetAvatar;

		public List<UpdateChatThreadTrustStatusNotification> UpdateChatThreadTrustStatus;
	}
}
