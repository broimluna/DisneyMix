using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.Connectivity;
using Mix.Entitlements;
using Mix.Session;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class OfficialAccountsScroller : IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IScrollItem
	{
		public delegate void OfficialAccountsLoadedCallback(bool didLoadOfficialAccounts);

		private List<OfficialAccountItem> accountItems = new List<OfficialAccountItem>();

		private Action<bool> toggleScroll;

		private GameObject officialAccountsScroller;

		private GameObject scrollerPrefab;

		private GameObject itemPrefab;

		private HorizontalLayoutGroup scrollerContent;

		private SdkActions actionGenerator = new SdkActions();

		private bool shouldGetOfficialAccountsFromServer;

		private IEnumerable<IOfficialAccount> availableOfficialAccounts;

		public OfficialAccountsScroller(GameObject aScrollerPrefab, GameObject aItemPrefab, Action<bool> aToggleScroll)
		{
			scrollerPrefab = aScrollerPrefab;
			itemPrefab = aItemPrefab;
			toggleScroll = aToggleScroll;
			shouldGetOfficialAccountsFromServer = true;
		}

		public void LoadOfficialAccounts(OfficialAccountsLoadedCallback callback)
		{
			availableOfficialAccounts = MixSession.User.AllOfficialAccounts.Where((IOfficialAccount account) => account != null && account.IsAvailable && Singleton<EntitlementsManager>.Instance.GetOfficialAccount(account.AccountId) != null && !account.AccountId.Equals(FakeFriendManager.OAID));
			if (availableOfficialAccounts.Count() > 0)
			{
				callback(true);
				Load(null);
			}
			else
			{
				Load(callback);
			}
		}

		private void Load(OfficialAccountsLoadedCallback callback)
		{
			if (MonoSingleton<ConnectionManager>.Instance.IsConnected && shouldGetOfficialAccountsFromServer)
			{
				shouldGetOfficialAccountsFromServer = false;
				MixSession.User.GetAllOfficialAccounts(actionGenerator.CreateAction(delegate(IGetAllOfficialAccountsResult result)
				{
					if (result.Success)
					{
						availableOfficialAccounts = MixSession.User.AllOfficialAccounts.Where((IOfficialAccount account) => account != null && account.IsAvailable && Singleton<EntitlementsManager>.Instance.GetOfficialAccount(account.AccountId) != null && !account.AccountId.Equals(FakeFriendManager.OAID));
						if (callback == null && availableOfficialAccounts.Count() != accountItems.Count())
						{
							PopulateScrollView(true);
						}
					}
					else
					{
						GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
						genericPanel.ShowSimpleError("customtokens.global.generic_error");
					}
					if (callback != null)
					{
						callback(result.Success);
					}
					shouldGetOfficialAccountsFromServer = true;
				}));
			}
			else if (callback != null)
			{
				callback(false);
			}
		}

		public GameObject GenerateGameObject(bool aGenerateForHeightOnly)
		{
			officialAccountsScroller = UnityEngine.Object.Instantiate(scrollerPrefab);
			scrollerContent = officialAccountsScroller.transform.Find("Content").GetComponent<HorizontalLayoutGroup>();
			if (aGenerateForHeightOnly)
			{
				return officialAccountsScroller;
			}
			PopulateScrollView(true);
			return officialAccountsScroller;
		}

		public void RefreshScroller(IChatThread aThread = null, IChatMessage aMessage = null)
		{
			if (aThread != null && aMessage != null)
			{
				foreach (OfficialAccountItem accountItem in accountItems)
				{
					if (accountItem.Thread != null && accountItem.Thread.Id == aThread.Id)
					{
						accountItem.Thread = aThread;
						accountItem.Message = aMessage;
						accountItem.UpdateUnreadCount();
					}
				}
			}
			PopulateScrollView(false);
		}

		public void OnProfilePanelClosed()
		{
			PopulateScrollView(false);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			toggleScroll(false);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			toggleScroll(true);
		}

		public void Destroy()
		{
			foreach (OfficialAccountItem accountItem in accountItems)
			{
				accountItem.Destroy();
			}
		}

		private void PopulateScrollView(bool createAccountItems)
		{
			if (createAccountItems)
			{
				accountItems.Clear();
				foreach (IOfficialAccount availableOfficialAccount in availableOfficialAccounts)
				{
					IChatThread aThread = null;
					if (MixSession.IsValidSession && MixSession.User != null)
					{
						foreach (IChatThread item2 in MixSession.User.ChatThreadsFromUsers())
						{
							if (item2 is IOfficialAccountChatThread && ((IOfficialAccountChatThread)item2).OfficialAccount.AccountId == availableOfficialAccount.AccountId)
							{
								aThread = item2;
							}
						}
					}
					OfficialAccountItem item = new OfficialAccountItem(itemPrefab, availableOfficialAccount, aThread, OnProfilePanelClosed, this);
					accountItems.Add(item);
				}
			}
			OfficialAccountItem.OfficialAccountItemComparer comparer = new OfficialAccountItem.OfficialAccountItemComparer();
			accountItems.Sort(comparer);
			if (!scrollerContent.IsNullOrDisposed())
			{
				scrollerContent.transform.DetachChildren();
				foreach (OfficialAccountItem accountItem in accountItems)
				{
					accountItem.ItemGameObject.transform.SetParent(scrollerContent.transform);
				}
			}
			Singleton<EntitlementsManager>.Instance.UpdateOfficialAccountNewStatus();
		}
	}
}
