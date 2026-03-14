using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.Session;
using UnityEngine;

namespace Mix.Ui
{
	public class FriendsInviteCategoryManager : BaseFriendsCategoryManager, InviteItem.IInviteListener
	{
		private SdkActions actionGenerator = new SdkActions();

		public event EventHandler<InviteAcceptedEventArgs> OnInviteAccepted = delegate
		{
		};

		public FriendsInviteCategoryManager(string aName, GameObject aHeaderPrefab, GameObject aItemPrefab, ScrollView aScrollView)
			: base(aName, aHeaderPrefab, aItemPrefab, aScrollView)
		{
		}

		public override void Refresh()
		{
			if (scrollView.IsNullOrDisposed() || !MixSession.IsValidSession)
			{
				return;
			}
			foreach (IIncomingFriendInvitation incomingFriendInvitation in MixSession.User.IncomingFriendInvitations)
			{
				CreateItem(incomingFriendInvitation);
			}
			List<BaseFriendsListItem> list = new List<BaseFriendsListItem>();
			BaseFriendsListItem item;
			foreach (BaseFriendsListItem scrollListItem in scrollListItems)
			{
				item = scrollListItem;
				if (!MixSession.User.IncomingFriendInvitations.Any((IIncomingFriendInvitation invite) => invite.Id == (item as InviteItem).invite.Id))
				{
					list.Add(item);
				}
			}
			list.ForEach(delegate(BaseFriendsListItem baseFriendsListItem)
			{
				RemoveItem((baseFriendsListItem as InviteItem).invite);
			});
		}

		public override int GetCount()
		{
			return MixSession.User.IncomingFriendInvitations.Count();
		}

		public void RemoveItem(IIncomingFriendInvitation aInvite)
		{
			InviteItem inviteItem = (InviteItem)scrollListItems.FirstOrDefault((BaseFriendsListItem item) => item is InviteItem && ((InviteItem)item).invite.Id == aInvite.Id);
			if (inviteItem != null)
			{
				scrollView.Remove(inviteItem);
				scrollListItems.Remove(inviteItem);
			}
			UpdateHeader();
		}

		public void CreateItem(IIncomingFriendInvitation aInvite)
		{
			if (!visible)
			{
				UpdateHeader();
				return;
			}
			bool aTrust = MixSession.Session.LocalUser.AgeBandType != AgeBandType.Child && aInvite.RequestTrust;
			InviteItem inviteItem = (InviteItem)scrollListItems.FirstOrDefault((BaseFriendsListItem item) => item is InviteItem && ((InviteItem)item).invite.Id == aInvite.Id);
			if (inviteItem == null)
			{
				inviteItem = new InviteItem(itemPrefab, aInvite, aTrust, this);
				ScrollItem scrollItem = scrollView.ScrollItems.LastOrDefault((ScrollItem item) => item.Generator is InviteItem && ((InviteItem)item.Generator).invite.Inviter.DisplayName.Text.CompareTo(aInvite.Inviter.DisplayName.Text) < 0);
				if (scrollItem != null)
				{
					scrollView.AddAfter(scrollItem.Generator, inviteItem);
				}
				else if (scrollView.Get(header) != null)
				{
					scrollView.AddAfter(header, inviteItem);
				}
				else
				{
					scrollView.Add(inviteItem, true, false);
				}
				scrollListItems.Add(inviteItem);
				UpdateHeader();
			}
		}

		public void OnAcceptClicked(IIncomingFriendInvitation aInvite, bool aWithTrust)
		{
			bool flag = MixChat.IsFriendByDisplayName(aInvite.Inviter);
			bool flag2 = MixSession.Session.LocalUser.AgeBandType != AgeBandType.Child;
			if (aInvite.RequestTrust && flag2 && flag && !aWithTrust)
			{
				OnDeclineClicked(aInvite);
				return;
			}
			AcceptInvite(aInvite, aWithTrust);
			RemoveItem(aInvite);
		}

		public void OnDeclineClicked(IIncomingFriendInvitation aInvite)
		{
			IFriend friend = MixChat.FindFriendByDisplayName(aInvite.Inviter.DisplayName.Text);
			if (friend == null)
			{
				Analytics.LogDeclineNewFriendRequest(aInvite.Inviter.DisplayName.Text, aInvite.RequestTrust);
			}
			else if (aInvite.RequestTrust)
			{
				Analytics.LogDeclineTrustToExistingFriendship(friend.Id);
			}
			else
			{
				Analytics.LogDeclineNewFriendRequest(friend.Id, false);
			}
			MixSession.User.RejectFriendInvitation(aInvite, actionGenerator.CreateAction(delegate(IRejectFriendInvitationResult result)
			{
				if (!result.Success)
				{
					GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
					genericPanel.ShowSimpleError("customtokens.global.generic_error");
					CreateItem(aInvite);
				}
			}));
			RemoveItem(aInvite);
		}

		private void AcceptInvite(IIncomingFriendInvitation aInvite, bool aWithTrust)
		{
			bool isFriendsBefore = MixChat.IsFriendByDisplayName(aInvite.Inviter);
			MixSession.User.AcceptFriendInvitation(aInvite, aWithTrust, actionGenerator.CreateAction(delegate(IAcceptFriendInvitationResult result)
			{
				if (!result.Success)
				{
					GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
					genericPanel.ShowSimpleError("customtokens.global.generic_error");
					CreateItem(aInvite);
				}
				else
				{
					if (!isFriendsBefore)
					{
						Analytics.LogAcceptNewFriendRequest(result.Friend.Id, aWithTrust);
					}
					if (aWithTrust)
					{
						Analytics.LogAddTrustToExistingFriendship(result.Friend.Id);
					}
					else
					{
						Analytics.LogDeclineTrustToExistingFriendship(result.Friend.Id);
					}
					this.OnInviteAccepted(this, new InviteAcceptedEventArgs(result.Friend, aWithTrust));
				}
			}));
		}
	}
}
