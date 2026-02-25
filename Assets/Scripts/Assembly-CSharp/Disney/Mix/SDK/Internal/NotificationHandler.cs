using System;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class NotificationHandler
	{
		public static void Handle(INotificationDispatcher dispatcher, IUserDatabase userDatabase, IInternalLocalUser localUser, IEpochTime epochTime)
		{
			dispatcher.OnAlertAdded += delegate(object sender, AbstractAddAlertNotificationEventArgs e)
			{
				HandleAlertAdded(localUser, userDatabase, e);
			};
			dispatcher.OnAlertCleared += delegate(object sender, AbstractClearAlertNotificationEventArgs e)
			{
				HandleAlertCleared(localUser, userDatabase, e);
			};
			dispatcher.OnAvatarSet += delegate(object sender, AbstractSetAvatarNotificationEventArgs e)
			{
				HandleAvatarSet(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadMembershipAdded += delegate(object sender, AbstractAddChatThreadMembershipNotificationEventArgs e)
			{
				HandleChatMembersAdded(userDatabase, localUser, e);
			};
			dispatcher.OnChatThreadMembershipRemoved += delegate(object sender, AbstractRemoveChatThreadMembershipNotificationEventArgs e)
			{
				HandleChatMemberRemoved(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadGagMessageAdded += delegate(object sender, AbstractAddChatThreadGagMessageNotificationEventArgs e)
			{
				HandleGagMessageAdded(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadGameEventMessageAdded += delegate(object sender, AbstractAddChatThreadGameEventMessageNotificationEventArgs e)
			{
				HandleGameEventMessageAdded(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadGameStateMessageAdded += delegate(object sender, AbstractAddChatThreadGameStateMessageNotificationEventArgs e)
			{
				HandleGameStateMessageAdded(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadGameStateMessageUpdated += delegate(object sender, AbstractUpdateChatThreadGameStateMessageNotificationEventArgs e)
			{
				HandleGameStateMessageUpdated(userDatabase, localUser, e);
			};
			dispatcher.OnChatThreadMemberListChangedMessageAdded += delegate(object sender, AbstractAddChatThreadMemberListChangedMessageNotificationEventArgs e)
			{
				HandleMemberListChangedMessageAdded(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadPhotoMessageAdded += delegate(object sender, AbstractAddChatThreadPhotoMessageNotificationEventArgs e)
			{
				HandlePhotoMessageAdded(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadStickerMessageAdded += delegate(object sender, AbstractAddChatThreadStickerMessageNotificationEventArgs e)
			{
				HandleStickerMessageAdded(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadTextMessageAdded += delegate(object sender, AbstractAddChatThreadTextMessageNotificationEventArgs e)
			{
				HandleTextMessageAdded(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadVideoMessageAdded += delegate(object sender, AbstractAddChatThreadVideoMessageNotificationEventArgs e)
			{
				HandleVideoMessageAdded(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadMembershipRemoved += delegate(object sender, AbstractRemoveChatThreadMembershipNotificationEventArgs e)
			{
				HandleChatMemberRemoved(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadHistoryCleared += delegate(object sender, AbstractClearMemberChatHistoryNotificationEventArgs e)
			{
				HandleChatHistoryCleared(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadAdded += delegate(object sender, AbstractAddChatThreadNotificationEventArgs e)
			{
				HandleChatThreadAdded(userDatabase, localUser, e);
			};
			dispatcher.OnChatThreadTrustStatusUpdate += delegate(object sender, AbstractUpdateChatThreadTrustStatusNotificationEventArgs e)
			{
				HandleChatThreadTrustChanged(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadNicknameAdded += delegate(object sender, AbstractAddChatThreadNicknameNotificationEventArgs e)
			{
				HandleChatThreadNicknameAdded(localUser, userDatabase, e);
			};
			dispatcher.OnChatThreadNicknameRemoved += delegate(object sender, AbstractRemoveChatThreadNicknameNotificationEventArgs e)
			{
				HandleChatThreadNicknameRemoved(localUser, userDatabase, e);
			};
			dispatcher.OnUnreadMessageCountCleared += delegate(object sender, AbstractClearUnreadMessageCountNotificationEventArgs e)
			{
				HandleUnreadMessageCountCleared(localUser, userDatabase, e);
			};
			dispatcher.OnFriendshipRemoved += delegate(object sender, AbstractRemoveFriendshipNotificationEventArgs e)
			{
				HandleFriendshipRemoved(userDatabase, localUser, e);
			};
			dispatcher.OnFriendshipTrustRemoved += delegate(object sender, AbstractRemoveFriendshipTrustNotificationEventArgs e)
			{
				HandleFriendshipTrustRemoved(userDatabase, localUser, e);
			};
			dispatcher.OnNicknameAdded += delegate(object sender, AbstractAddNicknameNotificationEventArgs e)
			{
				HandleNicknameAdded(localUser, userDatabase, e);
			};
			dispatcher.OnNicknameRemoved += delegate(object sender, AbstractRemoveNicknameNotificationEventArgs e)
			{
				HandleNicknameRemoved(localUser, userDatabase, e);
			};
			dispatcher.OnFriendshipInvitationAdded += delegate(object sender, AbstractAddFriendshipInvitationNotificationEventArgs e)
			{
				HandleFriendshipInvitationAdded(userDatabase, localUser, e);
			};
			dispatcher.OnFriendshipInvitationRemoved += delegate(object sender, AbstractRemoveFriendshipInvitationNotificationEventArgs e)
			{
				HandleFriendshipInvitationRemoved(userDatabase, localUser, e);
			};
			dispatcher.OnFriendshipAdded += delegate(object sender, AbstractAddFriendshipNotificationEventArgs e)
			{
				HandleFriendshipAdded(userDatabase, localUser, e);
			};
			dispatcher.OnOfficialAccountFollowed += delegate(object sender, AbstractAddFollowshipNotificationEventArgs e)
			{
				HandleOfficialAccountFollowed(localUser, userDatabase, epochTime, e);
			};
			dispatcher.OnOfficialAccountUnfollowed += delegate(object sender, AbstractRemoveFollowshipNotificationEventArgs e)
			{
				HandleOfficialAccountUnfollowed(localUser, userDatabase, epochTime, e);
			};
		}

		private static void HandleAlertAdded(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractAddAlertNotificationEventArgs e)
		{
			Alert alert = new Alert(e.Notification.Alert);
			userDatabase.AddAlert(alert);
			localUser.DispatchOnAlertsAdded(alert);
		}

		private static void HandleAlertCleared(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractClearAlertNotificationEventArgs e)
		{
			IInternalAlert[] array = (from a in userDatabase.GetAlerts()
				where e.Notification.AlertIds.Contains(a.AlertId)
				select a).ToArray();
			IInternalAlert[] array2 = array;
			foreach (IInternalAlert internalAlert in array2)
			{
				userDatabase.RemoveAlert(internalAlert.AlertId);
			}
			localUser.DispatchOnAlertsCleared(array);
		}

		private static void HandleAvatarSet(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractSetAvatarNotificationEventArgs e)
		{
			Disney.Mix.SDK.Internal.MixDomain.Avatar avatar = e.Notification.Avatar;
			userDatabase.UpdateAvatar(avatar);
			localUser.UpdateAvatar(avatar);
		}

		private static void HandleChatMembersAdded(IUserDatabase userDatabase, IInternalLocalUser localUser, AbstractAddChatThreadMembershipNotificationEventArgs e)
		{
			AddChatThreadMembershipNotification notification = e.Notification;
			long value = notification.ChatThreadId.Value;
			foreach (User member in notification.Members)
			{
				userDatabase.PersistUser(member.UserId, member.HashedUserId, member.DisplayName, member.FirstName, member.Avatar, member.Status);
				userDatabase.InsertChatMember(value, member.UserId);
			}
			localUser.AddChatThreadMembership(value, notification.Members);
		}

		private static void HandleChatThreadAdded(IUserDatabase userDatabase, IInternalLocalUser localUser, AbstractAddChatThreadNotificationEventArgs e)
		{
			ChatThread chatThread = e.Notification.ChatThread;
			long value = chatThread.ChatThreadId.Value;
			bool value2 = chatThread.IsTrusted.Value;
			foreach (User member in chatThread.Members)
			{
				userDatabase.PersistUser(member.UserId, member.HashedUserId, member.DisplayName, member.FirstName, member.Avatar, member.Status);
				userDatabase.InsertChatMember(value, member.UserId);
			}
			ChatThreadDatabaseType chatThreadType = ChatThreadTypeConverter.ConvertServerTypeToDatabaseType(chatThread.ChatThreadType);
			ChatThreadDocument chatThreadDocument = new ChatThreadDocument();
			chatThreadDocument.ChatThreadId = value;
			chatThreadDocument.ChatThreadType = (byte)chatThreadType;
			chatThreadDocument.IsTrusted = value2;
			chatThreadDocument.Nickname = null;
			chatThreadDocument.UnreadMessageCount = 0u;
			chatThreadDocument.OfficialAccountId = chatThread.OfficialAccountId;
			chatThreadDocument.AreSequenceNumbersIndexed = true;
			ChatThreadDocument doc = chatThreadDocument;
			userDatabase.InsertChatThread(doc);
			localUser.AddChatThread(chatThread);
		}

		private static void HandleGameStateMessageUpdated(IUserDatabase userDatabase, IInternalLocalUser localUser, AbstractUpdateChatThreadGameStateMessageNotificationEventArgs e)
		{
			GameStateChatMessage message = e.Notification.Message;
			long value = message.ChatThreadId.Value;
			userDatabase.IndexSequenceNumberField(value, delegate
			{
				string payload = ChatMessageDocumentPayloadFactory.Create(message);
				userDatabase.UpdateChatMessage(message.ChatThreadId.Value, message.ChatMessageId.Value, payload);
				localUser.UpdateChatGameStateMessage(message, e.Notification.Result);
			});
		}

		private static void HandleChatMemberRemoved(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractRemoveChatThreadMembershipNotificationEventArgs e)
		{
			RemoveChatThreadMembershipNotification notification = e.Notification;
			long value = notification.ChatThreadId.Value;
			string memberUserId = notification.MemberUserId;
			if (memberUserId == localUser.Swid)
			{
				userDatabase.DeleteChatMessages(value);
				userDatabase.DeleteChatThread(value);
			}
			userDatabase.DeleteChatMember(value, memberUserId);
			localUser.RemoveChatThreadMembership(value, memberUserId);
		}

		private static void HandleChatHistoryCleared(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractClearMemberChatHistoryNotificationEventArgs e)
		{
			long[] array = e.Notification.ChatThreadIds.Cast<long>().ToArray();
			long[] array2 = array;
			foreach (long chatThreadId in array2)
			{
				userDatabase.DeleteChatMessages(chatThreadId);
			}
			localUser.ClearChatThreadHistory(array);
		}

		private static void HandleChatThreadTrustChanged(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractUpdateChatThreadTrustStatusNotificationEventArgs e)
		{
			UpdateChatThreadTrustStatusNotification notification = e.Notification;
			long value = notification.ChatThreadId.Value;
			bool value2 = notification.IsTrusted.Value;
			userDatabase.SetChatThreadIsTrusted(value, value2);
			localUser.UpdateChatTrustLevel(value, value2);
		}

		private static void HandleChatThreadNicknameAdded(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractAddChatThreadNicknameNotificationEventArgs e)
		{
			Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname chatThreadNickname = e.Notification.ChatThreadNickname;
			long value = chatThreadNickname.ChatThreadId.Value;
			userDatabase.SetChatThreadNickname(value, chatThreadNickname.Nickname);
			localUser.AddChatThreadNickname(chatThreadNickname);
		}

		private static void HandleChatThreadNicknameRemoved(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractRemoveChatThreadNicknameNotificationEventArgs e)
		{
			long value = e.Notification.ChatThreadId.Value;
			userDatabase.SetChatThreadNickname(value, null);
			localUser.RemoveChatThreadNickname(value);
		}

		private static void HandleUnreadMessageCountCleared(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractClearUnreadMessageCountNotificationEventArgs e)
		{
			long[] array = e.Notification.ChatThreadIds.Cast<long>().ToArray();
			long[] array2 = array;
			foreach (long chatThreadId in array2)
			{
				userDatabase.SetChatThreadUnreadMessageCount(chatThreadId, 0u);
			}
			localUser.ClearUnreadMessageCount(array);
		}

		private static void HandleGagMessageAdded(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractAddChatThreadGagMessageNotificationEventArgs e)
		{
			GagChatMessage message = e.Notification.Message;
			IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(userDatabase, localUser, message);
			string payload = ChatMessageDocumentPayloadFactory.Create(message);
			InsertChatMessageDocument(message, payload, userDatabase, delegate
			{
				localUser.AddChatGagMessage(message);
			});
		}

		private static void HandleGameEventMessageAdded(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractAddChatThreadGameEventMessageNotificationEventArgs e)
		{
			GameEventChatMessage message = e.Notification.Message;
			IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(userDatabase, localUser, message);
			string payload = ChatMessageDocumentPayloadFactory.Create(message);
			InsertChatMessageDocument(message, payload, userDatabase, delegate
			{
				localUser.AddChatGameEventMessage(message);
			});
		}

		private static void HandleGameStateMessageAdded(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractAddChatThreadGameStateMessageNotificationEventArgs e)
		{
			GameStateChatMessage message = e.Notification.Message;
			IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(userDatabase, localUser, message);
			string payload = ChatMessageDocumentPayloadFactory.Create(message);
			InsertChatMessageDocument(message, payload, userDatabase, delegate
			{
				localUser.AddChatGameStateMessage(message);
			});
		}

		private static void HandleMemberListChangedMessageAdded(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractAddChatThreadMemberListChangedMessageNotificationEventArgs e)
		{
			MemberListChangedChatMessage message = e.Notification.Message;
			IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(userDatabase, localUser, message);
			if (!message.MemberUserIds.Any((string userId) => userId == localUser.Swid))
			{
				string payload = ChatMessageDocumentPayloadFactory.Create(message);
				InsertChatMessageDocument(message, payload, userDatabase, delegate
				{
					localUser.AddChatMemberListChangedMessage(message);
				});
			}
		}

		private static void HandlePhotoMessageAdded(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractAddChatThreadPhotoMessageNotificationEventArgs e)
		{
			PhotoChatMessage message = e.Notification.Message;
			IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(userDatabase, localUser, message);
			string payload = ChatMessageDocumentPayloadFactory.Create(message);
			InsertChatMessageDocument(message, payload, userDatabase, delegate
			{
				localUser.AddChatPhotoMessage(message);
			});
		}

		private static void HandleStickerMessageAdded(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractAddChatThreadStickerMessageNotificationEventArgs e)
		{
			StickerChatMessage message = e.Notification.Message;
			IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(userDatabase, localUser, message);
			string payload = ChatMessageDocumentPayloadFactory.Create(message);
			InsertChatMessageDocument(message, payload, userDatabase, delegate
			{
				localUser.AddChatStickerMessage(message);
			});
		}

		private static void HandleTextMessageAdded(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractAddChatThreadTextMessageNotificationEventArgs e)
		{
			TextChatMessage message = e.Notification.Message;
			IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(userDatabase, localUser, message);
			string payload = ChatMessageDocumentPayloadFactory.Create(message);
			InsertChatMessageDocument(message, payload, userDatabase, delegate
			{
				localUser.AddChatTextMessage(message);
			});
		}

		private static void HandleVideoMessageAdded(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractAddChatThreadVideoMessageNotificationEventArgs e)
		{
			VideoChatMessage message = e.Notification.Message;
			IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(userDatabase, localUser, message);
			string payload = ChatMessageDocumentPayloadFactory.Create(message);
			InsertChatMessageDocument(message, payload, userDatabase, delegate
			{
				localUser.AddChatVideoMessage(message);
			});
		}

		private static void HandleFriendshipAdded(IUserDatabase userDatabase, IInternalLocalUser localUser, AbstractAddFriendshipNotificationEventArgs e)
		{
			AddFriendshipNotification notification = e.Notification;
			User friend = notification.Friend;
			userDatabase.PersistUser(friend.UserId, friend.HashedUserId, friend.DisplayName, friend.FirstName, friend.Avatar, friend.Status);
			FriendDocument friendDocument = new FriendDocument();
			friendDocument.Swid = friend.UserId;
			friendDocument.IsTrusted = notification.IsTrusted.Value;
			friendDocument.Nickname = null;
			FriendDocument doc = friendDocument;
			userDatabase.InsertFriend(doc);
			long value = e.Notification.FriendshipInvitationId.Value;
			userDatabase.DeleteFriendInvitation(value);
			localUser.AddFriend(friend, notification.IsTrusted.Value, value);
		}

		private static void HandleFriendshipRemoved(IUserDatabase userDatabase, IInternalLocalUser localUser, AbstractRemoveFriendshipNotificationEventArgs e)
		{
			string swid = e.Notification.FriendUserId;
			string displayName = null;
			IInternalFriend internalFriend = localUser.InternalFriends.FirstOrDefault((IInternalFriend f) => f.Swid == swid);
			if (internalFriend != null)
			{
				displayName = internalFriend.DisplayName.Text;
			}
			else
			{
				UserDocument userBySwid = userDatabase.GetUserBySwid(swid);
				if (userBySwid != null)
				{
					displayName = userBySwid.DisplayName;
				}
			}
			userDatabase.DeleteFriend(swid);
			localUser.RemoveFriend(swid, true);
			if (displayName == null)
			{
				return;
			}
			FriendInvitationDocument incomingInvitation = userDatabase.GetFriendInvitationDocuments(false).FirstOrDefault((FriendInvitationDocument doc) => doc.DisplayName == displayName);
			if (incomingInvitation != null)
			{
				foreach (IInternalIncomingFriendInvitation item in localUser.InternalIncomingFriendInvitations.Where((IInternalIncomingFriendInvitation i) => i.InvitationId == incomingInvitation.FriendInvitationId))
				{
					localUser.RemoveIncomingFriendInvitation(item);
				}
				userDatabase.DeleteFriendInvitation(incomingInvitation.FriendInvitationId);
			}
			FriendInvitationDocument outgoingInvitation = userDatabase.GetFriendInvitationDocuments(true).FirstOrDefault((FriendInvitationDocument doc) => doc.DisplayName == displayName);
			if (outgoingInvitation == null)
			{
				return;
			}
			foreach (IInternalOutgoingFriendInvitation item2 in localUser.InternalOutgoingFriendInvitations.Where((IInternalOutgoingFriendInvitation i) => i.InvitationId == outgoingInvitation.FriendInvitationId))
			{
				localUser.RemoveOutgoingFriendInvitation(item2);
			}
			userDatabase.DeleteFriendInvitation(outgoingInvitation.FriendInvitationId);
		}

		private static void HandleFriendshipTrustRemoved(IUserDatabase userDatabase, IInternalLocalUser localUser, AbstractRemoveFriendshipTrustNotificationEventArgs e)
		{
			string friendUserId = e.Notification.FriendUserId;
			userDatabase.SetFriendIsTrusted(friendUserId, false);
			localUser.UntrustFriend(e.Notification.FriendUserId);
		}

		private static void HandleNicknameAdded(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractAddNicknameNotificationEventArgs e)
		{
			AddNicknameNotification notification = e.Notification;
			Disney.Mix.SDK.Internal.MixDomain.UserNickname nickname = notification.Nickname;
			string nicknamedUserId = nickname.NicknamedUserId;
			userDatabase.SetFriendNickname(nicknamedUserId, nickname.Nickname);
			localUser.AddNickname(nickname);
		}

		private static void HandleNicknameRemoved(IInternalLocalUser localUser, IUserDatabase userDatabase, AbstractRemoveNicknameNotificationEventArgs e)
		{
			string nicknamedUserId = e.Notification.NicknamedUserId;
			userDatabase.SetFriendNickname(nicknamedUserId, null);
			localUser.UpdateNickname(e.Notification.NicknamedUserId, null);
		}

		private static void HandleFriendshipInvitationAdded(IUserDatabase userDatabase, IInternalLocalUser localUser, AbstractAddFriendshipInvitationNotificationEventArgs e)
		{
			AddFriendshipInvitationNotification notification = e.Notification;
			FriendshipInvitation invitation = notification.Invitation;
			User friend = notification.Friend;
			userDatabase.PersistUser(null, null, invitation.FriendDisplayName, friend.FirstName, friend.Avatar, friend.Status);
			FriendInvitationDocument friendInvitationDocument = new FriendInvitationDocument();
			friendInvitationDocument.FriendInvitationId = invitation.FriendshipInvitationId.Value;
			friendInvitationDocument.IsInviter = invitation.IsInviter.Value;
			friendInvitationDocument.IsTrusted = invitation.IsTrusted.Value;
			friendInvitationDocument.DisplayName = invitation.FriendDisplayName;
			FriendInvitationDocument doc = friendInvitationDocument;
			userDatabase.InsertOrUpdateFriendInvitation(doc);
			localUser.AddFriendshipInvitation(invitation, friend);
		}

		private static void HandleFriendshipInvitationRemoved(IUserDatabase userDatabase, IInternalLocalUser localUser, AbstractRemoveFriendshipInvitationNotificationEventArgs e)
		{
			long value = e.Notification.InvitationId.Value;
			userDatabase.DeleteFriendInvitation(value);
			localUser.RemoveFriendshipInvitation(value);
		}

		private static void HandleOfficialAccountFollowed(IInternalLocalUser localUser, IUserDatabase userDatabase, IEpochTime epochTime, AbstractAddFollowshipNotificationEventArgs e)
		{
			AddFollowshipNotification notification = e.Notification;
			GuestOfficialAccount officialAccount = notification.OfficialAccount;
			string oaId = officialAccount.OaId;
			OfficialAccountDocument officialAccount2 = userDatabase.GetOfficialAccount(oaId);
			if (officialAccount2 != null)
			{
				userDatabase.UpdateOfficialAccount(oaId, delegate(OfficialAccountDocument oaDoc)
				{
					oaDoc.IsFollowing = true;
					oaDoc.LastUpdated = epochTime.Milliseconds;
				});
			}
			else
			{
				userDatabase.InsertOfficialAccount(officialAccount.OaId, officialAccount.OaName, true, notification.OfficialAccount.IsAvailable.Value, notification.OfficialAccount.CanUnfollow.Value);
			}
			localUser.FollowOfficialAccount(officialAccount);
		}

		private static void HandleOfficialAccountUnfollowed(IInternalLocalUser localUser, IUserDatabase userDatabase, IEpochTime epochTime, AbstractRemoveFollowshipNotificationEventArgs e)
		{
			RemoveFollowshipNotification notification = e.Notification;
			string officialAccountId = notification.OfficialAccountId;
			OfficialAccountDocument officialAccount = userDatabase.GetOfficialAccount(officialAccountId);
			if (officialAccount != null)
			{
				userDatabase.UpdateOfficialAccount(officialAccountId, delegate(OfficialAccountDocument oaDoc)
				{
					oaDoc.IsFollowing = false;
					oaDoc.LastUpdated = epochTime.Milliseconds;
				});
			}
			localUser.UnfollowOfficialAccount(officialAccountId);
		}

		private static void InsertChatMessageDocument(BaseChatMessage message, string payload, IUserDatabase userDatabase, Action callback)
		{
			long chatThreadId = message.ChatThreadId.Value;
			userDatabase.IndexSequenceNumberField(chatThreadId, delegate
			{
				ChatMessageDocument document = new ChatMessageDocument
				{
					ChatMessageId = message.ChatMessageId.Value,
					SenderId = message.SenderUserId,
					Created = message.Created.Value,
					ChatMessageType = message.MessageType,
					SequenceNumber = message.SequenceNumber.Value,
					Payload = payload,
					IsSent = true,
					LocalChatMessageId = message.ClientChatMessageId.Value
				};
				userDatabase.InsertChatMessage(chatThreadId, document);
				callback();
			});
		}

		private static void IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(IUserDatabase userDatabase, IInternalLocalUser localUser, BaseChatMessage message)
		{
			long value = message.ChatThreadId.Value;
			long value2 = message.SequenceNumber.Value;
			if (message.SenderUserId != localUser.Swid)
			{
				userDatabase.IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(value, value2);
			}
			else
			{
				userDatabase.UpdateLatestSequenceNumber(value, value2);
			}
		}
	}
}
