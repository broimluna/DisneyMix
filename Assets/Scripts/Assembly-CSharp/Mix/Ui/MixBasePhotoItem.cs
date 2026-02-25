using System;
using System.Linq;
using Disney.Mix.SDK;
using Mix.Assets;
using Mix.Connectivity;
using Mix.DeviceDb;
using Mix.Games;
using Mix.Localization;
using Mix.Native;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class MixBasePhotoItem : BaseChatItem, IScrollItem, IScrollItemHelper
	{
		protected static GameObject leftPrefab;

		protected static GameObject rightPrefab;

		protected static int MAX_WIDTH = 460;

		protected GameObject currentPrefab;

		protected IPhotoMessage photoMessage;

		protected IPhotoFlavor flavor;

		protected Image photoThumb;

		protected Button photoButton;

		protected Texture2D photoTexture;

		protected string cacheKey;

		protected IShowNotification showNotification;

		protected SdkActions actionGenerator = new SdkActions();

		public MixBasePhotoItem(IChatThread aThread, IPhotoMessage aMessage, ScrollView aScrollView, IShowNotification aShowNotification)
			: base(aThread, aMessage, aScrollView)
		{
			photoMessage = aMessage;
			showNotification = aShowNotification;
			flavor = GetPhotoFlavor();
			if (aMessage.IsMine() && rightPrefab == null)
			{
				rightPrefab = Resources.Load<GameObject>("Prefabs/Screens/ChatMix/UGCItemRight");
			}
			else if (!aMessage.IsMine() && leftPrefab == null)
			{
				leftPrefab = Resources.Load<GameObject>("Prefabs/Screens/ChatMix/UGCItemLeft");
			}
			currentPrefab = ((!aMessage.IsMine()) ? leftPrefab : rightPrefab);
		}

		float IScrollItemHelper.GetGameObjectHeight()
		{
			float height = Singleton<MixDocumentCollections>.Instance.chatMetaDataDocumentCollectionApi.GetHeight(message.Id);
			return (!(height > 0f)) ? (-1f) : height;
		}

		void IScrollItem.Destroy()
		{
			if (Equals(null))
			{
				return;
			}
			if (!base.instance.IsNullOrDisposed())
			{
				if (!MonoSingleton<AssetManager>.Instance.IsNullOrDisposed() && photoTexture != null)
				{
					MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(cacheKey, photoTexture);
				}
				if (base.instance.GetComponent<RectTransform>() != null)
				{
					SaveHeight(base.instance.GetComponent<RectTransform>().sizeDelta.y);
				}
			}
			OnDestroy();
		}

		protected override void LoadObject()
		{
		}

		public override void UpdateClientMessage(IChatMessage aClientMessage)
		{
			message = aClientMessage;
		}

		public virtual GameObject GenerateGameObject(bool aGenerateForHeightOnly)
		{
			base.instance = UnityEngine.Object.Instantiate(currentPrefab);
			SetupOffline("N/A", "Content/Holder/ContextualLoader", "Content/Holder/Error", string.Empty);
			float minHeight = base.instance.transform.Find("Content/Holder").GetComponent<LayoutElement>().minHeight;
			RectTransform component = base.instance.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component.sizeDelta.x, minHeight + avatarNameHeight);
			if (flavor == null)
			{
				ErrorObject.SetActive(true);
				LoaderObject.SetActive(false);
				if (!aGenerateForHeightOnly)
				{
					SetupAndSkinAvatar();
				}
				return base.instance;
			}
			photoThumb = base.instance.transform.Find("Content/Holder/Mask/Thumbnail").GetComponent<Image>();
			base.instance.transform.Find("Content/Holder/Hit").gameObject.SetActive(false);
			if (aGenerateForHeightOnly)
			{
				return base.instance;
			}
			cacheKey = message.GetHashCode().ToString();
			if (MonoSingleton<AssetManager>.Instance.HasBundleInstance(cacheKey))
			{
				photoTexture = (Texture2D)MonoSingleton<AssetManager>.Instance.GetBundleInstance(cacheKey);
				photoThumb.sprite = Sprite.Create(photoTexture, new Rect(0f, 0f, photoTexture.width, photoTexture.height), Vector2.zero);
				LoaderObject.SetActive(false);
			}
			else
			{
				flavor.GetFile(actionGenerator.CreateAction(delegate(IGetPhotoFlavorFileResult result)
				{
					if (result != null && result.Success && !base.instance.IsNullOrDisposed())
					{
						Texture2D texture2D = new Texture2D(4, 4, TextureFormat.ARGB4444, false);
						texture2D.LoadImage(result.File);
						MonoSingleton<AssetManager>.Instance.assetCache.Add(cacheKey, texture2D, texture2D.width * texture2D.height * 8, true);
						MonoSingleton<AssetManager>.Instance.AddBundleInstance(cacheKey, texture2D, 1);
						photoTexture = (Texture2D)MonoSingleton<AssetManager>.Instance.GetBundleInstance(cacheKey);
						photoThumb.sprite = Sprite.Create(photoTexture, new Rect(0f, 0f, photoTexture.width, photoTexture.height), Vector2.zero);
						LoaderObject.SetActive(false);
					}
					else
					{
						NetworkError();
					}
				}));
			}
			photoButton = base.instance.transform.Find("Content/Holder/Mask").GetComponent<Button>();
			photoButton.onClick.AddListener(OnPhotoButtonClicked);
			SaveMediaController controller = base.instance.GetComponent<SaveMediaController>();
			controller.OnPhotoSaved += OnPhotoSaved;
			controller.SaveButton.onClick.AddListener(delegate
			{
				if (photoThumb.sprite != null && photoThumb.sprite.texture != null)
				{
					controller.OnSavePhotoClicked(photoThumb.sprite.texture);
				}
			});
			SetupAndSkinAvatar();
			return base.instance;
		}

		protected void NetworkError()
		{
			if (LoaderObject != null && ErrorObject != null)
			{
				LoaderObject.SetActive(false);
				ErrorObject.SetActive(true);
			}
		}

		protected IPhotoFlavor GetPhotoFlavor()
		{
			if (photoMessage.PhotoFlavors.Select((IPhotoFlavor result) => result.Encoding != PhotoEncoding.Gif).Count() <= 0)
			{
				return null;
			}
			IPhotoFlavor photoFlavor = photoMessage.PhotoFlavors.FirstOrDefault((IPhotoFlavor result) => result.Width < MAX_WIDTH && result.Encoding != PhotoEncoding.Gif);
			bool flag = photoFlavor == null;
			foreach (IPhotoFlavor photoFlavor2 in photoMessage.PhotoFlavors)
			{
				if (photoFlavor2.Encoding == PhotoEncoding.Gif)
				{
					continue;
				}
				if (flag)
				{
					if (photoFlavor == null || photoFlavor2.Width < photoFlavor.Width)
					{
						photoFlavor = photoFlavor2;
					}
				}
				else if (photoFlavor == null)
				{
					photoFlavor = photoFlavor2;
				}
				else if (photoFlavor2.Width > MAX_WIDTH && photoFlavor2.Width < photoFlavor.Width)
				{
					photoFlavor = photoFlavor2;
				}
			}
			return photoFlavor;
		}

		protected void OnPhotoButtonClicked()
		{
			if (base.instance.IsNullOrDisposed() || photoThumb == null || photoThumb.sprite == null || base.instance.GetComponent<SaveMediaController>().ContextMenu.activeSelf || (!MonoSingleton<ConnectionManager>.Instance.IsConnected && photoThumb.sprite == null))
			{
				return;
			}
			MonoSingleton<GameManager>.Instance.PauseGameSession();
			MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.Hide();
			Analytics.LogOAPhotoMessageClicked(thread, message);
			FullScreenPicturePanel panel = (FullScreenPicturePanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.FULL_SCREEN_MEDIA);
			panel.Init(Sprite.Create(photoThumb.sprite.texture, new Rect(0f, 0f, photoThumb.sprite.texture.width, photoThumb.sprite.texture.height), Vector2.zero));
			base.instance.GetComponent<SaveMediaController>().AllowShowingOfMenu = false;
			panel.MediaController.OnPhotoSaved += OnPhotoSaved;
			panel.MediaController.SaveButton.onClick.AddListener(delegate
			{
				if (photoThumb.sprite != null && photoThumb.sprite.texture != null)
				{
					panel.MediaController.OnSavePhotoClicked(photoThumb.sprite.texture);
				}
			});
			panel.PanelClosedEvent += OnFullScreenPhotoClosed;
		}

		protected void OnFullScreenPhotoClosed(BasePanel aPanel)
		{
			base.instance.GetComponent<SaveMediaController>().AllowShowingOfMenu = true;
			aPanel.PanelClosedEvent -= OnFullScreenPhotoClosed;
		}

		protected void OnPhotoSaved(object obj, EventArgs args)
		{
			if (showNotification != null)
			{
				FullScreenPicturePanel fullScreenPicturePanel = (FullScreenPicturePanel)Singleton<PanelManager>.Instance.FindPanel(typeof(FullScreenPicturePanel));
				if (fullScreenPicturePanel != null)
				{
					fullScreenPicturePanel.ShowNotification(Singleton<Localizer>.Instance.getString("customtokens.media.photo_saved"));
				}
				else
				{
					showNotification.OnShowNotification(Singleton<Localizer>.Instance.getString("customtokens.media.photo_saved"));
				}
			}
		}
	}
}
