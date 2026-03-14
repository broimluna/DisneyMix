using System;
using Disney.Mix.SDK;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.Avatar;
using Mix.Data;
using Mix.Entitlements;
using Mix.Games;
using Mix.Session.Extensions;
using UnityEngine;

namespace Mix.Ui
{
	public class MixGagItem : BaseChatItem, IBundleObject, IScrollItem, IScrollItemHelper
	{
		private static GameObject leftPrefab;

		private static GameObject rightPrefab;

		public bool hasAutoPlayed;

		public bool hasBeenSeen;

		public Transform gagPresentImage;

		private bool isNewMessage;

		private Action cancelSnapshotSender;

		private Action cancelSnapshotReceiver;

		public MixGagItem(IChatThread aThread, IChatMessage aMessage, ScrollView aScrollView, bool aIsNewMessage)
			: base(aThread, aMessage, aScrollView)
		{
			hasAutoPlayed = false;
			isNewMessage = aIsNewMessage;
			if (message.IsMine() && rightPrefab == null)
			{
				rightPrefab = Resources.Load<GameObject>("Prefabs/Screens/ChatMix/GagHolderRight");
			}
			else if (leftPrefab == null)
			{
				leftPrefab = Resources.Load<GameObject>("Prefabs/Screens/ChatMix/GagHolderLeft");
			}
		}

		float IScrollItemHelper.GetGameObjectHeight()
		{
			return BaseChatItem.GAG_PREFAB_HEIGHT;
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			base.instance = UnityEngine.Object.Instantiate((!message.IsMine()) ? leftPrefab : rightPrefab);
			if (aGenerateForHeightOnly)
			{
				return base.instance;
			}
			gagPresentImage = base.instance.transform.Find("Content/GagHolder/GagImage");
			gagPresentImage.gameObject.SetActive(false);
			SetupOffline("Content/GagHolder/ResendBtn", "Content/GagHolder/ContextualLoader", "Content/GagHolder/Error", "Content/GagHolder/ForceUpdate");
			IGagMessage gagMessage = message as IGagMessage;
			Gag gagData = Singleton<EntitlementsManager>.Instance.GetGagData(gagMessage.ContentId);
			if (gagData == null)
			{
				OnForceUpdateDetected(gagMessage.ContentId);
				return base.instance;
			}
			if (!aGenerateForHeightOnly)
			{
				if (MonoSingleton<AssetManager>.Instance.WillBundleLoadFromWeb(gagData.GetHd()) || MonoSingleton<AssetManager>.Instance.WillBundleLoadFromWeb(gagData.GetThumb()))
				{
					LoaderObject.SetActive(true);
				}
				CreateGagItem(false);
				MonoSingleton<AssetManager>.Instance.LoadABundle(this, gagData.GetHd(), null, string.Empty, false, false, true);
			}
			if (base.instance.GetComponent<RectTransform>().sizeDelta.y.CompareTo(BaseChatItem.GAG_PREFAB_HEIGHT) != 0)
			{
			}
			return base.instance;
		}

		void IScrollItem.Destroy()
		{
			if (cancelSnapshotSender != null)
			{
				cancelSnapshotSender();
			}
			if (cancelSnapshotReceiver != null)
			{
				cancelSnapshotReceiver();
			}
		}

		public override void UpdateClientMessage(IChatMessage aMessage)
		{
			message = aMessage;
			if (!base.instance.IsNullOrDisposed() && message.IsMine())
			{
				ResendObject.SetActive(FailedToSend);
			}
		}

		protected override void OnResendClicked()
		{
			ResendObject.SetActive(false);
			ResendFailedMessage(message);
		}

		public void OnBundleAssetObject(UnityEngine.Object aGameObject, object aUserData)
		{
			try
			{
				if (this != null && !LoaderObject.IsNullOrDisposed())
				{
					if (base.instance.IsNullOrDisposed() || aGameObject.IsNullOrDisposed())
					{
						ErrorObject.SetActive(true);
					}
					else
					{
						LoaderObject.SetActive(false);
					}
				}
			}
			catch (Exception exception)
			{
				Log.Exception(string.Empty, exception);
			}
		}

		private void CreateGagItem(bool aGenerateForHeightOnly)
		{
			IGagMessage gagMessage = message as IGagMessage;
			Gag gagData = Singleton<EntitlementsManager>.Instance.GetGagData(gagMessage.ContentId);
			GameObject gameObject = base.instance.transform.Find("Content/GagHolder").gameObject;
			MixGagController component = gameObject.GetComponent<MixGagController>();
			component.chatItem = this;
			if (gagMessage != null && isNewMessage && !hasAutoPlayed && gagData.IsAutoPlay())
			{
				if (!MonoSingleton<GameManager>.Instance.IsToastPanelActive)
				{
					component.AutoPlayGag();
				}
				hasAutoPlayed = true;
			}
			if (thread is IGroupChatThread && !aGenerateForHeightOnly)
			{
				GameObject gameObject2 = base.instance.transform.Find(message.IsMine() ? "Content/PlaceholderAvatarRight" : "Content/PlaceholderAvatarLeft").gameObject;
				gameObject2.SetActive(true);
				MonoSingleton<AvatarManager>.Instance.RenderAvatarSnapshotWithCancel(thread.GetAvatarHolderFromId(message.SenderId), gameObject2, "AvatarHeadRight/ImageTarget", cancelSnapshotSender);
				MonoSingleton<AvatarManager>.Instance.RenderAvatarSnapshotWithCancel(thread.GetAvatarHolderFromId(gagMessage.TargetUserId), gameObject2, "RecipientAvatar/Avatar/ImageTarget", cancelSnapshotReceiver);
			}
			else if (!aGenerateForHeightOnly)
			{
				base.instance.transform.Find(message.IsMine() ? "Content/PlaceholderAvatarRight" : "Content/PlaceholderAvatarLeft").gameObject.SetActive(false);
			}
		}

		protected override void LoadObject()
		{
			CreateGagItem(false);
		}
	}
}
