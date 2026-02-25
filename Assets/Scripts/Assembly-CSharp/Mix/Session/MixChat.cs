using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.Session.Extensions;

namespace Mix.Session
{
	public class MixChat
	{
		public static ILocalUser user;

		public static void Init(ILocalUser aUser)
		{
			user = aUser;
		}

		public static IFriend FindFriend(string id)
		{
			return user.Friends.FirstOrDefault((IFriend f) => f.Id == id);
		}

		public static IFriend FindFriendByDisplayName(string displayName)
		{
			return user.Friends.FirstOrDefault((IFriend f) => f.DisplayName.Text == displayName);
		}

		public static IChatThread FindThreadWithSameMembers(IEnumerable<IFriend> members)
		{
			bool flag = members.Count() == 1;
			if (!flag)
			{
				return null;
			}
			IEnumerable<string> members2 = members.Select((IFriend f) => f.Id);
			foreach (IOneOnOneChatThread oneOnOneChatThread in user.OneOnOneChatThreads)
			{
				if (oneOnOneChatThread.MemberListMatchesThread(members2) && flag != oneOnOneChatThread is IGroupChatThread)
				{
					return oneOnOneChatThread;
				}
			}
			return null;
		}

		public static bool IsFriendByDisplayName(IRemoteChatMember otherUser)
		{
			return user.Friends.Any((IFriend fr) => fr.DisplayName.Text == otherUser.DisplayName.Text);
		}

		public static bool IsFriendByDisplayName(IUnidentifiedUser otherUser)
		{
			return user.Friends.Any((IFriend fr) => fr.DisplayName.Text == otherUser.DisplayName.Text);
		}

		public static uint GetTotalUnreadMessageCount()
		{
			uint num = 0u;
			if (MixSession.IsValidSession)
			{
				foreach (IOneOnOneChatThread oneOnOneChatThread in user.OneOnOneChatThreads)
				{
					if (oneOnOneChatThread.ChatMessages.Count() > 0)
					{
						num += oneOnOneChatThread.UnreadMessageCount;
					}
				}
				foreach (IGroupChatThread groupChatThread in user.GroupChatThreads)
				{
					if (groupChatThread.ChatMessages.Count() > 0)
					{
						num += groupChatThread.UnreadMessageCount;
					}
				}
				foreach (IOfficialAccountChatThread officialAccountChatThread in user.OfficialAccountChatThreads)
				{
					if (!officialAccountChatThread.OfficialAccount.AccountId.Equals(FakeFriendManager.OAID) && officialAccountChatThread.ChatMessages.Count() > 0)
					{
						num += officialAccountChatThread.UnreadMessageCount;
					}
				}
			}
			return num;
		}

		public static uint GetTotalDisplayableUnreadMessageCount()
		{
			uint num = GetTotalUnreadMessageCount();
			if (num > 99)
			{
				num = 99u;
			}
			return num;
		}

		public static string FormatDisplayName(string displayName)
		{
			return "(" + displayName + ")";
		}
	}
}
