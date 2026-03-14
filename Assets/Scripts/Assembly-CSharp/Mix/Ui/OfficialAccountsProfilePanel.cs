using Disney.Mix.SDK;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.Connectivity;
using Mix.Data;
using Mix.Entitlements;
using Mix.Games;
using Mix.Session;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class OfficialAccountsProfilePanel : BasePanel, IBundleObject
	{
		public GameObject AddButton;

		public GameObject RemoveButton;

		public GameObject BackgroundHolder;

		public GameObject IconHolder;

		public Text Name;

		public Text Description;

		private SdkActions actionGenerator = new SdkActions();

		private IOfficialAccount account;

		private Official_Account accountData;

		private IOfficialAccountChatThread thread;

		private GameObject backgroundObject;

		private GameObject iconObject;

		void IBundleObject.OnBundleAssetObject(Object aGameObject, object aUserData)
		{
			if (aUserData == null || this.IsNullOrDisposed() || accountData == null)
			{
				return;
			}
			if (aUserData.Equals(BackgroundHolder))
			{
				backgroundObject = (GameObject)MonoSingleton<AssetManager>.Instance.GetBundleInstance(accountData.GetBackground());
				if (backgroundObject != null)
				{
					BackgroundHolder.GetComponent<Image>().color = Color.white;
					((GameObject)aUserData).GetComponent<Image>().sprite = backgroundObject.GetComponent<Image>().sprite;
					Object.Destroy(backgroundObject);
				}
			}
			else
			{
				iconObject = (GameObject)MonoSingleton<AssetManager>.Instance.GetBundleInstance(accountData.GetIcon());
				if (iconObject != null)
				{
					((GameObject)aUserData).GetComponent<Image>().sprite = iconObject.GetComponent<Image>().sprite;
					((GameObject)aUserData).GetComponent<Image>().enabled = true;
					Object.Destroy(iconObject);
				}
			}
		}

		public void Show(IOfficialAccount aAccount, IOfficialAccountChatThread aThread)
		{
			account = aAccount;
			thread = aThread;
			accountData = Singleton<EntitlementsManager>.Instance.GetOfficialAccount(account.AccountId);
			Color color = Util.HexToColor(accountData.GetTintColor1());
			color.a = 0.25f;
			BackgroundHolder.GetComponent<Image>().color = color;
			MonoSingleton<AssetManager>.Instance.LoadABundle(this, accountData.GetBackground(), BackgroundHolder, string.Empty);
			MonoSingleton<AssetManager>.Instance.LoadABundle(this, accountData.GetIcon(), IconHolder, string.Empty);
			Description.text = accountData.GetDescription();
			ToggleAddRemoveButtons();
			Name.text = account.DisplayName.Text;
			Util.UpdateTintablesForOfficialAccount(accountData, base.gameObject);
			if ((double)(MixConstants.CANVAS_HEIGHT / MixConstants.CANVAS_WIDTH) < 1.33)
			{
				RectTransform component = BackgroundHolder.GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(MixConstants.CANVAS_WIDTH, component.rect.height);
			}
			MixSession.OnConnectionChanged += OnConnectionChanged;
		}

		public void OnDestroy()
		{
			if (MonoSingleton<AssetManager>.Instance != null)
			{
				MonoSingleton<AssetManager>.Instance.CancelBundles(this);
				if (backgroundObject != null)
				{
					MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(accountData.GetBackground(), backgroundObject);
				}
				if (iconObject != null)
				{
					MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(accountData.GetIcon(), iconObject);
				}
			}
			MixSession.OnConnectionChanged -= OnConnectionChanged;
		}

		public void OnAddAccount()
		{
			AddButton.GetComponent<Button>().interactable = false;
			AddButton.transform.Find("ContextualLoader").gameObject.SetActive(true);
			MixSession.User.FollowOfficialAccount(account, actionGenerator.CreateAction(delegate(IFollowOfficialAccountResult aResult)
			{
				if (!this.IsNullOrDisposed())
				{
					AddButton.GetComponent<Button>().interactable = true;
					AddButton.transform.Find("ContextualLoader").gameObject.SetActive(false);
					if (aResult.Success)
					{
						Analytics.LogFollowOfficialAccount(account);
					}
					ClosePanel();
				}
			}));
		}

		public void OnRemoveAccount()
		{
			RemoveButton.GetComponent<Button>().interactable = false;
			RemoveButton.transform.Find("ContextualLoader").gameObject.SetActive(true);
			MixSession.User.UnfollowOfficialAccount(account, actionGenerator.CreateAction(delegate(IUnfollowOfficialAccountResult aResult)
			{
				if (!this.IsNullOrDisposed())
				{
					RemoveButton.GetComponent<Button>().interactable = true;
					RemoveButton.transform.Find("ContextualLoader").gameObject.SetActive(false);
					if (aResult.Success)
					{
						Analytics.LogUnFollowOfficialAccount(account);
						MonoSingleton<GameManager>.Instance.QuitGameSession();
						if (!MonoSingleton<ConnectionManager>.Instance.IsNullOrDisposed() && MonoSingleton<ConnectionManager>.Instance.IsConnected && thread != null)
						{
							thread.ClearUnreadMessageCount(actionGenerator.CreateAction<IClearUnreadMessageCountResult>(delegate
							{
							}));
						}
						NavigationRequest aRequest = new NavigationRequest("Prefabs/Screens/Conversations/ConversationsScreen", new TransitionSlideRight());
						MonoSingleton<NavigationManager>.Instance.AddRequest(aRequest);
					}
					ClosePanel();
				}
			}));
		}

		private void ToggleAddRemoveButtons()
		{
			if (!this.IsNullOrDisposed() && MixSession.IsValidSession && MixSession.User != null && MixSession.User.Followships != null)
			{
				if (MixSession.User.IsFollowingOfficialAccount(account.AccountId))
				{
					AddButton.SetActive(false);
					RemoveButton.SetActive(true);
					RemoveButton.GetComponent<Button>().interactable = MixSession.connection == MixSession.ConnectionState.ONLINE;
				}
				else
				{
					AddButton.SetActive(true);
					AddButton.GetComponent<Button>().interactable = MixSession.connection == MixSession.ConnectionState.ONLINE;
					RemoveButton.SetActive(false);
				}
			}
		}

		private void OnConnectionChanged(MixSession.ConnectionState newState, MixSession.ConnectionState oldState)
		{
			ToggleAddRemoveButtons();
		}
	}
}
