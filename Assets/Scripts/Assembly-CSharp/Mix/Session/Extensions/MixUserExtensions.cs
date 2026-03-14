using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;

namespace Mix.Session.Extensions
{
	public static class MixUserExtensions
	{
		public static IEnumerable<IChatThread> ChatThreadsFromUsers(this ILocalUser user)
		{
			IEnumerable<IChatThread> first = user.OneOnOneChatThreads.Cast<IChatThread>();
			IEnumerable<IChatThread> second = user.GroupChatThreads.Cast<IChatThread>();
			IEnumerable<IChatThread> second2 = user.OfficialAccountChatThreads.Cast<IChatThread>();
			return first.Concat(second).Concat(second2);
		}

		public static bool IsFriend(this IRemoteChatMember aUser)
		{
			if (aUser == null)
			{
				return false;
			}
			return MixChat.user.Friends.Any((IFriend fr) => fr.Id == aUser.Id);
		}

		public static bool IsSameUser(this IRemoteChatMember aUser, IRemoteChatMember otherUser)
		{
			return aUser.Id == otherUser.Id;
		}

		public static bool IsSameUser(this IFriend aUser, IRemoteChatMember otherUser)
		{
			return aUser.Id == otherUser.Id;
		}

		public static bool IsSameUser(this IFriend aUser, IFriend otherUser)
		{
			return aUser.Id == otherUser.Id;
		}

		public static string NickFirstOrDisplayName(this IRemoteChatMember aUser)
		{
			if (aUser == null)
			{
				return string.Empty;
			}
			IFriend friend = MixChat.FindFriend(aUser.Id);
			if (friend != null && friend.Nickname != null && !string.IsNullOrEmpty(friend.Nickname.Text))
			{
				return friend.Nickname.Text;
			}
			if (string.IsNullOrEmpty(aUser.FirstName) && aUser.DisplayName != null)
			{
				return aUser.DisplayName.Text;
			}
			return aUser.FirstName;
		}

		public static string NickFirstOrDisplayName(this IFriend aUser)
		{
			if (aUser == null)
			{
				return string.Empty;
			}
			if (aUser.Nickname != null && !string.IsNullOrEmpty(aUser.Nickname.Text))
			{
				return aUser.Nickname.Text;
			}
			if (string.IsNullOrEmpty(aUser.FirstName) && aUser.DisplayName != null)
			{
				return aUser.DisplayName.Text;
			}
			return aUser.FirstName;
		}

		public static string NickOrFirstName(this IRemoteChatMember aUser)
		{
			if (aUser == null)
			{
				return string.Empty;
			}
			IFriend friend = MixChat.FindFriend(aUser.Id);
			if (friend != null && friend.Nickname != null && !string.IsNullOrEmpty(friend.Nickname.Text))
			{
				return friend.Nickname.Text;
			}
			return aUser.FirstName;
		}

		public static string NickOrFirstName(this IFriend aUser)
		{
			if (aUser == null)
			{
				return string.Empty;
			}
			if (aUser.Nickname != null && !string.IsNullOrEmpty(aUser.Nickname.Text))
			{
				return aUser.Nickname.Text;
			}
			return aUser.FirstName;
		}

		public static IOutgoingFriendInvitation SendFriendInvitation(this ILocalUser localUser, IInvitableUser user, bool requestTrust, Action<ISendFriendInvitationResult> callback)
		{
			if (user.userType == InvitableUser.UserType.remote)
			{
				return localUser.SendFriendInvitation(user.RemoteChatMember, requestTrust, callback);
			}
			return localUser.SendFriendInvitation(user.UnidentifiedUser, requestTrust, callback);
		}

		public static bool IsFollowingOfficialAccount(this ILocalUser localUser, string accountId)
		{
			if (localUser != null && localUser.Followships != null)
			{
				return localUser.Followships.Any((IOfficialAccount f) => f.AccountId == accountId);
			}
			return false;
		}
	}
}
