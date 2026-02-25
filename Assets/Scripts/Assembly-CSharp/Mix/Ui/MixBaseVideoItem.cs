using System;
using System.Linq;
using Disney.Mix.SDK;
using Disney.Native;
using Mix.Assets;
using Mix.Connectivity;
using Mix.DeviceDb;
using Mix.Games;
using Mix.Native;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class MixBaseVideoItem : BaseChatItem, IScrollItem, IScrollItemHelper
	{
		protected static GameObject leftPrefab;

		protected static GameObject rightPrefab;

		protected static int MAX_IMAGE_WIDTH = 460;

		protected static int MAX_VIDEO_WIDTH = 640;

		protected GameObject currentPrefab;

		protected IVideoMessage videoMessage;

		protected IVideoFlavor flavor;

		protected Image videoThumb;

		protected LayoutElement videoThumbHolder;

		protected Button watchButton;

		protected Texture2D thumbTexture;

		protected string cacheKey;

		public SdkActions actionGenerator = new SdkActions();

		public MixBaseVideoItem(IChatThread aThread, IVideoMessage aMessage, ScrollView aScrollView)
			: base(aThread, aMessage, aScrollView)
		{
			videoMessage = aMessage;
			flavor = GetVideoFlavor(VideoFormat.Mp4);
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

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
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
			videoThumb = base.instance.transform.Find("Content/Holder/Mask/Thumbnail").GetComponent<Image>();
			if (aGenerateForHeightOnly)
			{
				return base.instance;
			}
			cacheKey = message.GetHashCode().ToString();
			if (MonoSingleton<AssetManager>.Instance.HasBundleInstance(cacheKey))
			{
				thumbTexture = (Texture2D)MonoSingleton<AssetManager>.Instance.GetBundleInstance(cacheKey);
				videoThumb.sprite = Sprite.Create(thumbTexture, new Rect(0f, 0f, thumbTexture.width, thumbTexture.height), Vector2.zero);
				LoaderObject.SetActive(false);
			}
			else
			{
				videoMessage.Thumbnail.GetFile(actionGenerator.CreateAction(delegate(IGetPhotoFlavorFileResult result)
				{
					if (result != null && result.Success && !base.instance.IsNullOrDisposed())
					{
						Texture2D texture2D = new Texture2D(4, 4, TextureFormat.ARGB4444, false);
						texture2D.LoadImage(result.File);
						MonoSingleton<AssetManager>.Instance.assetCache.Add(cacheKey, texture2D, texture2D.width * texture2D.height * 8, true);
						MonoSingleton<AssetManager>.Instance.AddBundleInstance(cacheKey, texture2D, 1);
						thumbTexture = (Texture2D)MonoSingleton<AssetManager>.Instance.GetBundleInstance(cacheKey);
						videoThumb.sprite = Sprite.Create(thumbTexture, new Rect(0f, 0f, thumbTexture.width, thumbTexture.height), Vector2.zero);
						LoaderObject.SetActive(false);
					}
					else
					{
						NetworkError();
					}
				}));
			}
			watchButton = base.instance.transform.Find("Content/Holder/Hit").GetComponent<Button>();
			watchButton.onClick.AddListener(OnWatchVideoButtonClicked);
			watchButton.interactable = MonoSingleton<ConnectionManager>.Instance.IsConnected;
			MonoSingleton<ConnectionManager>.Instance.ConnectedEvent += OnConnected;
			MonoSingleton<ConnectionManager>.Instance.ConnectedEvent += OnDisconnected;
			SetupAndSkinAvatar();
			return base.instance;
		}

		void IScrollItem.Destroy()
		{
			if (Equals(null))
			{
				return;
			}
			if (!base.instance.IsNullOrDisposed())
			{
				if (!MonoSingleton<AssetManager>.Instance.IsNullOrDisposed() && thumbTexture != null)
				{
					MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(cacheKey, thumbTexture);
				}
				if (base.instance.GetComponent<RectTransform>() != null)
				{
					SaveHeight(base.instance.GetComponent<RectTransform>().sizeDelta.y);
				}
			}
			OnDestroy();
			if (!MonoSingleton<ConnectionManager>.Instance.IsNullOrDisposed())
			{
				MonoSingleton<ConnectionManager>.Instance.ConnectedEvent -= OnConnected;
				MonoSingleton<ConnectionManager>.Instance.ConnectedEvent -= OnDisconnected;
			}
		}

		protected override void LoadObject()
		{
		}

		public override void UpdateClientMessage(IChatMessage aClientMessage)
		{
			message = aClientMessage;
		}

		protected void OnConnected()
		{
			if (watchButton != null)
			{
				watchButton.interactable = true;
			}
		}

		protected void OnDisconnected()
		{
			if (watchButton != null)
			{
				watchButton.interactable = false;
			}
		}

		protected void NetworkError()
		{
			if (LoaderObject != null && ErrorObject != null)
			{
				LoaderObject.SetActive(false);
				ErrorObject.SetActive(true);
			}
		}

		protected IVideoFlavor GetVideoFlavor(VideoFormat aFormat)
		{
			if (videoMessage.VideoFlavors.Select((IVideoFlavor result) => result.Format == aFormat).Count() <= 0)
			{
				return null;
			}
			IVideoFlavor videoFlavor = videoMessage.VideoFlavors.FirstOrDefault((IVideoFlavor result) => result.BitRate == 99999);
			if (videoFlavor != null)
			{
				return videoFlavor;
			}
			IVideoFlavor videoFlavor2 = null;
			foreach (IVideoFlavor videoFlavor3 in videoMessage.VideoFlavors)
			{
				if (videoFlavor3.Format == aFormat)
				{
					if (videoFlavor2 == null)
					{
						videoFlavor2 = videoFlavor3;
					}
					else if (videoFlavor3.Width > videoFlavor2.Width && videoFlavor3.Width < MAX_VIDEO_WIDTH)
					{
						videoFlavor2 = videoFlavor3;
					}
				}
			}
			return videoFlavor2;
		}

		protected void OnWatchVideoButtonClicked()
		{
			if (!MonoSingleton<ConnectionManager>.Instance.IsConnected)
			{
				return;
			}
			MonoSingleton<GameManager>.Instance.PauseGameSession();
			MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.Hide();
			watchButton.interactable = false;
			watchButton.transform.Find("Icon").gameObject.SetActive(false);
			watchButton.transform.Find("ContextualLoader").gameObject.SetActive(true);
			Analytics.LogOAVideoMessageClicked(thread, message);
			flavor.GetSource(actionGenerator.CreateAction(delegate(IGetVideoSourceResult result)
			{
				if (MonoSingleton<NavigationManager>.Instance.FindCurrentRequest() == null)
				{
					if (watchButton != null)
					{
						watchButton.interactable = true;
						watchButton.transform.Find("Icon").gameObject.SetActive(true);
						watchButton.transform.Find("ContextualLoader").gameObject.SetActive(false);
					}
					if (result.Success && !base.instance.IsNullOrDisposed())
					{
						if (MonoSingleton<NativeVideoPlaybackManager>.Instance.Native.IsVideoPlaying())
						{
							MonoSingleton<NativeVideoPlaybackManager>.Instance.Native.Unload();
						}
						string Url = string.Empty;
						if (result.Source is IFileVideoSource)
						{
							Url = ((IFileVideoSource)result.Source).Path;
						}
						else
						{
							if (!(result.Source is IUrlVideoSource))
							{
								return;
							}
							Url = ((IUrlVideoSource)result.Source).Url;
						}
						float num = Mathf.Min(320f / (float)flavor.Width, 320f / (float)flavor.Height);
						float num2 = (float)flavor.Width * num;
						float num3 = (float)flavor.Height * num;
						Rect playerSize = new Rect(0f, 0f, num2 / Singleton<SettingsManager>.Instance.GetWidthScale(), num3 / Singleton<SettingsManager>.Instance.GetHeightScale());
						playerSize.x = (MixConstants.CANVAS_WIDTH - 20f) / Singleton<SettingsManager>.Instance.GetWidthScale() - playerSize.width;
						playerSize.y = 40f / Singleton<SettingsManager>.Instance.GetHeightScale();
						GameObject original = Resources.Load<GameObject>("Prefabs/Screens/ChatMix/PictureInPicture_Screen");
						GameObject gameObject = UnityEngine.Object.Instantiate(original);
						Transform child = gameObject.transform.GetChild(0);
						gameObject.transform.SetParent(GameObject.Find("Persistent_Holder").transform, false);
						gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(playerSize.x * Singleton<SettingsManager>.Instance.GetWidthScale(), 0f - playerSize.y * Singleton<SettingsManager>.Instance.GetHeightScale());
						gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(playerSize.width * Singleton<SettingsManager>.Instance.GetWidthScale(), playerSize.height * Singleton<SettingsManager>.Instance.GetHeightScale());
						child.GetComponent<AnimationEvents>().OnAnimationEnd += delegate
						{
							DateTime startTime = DateTime.Now;
							MonoSingleton<NativeVideoPlaybackManager>.Instance.Native.Play(Url, playerSize, delegate
							{
								Analytics.LogOAVideoClosed(thread, message, (int)DateTime.Now.Subtract(startTime).Duration().TotalSeconds);
								if (result.Source is IFileVideoSource)
								{
									(result.Source as IFileVideoSource).FinishPlaying();
								}
							});
						};
						child.GetComponent<Animator>().Play("Open");
					}
				}
			}));
		}
	}
}
