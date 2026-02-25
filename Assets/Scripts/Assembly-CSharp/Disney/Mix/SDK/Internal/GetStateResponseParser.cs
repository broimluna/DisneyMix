using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class GetStateResponseParser : IGetStateResponseParser
	{
		private readonly AbstractLogger logger;

		private readonly IChatThreadFactory chatThreadFactory;

		public GetStateResponseParser(AbstractLogger logger, IChatThreadFactory chatThreadFactory)
		{
			this.logger = logger;
			this.chatThreadFactory = chatThreadFactory;
		}

		public IInternalAvatar ParseAvatar(GetStateResponse response, IUserDatabase userDatabase, string swid, string displayName)
		{
			List<User> users = response.Users;
			if (users != null)
			{
				User user = users.FirstOrDefault((User u) => (u.UserId == swid && swid != null) || (u.DisplayName == displayName && displayName != null));
				if (user != null && !OfficialAccountUtils.IsBotId(user.UserId))
				{
					Disney.Mix.SDK.Internal.MixDomain.Avatar avatar = user.Avatar;
					if (avatar == null)
					{
						UserDocument userDocument = null;
						if (swid != null)
						{
							userDocument = userDatabase.GetUserBySwid(swid);
						}
						if (userDocument == null && displayName != null)
						{
							userDocument = userDatabase.GetUserByDisplayName(displayName);
						}
						if (userDocument != null)
						{
							long avatarId = userDocument.AvatarId;
							AvatarDocument avatarDocument = ((avatarId != 0L) ? userDatabase.GetAvatar(avatarId) : null);
							return AvatarBuilder.Build(avatarDocument);
						}
					}
					return AvatarBuilder.Build(avatar);
				}
			}
			return null;
		}

		public IList<IInternalFriend> ParseFriendships(GetStateResponse response, IUserDatabase userDatabase)
		{
			List<IInternalFriend> list = new List<IInternalFriend>();
			List<User> users = response.Users;
			List<Disney.Mix.SDK.Internal.MixDomain.UserNickname> userNicknames = response.UserNicknames;
			if (response.Friendships != null)
			{
				foreach (Friendship friendship in response.Friendships)
				{
					string userId = friendship.FriendUserId;
					bool value = friendship.IsTrusted.Value;
					string displayNameText = string.Empty;
					string firstName = string.Empty;
					if (users != null)
					{
						User user = users.FirstOrDefault((User u) => u.UserId == userId);
						if (user != null)
						{
							displayNameText = user.DisplayName;
							firstName = user.FirstName;
						}
					}
					IInternalFriend internalFriend = RemoteUserFactory.CreateFriend(userId, value, displayNameText, firstName, userDatabase);
					if (userNicknames != null)
					{
						Disney.Mix.SDK.Internal.MixDomain.UserNickname userNickname = userNicknames.FirstOrDefault((Disney.Mix.SDK.Internal.MixDomain.UserNickname n) => n.NicknamedUserId == userId);
						if (userNickname != null)
						{
							internalFriend.UpdateNickname(new UserNickname(userNickname.Nickname));
						}
					}
					list.Add(internalFriend);
				}
			}
			return list;
		}

		public void ParseChatThreads(IMixWebCallFactory mixWebCallFactory, GetStateResponse response, string swid, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler, out IList<IInternalOneOnOneChatThread> oneOnOneChatThreads, out IList<IInternalGroupChatThread> groupChatThreads, out IList<IInternalOfficialAccountChatThread> officialAccountChatThreads)
		{
			oneOnOneChatThreads = new List<IInternalOneOnOneChatThread>();
			groupChatThreads = new List<IInternalGroupChatThread>();
			officialAccountChatThreads = new List<IInternalOfficialAccountChatThread>();
			if (response.ChatThreads == null)
			{
				return;
			}
			List<Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname> chatThreadNicknames = response.ChatThreadNicknames;
			IDictionary<long, long> chatLatestSequenceNumbers = SequenceNumberUtils.CreateSequenceNumberDictionary(response.ChatThreadLatestMessageSequenceNumbers);
			IDictionary<long, long> chatLastSeenSequenceNumbers = SequenceNumberUtils.CreateSequenceNumberDictionary(response.ChatThreadLastSeenMessageSequenceNumbers);
			foreach (ChatThread chatThread in response.ChatThreads)
			{
				List<IInternalRemoteChatMember> list = new List<IInternalRemoteChatMember>();
				if (chatThread.Members != null)
				{
					foreach (User member in chatThread.Members)
					{
						string userId = member.UserId;
						if (!(userId == swid))
						{
							IInternalRemoteChatMember item = RemoteUserFactory.CreateRemoteChatMember(userId, member.DisplayName, member.FirstName, userDatabase);
							list.Add(item);
						}
					}
				}
				ChatThreadTrustLevel trustLevel = new ChatThreadTrustLevel(chatThread.IsTrusted.Value);
				long value = chatThread.ChatThreadId.Value;
				long chatThreadLatestSequenceNumber = GetChatThreadLatestSequenceNumber(chatLatestSequenceNumbers, value);
				switch (chatThread.ChatThreadType)
				{
				case "ONE_ON_ONE":
				{
					IInternalOneOnOneChatThread internalOneOnOneChatThread = chatThreadFactory.CreateOneOnOneChatThread(mixWebCallFactory, value, trustLevel, swid, chatThreadLatestSequenceNumber, true, list, userDatabase, chatMessageParser, chatMessageHandler);
					UpdateChatThreadNickname(chatThreadNicknames, internalOneOnOneChatThread);
					UpdateChatThreadUnreadMessageCount(chatLatestSequenceNumbers, chatLastSeenSequenceNumbers, internalOneOnOneChatThread);
					oneOnOneChatThreads.Add(internalOneOnOneChatThread);
					break;
				}
				case "GROUP":
				{
					IInternalGroupChatThread internalGroupChatThread = chatThreadFactory.CreateGroupChatThread(mixWebCallFactory, value, trustLevel, swid, chatThreadLatestSequenceNumber, true, list, userDatabase, chatMessageParser, chatMessageHandler);
					UpdateChatThreadNickname(chatThreadNicknames, internalGroupChatThread);
					UpdateChatThreadUnreadMessageCount(chatLatestSequenceNumbers, chatLastSeenSequenceNumbers, internalGroupChatThread);
					groupChatThreads.Add(internalGroupChatThread);
					break;
				}
				case "OFFICIAL_ACCOUNT":
					try
					{
						IInternalOfficialAccountChatThread internalOfficialAccountChatThread = chatThreadFactory.CreateOfficialAccountChatThread(mixWebCallFactory, value, trustLevel, swid, chatThread.OfficialAccountId, chatThreadLatestSequenceNumber, true, list, userDatabase, chatMessageParser, chatMessageHandler);
						UpdateChatThreadUnreadMessageCount(chatLatestSequenceNumbers, chatLastSeenSequenceNumbers, internalOfficialAccountChatThread);
						officialAccountChatThreads.Add(internalOfficialAccountChatThread);
					}
					catch (Exception ex)
					{
						logger.Critical("Error creating official account thread: " + ex);
					}
					break;
				}
			}
		}

		private static void UpdateChatThreadNickname(IList<Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname> chatNicknames, IInternalChatThread chatThread)
		{
			if (chatNicknames != null)
			{
				Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname chatThreadNickname = chatNicknames.FirstOrDefault(delegate(Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname n)
				{
					long? chatThreadId = n.ChatThreadId;
					return chatThreadId.GetValueOrDefault() == chatThread.ChatThreadId && chatThreadId.HasValue;
				});
				if (chatThreadNickname != null)
				{
					chatThread.UpdateNickname(chatThreadNickname.Nickname);
				}
			}
		}

		private static void UpdateChatThreadUnreadMessageCount(IDictionary<long, long> chatLatestSequenceNumbers, IDictionary<long, long> chatLastSeenSequenceNumbers, IInternalChatThread chatThread)
		{
			if (chatLatestSequenceNumbers != null && chatLastSeenSequenceNumbers != null)
			{
				long chatThreadId = chatThread.ChatThreadId;
				if (chatLatestSequenceNumbers.ContainsKey(chatThreadId))
				{
					long value;
					chatLastSeenSequenceNumbers.TryGetValue(chatThreadId, out value);
					long num = chatLatestSequenceNumbers[chatThreadId] - value;
					chatThread.UpdateUnreadMessageCount((uint)num);
				}
			}
		}

		private static void UpdateChatThreadLatestSequenceNumber(IDictionary<long, long> chatLatestSequenceNumbers, IInternalChatThread chatThread)
		{
			long chatThreadLatestSequenceNumber = GetChatThreadLatestSequenceNumber(chatLatestSequenceNumbers, chatThread.ChatThreadId);
			if (chatThreadLatestSequenceNumber > chatThread.LatestSequenceNumber)
			{
				chatThread.LatestSequenceNumber = chatThreadLatestSequenceNumber;
			}
		}

		private static long GetChatThreadLatestSequenceNumber(IDictionary<long, long> chatLatestSequenceNumbers, long chatThreadId)
		{
			long value;
			chatLatestSequenceNumbers.TryGetValue(chatThreadId, out value);
			return value;
		}

		public void ParseFriendshipInvitations(GetStateResponse response, IUserDatabase userDatabase, IInternalLocalUser localUser, out IList<IInternalIncomingFriendInvitation> incomingFriendInvitations, out IList<IInternalOutgoingFriendInvitation> outgoingFriendInvitations)
		{
			incomingFriendInvitations = new List<IInternalIncomingFriendInvitation>();
			outgoingFriendInvitations = new List<IInternalOutgoingFriendInvitation>();
			if (response.FriendshipInvitations == null)
			{
				return;
			}
			FriendshipInvitation invitation;
			foreach (FriendshipInvitation friendshipInvitation in response.FriendshipInvitations)
			{
				invitation = friendshipInvitation;
				User user = response.Users.FirstOrDefault((User user2) => user2.DisplayName == invitation.FriendDisplayName);
				string firstName = ((user != null) ? user.FirstName : null);
				if (invitation.IsInviter.Value)
				{
					IInternalUnidentifiedUser invitee = RemoteUserFactory.CreateUnidentifiedUser(invitation.FriendDisplayName, firstName, userDatabase);
					OutgoingFriendInvitation outgoingFriendInvitation = new OutgoingFriendInvitation(localUser, invitee, invitation.IsTrusted.Value);
					outgoingFriendInvitation.SendComplete(invitation.FriendshipInvitationId.Value);
					outgoingFriendInvitations.Add(outgoingFriendInvitation);
				}
				else
				{
					IInternalUnidentifiedUser inviter = RemoteUserFactory.CreateUnidentifiedUser(invitation.FriendDisplayName, firstName, userDatabase);
					IncomingFriendInvitation incomingFriendInvitation = new IncomingFriendInvitation(inviter, localUser, invitation.IsTrusted.Value);
					incomingFriendInvitation.SendComplete(invitation.FriendshipInvitationId.Value);
					incomingFriendInvitations.Add(incomingFriendInvitation);
				}
			}
		}

		public void ReconcileWithLocalUser(IMixWebCallFactory mixWebCallFactory, GetStateResponse response, IInternalLocalUser localUser, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler)
		{
			ReconcileAvatar(response, localUser, userDatabase);
			ReconcileFriends(response, localUser, userDatabase);
			ReconcileFriendInvitations(response, localUser, userDatabase);
			ReconcileChatThreads(mixWebCallFactory, response, localUser, userDatabase, chatMessageParser, chatMessageHandler);
			ReconcileGameStateMessages(response, localUser);
			ReconcileOfficialAccounts(response, localUser, userDatabase);
		}

		private void ReconcileChatThreads(IMixWebCallFactory mixWebCallFactory, GetStateResponse response, IInternalLocalUser localUser, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler)
		{
			List<ChatThread> chatThreads = response.ChatThreads;
			List<Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname> chatThreadNicknames = response.ChatThreadNicknames;
			IDictionary<long, long> chatLatestSequenceNumbers = SequenceNumberUtils.CreateSequenceNumberDictionary(response.ChatThreadLatestMessageSequenceNumbers);
			IDictionary<long, long> chatLastSeenSequenceNumbers = SequenceNumberUtils.CreateSequenceNumberDictionary(response.ChatThreadLastSeenMessageSequenceNumbers);
			List<IInternalOneOnOneChatThread> list = localUser.InternalOneOnOneChatThreads.ToList();
			List<IInternalGroupChatThread> list2 = localUser.InternalGroupChatThreads.ToList();
			List<IInternalOfficialAccountChatThread> list3 = localUser.InternalOfficialAccountChatThreads.ToList();
			if (chatThreads != null)
			{
				ChatThread chatThread;
				foreach (ChatThread item2 in chatThreads)
				{
					chatThread = item2;
					Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname chatThreadNickname = ((chatThreadNicknames == null) ? null : chatThreadNicknames.FirstOrDefault(delegate(Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname n)
					{
						long? chatThreadId = n.ChatThreadId;
						long valueOrDefault = chatThreadId.GetValueOrDefault();
						long? chatThreadId2 = chatThread.ChatThreadId;
						return valueOrDefault == chatThreadId2.GetValueOrDefault() && chatThreadId.HasValue == chatThreadId2.HasValue;
					}));
					IInternalOneOnOneChatThread internalOneOnOneChatThread = list.FirstOrDefault((IInternalOneOnOneChatThread ct) => ct.ChatThreadId == chatThread.ChatThreadId);
					if (internalOneOnOneChatThread != null)
					{
						ReconcileChatThread(internalOneOnOneChatThread, chatThread, response, localUser, userDatabase, chatThreadNickname, chatLatestSequenceNumbers, chatLastSeenSequenceNumbers);
						list.Remove(internalOneOnOneChatThread);
						continue;
					}
					IInternalGroupChatThread internalGroupChatThread = list2.FirstOrDefault((IInternalGroupChatThread ct) => ct.ChatThreadId == chatThread.ChatThreadId);
					if (internalGroupChatThread != null)
					{
						ReconcileChatThread(internalGroupChatThread, chatThread, response, localUser, userDatabase, chatThreadNickname, chatLatestSequenceNumbers, chatLastSeenSequenceNumbers);
						list2.Remove(internalGroupChatThread);
						continue;
					}
					IInternalOfficialAccountChatThread internalOfficialAccountChatThread = list3.FirstOrDefault((IInternalOfficialAccountChatThread ct) => ct.ChatThreadId == chatThread.ChatThreadId);
					if (internalOfficialAccountChatThread != null)
					{
						ReconcileChatThread(internalOfficialAccountChatThread, chatThread, response, localUser, userDatabase, chatThreadNickname, chatLatestSequenceNumbers, chatLastSeenSequenceNumbers);
						list3.Remove(internalOfficialAccountChatThread);
						continue;
					}
					List<IInternalRemoteChatMember> list4 = new List<IInternalRemoteChatMember>();
					if (chatThread.Members != null)
					{
						foreach (User member in chatThread.Members)
						{
							if (!(member.UserId == localUser.Swid))
							{
								IInternalRemoteChatMember item = RemoteUserFactory.CreateRemoteChatMember(member.UserId, member.DisplayName, member.FirstName, userDatabase);
								list4.Add(item);
							}
						}
					}
					ChatThreadTrustLevel trustLevel = new ChatThreadTrustLevel(chatThread.IsTrusted.Value);
					long value = chatThread.ChatThreadId.Value;
					long chatThreadLatestSequenceNumber = GetChatThreadLatestSequenceNumber(chatLatestSequenceNumbers, value);
					switch (chatThread.ChatThreadType)
					{
					case "ONE_ON_ONE":
					{
						IInternalOneOnOneChatThread chatThread4 = chatThreadFactory.CreateOneOnOneChatThread(mixWebCallFactory, value, trustLevel, localUser.Swid, chatThreadLatestSequenceNumber, true, list4, userDatabase, chatMessageParser, chatMessageHandler);
						UpdateChatThreadNickname(chatThreadNickname, chatThread4);
						UpdateChatThreadUnreadMessageCount(chatLatestSequenceNumbers, chatLastSeenSequenceNumbers, chatThread4);
						UpdateChatThreadLatestSequenceNumber(chatLatestSequenceNumbers, chatThread4);
						localUser.AddOneOnOneChatThread(chatThread4);
						break;
					}
					case "GROUP":
					{
						IInternalGroupChatThread chatThread3 = chatThreadFactory.CreateGroupChatThread(mixWebCallFactory, value, trustLevel, localUser.Swid, chatThreadLatestSequenceNumber, true, list4, userDatabase, chatMessageParser, chatMessageHandler);
						UpdateChatThreadNickname(chatThreadNickname, chatThread3);
						UpdateChatThreadUnreadMessageCount(chatLatestSequenceNumbers, chatLastSeenSequenceNumbers, chatThread3);
						UpdateChatThreadLatestSequenceNumber(chatLatestSequenceNumbers, chatThread3);
						localUser.AddGroupChatThread(chatThread3);
						break;
					}
					case "OFFICIAL_ACCOUNT":
					{
						IInternalOfficialAccountChatThread chatThread2 = chatThreadFactory.CreateOfficialAccountChatThread(mixWebCallFactory, value, trustLevel, localUser.Swid, chatThread.OfficialAccountId, chatThreadLatestSequenceNumber, true, list4, userDatabase, chatMessageParser, chatMessageHandler);
						UpdateChatThreadUnreadMessageCount(chatLatestSequenceNumbers, chatLastSeenSequenceNumbers, chatThread2);
						UpdateChatThreadLatestSequenceNumber(chatLatestSequenceNumbers, chatThread2);
						localUser.AddOfficialAccountChatThread(chatThread2);
						break;
					}
					}
				}
			}
			foreach (IInternalOneOnOneChatThread item3 in list)
			{
				localUser.RemoveOneOnOneChatThread(item3);
			}
			foreach (IInternalGroupChatThread item4 in list2)
			{
				localUser.RemoveGroupChatThread(item4);
			}
		}

		private void ReconcileChatThread(IInternalChatThread localChatThread, ChatThread chatThread, GetStateResponse response, IInternalLocalUser localUser, IUserDatabase userDatabase, Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname chatThreadNickname, IDictionary<long, long> chatLatestSequenceNumbers, IDictionary<long, long> chatLastSeenSequenceNumbers)
		{
			List<User> users = response.Users;
			List<IInternalRemoteChatMember> list = localChatThread.InternalMembers.ToList();
			User chatMember;
			foreach (User member2 in chatThread.Members)
			{
				chatMember = member2;
				if (chatMember.UserId == localUser.Swid)
				{
					continue;
				}
				IInternalAvatar internalAvatar = ParseAvatar(response, userDatabase, chatMember.UserId, chatMember.DisplayName);
				IInternalRemoteChatMember internalRemoteChatMember = list.FirstOrDefault((IInternalRemoteChatMember m) => m.Swid == chatMember.UserId);
				if (internalRemoteChatMember != null)
				{
					internalRemoteChatMember.UpdateNames(chatMember.DisplayName, chatMember.FirstName);
					if (internalAvatar != null)
					{
						internalRemoteChatMember.DispatchOnAvatarChanged();
					}
					list.Remove(internalRemoteChatMember);
				}
				else
				{
					User user = users.First((User u) => u.UserId == chatMember.UserId);
					IInternalRemoteChatMember member = RemoteUserFactory.CreateRemoteChatMember(chatMember.UserId, user.DisplayName, user.FirstName, userDatabase);
					localChatThread.AddRemoteMember(member, true);
				}
			}
			foreach (IInternalRemoteChatMember item in list)
			{
				localChatThread.RemoveRemoteMember(item, true, true);
			}
			if (localChatThread.InternalTrustLevel.AllMembersTrustEachOther != chatThread.IsTrusted)
			{
				localChatThread.UpdateChatTrustLevel(chatThread.IsTrusted.Value);
			}
			if (chatThreadNickname != null && (localChatThread.InternalNickname == null || localChatThread.InternalNickname.Nickname != chatThreadNickname.Nickname))
			{
				localChatThread.UpdateNickname(chatThreadNickname.Nickname);
			}
			UpdateChatThreadUnreadMessageCount(chatLatestSequenceNumbers, chatLastSeenSequenceNumbers, localChatThread);
			UpdateChatThreadLatestSequenceNumber(chatLatestSequenceNumbers, localChatThread);
		}

		private static void UpdateChatThreadNickname(Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname chatThreadNickname, IInternalChatThread chatThread)
		{
			if (chatThreadNickname != null)
			{
				chatThread.UpdateNickname(chatThreadNickname.Nickname);
			}
		}

		private void ReconcileGameStateMessages(GetStateResponse response, IInternalLocalUser localUser)
		{
			List<GameStateChatMessage> gameStateChatMessages = response.GameStateChatMessages;
			if (gameStateChatMessages == null)
			{
				return;
			}
			GameStateChatMessage gameStateChatMessage;
			foreach (GameStateChatMessage item in gameStateChatMessages)
			{
				gameStateChatMessage = item;
				IInternalChatThread internalChatThread = localUser.InternalChatThreads.FirstOrDefault((IInternalChatThread ct) => ct.ChatThreadId == gameStateChatMessage.ChatThreadId);
				if (internalChatThread != null)
				{
					IInternalGameStateMessage internalGameStateMessage = internalChatThread.InternalChatMessages.FirstOrDefault((IInternalChatMessage m) => m.ChatMessageId == gameStateChatMessage.ChatMessageId) as IInternalGameStateMessage;
					if (internalGameStateMessage != null)
					{
						internalGameStateMessage.UpdateState(null, JsonParser.FromJson<Dictionary<string, object>>(gameStateChatMessage.State));
					}
				}
			}
		}

		private void ReconcileAvatar(GetStateResponse response, IInternalLocalUser localUser, IUserDatabase userDatabase)
		{
			IInternalAvatar internalAvatar = ParseAvatar(response, userDatabase, localUser.Swid, localUser.DisplayName.Text);
			if (internalAvatar != null)
			{
				localUser.DispatchOnAvatarChanged();
			}
		}

		private void ReconcileFriends(GetStateResponse response, IInternalLocalUser localUser, IUserDatabase userDatabase)
		{
			List<IInternalFriend> list = localUser.InternalFriends.ToList();
			List<IInternalIncomingFriendInvitation> list2 = localUser.InternalIncomingFriendInvitations.ToList();
			List<IInternalOutgoingFriendInvitation> list3 = localUser.InternalOutgoingFriendInvitations.ToList();
			if (response.Friendships != null)
			{
				foreach (Friendship friendship in response.Friendships)
				{
					string friendUserId = friendship.FriendUserId;
					IInternalFriend internalFriend = list.FirstOrDefault((IInternalFriend f) => f.Swid == friendUserId);
					Disney.Mix.SDK.Internal.MixDomain.UserNickname userNickname = ((response.UserNicknames == null) ? null : response.UserNicknames.FirstOrDefault((Disney.Mix.SDK.Internal.MixDomain.UserNickname n) => n.NicknamedUserId == friendUserId));
					User user = response.Users.First((User u) => u.UserId == friendUserId);
					IInternalAvatar internalAvatar = ParseAvatar(response, userDatabase, friendship.FriendUserId, user.DisplayName);
					if (internalFriend != null)
					{
						if (internalFriend.IsTrusted != friendship.IsTrusted)
						{
							if (friendship.IsTrusted.Value)
							{
								internalFriend.ChangeTrust(true);
							}
							else
							{
								localUser.UntrustFriend(internalFriend);
							}
						}
						if (internalAvatar != null)
						{
							internalFriend.DispatchOnAvatarChanged();
						}
						if (userNickname != null && (internalFriend.Nickname == null || internalFriend.Nickname.Text != userNickname.Nickname))
						{
							UserNickname nickname = new UserNickname(userNickname.Nickname);
							internalFriend.UpdateNickname(nickname);
						}
						list.Remove(internalFriend);
						continue;
					}
					IInternalFriend internalFriend2 = RemoteUserFactory.CreateFriend(friendship.FriendUserId, friendship.IsTrusted.Value, user.DisplayName, user.FirstName, userDatabase);
					if (userNickname != null)
					{
						UserNickname nickname2 = new UserNickname(userNickname.Nickname);
						internalFriend2.UpdateNickname(nickname2);
					}
					IInternalIncomingFriendInvitation internalIncomingFriendInvitation = list2.FirstOrDefault((IInternalIncomingFriendInvitation i) => i.InternalInviter.DisplayName.Text == user.DisplayName);
					if (internalIncomingFriendInvitation != null)
					{
						localUser.AddFriend(internalFriend2);
						internalIncomingFriendInvitation.Accepted(internalFriend2.IsTrusted, internalFriend2);
						list2.Remove(internalIncomingFriendInvitation);
						localUser.RemoveIncomingFriendInvitation(internalIncomingFriendInvitation);
						continue;
					}
					IInternalOutgoingFriendInvitation internalOutgoingFriendInvitation = list3.FirstOrDefault((IInternalOutgoingFriendInvitation i) => i.InternalInvitee.DisplayName.Text == user.DisplayName);
					if (internalOutgoingFriendInvitation != null)
					{
						localUser.AddFriend(internalFriend2);
						internalOutgoingFriendInvitation.Accepted(internalFriend2.IsTrusted, internalFriend2);
						list3.Remove(internalOutgoingFriendInvitation);
						localUser.RemoveOutgoingFriendInvitation(internalOutgoingFriendInvitation);
					}
					else
					{
						localUser.AddFriend(internalFriend2);
					}
				}
			}
			foreach (IInternalFriend item in list)
			{
				localUser.RemoveFriend(item);
			}
		}

		private void ReconcileFriendInvitations(GetStateResponse response, IInternalLocalUser localUser, IUserDatabase userDatabase)
		{
			List<FriendshipInvitation> friendshipInvitations = response.FriendshipInvitations;
			List<IInternalIncomingFriendInvitation> list = localUser.InternalIncomingFriendInvitations.ToList();
			List<IInternalOutgoingFriendInvitation> list2 = localUser.InternalOutgoingFriendInvitations.ToList();
			if (friendshipInvitations != null)
			{
				FriendshipInvitation friendInvitation;
				foreach (FriendshipInvitation item in friendshipInvitations)
				{
					friendInvitation = item;
					IInternalIncomingFriendInvitation internalIncomingFriendInvitation = list.FirstOrDefault((IInternalIncomingFriendInvitation i) => i.InvitationId == friendInvitation.FriendshipInvitationId);
					if (internalIncomingFriendInvitation != null)
					{
						if (internalIncomingFriendInvitation.RequestTrust != friendInvitation.IsTrusted)
						{
							internalIncomingFriendInvitation.RequestTrust = friendInvitation.IsTrusted.Value;
						}
						list.Remove(internalIncomingFriendInvitation);
						continue;
					}
					IInternalOutgoingFriendInvitation internalOutgoingFriendInvitation = list2.FirstOrDefault((IInternalOutgoingFriendInvitation i) => i.InvitationId == friendInvitation.FriendshipInvitationId);
					if (internalOutgoingFriendInvitation != null)
					{
						if (internalOutgoingFriendInvitation.RequestTrust != friendInvitation.IsTrusted)
						{
							internalOutgoingFriendInvitation.RequestTrust = friendInvitation.IsTrusted.Value;
						}
						list2.Remove(internalOutgoingFriendInvitation);
						continue;
					}
					User user = response.Users.FirstOrDefault((User user2) => user2.DisplayName == friendInvitation.FriendDisplayName);
					string firstName = ((user != null) ? user.FirstName : null);
					if (friendInvitation.IsInviter.Value)
					{
						IInternalUnidentifiedUser invitee = RemoteUserFactory.CreateUnidentifiedUser(friendInvitation.FriendDisplayName, firstName, userDatabase);
						OutgoingFriendInvitation outgoingFriendInvitation = new OutgoingFriendInvitation(localUser, invitee, friendInvitation.IsTrusted.Value);
						outgoingFriendInvitation.SendComplete(friendInvitation.FriendshipInvitationId.Value);
						localUser.AddOutgoingFriendInvitation(outgoingFriendInvitation);
					}
					else
					{
						IInternalUnidentifiedUser inviter = RemoteUserFactory.CreateUnidentifiedUser(friendInvitation.FriendDisplayName, firstName, userDatabase);
						IncomingFriendInvitation incomingFriendInvitation = new IncomingFriendInvitation(inviter, localUser, friendInvitation.IsTrusted.Value);
						incomingFriendInvitation.SendComplete(friendInvitation.FriendshipInvitationId.Value);
						localUser.AddIncomingFriendInvitation(incomingFriendInvitation);
					}
				}
			}
			foreach (IInternalIncomingFriendInvitation item2 in list)
			{
				localUser.RemoveIncomingFriendInvitation(item2);
				item2.Rejected();
			}
			foreach (IInternalOutgoingFriendInvitation item3 in list2)
			{
				localUser.RemoveOutgoingFriendInvitation(item3);
				item3.Rejected();
			}
		}

		private void ReconcileOfficialAccounts(GetStateResponse response, IInternalLocalUser localUser, IUserDatabase userDatabase)
		{
			IList<IOfficialAccount> officialAccounts = ParseOfficialAccounts(response, userDatabase);
			localUser.UpdateFollowships(officialAccounts);
		}

		public void ParsePollIntervals(GetStateResponse response, out int[] pollIntervals, out int[] pokeIntervals)
		{
			pollIntervals = response.PollIntervals.Select((int? i) => i.Value * 1000).ToArray();
			pokeIntervals = response.PokeIntervals.Select((int? i) => i.Value * 1000).ToArray();
		}

		public IList<IInternalAlert> ParseAlerts(GetStateResponse response)
		{
			return ((IEnumerable<Disney.Mix.SDK.Internal.MixDomain.Alert>)response.Alerts).Select((Func<Disney.Mix.SDK.Internal.MixDomain.Alert, IInternalAlert>)((Disney.Mix.SDK.Internal.MixDomain.Alert a) => new Alert(a))).ToList();
		}

		public IList<IOfficialAccount> ParseOfficialAccounts(GetStateResponse response, IUserDatabase userDatabase)
		{
			List<IOfficialAccount> list = new List<IOfficialAccount>();
			if (response != null && response.OfficialAccounts != null)
			{
				foreach (string officialAccount2 in response.OfficialAccounts)
				{
					OfficialAccountDocument officialAccount = userDatabase.GetOfficialAccount(officialAccount2);
					if (officialAccount != null)
					{
						IOfficialAccount item = OfficialAccountFactory.CreateOfficialAccount(officialAccount.AccountId, officialAccount.DisplayName, officialAccount.IsAvailable, officialAccount.CanUnfollow);
						list.Add(item);
						continue;
					}
					throw new InvalidOperationException("Official Account (" + officialAccount2 + ") not in DB");
				}
			}
			return list;
		}

		public List<BaseNotification> CollectNotifications(GetNotificationsResponse response)
		{
			return EnumerateValidNotifications(logger, response.AddChatThread, NotificationValidator.Validate).Concat(EnumerateValidNotifications(logger, response.AddChatThreadMembership, NotificationValidator.Validate)).Concat(EnumerateValidNotifications(logger, response.AddChatThreadGagMessage, NotificationValidator.Validate)).Concat(EnumerateValidNotifications(logger, response.AddChatThreadGameEventMessage, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddChatThreadGameStateMessage, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.UpdateChatThreadGameStateMessage, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddChatThreadPhotoMessage, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddChatThreadStickerMessage, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddChatThreadTextMessage, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddChatThreadVideoMessage, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddChatThreadMemberListChangedMessage, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.RemoveChatThreadMembership, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddFriendshipInvitation, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddFriendship, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.RemoveFriendship, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.RemoveFriendshipInvitation, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.RemoveFriendshipTrust, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddNickname, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.RemoveNickname, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.SetAvatar, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.UpdateChatThreadTrustStatus, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddChatThreadNickname, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.RemoveChatThreadNickname, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.ClearMemberChatHistory, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.ClearUnreadMessageCount, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddFollowship, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.RemoveFollowship, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.AddAlert, NotificationValidator.Validate))
				.Concat(EnumerateValidNotifications(logger, response.ClearAlert, NotificationValidator.Validate))
				.ToList();
		}

		private static IEnumerable<BaseNotification> EnumerateValidNotifications<TNotification>(AbstractLogger logger, IList<TNotification> notifications, Func<TNotification, bool> isValid) where TNotification : BaseNotification
		{
			if (notifications == null)
			{
				yield break;
			}
			foreach (TNotification notification in notifications)
			{
				if (isValid(notification))
				{
					yield return notification;
				}
				else
				{
					logger.Critical("Invalid notification: " + JsonParser.ToJson(notification));
				}
			}
		}
	}
}
