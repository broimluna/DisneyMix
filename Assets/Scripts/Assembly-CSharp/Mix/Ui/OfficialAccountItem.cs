using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.Data;
using Mix.DeviceDb;
using Mix.Entitlements;
using Mix.Session;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class OfficialAccountItem : IBundleObject
	{
		public class OfficialAccountItemComparer : IComparer<OfficialAccountItem>
		{
			public int Compare(OfficialAccountItem x, OfficialAccountItem y)
			{
				if (x != null && y != null)
				{
					if (x.GetIsFollowed() && !y.GetIsFollowed())
					{
						return -1;
					}
					if (!x.GetIsFollowed() && y.GetIsFollowed())
					{
						return 1;
					}
					if (!x.GetIsFollowed() && !y.GetIsFollowed())
					{
						Official_Account officialAccount = Singleton<EntitlementsManager>.Instance.GetOfficialAccount(x.account.AccountId);
						Official_Account officialAccount2 = Singleton<EntitlementsManager>.Instance.GetOfficialAccount(y.account.AccountId);
						if (officialAccount == null || officialAccount2 == null)
						{
							return 0;
						}
						return officialAccount.GetOrder().CompareTo(officialAccount2.GetOrder());
					}
					if (x.GetIsFollowed() && y.GetIsFollowed())
					{
						if (x.Message != null && y.Message != null)
						{
							return y.Message.TimeSent.CompareTo(x.Message.TimeSent);
						}
						if (x.Message != null || y.Message != null)
						{
							return int.MaxValue * ((y.Message != null) ? 1 : (-1));
						}
						return 0;
					}
				}
				return 0;
			}
		}

		private GameObject itemGO;

		private GameObject prefab;

		private IOfficialAccount account;

		private IChatThread thread;

		private IChatMessage message;

		private OfficialAccountsScroller scroller;

		private Action profilePanelClosed;

		private Official_Account accountData;

		private SdkEvents eventGenerator;

		private Button oaButton;

		private GameObject addIcon;

		private GameObject newMessageIcon;

		private Text unreadMessageCount;

		public GameObject ItemGameObject
		{
			get
			{
				return itemGO;
			}
		}

		public IChatMessage Message
		{
			get
			{
				return message;
			}
			set
			{
				message = value;
			}
		}

		public IChatThread Thread
		{
			get
			{
				return thread;
			}
			set
			{
				thread = value;
			}
		}

		public OfficialAccountItem(GameObject aPrefab, IOfficialAccount aAccount, IChatThread aThread, Action aProfilePanelClosed, OfficialAccountsScroller aScroller)
		{
			prefab = aPrefab;
			account = aAccount;
			thread = aThread;
			scroller = aScroller;
			if (thread != null)
			{
				message = thread.ChatMessages.LastOrDefault();
			}
			profilePanelClosed = aProfilePanelClosed;
			eventGenerator = new SdkEvents();
			accountData = Singleton<EntitlementsManager>.Instance.GetOfficialAccount(account.AccountId);
			if (accountData != null)
			{
				Singleton<MixDocumentCollections>.Instance.contentSeenDocumentCollectionApi.SetContentSeen(accountData.GetUid());
				itemGO = UnityEngine.Object.Instantiate(prefab);
				itemGO.SetActive(false);
				oaButton = itemGO.GetComponent<Button>();
				oaButton.onClick.AddListener(OnButtonClicked);
				MonoSingleton<AssetManager>.Instance.LoadABundle(this, accountData.GetThumb(), null, string.Empty);
				addIcon = itemGO.transform.Find("AddIcon").gameObject;
				addIcon.SetActive(!GetIsFollowed());
				MakeIconTranslucent(!GetIsFollowed());
				newMessageIcon = itemGO.transform.Find("NewMessageIcon").gameObject;
				unreadMessageCount = newMessageIcon.transform.Find("Icon/MessageCounterText").gameObject.GetComponent<Text>();
				UpdateUnreadCount();
				MixSession.User.OnAddedToOfficialAccountChatThread += eventGenerator.AddEventHandler<AbstractAddedToOfficialAccountThreadEventArgs>(null, OnAddedToOAThread);
				MixSession.User.OnOfficialAccountFollowed += eventGenerator.AddEventHandler<AbstractOfficialAccountFollowedEventArgs>(null, OnOAFollowed);
				MixSession.User.OnOfficialAccountUnfollowed += eventGenerator.AddEventHandler<AbstractOfficialAccountUnfollowedEventArgs>(null, OnOAUnfollowed);
			}
		}

		void IBundleObject.OnBundleAssetObject(UnityEngine.Object aGameObject, object aUserData)
		{
			Texture2D texture2D = (Texture2D)MonoSingleton<AssetManager>.Instance.GetBundleInstance(accountData.GetThumb());
			if (!(texture2D == null) && (!(oaButton == null) || texture2D.width <= 0))
			{
				Texture2D texture2D2 = texture2D;
				Sprite sprite = Sprite.Create(texture2D2, new Rect(0f, 0f, texture2D2.width, texture2D2.height), new Vector2(0f, 0f));
				oaButton.GetComponent<Image>().sprite = sprite;
				oaButton.GetComponent<Image>().enabled = true;
				itemGO.SetActive(true);
			}
		}

		public void OnButtonClicked()
		{
			if (GetIsFollowed())
			{
				foreach (IOfficialAccountChatThread officialAccountChatThread in MixSession.User.OfficialAccountChatThreads)
				{
					if (officialAccountChatThread.OfficialAccount.AccountId == account.AccountId)
					{
						ChatHelper.NavigateToChatScreen(officialAccountChatThread, new TransitionSlideLeft());
						break;
					}
				}
				return;
			}
			if (Singleton<PanelManager>.Instance.FindPanel(typeof(OfficialAccountsProfilePanel)) == null)
			{
				OfficialAccountsProfilePanel officialAccountsProfilePanel = (OfficialAccountsProfilePanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.OFFICIAL_ACCOUNT_PROFILE);
				officialAccountsProfilePanel.PanelClosingEvent += OnProfileClosed;
				officialAccountsProfilePanel.Show(account, (IOfficialAccountChatThread)thread);
			}
		}

		public void UpdateUnreadCount()
		{
			if (newMessageIcon == null)
			{
				return;
			}
			if (thread != null && GetIsFollowed())
			{
				uint num = thread.UnreadMessageCount;
				if (num != 0)
				{
					if (num > 99)
					{
						num = 99u;
					}
					unreadMessageCount.text = num.ToString();
					newMessageIcon.SetActive(true);
				}
				else
				{
					newMessageIcon.SetActive(false);
				}
			}
			else
			{
				newMessageIcon.SetActive(false);
			}
		}

		public void Destroy()
		{
			if (MixSession.IsValidSession && MixSession.User != null)
			{
				MixSession.User.OnAddedToOfficialAccountChatThread -= eventGenerator.GetEventHandler<AbstractAddedToOfficialAccountThreadEventArgs>(null, OnAddedToOAThread);
				MixSession.User.OnOfficialAccountFollowed -= eventGenerator.GetEventHandler<AbstractOfficialAccountFollowedEventArgs>(null, OnOAFollowed);
				MixSession.User.OnOfficialAccountUnfollowed -= eventGenerator.GetEventHandler<AbstractOfficialAccountUnfollowedEventArgs>(null, OnOAUnfollowed);
			}
		}

		private void OnProfileClosed(BasePanel aPanel)
		{
			if (addIcon != null)
			{
				addIcon.SetActive(!GetIsFollowed());
				MakeIconTranslucent(!GetIsFollowed());
			}
			profilePanelClosed();
			aPanel.PanelClosedEvent -= OnProfileClosed;
		}

		private void OnOAUnfollowed(object obj, AbstractOfficialAccountUnfollowedEventArgs args)
		{
			if (args.ExOfficialAccount.AccountId.Equals(account.AccountId) && addIcon != null)
			{
				RefreshItem();
			}
		}

		private void OnOAFollowed(object obj, AbstractOfficialAccountFollowedEventArgs args)
		{
			if (args.OfficialAccount.AccountId.Equals(account.AccountId) && addIcon != null)
			{
				RefreshItem();
			}
		}

		private void OnAddedToOAThread(object obj, AbstractAddedToOfficialAccountThreadEventArgs args)
		{
			if (args.ChatThread.OfficialAccount.AccountId.Equals(account.AccountId) && addIcon != null)
			{
				Thread = args.ChatThread;
				RefreshItem();
			}
		}

		private void RefreshItem()
		{
			addIcon.SetActive(!GetIsFollowed());
			MakeIconTranslucent(!GetIsFollowed());
			UpdateUnreadCount();
			scroller.RefreshScroller();
		}

		private bool GetIsFollowed()
		{
			bool result = false;
			if (MixSession.IsValidSession && MixSession.User.Followships != null)
			{
				result = MixSession.User.IsFollowingOfficialAccount(account.AccountId);
			}
			return result;
		}

		private void MakeIconTranslucent(bool doTranslucence)
		{
			if (oaButton != null)
			{
				Color color = oaButton.GetComponent<Image>().color;
				color.a = ((!doTranslucence) ? 1f : 0.3f);
				oaButton.GetComponent<Image>().color = color;
			}
		}
	}
}
