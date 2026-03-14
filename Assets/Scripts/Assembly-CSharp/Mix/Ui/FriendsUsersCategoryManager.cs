using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.FakeFriend.Datatypes;
using Mix.Session;
using Mix.Session.Extensions;
using Mix.Session.Local.Thread;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class FriendsUsersCategoryManager : BaseFriendsCategoryManager, IFriendListItem
	{
		private SdkActions actionGenerator = new SdkActions();

		private Vector2 scrollViewStartPos = Vector2.zero;

		public event EventHandler OnTrustInviteSent = delegate
		{
		};

		public event EventHandler OnHeaderUpdated = delegate
		{
		};

		public FriendsUsersCategoryManager(string aName, GameObject aHeaderPrefab, GameObject aItemPrefab, ScrollView aScrollView)
			: base(aName, aHeaderPrefab, aItemPrefab, aScrollView)
		{
			scrollViewStartPos = scrollView.GetComponent<RectTransform>().anchoredPosition;
		}

		public override void Refresh()
		{
			if (scrollView.IsNullOrDisposed() || !MixSession.IsValidSession)
			{
				return;
			}
			foreach (IFriend friend in MixSession.User.Friends)
			{
				CreateItem(friend);
			}
			List<BaseFriendsListItem> list = new List<BaseFriendsListItem>();
			foreach (BaseFriendsListItem scrollListItem in scrollListItems)
			{
				if (!MixSession.User.Friends.Contains((scrollListItem as FriendListItem).friend))
				{
					list.Add(scrollListItem);
				}
			}
			list.ForEach(delegate(BaseFriendsListItem item)
			{
				RemoveItem((item as FriendListItem).friend);
			});
		}

		public override int GetCount()
		{
			return MixSession.User.Friends.Count();
		}

		protected override void UpdateHeader()
		{
			base.UpdateHeader();
			this.OnHeaderUpdated(this, new EventArgs());
		}

		public void FixedUpdate()
		{
			scrollListItems.ForEach(delegate(BaseFriendsListItem item)
			{
				item.FixedUpdate();
			});
		}

		public void RemoveItem(IFriend aFriend)
		{
			FriendListItem friendListItem = (FriendListItem)scrollListItems.FirstOrDefault((BaseFriendsListItem item) => ((FriendListItem)item).friend.Id == aFriend.Id);
			if (friendListItem != null)
			{
				scrollView.Remove(friendListItem);
				scrollListItems.Remove(friendListItem);
			}
			UpdateHeader();
		}

		public void CreateItem(IFriend aFriend)
		{
			if (!visible)
			{
				UpdateHeader();
				return;
			}
			FriendListItem friendListItem = (FriendListItem)scrollListItems.FirstOrDefault((BaseFriendsListItem item) => ((FriendListItem)item).friend.Id == aFriend.Id);
			if (friendListItem != null)
			{
				friendListItem.SetTrust(aFriend.IsTrusted);
				return;
			}
			friendListItem = new FriendListItem(itemPrefab, aFriend, this);
			ScrollItem aBeforeItem = scrollView.ScrollItems.LastOrDefault((ScrollItem item) => Compare(item, aFriend.NickFirstOrDisplayName()));
			FinishAdding(aBeforeItem, friendListItem);
		}

		private void FinishAdding(ScrollItem aBeforeItem, BaseFriendsListItem aItem)
		{
			if (aBeforeItem != null)
			{
				scrollView.AddAfter(aBeforeItem.Generator, aItem as IScrollItem, true);
			}
			else if (scrollView.Get(header) != null)
			{
				scrollView.AddAfter(header, aItem as IScrollItem, true);
			}
			else
			{
				ScrollItem scrollItem = scrollView.ScrollItems.FirstOrDefault((ScrollItem item) => item.Generator is CategoryItem && (item.Generator as CategoryItem).CategoryId == FriendsRecommendedCategoryManager.CATEGORY_ID);
				if (scrollItem != null)
				{
					scrollView.AddBefore(scrollItem.Generator, aItem as IScrollItem, true);
				}
				else
				{
					scrollView.Add(aItem as IScrollItem, false);
				}
			}
			scrollListItems.Add(aItem);
			UpdateHeader();
		}

		private bool Compare(ScrollItem aItem, string aName)
		{
			if (!(aItem.Generator is FriendListItem))
			{
				return false;
			}
			return (aItem.Generator as BaseFriendsListItem).SortName.CompareTo(aName) < 0;
		}

		public void OnFriendListSendTrustInviteClicked(IFriend friend)
		{
			if (MixSession.User.OutgoingFriendInvitations.Any((IOutgoingFriendInvitation x) => x.Invitee.DisplayName.Text == friend.DisplayName.Text))
			{
				return;
			}
			MixSession.User.SendFriendInvitation(friend, true, actionGenerator.CreateAction(delegate(ISendFriendInvitationResult result)
			{
				if (result.Success)
				{
					this.OnTrustInviteSent(result.Invitation, new EventArgs());
				}
				else if (!(result is ISendFriendInvitationAlreadyExistsResult))
				{
					GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
					genericPanel.ShowSimpleError("customtokens.global.generic_error");
				}
			}));
		}

		public void OnFriendListRemoveClicked(IFriend friend)
		{
			MixSession.User.Unfriend(friend, actionGenerator.CreateAction(delegate(IUnfriendResult result)
			{
				if (result.Success)
				{
					UpdateHeader();
				}
				else
				{
					GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
					genericPanel.ShowSimpleError("customtokens.global.generic_error");
					CreateItem(friend);
				}
			}));
			RemoveItem(friend);
			Analytics.LogUnfriendAction(friend.Id);
		}

		public void OnFriendListItemClicked(IFriend friend)
		{
			if (!MixSession.IsValidSession)
			{
				return;
			}
			if (MixSession.ParentalConsentRequired && !(friend is FakeFriendData))
			{
				ParentalConsent.ShowGateDialog();
				return;
			}
			List<IFriend> list = new List<IFriend>();
			list.Add(friend);
			IChatThread chatThread = MixChat.FindThreadWithSameMembers(list);
			if (chatThread == null)
			{
				LocalThread aThread = new OneOnOneLocalThread(MixSession.User, list);
				ChatHelper.NavigateToChatScreen(aThread, new TransitionSlideLeft());
			}
			else
			{
				ChatHelper.NavigateToChatScreen(chatThread, new TransitionSlideLeft());
			}
		}

		public void OnToggleScroll(bool aState)
		{
			scrollView.GetComponent<ScrollRect>().vertical = aState;
		}

		public void OnScrollToListItem(RectTransform aTransform, int aHeight)
		{
			RectTransform component = scrollView.GetComponent<RectTransform>();
			if (aHeight > 0)
			{
				scrollView.GetComponent<ScrollRect>().vertical = false;
				Rect rectInPhysicalScreenSpace = Util.GetRectInPhysicalScreenSpace(aTransform);
				if ((float)Singleton<SettingsManager>.Instance.GetScreenHeight() - rectInPhysicalScreenSpace.y - rectInPhysicalScreenSpace.height < (float)aHeight / Singleton<SettingsManager>.Instance.GetHeightScale())
				{
					float num = (float)aHeight / Singleton<SettingsManager>.Instance.GetHeightScale() - ((float)Singleton<SettingsManager>.Instance.GetScreenHeight() - rectInPhysicalScreenSpace.y - rectInPhysicalScreenSpace.height);
					component.anchoredPosition = new Vector2(component.anchoredPosition.x, component.anchoredPosition.y + num * Singleton<SettingsManager>.Instance.GetHeightScale());
				}
			}
			else
			{
				scrollView.GetComponent<ScrollRect>().vertical = true;
				component.anchoredPosition = scrollViewStartPos;
			}
		}
	}
}
