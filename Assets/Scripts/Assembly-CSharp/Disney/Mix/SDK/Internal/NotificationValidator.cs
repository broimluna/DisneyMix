using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class NotificationValidator
	{
		private static bool Validate(BaseNotification notification)
		{
			int result;
			if (notification != null)
			{
				long? sequenceNumber = notification.SequenceNumber;
				if (sequenceNumber.HasValue)
				{
					result = ((notification.SequenceNumber != 0) ? 1 : 0);
					goto IL_003b;
				}
			}
			result = 0;
			goto IL_003b;
			IL_003b:
			return (byte)result != 0;
		}

		public static bool Validate(AddNicknameNotification notification)
		{
			return Validate((BaseNotification)notification) && notification.Nickname != null && notification.Nickname.Nickname != null && notification.Nickname.NicknamedUserId != null;
		}

		public static bool Validate(RemoveNicknameNotification notification)
		{
			return Validate((BaseNotification)notification) && notification.NicknamedUserId != null;
		}

		public static bool Validate(AddChatThreadNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification) && notification.WelcomingUserId != null && notification.ChatThread != null)
			{
				long? chatThreadId = notification.ChatThread.ChatThreadId;
				if (chatThreadId.HasValue && ChatThreadTypeConverter.ValidateServerType(notification.ChatThread.ChatThreadType))
				{
					bool? isTrusted = notification.ChatThread.IsTrusted;
					if (isTrusted.HasValue)
					{
						result = ((notification.ChatThread.Members != null) ? 1 : 0);
						goto IL_007a;
					}
				}
			}
			result = 0;
			goto IL_007a;
			IL_007a:
			return (byte)result != 0;
		}

		public static bool Validate(AddChatThreadMembershipNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification) && notification.WelcomingUserId != null)
			{
				long? chatThreadId = notification.ChatThreadId;
				if (chatThreadId.HasValue && notification.Members != null && notification.Members.All((User m) => m.DisplayName != null))
				{
					result = (notification.Members.All((User m) => m.UserId != null) ? 1 : 0);
					goto IL_008c;
				}
			}
			result = 0;
			goto IL_008c;
			IL_008c:
			return (byte)result != 0;
		}

		public static bool Validate(RemoveChatThreadMembershipNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification))
			{
				long? chatThreadId = notification.ChatThreadId;
				if (chatThreadId.HasValue)
				{
					result = ((notification.MemberUserId != null) ? 1 : 0);
					goto IL_002d;
				}
			}
			result = 0;
			goto IL_002d;
			IL_002d:
			return (byte)result != 0;
		}

		private static bool Validate(BaseChatMessage message)
		{
			int result;
			if (message != null)
			{
				long? created = message.Created;
				if (created.HasValue)
				{
					long? chatThreadId = message.ChatThreadId;
					if (chatThreadId.HasValue)
					{
						long? chatMessageId = message.ChatMessageId;
						if (chatMessageId.HasValue && message.MessageType != null && message.SenderUserId != null)
						{
							long? clientChatMessageId = message.ClientChatMessageId;
							if (clientChatMessageId.HasValue)
							{
								long? sequenceNumber = message.SequenceNumber;
								result = (sequenceNumber.HasValue ? 1 : 0);
								goto IL_007a;
							}
						}
					}
				}
			}
			result = 0;
			goto IL_007a;
			IL_007a:
			return (byte)result != 0;
		}

		public static bool Validate(AddChatThreadTextMessageNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification) && Validate(notification.Message))
			{
				bool? isModerated = notification.Message.IsModerated;
				if (isModerated.HasValue)
				{
					result = ((notification.Message.Text != null) ? 1 : 0);
					goto IL_0047;
				}
			}
			result = 0;
			goto IL_0047;
			IL_0047:
			return (byte)result != 0;
		}

		public static bool Validate(AddChatThreadStickerMessageNotification notification)
		{
			return Validate((BaseNotification)notification) && Validate(notification.Message) && notification.Message.ContentId != null;
		}

		public static bool Validate(AddChatThreadGagMessageNotification notification)
		{
			return Validate((BaseNotification)notification) && Validate(notification.Message) && notification.Message.ContentId != null && notification.Message.TargetUserId != null;
		}

		public static bool Validate(AddChatThreadPhotoMessageNotification notification)
		{
			return Validate((BaseNotification)notification) && Validate(notification.Message) && notification.Message.PhotoId != null && notification.Message.Caption != null && Validate(notification.Message.PhotoFlavors);
		}

		private static bool Validate(List<Disney.Mix.SDK.Internal.MixDomain.PhotoFlavor> flavors)
		{
			return flavors != null && flavors.Count > 0 && flavors.All(Validate);
		}

		private static bool Validate(Disney.Mix.SDK.Internal.MixDomain.PhotoFlavor flavor)
		{
			int result;
			if (flavor.PhotoFlavorId != null && flavor.Encoding != null)
			{
				int? height = flavor.Height;
				if (height.HasValue)
				{
					int? width = flavor.Width;
					if (width.HasValue)
					{
						result = ((flavor.Url != null) ? 1 : 0);
						goto IL_004b;
					}
				}
			}
			result = 0;
			goto IL_004b;
			IL_004b:
			return (byte)result != 0;
		}

		public static bool Validate(AddChatThreadVideoMessageNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification) && Validate(notification.Message) && notification.Message.VideoId != null && notification.Message.Caption != null)
			{
				int? duration = notification.Message.Duration;
				if (duration.HasValue && notification.Message.Thumbnail != null && Validate(notification.Message.Thumbnail))
				{
					result = (Validate(notification.Message.VideoFlavors) ? 1 : 0);
					goto IL_008b;
				}
			}
			result = 0;
			goto IL_008b;
			IL_008b:
			return (byte)result != 0;
		}

		private static bool Validate(List<Disney.Mix.SDK.Internal.MixDomain.VideoFlavor> flavors)
		{
			return flavors != null && flavors.Count > 0 && flavors.All(Validate);
		}

		private static bool Validate(Disney.Mix.SDK.Internal.MixDomain.VideoFlavor flavor)
		{
			int result;
			if (flavor.VideoFlavorId != null && flavor.Format != null)
			{
				int? height = flavor.Height;
				if (height.HasValue)
				{
					int? width = flavor.Width;
					if (width.HasValue)
					{
						int? bitrate = flavor.Bitrate;
						result = (bitrate.HasValue ? 1 : 0);
						goto IL_004d;
					}
				}
			}
			result = 0;
			goto IL_004d;
			IL_004d:
			return (byte)result != 0;
		}

		public static bool Validate(AddChatThreadMemberListChangedMessageNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification))
			{
				long? chatThreadId = notification.Message.ChatThreadId;
				if (chatThreadId.HasValue)
				{
					long? chatMessageId = notification.Message.ChatMessageId;
					if (chatMessageId.HasValue)
					{
						long? created = notification.Message.Created;
						if (created.HasValue && notification.Message.MessageType != null)
						{
							long? sequenceNumber = notification.Message.SequenceNumber;
							if (sequenceNumber.HasValue && notification.Message.MemberUserIds != null && !notification.Message.MemberUserIds.Any((string i) => i == null))
							{
								bool? isAdded = notification.Message.IsAdded;
								result = (isAdded.HasValue ? 1 : 0);
								goto IL_00d4;
							}
						}
					}
				}
			}
			result = 0;
			goto IL_00d4;
			IL_00d4:
			return (byte)result != 0;
		}

		public static bool Validate(AddChatThreadGameStateMessageNotification notification)
		{
			return Validate((BaseNotification)notification) && Validate(notification.Message) && notification.Message.GameName != null && notification.Message.State != null;
		}

		public static bool Validate(UpdateChatThreadGameStateMessageNotification notification)
		{
			return Validate((BaseNotification)notification) && Validate(notification.Message) && notification.Message.State != null;
		}

		public static bool Validate(AddChatThreadGameEventMessageNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification) && Validate(notification.Message))
			{
				long? gameStateMessageId = notification.Message.GameStateMessageId;
				if (gameStateMessageId.HasValue && notification.Message.GameName != null)
				{
					result = ((notification.Message.Payload != null) ? 1 : 0);
					goto IL_0057;
				}
			}
			result = 0;
			goto IL_0057;
			IL_0057:
			return (byte)result != 0;
		}

		public static bool Validate(AddFriendshipInvitationNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification) && notification.Invitation != null && notification.Invitation.FriendDisplayName != null)
			{
				bool? isInviter = notification.Invitation.IsInviter;
				if (isInviter.HasValue)
				{
					long? friendshipInvitationId = notification.Invitation.FriendshipInvitationId;
					if (friendshipInvitationId.HasValue)
					{
						bool? isTrusted = notification.Invitation.IsTrusted;
						result = (isTrusted.HasValue ? 1 : 0);
						goto IL_006c;
					}
				}
			}
			result = 0;
			goto IL_006c;
			IL_006c:
			return (byte)result != 0;
		}

		public static bool Validate(AddFriendshipNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification))
			{
				long? friendshipInvitationId = notification.FriendshipInvitationId;
				if (friendshipInvitationId.HasValue && notification.Friend != null && notification.Friend.DisplayName != null && notification.Friend.UserId != null)
				{
					bool? isTrusted = notification.IsTrusted;
					result = (isTrusted.HasValue ? 1 : 0);
					goto IL_005a;
				}
			}
			result = 0;
			goto IL_005a;
			IL_005a:
			return (byte)result != 0;
		}

		public static bool Validate(RemoveFriendshipInvitationNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification))
			{
				long? invitationId = notification.InvitationId;
				result = (invitationId.HasValue ? 1 : 0);
			}
			else
			{
				result = 0;
			}
			return (byte)result != 0;
		}

		public static bool Validate(RemoveFriendshipNotification notification)
		{
			return Validate((BaseNotification)notification) && notification.FriendUserId != null;
		}

		public static bool Validate(RemoveFriendshipTrustNotification notification)
		{
			return Validate((BaseNotification)notification) && notification.FriendUserId != null;
		}

		public static bool Validate(UpdateChatThreadTrustStatusNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification))
			{
				long? chatThreadId = notification.ChatThreadId;
				if (chatThreadId.HasValue)
				{
					bool? isTrusted = notification.IsTrusted;
					result = (isTrusted.HasValue ? 1 : 0);
					goto IL_002f;
				}
			}
			result = 0;
			goto IL_002f;
			IL_002f:
			return (byte)result != 0;
		}

		public static bool Validate(AddChatThreadNicknameNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification) && notification.ChatThreadNickname != null)
			{
				long? chatThreadId = notification.ChatThreadNickname.ChatThreadId;
				if (chatThreadId.HasValue)
				{
					result = ((notification.ChatThreadNickname.Nickname != null) ? 1 : 0);
					goto IL_0042;
				}
			}
			result = 0;
			goto IL_0042;
			IL_0042:
			return (byte)result != 0;
		}

		public static bool Validate(RemoveChatThreadNicknameNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification))
			{
				long? chatThreadId = notification.ChatThreadId;
				result = (chatThreadId.HasValue ? 1 : 0);
			}
			else
			{
				result = 0;
			}
			return (byte)result != 0;
		}

		public static bool Validate(ClearMemberChatHistoryNotification notification)
		{
			return Validate((BaseNotification)notification) && notification.ChatThreadIds != null && notification.ChatThreadIds.All((long? t) => t.HasValue);
		}

		public static bool Validate(ClearUnreadMessageCountNotification notification)
		{
			return Validate((BaseNotification)notification) && notification.ChatThreadIds != null && notification.ChatThreadIds.All((long? t) => t.HasValue);
		}

		public static bool Validate(SetAvatarNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification) && notification.Avatar != null)
			{
				long? avatarId = notification.Avatar.AvatarId;
				if (avatarId.HasValue && notification.Avatar.Accessory != null && notification.Avatar.Brow != null && notification.Avatar.Costume != null && notification.Avatar.Eyes != null && notification.Avatar.Hair != null && notification.Avatar.Nose != null && notification.Avatar.Mouth != null && notification.Avatar.Skin != null)
				{
					result = ((notification.Avatar.Hat != null) ? 1 : 0);
					goto IL_00c2;
				}
			}
			result = 0;
			goto IL_00c2;
			IL_00c2:
			return (byte)result != 0;
		}

		public static bool Validate(AddFollowshipNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification) && notification.OfficialAccount != null && notification.OfficialAccount.OaId != null && notification.OfficialAccount.OaName != null)
			{
				bool? isAvailable = notification.OfficialAccount.IsAvailable;
				if (isAvailable.HasValue)
				{
					bool? canUnfollow = notification.OfficialAccount.CanUnfollow;
					result = (canUnfollow.HasValue ? 1 : 0);
					goto IL_0064;
				}
			}
			result = 0;
			goto IL_0064;
			IL_0064:
			return (byte)result != 0;
		}

		public static bool Validate(RemoveFollowshipNotification notification)
		{
			return Validate((BaseNotification)notification) && notification.OfficialAccountId != null;
		}

		public static bool Validate(AddAlertNotification notification)
		{
			int result;
			if (Validate((BaseNotification)notification) && notification.Alert != null && notification.Alert.Text != null)
			{
				long? alertId = notification.Alert.AlertId;
				if (alertId.HasValue && notification.Alert.Level != null)
				{
					long? timestamp = notification.Alert.Timestamp;
					result = (timestamp.HasValue ? 1 : 0);
					goto IL_0064;
				}
			}
			result = 0;
			goto IL_0064;
			IL_0064:
			return (byte)result != 0;
		}

		public static bool Validate(ClearAlertNotification notification)
		{
			return Validate((BaseNotification)notification) && notification.AlertIds != null && !notification.AlertIds.Any((long? id) => !id.HasValue);
		}
	}
}
