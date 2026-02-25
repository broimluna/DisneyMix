using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.FakeFriend.Datatypes;
using Mix.Localization;
using Mix.Session.Local.Thread;

namespace Mix.Session.Extensions
{
	public static class MixChatThreadExtensions
	{
		public static bool Equals(this IChatThread mainThread, IChatThread other)
		{
			if (other == null)
			{
				return false;
			}
			if ((mainThread is FakeThread || other is FakeThread) && (!(mainThread is FakeThread) || !(other is FakeThread)))
			{
				return false;
			}
			if (other is LocalThread)
			{
				LocalThread localThread = other as LocalThread;
				if (localThread.realThread != null)
				{
					return mainThread.Equals(localThread.realThread);
				}
				if (mainThread is LocalThread)
				{
					IRemoteChatMember member;
					foreach (IRemoteChatMember remoteMember in other.RemoteMembers)
					{
						member = remoteMember;
						if (!mainThread.RemoteMembers.Any((IRemoteChatMember otherMember) => otherMember.IsSameUser(member)))
						{
							return false;
						}
					}
					return other.RemoteMembers.Count() == mainThread.RemoteMembers.Count();
				}
				return false;
			}
			if (!(mainThread is LocalThread))
			{
				return mainThread.Id == other.Id;
			}
			LocalThread localThread2 = mainThread as LocalThread;
			return localThread2.Id == other.Id;
		}

		public static bool IsFakeOrLocal(this IChatThread thread)
		{
			return (thread is LocalThread && ((LocalThread)thread).realThread == null) || thread is FakeThread;
		}

		public static int MemberCount(this IChatThread thread)
		{
			return thread.RemoteMembers.Count() + 1;
		}

		public static bool MemberListMatchesThread(this IChatThread thread, IEnumerable<string> members)
		{
			if (thread is IOfficialAccountChatThread)
			{
				return false;
			}
			int num = 0;
			foreach (string member in members)
			{
				if (thread.IsThreadMember(member))
				{
					num++;
				}
			}
			return num == members.Count() && members.Count() == thread.MemberCount() - 1;
		}

		public static bool IsOtherUserFriend(this IOneOnOneChatThread thread)
		{
			IRemoteChatMember otherUser = thread.GetOtherUser();
			return otherUser != null && otherUser.IsFriend();
		}

		public static IAvatarHolder GetOtherAvatarHolder(this IOneOnOneChatThread thread)
		{
			return new AvatarHolder(thread.GetOtherUser());
		}

		public static IRemoteChatMember GetOtherUser(this IOneOnOneChatThread thread)
		{
			if (thread.RemoteMembers.Count() == 1)
			{
				return thread.RemoteMembers.ElementAt(0);
			}
			if (thread.FormerRemoteMembers.Count() == 1)
			{
				return thread.FormerRemoteMembers.ElementAt(0);
			}
			Log.ExceptionOnce("GetOtherUser" + thread.Id, "Attempt to GetOtherUser from 1on1 thread" + thread.Id + ", with member count " + thread.RemoteMembers.Count() + " former count " + thread.FormerRemoteMembers.Count());
			return null;
		}

		public static string GetAnalyticsContext(this IChatThread thread)
		{
			if (thread is IGroupChatThread)
			{
				return "group_chat";
			}
			if (thread is IOneOnOneChatThread)
			{
				return "chat";
			}
			if (thread is IOfficialAccountChatThread)
			{
				return "oa_chat";
			}
			return "chat";
		}

		public static string GetAnalyticsId(this IChatThread thread)
		{
			if (!(thread is IOneOnOneChatThread))
			{
				return thread.Id;
			}
			if (thread is IOfficialAccountChatThread)
			{
				return ((IOfficialAccountChatThread)thread).OfficialAccount.AccountId;
			}
			IOneOnOneChatThread thread2 = thread as IOneOnOneChatThread;
			return (thread2.GetOtherUser() != null) ? thread2.GetOtherUser().Id : null;
		}

		public static bool IsNicknamed(this IChatThread thread)
		{
			if (thread is IOneOnOneChatThread && ((IOneOnOneChatThread)thread).Nickname != null && !string.IsNullOrEmpty(((IOneOnOneChatThread)thread).Nickname.Nickname))
			{
				return true;
			}
			if (thread is IGroupChatThread && ((IGroupChatThread)thread).Nickname != null && !string.IsNullOrEmpty(((IGroupChatThread)thread).Nickname.Nickname))
			{
				return true;
			}
			return false;
		}

