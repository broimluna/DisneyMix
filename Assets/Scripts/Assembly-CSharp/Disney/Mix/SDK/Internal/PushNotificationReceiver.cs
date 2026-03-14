using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Disney.Mix.SDK.Internal
{
	public static class PushNotificationReceiver
	{
		private static readonly Encoding stringEncoding = Encoding.UTF8;

		public static IInternalPushNotification Receive(AbstractLogger logger, IEncryptor encryptor, IDatabase database, string swid, IDictionary notification)
		{
			SessionDocument sessionDocument = database.GetSessionDocument(swid);
			byte[] currentSymmetricEncryptionKey = sessionDocument.CurrentSymmetricEncryptionKey;
			string optionalString = GetOptionalString(notification, "payload");
			IDictionary dictionary;
			if (optionalString == null)
			{
				dictionary = notification;
			}
			else
			{
				byte[] payloadEncryptedBytes;
				try
				{
					payloadEncryptedBytes = Convert.FromBase64String(optionalString);
				}
				catch (Exception ex)
				{
					throw new ArgumentException("Couldn't deserialize push notification: " + ex);
				}
				try
				{
					dictionary = DecryptPayload(encryptor, currentSymmetricEncryptionKey, payloadEncryptedBytes);
				}
				catch (Exception ex2)
				{
					byte[] previousSymmetricEncryptionKey = sessionDocument.PreviousSymmetricEncryptionKey;
					if (previousSymmetricEncryptionKey == null)
					{
						throw new ArgumentException("Couldn't decrypt push notification: " + ex2);
					}
					try
					{
						dictionary = DecryptPayload(encryptor, previousSymmetricEncryptionKey, payloadEncryptedBytes);
					}
					catch (Exception ex3)
					{
						throw new ArgumentException(string.Concat("Couldn't decrypt push notification: ", ex2, "\n", ex3));
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder("Received push notification with payload:\n");
			foreach (DictionaryEntry item in dictionary)
			{
				stringBuilder.Append(item.Key);
				stringBuilder.Append(" = ");
				stringBuilder.Append(item.Value);
				stringBuilder.Append('\n');
			}
			string optionalString2 = GetOptionalString(dictionary, "type");
			if (optionalString2 == null)
			{
				throw new ArgumentException("Push notification doesn't have a type");
			}
			string optionalString3 = GetOptionalString(dictionary, "notifications_available");
			bool notificationsAvailable = optionalString3 == null || optionalString3 == "true";
			switch (optionalString2)
			{
			case "TEXT_MESSAGE":
			{
				string optionalString18 = GetOptionalString(dictionary, "chatThreadId");
				string optionalString19 = GetOptionalString(dictionary, "chatMessageId");
				return new TextMessageAddedPushNotification(notificationsAvailable, optionalString18, optionalString19);
			}
			case "STICKER_MESSAGE":
			{
				string optionalString16 = GetOptionalString(dictionary, "chatThreadId");
				string optionalString17 = GetOptionalString(dictionary, "chatMessageId");
				return new StickerMessageAddedPushNotification(notificationsAvailable, optionalString16, optionalString17);
			}
			case "GAG_MESSAGE":
			{
				string optionalString14 = GetOptionalString(dictionary, "chatThreadId");
				string optionalString15 = GetOptionalString(dictionary, "chatMessageId");
				return new GagMessageAddedPushNotification(notificationsAvailable, optionalString14, optionalString15);
			}
			case "PHOTO_MESSAGE":
			{
				string optionalString12 = GetOptionalString(dictionary, "chatThreadId");
				string optionalString13 = GetOptionalString(dictionary, "chatMessageId");
				return new PhotoMessageAddedPushNotification(notificationsAvailable, optionalString12, optionalString13);
			}
			case "VIDEO_MESSAGE":
			{
				string optionalString10 = GetOptionalString(dictionary, "chatThreadId");
				string optionalString11 = GetOptionalString(dictionary, "chatMessageId");
				return new VideoMessageAddedPushNotification(notificationsAvailable, optionalString10, optionalString11);
			}
			case "GAME_STATE_MESSAGE":
			{
				string optionalString8 = GetOptionalString(dictionary, "chatThreadId");
				string optionalString9 = GetOptionalString(dictionary, "chatMessageId");
				return new GameStateMessageAddedPushNotification(notificationsAvailable, optionalString8, optionalString9);
			}
			case "GAME_EVENT_MESSAGE":
			{
				string optionalString6 = GetOptionalString(dictionary, "chatThreadId");
				string optionalString7 = GetOptionalString(dictionary, "chatMessageId");
				return new GameEventMessageAddedPushNotification(notificationsAvailable, optionalString6, optionalString7);
			}
			case "FRIENDSHIP_INVITATION_MESSAGE":
			{
				string optionalString5 = GetOptionalString(dictionary, "friendshipInvitationId");
				return new FriendshipInvitationReceivedPushNotification(notificationsAvailable, optionalString5);
			}
			case "FRIENDSHIP_MESSAGE":
			{
				string optionalString4 = GetOptionalString(dictionary, "friendId");
				return new FriendshipAddedPushNotification(notificationsAvailable, optionalString4);
			}
			case "BROADCAST":
				return new BroadcastPushNotification(notificationsAvailable);
			case "ADD_ALERT":
				return new AlertAddedPushNotification(notificationsAvailable);
			case "ADD_CHAT_THREAD":
				return new AddedToChatThreadPushNotification(notificationsAvailable);
			case "ADD_CHAT_THREAD_MEMBER":
				return new ChatThreadMemberAddedPushNotification(notificationsAvailable);
			case "ADD_CHAT_THREAD_NICKNAME":
				return new ChatThreadNicknameAddedPushNotification(notificationsAvailable);
			case "ADD_FOLLOWSHIP":
				return new OfficialAccountFollowedPushNotification(notificationsAvailable);
			case "CLEAR_ALERT":
				return new AlertsClearedPushNotification(notificationsAvailable);
			case "CLEAR_CHAT_MEMBER_HISTORY":
				return new ChatThreadHistoryClearedPushNotification(notificationsAvailable);
			case "CLEAR_UNREAD_MESSAGE_COUNT":
				return new ChatThreadUnreadMessageCountClearedPushNotification(notificationsAvailable);
			case "REMOVE_CHAT_THREAD_MEMBERSHIP":
				return new ChatThreadMemberRemovedPushNotification(notificationsAvailable);
			case "REMOVE_CHAT_THREAD_NICKNAME":
				return new ChatThreadNicknameRemovedPushNotification(notificationsAvailable);
			case "REMOVE_FOLLOWSHIP":
				return new OfficialAccountUnfollowedPushNotification(notificationsAvailable);
			case "REMOVE_FRIENDSHIP":
				return new UnfriendedPushNotification(notificationsAvailable);
			case "REMOVE_FRIENDSHIP_INVITATION":
				return new FriendshipInvitationRemovedPushNotification(notificationsAvailable);
			case "REMOVE_FRIENDSHIP_TRUST":
				return new UntrustedPushNotification(notificationsAvailable);
			case "SET_AVATAR":
				return new AvatarChangedPushNotification(notificationsAvailable);
			case "UPDATE_CHAT_THREAD_TRUST":
				return new ChatThreadTrustStatusChangedPushNotification(notificationsAvailable);
			case "UPDATE_GAME_STATE_MESSAGE":
				return new ChatThreadGameStateMessageUpdatedPushNotification(notificationsAvailable);
			default:
				return new GenericPushNotification(notificationsAvailable);
			}
		}

		private static IDictionary DecryptPayload(IEncryptor encryptor, byte[] key, byte[] payloadEncryptedBytes)
		{
			byte[] bytes = encryptor.Decrypt(payloadEncryptedBytes, key);
			string json = stringEncoding.GetString(bytes);
			return JsonParser.FromJson<Dictionary<string, object>>(json);
		}

		private static string GetOptionalString(IDictionary dict, string name)
		{
			if (!dict.Contains(name))
			{
				return null;
			}
			object obj = dict[name];
			if (!(obj is string))
			{
				return null;
			}
			return (string)obj;
		}
	}
}
