using System;
using System.Collections;
using Avatar;
using Disney.Mix.SDK;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.Avatar;
using Mix.Data;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class GagSendTray : MonoBehaviour, IBundleObject
	{
		public Transform ContentHolder;

		public GameObject FriendGagItem;

		public Image GagThumb;

		public GameObject ToolTip;

		private IGagSendTray listener;

		private Gag gag;

		private IChatThread chatThread;

		private Action avatarCleanup;

		private Action avatarUpdateCleanup;

		private SdkEvents eventGenerator = new SdkEvents();

		void IBundleObject.OnBundleAssetObject(UnityEngine.Object aGameObject, object aUserData)
		{
			UnityEngine.Object bundleInstance = MonoSingleton<AssetManager>.Instance.GetBundleInstance(gag.GetThumb());
			if (bundleInstance is Texture2D)
			{
				Texture2D texture2D = (Texture2D)bundleInstance;
				Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0f, 0f));
				GagThumb.sprite = sprite;
				return;
			}
			GameObject gameObject = (GameObject)bundleInstance;
			if (gameObject != null)
			{
				GagThumb.sprite = gameObject.GetComponent<Image>().sprite;
			}
		}

		public void Init(IGagSendTray aListener, Gag aGag, IChatThread aThread)
		{
			listener = aListener;
			gag = aGag;
			chatThread = aThread;
			Analytics.LogGroupGagScreen(chatThread);
		}

		private void OnDestroy()
		{
			if (avatarCleanup != null && MonoSingleton<AvatarManager>.Instance != null)
			{
				avatarCleanup();
			}
			if (avatarUpdateCleanup != null)
			{
				avatarUpdateCleanup();
			}
		}

		public void UpdateState(bool aState)
		{
			base.gameObject.SetActive(aState);
			if (!aState)
			{
				return;
			}
			ToolTip.SetActive(false);
			for (int i = 0; i < ContentHolder.childCount; i++)
			{
				UnityEngine.Object.Destroy(ContentHolder.GetChild(i).gameObject);
			}
			MonoSingleton<AssetManager>.Instance.LoadABundle(this, gag.GetThumb(), new UnityEngine.Object(), string.Empty, false, false, true);
			foreach (IRemoteChatMember user in chatThread.RemoteMembers)
			{
				IRemoteChatMember friendUser = user;
				GameObject inst = UnityEngine.Object.Instantiate(FriendGagItem);
				inst.transform.Find("FriendNameText").GetComponent<Text>().text = friendUser.NickFirstOrDisplayName();
				inst.GetComponent<Button>().onClick.AddListener(delegate
				{
					OnItemClicked(friendUser);
				});
				Transform imageTarget = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(user.Avatar)) ? inst.transform.Find("AvatarImage/ImageTarget") : inst.transform.Find("AvatarImage/ImageTarget_Geo"));
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
				avatarCleanup = MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(friendUser.Avatar, (AvatarFlags)0, imageTargetSize, cb);
				EventHandler<AbstractAvatarChangedEventArgs> avatarUpdateListener = delegate(object sender, AbstractAvatarChangedEventArgs args)
				{
					if (!inst.IsNullOrDisposed())
					{
						Transform transform = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(args.Avatar)) ? inst.transform.Find("AvatarImage/ImageTarget") : inst.transform.Find("AvatarImage/ImageTarget_Geo"));
						if (!transform.Equals(imageTarget))
						{
							imageTarget.gameObject.SetActive(false);
						}
						imageTarget = transform;
						imageTargetSize = (int)imageTarget.GetComponent<RectTransform>().rect.height;
						if (avatarCleanup != null)
						{
							avatarCleanup();
						}
						if (imageTarget != null && MonoSingleton<AvatarManager>.Instance != null)
						{
							avatarCleanup = MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(args.Avatar, (AvatarFlags)0, imageTargetSize, cb);
						}
					}
				};
				user.OnAvatarChanged += eventGenerator.AddEventHandler(friendUser, avatarUpdateListener);
				avatarUpdateCleanup = delegate
				{
					user.OnAvatarChanged -= eventGenerator.GetEventHandler(friendUser, avatarUpdateListener);
				};
				inst.transform.SetParent(ContentHolder, false);
			}
		}

		private IEnumerator DelayedInvoke(float secs, Action call)
		{
			yield return new WaitForSeconds(secs);
			if (!this.IsNullOrDisposed() && call != null)
			{
				call();
			}
		}

		private void OnItemClicked(IRemoteChatMember targetUser)
		{
			listener.OnSendGroupGag(gag, targetUser);
		}
	}
}