		public static string GetThreadTitle(this IChatThread thread)
		{
			if (thread is IOneOnOneChatThread && ((IOneOnOneChatThread)thread).Nickname != null && !string.IsNullOrEmpty(((IOneOnOneChatThread)thread).Nickname.Nickname))
			{
				return ((IOneOnOneChatThread)thread).Nickname.Nickname;
			}
			if (thread is IGroupChatThread && ((IGroupChatThread)thread).Nickname != null && !string.IsNullOrEmpty(((IGroupChatThread)thread).Nickname.Nickname))
			{
				return ((IGroupChatThread)thread).Nickname.Nickname;
			}
			if (thread is IOneOnOneChatThread)
			{
				IOneOnOneChatThread thread2 = thread as IOneOnOneChatThread;
				IRemoteChatMember otherUser = thread2.GetOtherUser();
				if (otherUser == null)
				{
					return string.Empty;
				}
				return otherUser.NickFirstOrDisplayName();
			}
			if (thread is IOfficialAccountChatThread)
			{
				return ((IOfficialAccountChatThread)thread).OfficialAccount.DisplayName.Text;
			}
			string text = string.Empty;
			bool flag = true;
			foreach (IRemoteChatMember remoteMember in thread.RemoteMembers)
			{
				text = text + ((!flag) ? ", " : string.Empty) + remoteMember.NickFirstOrDisplayName();
				flag = false;
			}
			if (flag)
			{
				text = Singleton<Localizer>.Instance.getString("customtokens.chat.empty_thread_title");
			}
			return text;
		}

		public static IRemoteChatMember GetRemoteMemberById(this IChatThread thread, string id)
		{
			foreach (IRemoteChatMember remoteMember in thread.RemoteMembers)
			{
				if (remoteMember.Id == id)
				{
					return remoteMember;
				}
			}
			foreach (IRemoteChatMember formerRemoteMember in thread.FormerRemoteMembers)
			{
				if (formerRemoteMember.Id == id)
				{
					return formerRemoteMember;
				}
			}
			return null;
		}

		public static string GetNickOrDisplayById(this IChatThread thread, string id)
		{
			IFriend friend = MixChat.FindFriend(id);
			if (friend != null)
			{
				return friend.NickFirstOrDisplayName();
			}
			IRemoteChatMember remoteMemberById = thread.GetRemoteMemberById(id);
			if (remoteMemberById != null)
			{
				return remoteMemberById.NickFirstOrDisplayName();
			}
			if (MixChat.user.Id == id)
			{
				if (!string.IsNullOrEmpty(MixChat.user.FirstName))
				{
					return MixChat.user.FirstName;
				}
				return MixChat.user.DisplayName.Text;
			}
			return null;
		}

		public static IAvatarHolder GetAvatarHolderFromId(this IChatThread chatThread, string id)
		{
			IFriend friend = MixChat.FindFriend(id);
			if (friend == null)
			{
				IRemoteChatMember remoteMemberById = chatThread.GetRemoteMemberById(id);
				if (remoteMemberById == null)
				{
					if (id == MixChat.user.Id)
					{
						return new AvatarHolder(MixChat.user);
					}
					return null;
				}
				return new AvatarHolder(remoteMemberById);
			}
			return new AvatarHolder(friend);
		}

		public static IGagMessage SendGagMessage(this IChatThread thread, string contentId, string targetId, Action<ISendGagMessageResult> callback)
		{
			if (thread is IOneOnOneChatThread)
			{
				IOneOnOneChatThread oneOnOneChatThread = thread as IOneOnOneChatThread;
				IRemoteChatMember otherUser = oneOnOneChatThread.GetOtherUser();
				if (otherUser != null)
				{
					return oneOnOneChatThread.SendGagMessage(contentId, oneOnOneChatThread.GetOtherUser(), callback);
				}
				return null;
			}
			if (thread is IGroupChatThread)
			{
				IGroupChatThread groupChatThread = thread as IGroupChatThread;
				return groupChatThread.SendGagMessage(contentId, groupChatThread.GetRemoteMemberById(targetId), callback);
			}
			return null;
		}

		public static bool IsThreadMember(this IChatThread thread, string id)
		{
			return thread.RemoteMembers.Any((IRemoteChatMember f) => f.Id == id);
		}

		public static bool IsOrWasThreadMember(this IChatThread thread, string id)
		{
			return thread.IsThreadMember(id) || thread.FormerRemoteMembers.Any((IRemoteChatMember f) => f.Id == id);
		}

		public static bool ThreadRequiresParentalConsent(this IChatThread thread)
		{
			return MixSession.ParentalConsentRequired && !(thread is FakeThread);
		}

		public static IChatMessage GetNewest(this IChatThread thread)
		{
			return thread.ChatMessages.LastOrDefault();
		}
	}
}
