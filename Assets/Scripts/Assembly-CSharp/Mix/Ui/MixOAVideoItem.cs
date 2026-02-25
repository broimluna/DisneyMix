using Disney.Mix.SDK;
using Mix.Assets;
using Mix.Connectivity;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class MixOAVideoItem : MixBaseVideoItem, IScrollItem, IScrollItemHelper
	{
		private static GameObject prefab;

		private Text titleText;

		public MixOAVideoItem(IChatThread aThread, IVideoMessage aMessage, ScrollView aScrollView)
			: base(aThread, aMessage, aScrollView)
		{
			if (prefab == null)
			{
				prefab = Resources.Load<GameObject>("Prefabs/Screens/ChatMix/OA_VideoItem");
			}
			flavor = GetVideoFlavor(VideoFormat.Hls);
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			base.instance = Object.Instantiate(prefab);
			SetupOffline("N/A", "Content/Holder/PostContent/ContentImage/ContextualLoader", "Content/Holder/PostContent/ContentImage/Error", "Content/Holder/PostContent/ContentImage/ForceUpdate");
			titleText = base.instance.transform.Find("Content/Holder/PostContent/Description/Text").GetComponent<Text>();
			titleText.text = videoMessage.Caption;
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
			videoThumb = base.instance.transform.Find("Content/Holder/PostContent/ContentImage/Mask/Image").GetComponent<Image>();
			float num = ((videoMessage.Thumbnail.Width <= MixBaseVideoItem.MAX_IMAGE_WIDTH) ? 1f : ((float)MixBaseVideoItem.MAX_IMAGE_WIDTH / (float)videoMessage.Thumbnail.Width));
			videoThumbHolder = base.instance.transform.Find("Content/Holder/PostContent/ContentImage").GetComponent<LayoutElement>();
			videoThumbHolder.minHeight = (float)videoMessage.Thumbnail.Height * num;
			videoThumbHolder.preferredHeight = (float)videoMessage.Thumbnail.Height * num;
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
			watchButton = base.instance.transform.Find("Content/Holder/PostContent/ContentImage/Hit").GetComponent<Button>();
			watchButton.onClick.AddListener(base.OnWatchVideoButtonClicked);
			watchButton.interactable = MonoSingleton<ConnectionManager>.Instance.IsConnected;
			MonoSingleton<ConnectionManager>.Instance.ConnectedEvent += base.OnConnected;
			MonoSingleton<ConnectionManager>.Instance.ConnectedEvent += base.OnDisconnected;
			SetupAndSkinAvatar();
			return base.instance;
		}
	}
}
