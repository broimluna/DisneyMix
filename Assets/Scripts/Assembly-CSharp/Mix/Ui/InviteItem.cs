using System;
using Avatar;
using Disney.Mix.SDK;
using Mix.Avatar;
using Mix.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class InviteItem : BaseFriendsListItem, IScrollItem, IScrollItemHelper
	{
		public interface IInviteListener
		{
			void OnAcceptClicked(IIncomingFriendInvitation invite, bool aWithTrust);

			void OnDeclineClicked(IIncomingFriendInvitation invite);
		}

		public const byte YF = 236;

		private GameObject item;

		public IIncomingFriendInvitation invite;

		private readonly IInviteListener currentListener;

		private GameObject instance;

		private Transform updateConfirmBar;

		private SdkEvents eventGenerator = new SdkEvents();

		private Action avatarCleanup;

		private Action avatarUpdateCleanup;

		private string primaryName;

		private bool isTrustUpgrade;

		public bool Trust { get; set; }

		public InviteItem(GameObject inviteItem, IIncomingFriendInvitation invite, bool aTrust, IInviteListener listener)
		{
			this.invite = invite;
			item = inviteItem;
			Trust = aTrust;
			currentListener = listener;
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(item);
			if (BaseFriendsListItem.GAMEOBJECT_HEIGHT < 1f)
			{
				BaseFriendsListItem.GAMEOBJECT_HEIGHT = gameObject.GetComponent<RectTransform>().sizeDelta.y;
			}
			if (aGenerateForHeightOnly)
			{
				return gameObject;
			}
			Init(gameObject);
			return gameObject;
		}

		void IScrollItem.Destroy()
		{
			if (MonoSingleton<AvatarManager>.Instance != null && avatarCleanup != null)
			{
				avatarCleanup();
			}
			if (avatarUpdateCleanup != null)
			{
				avatarUpdateCleanup();
			}
			MixSession.OnConnectionChanged -= HandleConnectionChange;
		}

		public void Init(GameObject aInst)
		{
			instance = aInst;
			if (string.IsNullOrEmpty(invite.Inviter.FirstName))
			{
				primaryName = invite.Inviter.DisplayName.Text;
				instance.transform.Find("NameHolder/NameDetails/DisplayNameText").gameObject.SetActive(false);
			}
			else
			{
				primaryName = invite.Inviter.FirstName;
				instance.transform.Find("NameHolder/NameDetails/DisplayNameText").GetComponent<Text>().text = MixChat.FormatDisplayName(invite.Inviter.DisplayName.Text);
			}
			instance.transform.Find("NameHolder/NameDetails/FriendNameText").GetComponent<Text>().text = primaryName;
			isTrustUpgrade = MixChat.FindFriendByDisplayName(invite.Inviter.DisplayName.Text) != null;
			Transform transform = instance.transform.Find("NameHolder/NameDetails/UpgradeOnlyText");
			if (isTrustUpgrade && transform != null)
			{
				transform.gameObject.SetActive(true);
			}
			else if (Trust)
			{
				instance.transform.Find("NameHolder/TrustIcon").gameObject.SetActive(true);
			}
			SkinnedMeshRenderer component = instance.transform.Find("AvatarImage/cube_rig/grp_mesh/avatar_cube").GetComponent<SkinnedMeshRenderer>();
			if (component != null)
			{
				Transform imageTarget = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(invite.Inviter.Avatar)) ? instance.transform.Find("AvatarImage/ImageTarget") : instance.transform.Find("AvatarImage/Mask/ImageTarget_Geo"));
				int imageTargetSize = (int)imageTarget.GetComponent<RectTransform>().rect.height;
				SnapshotCallback cb = delegate(bool success, Sprite sprite)
				{
					avatarCleanup = null;
					if (sprite != null && imageTarget != null && imageTarget.GetComponent<Image>() != null)
					{
						imageTarget.GetComponent<Image>().sprite = sprite;
						imageTarget.gameObject.SetActive(true);
					}
				};
				avatarCleanup = MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(invite.Inviter.Avatar, (AvatarFlags)0, imageTargetSize, cb);
				EventHandler<AbstractAvatarChangedEventArgs> listener = delegate(object sender, AbstractAvatarChangedEventArgs args)
				{
					Transform transform2 = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(args.Avatar)) ? instance.transform.Find("AvatarImage/ImageTarget") : instance.transform.Find("AvatarImage/Mask/ImageTarget_Geo"));
					if (!transform2.Equals(imageTarget))
					{
						imageTarget.gameObject.SetActive(false);
					}
					imageTarget = transform2;
					imageTargetSize = (int)imageTarget.GetComponent<RectTransform>().rect.height;
					if (avatarCleanup != null)
					{
						avatarCleanup();
					}
					if (imageTarget != null && MonoSingleton<AvatarManager>.Instance != null)
					{
						avatarCleanup = MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(args.Avatar, (AvatarFlags)0, imageTargetSize, cb);
					}
				};
				invite.Inviter.OnAvatarChanged += eventGenerator.AddEventHandler(invite.Inviter, listener);
				avatarUpdateCleanup = delegate
				{
					invite.Inviter.OnAvatarChanged -= eventGenerator.GetEventHandler(invite.Inviter, listener);
				};
			}
			instance.transform.Find("AcceptBtn").GetComponent<Button>().onClick.AddListener(onAcceptClicked);
			instance.transform.Find("DeclineBtn").GetComponent<Button>().onClick.AddListener(onDeclineClicked);
			instance.transform.Find("AcceptBtn").GetComponent<Button>().interactable = MixSession.connection == MixSession.ConnectionState.ONLINE;
			instance.transform.Find("DeclineBtn").GetComponent<Button>().interactable = MixSession.connection == MixSession.ConnectionState.ONLINE;
			MixSession.OnConnectionChanged += HandleConnectionChange;
			updateConfirmBar = instance.transform.Find("UpgradeConfirmBar");
		}

		public void HandleConnectionChange(MixSession.ConnectionState newState, MixSession.ConnectionState oldState)
		{
			if (!instance.IsNullOrDisposed())
			{
				instance.transform.Find("AcceptBtn").GetComponent<Button>().interactable = newState == MixSession.ConnectionState.ONLINE;
				instance.transform.Find("DeclineBtn").GetComponent<Button>().interactable = newState == MixSession.ConnectionState.ONLINE;
			}
		}

		public float GetGameObjectHeight()
		{
			return BaseFriendsListItem.GAMEOBJECT_HEIGHT;
		}

		public void ResetItem()
		{
			instance.transform.Find("AcceptBtn").GetComponent<Button>().onClick.RemoveAllListeners();
			instance.transform.Find("DeclineBtn").GetComponent<Button>().onClick.RemoveAllListeners();
			if (updateConfirmBar != null)
			{
				updateConfirmBar.Find("AcceptBtn").GetComponent<Button>().onClick.RemoveAllListeners();
				updateConfirmBar.Find("CancelBtn").GetComponent<Button>().onClick.RemoveAllListeners();
				updateConfirmBar.gameObject.SetActive(false);
			}
			Transform transform = instance.transform.Find("NameHolder/NameDetails/UpgradeOnlyText");
			if (transform != null)
			{
				transform.gameObject.SetActive(false);
			}
		}

		private void onAcceptClicked()
		{
			if (Trust && !isTrustUpgrade && instance != null)
			{
				updateConfirmBar.Find("DisplayNameText").GetComponent<Text>().text = primaryName;
				updateConfirmBar.Find("AcceptBtn").GetComponent<Button>().onClick.AddListener(delegate
				{
					currentListener.OnAcceptClicked(invite, true);
				});
				updateConfirmBar.Find("CancelBtn").GetComponent<Button>().onClick.AddListener(delegate
				{
					currentListener.OnAcceptClicked(invite, false);
				});
				updateConfirmBar.gameObject.SetActive(true);
			}
			else
			{
				currentListener.OnAcceptClicked(invite, true);
			}
			Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_2_SendDisney");
		}

		private void onDeclineClicked()
		{
			currentListener.OnDeclineClicked(invite);
		}
	}
}
