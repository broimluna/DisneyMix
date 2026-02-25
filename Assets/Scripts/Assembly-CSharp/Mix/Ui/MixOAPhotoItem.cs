using Disney.Mix.SDK;
using Mix.Assets;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class MixOAPhotoItem : MixBasePhotoItem
	{
		protected static GameObject prefab;

		private Text titleText;

		public MixOAPhotoItem(IChatThread aThread, IPhotoMessage aMessage, ScrollView aScrollView, IShowNotification aShowNotification)
			: base(aThread, aMessage, aScrollView, aShowNotification)
		{
			if (prefab == null)
			{
				prefab = Resources.Load<GameObject>("Prefabs/Screens/ChatMix/OA_PhotoItem");
			}
		}

		public override GameObject GenerateGameObject(bool aGenerateForHeightOnly)
		{
			base.instance = Object.Instantiate(prefab);
			SetupOffline("N/A", "Content/Holder/PostContent/ContentImage/ContextualLoader", "Content/Holder/PostContent/ContentImage/Error", "Content/Holder/PostContent/ContentImage/ForceUpdate");
			titleText = base.instance.transform.Find("Content/Holder/PostContent/Description/Text").GetComponent<Text>();
			titleText.text = photoMessage.Caption;
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
			photoThumb = base.instance.transform.Find("Content/Holder/PostContent/ContentImage/Mask/Image").GetComponent<Image>();
			float num = ((flavor.Width <= MixBasePhotoItem.MAX_WIDTH) ? 1f : ((float)MixBasePhotoItem.MAX_WIDTH / (float)flavor.Width));
			base.instance.transform.Find("Content/Holder/PostContent/ContentImage").GetComponent<LayoutElement>().minHeight = (float)flavor.Height * num;
			base.instance.transform.Find("Content/Holder/PostContent/ContentImage").GetComponent<LayoutElement>().preferredHeight = (float)flavor.Height * num;
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
			photoButton = base.instance.transform.Find("Content/Holder/PostContent/ContentImage/Mask/Image").GetComponent<Button>();
			photoButton.onClick.AddListener(base.OnPhotoButtonClicked);
			SaveMediaController controller = base.instance.GetComponent<SaveMediaController>();
			controller.OnPhotoSaved += base.OnPhotoSaved;
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
	}
}
