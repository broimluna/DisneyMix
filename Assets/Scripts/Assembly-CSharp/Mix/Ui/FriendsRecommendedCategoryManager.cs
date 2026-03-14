using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.Friends;
using Mix.Session;
using Mix.Session.Extensions;
using UnityEngine;

namespace Mix.Ui
{
	public class FriendsRecommendedCategoryManager : BaseFriendsCategoryManager, RecommendedFriendListItem.IRecommendedFriendListItem
	{
		public static int CATEGORY_ID = 2;

		private static int MAX_SHOWN_USERS = 8;

		public event EventHandler<FriendInvitedEventArgs> OnFriendInvited = delegate
		{
		};

		public FriendsRecommendedCategoryManager(string aName, GameObject aHeaderPrefab, GameObject aItemPrefab, ScrollView aScrollView)
			: base(aName, aHeaderPrefab, aItemPrefab, aScrollView, CATEGORY_ID)
		{
			if (Singleton<RecommendedFriends>.Instance.Users.Count() >= MAX_SHOWN_USERS)
			{
				return;
			}
			Singleton<RecommendedFriends>.Instance.GetRecommendedFriends(delegate(GetRecommendedFriendsResult result)
			{
				if (result != null && result.Status)
				{
					Refresh();
				}
			});
		}

		public override void Refresh()
		{
			if (scrollView.IsNullOrDisposed() || !MixSession.IsValidSession || Singleton<RecommendedFriends>.Instance.Users == null)
			{
				return;
			}
			foreach (IUnidentifiedUser user in Singleton<RecommendedFriends>.Instance.Users)
			{
				if (scrollListItems.Count < MAX_SHOWN_USERS)
				{
					CreateItem(user);
				}
			}
			List<BaseFriendsListItem> list = new List<BaseFriendsListItem>();
			BaseFriendsListItem item;
			foreach (BaseFriendsListItem scrollListItem in scrollListItems)
			{
				item = scrollListItem;
				if (!Singleton<RecommendedFriends>.Instance.Users.Any((IUnidentifiedUser friend) => friend.DisplayName.Text == (item as RecommendedFriendListItem).User.DisplayName.Text))
				{
					list.Add(item);
				}
			}
			list.ForEach(delegate(BaseFriendsListItem baseFriendsListItem)
			{
				RemoveItem((baseFriendsListItem as RecommendedFriendListItem).User.DisplayName.Text);
			});
		}

		public override int GetCount()
		{
			return (Singleton<RecommendedFriends>.Instance.Users.Count() <= MAX_SHOWN_USERS) ? Singleton<RecommendedFriends>.Instance.Users.Count() : MAX_SHOWN_USERS;
		}

		public void RemoveItem(string aDisplayName, bool aForceReload = false)
		{
			Singleton<RecommendedFriends>.Instance.RemoveRecommendedFriend(aDisplayName);
			RecommendedFriendListItem recommendedFriendListItem = (RecommendedFriendListItem)scrollListItems.FirstOrDefault((BaseFriendsListItem item) => item is RecommendedFriendListItem && ((RecommendedFriendListItem)item).User.DisplayName.Text == aDisplayName);
			if (recommendedFriendListItem != null)
			{
				scrollView.Remove(recommendedFriendListItem);
				scrollListItems.Remove(recommendedFriendListItem);
			}
			if (Singleton<RecommendedFriends>.Instance.Users.Count() == 0)
			{
				Singleton<RecommendedFriends>.Instance.GetRecommendedFriends(delegate(GetRecommendedFriendsResult result)
				{
					if (result != null && result.Status)
					{
						Refresh();
					}
				}, aForceReload);
			}
			else if (recommendedFriendListItem != null)
			{
				Refresh();
			}
			UpdateHeader();
		}

		public void CreateItem(IUnidentifiedUser aUser)
		{
			if (!visible)
			{
				UpdateHeader();
				return;
			}
			RecommendedFriendListItem recommendedFriendListItem = (RecommendedFriendListItem)scrollListItems.FirstOrDefault((BaseFriendsListItem item) => item is RecommendedFriendListItem && ((RecommendedFriendListItem)item).User.DisplayName.Text == aUser.DisplayName.Text);
			if (recommendedFriendListItem == null)
			{
				recommendedFriendListItem = new RecommendedFriendListItem(itemPrefab, aUser, this);
				scrollView.Add(recommendedFriendListItem, false);
				scrollListItems.Add(recommendedFriendListItem);
				UpdateHeader();
			}
		}

		public void OnRecommendedFriendListItemClicked(IUnidentifiedUser user)
		{
			if (user != null && !string.IsNullOrEmpty(user.DisplayName.Text))
			{
				if (!(Singleton<PanelManager>.Instance.FindPanel(typeof(FriendResultPanel)) != null))
				{
					FriendResultPanel friendResultPanel = (FriendResultPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.FRIEND_RESULT);
					friendResultPanel.Init(true, new InvitableUser(user), FriendResultPanel.Location.RecommendedFriend, FriendInvited);
					Analytics.LogFoundFriendPageView(user.DisplayName.Text, "recommended");
				}
			}
			else
			{
				GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
				genericPanel.ShowSimpleError("customtokens.global.generic_error");
			}
		}

		private void FriendInvited(IOutgoingFriendInvitation aInvite)
		{
			this.OnFriendInvited(this, new FriendInvitedEventArgs(aInvite));
		}
	}
}
