using System;
using System.Linq;
using Disney.Mix.SDK;
using Mix.Session.Extensions;

namespace Mix.Session
{
	public class MixFriends
	{
		public delegate void BadgesChanged();

		private static ILocalUser user;

		public static event BadgesChanged OnBadgesChanged;

		static MixFriends()
		{
			MixFriends.OnBadgesChanged = delegate
			{
			};
		}

		public static void Init(ILocalUser aUser)
		{
			user = aUser;
			foreach (IChatThread item in user.ChatThreadsFromUsers())
			{
				addChatListeners(item);
			}
			foreach (IIncomingFriendInvitation incomingFriendInvitation in user.IncomingFriendInvitations)
			{
				addInviteListeners(incomingFriendInvitation);
			}
			foreach (IOutgoingFriendInvitation outgoingFriendInvitation in user.OutgoingFriendInvitations)
			{
				addInviteListeners(outgoingFriendInvitation);
			}
			user.OnAddedToOneOnOneChatThread += delegate(object sender, AbstractAddedToOneOnOneChatThreadEventArgs e)
			{
				addChatListeners(e.ChatThread);
				MixFriends.OnBadgesChanged();
			};
			user.OnAddedToGroupChatThread += delegate(object sender, AbstractAddedToGroupChatThreadEventArgs e)
			{
				addChatListeners(e.ChatThread);
				MixFriends.OnBadgesChanged();
			};
			user.OnReceivedIncomingFriendInvitation += delegate(object sender, AbstractReceivedIncomingFriendInvitationEventArgs e)
			{
				addInviteListeners(e.Invitation);
				MixFriends.OnBadgesChanged();
			};
			user.OnReceivedOutgoingFriendInvitation += delegate(object sender, AbstractReceivedOutgoingFriendInvitationEventArgs e)
			{
				addInviteListeners(e.Invitation);
				MixFriends.OnBadgesChanged();
			};
			user.OnUnfriended += delegate
			{
				MixFriends.OnBadgesChanged();
			};
		}

		private static void removeInviteListeners(IIncomingFriendInvitation invite)
		{
			invite.OnAccepted -= CallBadgesChanged;
			invite.OnRejected -= CallBadgesChanged;
		}

		private static void removeInviteListeners(IOutgoingFriendInvitation invite)
		{
			invite.OnAccepted -= CallBadgesChanged;
			invite.OnRejected -= CallBadgesChanged;
		}

		private static void addInviteListeners(IIncomingFriendInvitation invite)
		{
			invite.OnAccepted += CallBadgesChanged;
			invite.OnRejected += CallBadgesChanged;
		}

		private static void addInviteListeners(IOutgoingFriendInvitation invite)
		{
			invite.OnAccepted += CallBadgesChanged;
			invite.OnRejected += CallBadgesChanged;
		}

		private static void CallBadgesChanged(object sender, EventArgs args)
		{
			try
			{
				MixFriends.OnBadgesChanged();
			}
			catch (Exception exception)
			{
				Log.Exception("Mix event handler threw an exception!", exception);
			}
		}

		private static void removeChatListeners(IChatThread thread)
		{
			if (thread is IOneOnOneChatThread)
			{
				((IOneOnOneChatThread)thread).OnGagMessageAdded -= CallBadgesChanged;
			}
			if (thread is IGroupChatThread)
			{
				((IGroupChatThread)thread).OnGagMessageAdded -= CallBadgesChanged;
			}
			thread.OnGameEventMessageAdded -= CallBadgesChanged;
			thread.OnGameStateMessageAdded -= CallBadgesChanged;
			thread.OnMemberAddedMessageAdded -= CallBadgesChanged;
			thread.OnMemberRemovedMessageAdded -= CallBadgesChanged;
			thread.OnPhotoMessageAdded -= CallBadgesChanged;
			thread.OnStickerMessageAdded -= CallBadgesChanged;
			thread.OnTextMessageAdded -= CallBadgesChanged;
			thread.OnVideoMessageAdded -= CallBadgesChanged;
		}

		private static void addChatListeners(IChatThread thread)
		{
			if (thread is IOneOnOneChatThread)
			{
				((IOneOnOneChatThread)thread).OnGagMessageAdded += CallBadgesChanged;
			}
			if (thread is IGroupChatThread)
			{
				((IGroupChatThread)thread).OnGagMessageAdded += CallBadgesChanged;
			}
			thread.OnGameEventMessageAdded += CallBadgesChanged;
			thread.OnGameStateMessageAdded += CallBadgesChanged;
			thread.OnMemberAddedMessageAdded += CallBadgesChanged;
			thread.OnMemberRemovedMessageAdded += CallBadgesChanged;
			thread.OnPhotoMessageAdded += CallBadgesChanged;
			thread.OnStickerMessageAdded += CallBadgesChanged;
			thread.OnTextMessageAdded += CallBadgesChanged;
			thread.OnVideoMessageAdded += CallBadgesChanged;
		}

		public static int GetMaxDisplayableFriendInvites()
		{
			int num = user.IncomingFriendInvitations.Count();
			if (num > 99)
			{
				num = 99;
			}
			return num;
		}
	}
}
