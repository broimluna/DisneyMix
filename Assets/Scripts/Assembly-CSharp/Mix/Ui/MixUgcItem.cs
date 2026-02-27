using System.Collections.Generic;
using System.IO;
using Avatar;
using Disney.Mix.SDK;
using Mix.Assets;
using Mix.Avatar;
using Mix.DeviceDb;
using Mix.Native;
using Mix.Session;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class MixUgcItem : BaseChatItem, IPNGAssetObject, IVideoAssetObject, IScrollItem
	{
		private static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

		private static float AVATAR_OFFSET = 100f;

		private bool trusted;

		private bool mediaIsVideo;

		private string filePath;

		private string fileName;

		private string currentPrefab;

		private float messageHeight;

		private float messageWidth;

		private GameObject loader;

		private GameObject inst;

		private float imageScale;

		private float maxSize = 400f;

		private string cacheUrl = string.Empty;

		public MixUgcItem(IChatThread aThread, IChatMessage aMessage, ScrollView aScrollView)
			: base(aThread, aMessage, aScrollView)
		{
			mediaIsVideo = aMessage is IVideoMessage;
			fileName = "0";
			messageHeight = 100f;
			messageWidth = 100f;
			filePath = Application.PersistentDataPath + "/" + fileName;
			IFriend friend = MixChat.FindFriend(aMessage.SenderId);
			if (aMessage.IsMine() || (friend != null && friend.IsTrusted))
			{
				trusted = true;
			}
			if (messageWidth > messageHeight)
			{
				imageScale = maxSize / messageWidth;
			}
			else
			{
				imageScale = maxSize / messageHeight;
			}
			if (messageHeight > 0f)
			{
				messageHeight *= imageScale;
				messageWidth *= imageScale;
			}
			string text = ((!aMessage.IsMine()) ? "HolderLeft" : "HolderRight");
			currentPrefab = "Prefabs/Screens/ChatMix/" + ((!mediaIsVideo) ? "Picture" : "Video") + text;
			if (!prefabs.ContainsKey(currentPrefab) || prefabs[currentPrefab] == null)
			{
				prefabs[currentPrefab] = Resources.Load<GameObject>(currentPrefab);
			}
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			inst = Object.Instantiate(prefabs[currentPrefab]);
			RectTransform component = inst.GetComponent<RectTransform>();
			float height = Singleton<MixDocumentCollections>.Instance.chatMetaDataDocumentCollectionApi.GetHeight(message.Id);
			float num = ((!(height > 0f)) ? messageHeight : height);
			if (num > 0f)
			{
				RectTransform component2 = inst.GetComponent<RectTransform>();
				component2.sizeDelta = new Vector2(component2.sizeDelta.x, num);
				if (aGenerateForHeightOnly)
				{
					return inst;
				}
			}
			loader = inst.transform.Find("Content/MediaTarget/ContextualLoader").gameObject;
			inst.transform.Find("Content/MediaTarget").GetComponent<Button>().onClick.AddListener(OnClicked);
			component.anchoredPosition = new Vector2((!message.IsMine()) ? 10 : (-10), 0f);
			if (trusted)
			{
				string aId = fileName.Split('.')[0];
				if (!File.Exists(filePath))
				{
					if (!mediaIsVideo)
					{
						cacheUrl = MonoSingleton<AssetManager>.Instance.LoadAssetStoreImage(this, message.SenderId, aId).GetFilePath();
					}
					else
					{
						MonoSingleton<AssetManager>.Instance.LoadAssetStoreImage(this, message.SenderId, aId, true);
						cacheUrl = MonoSingleton<AssetManager>.Instance.LoadAssetStoreVideo(this, message.SenderId, aId).GetFilePath();
					}
				}
				else
				{
					loader.SetActive(false);
					cacheUrl = MonoSingleton<AssetManager>.Instance.LoadAssetStoreImage(this, message.SenderId, aId, mediaIsVideo).GetFilePath();
				}
			}
			else
			{
				loader.SetActive(false);
			}
			if (thread is IGroupChatThread)
			{
				SkinnedMeshRenderer componentInChildren = inst.GetComponentInChildren<SkinnedMeshRenderer>();
				if (componentInChildren != null && !aGenerateForHeightOnly)
				{
					IAvatarHolder avatarHolderFromId = thread.GetAvatarHolderFromId(message.SenderId);
					MonoSingleton<AvatarManager>.Instance.SkinAvatar(componentInChildren.gameObject, avatarHolderFromId.Avatar, (AvatarFlags)0, null);
				}
			}
			else
			{
				inst.transform.Find(message.IsMine() ? "PlaceholderAvatarRight" : "PlaceholderAvatarLeft").gameObject.SetActive(false);
			}
			return inst;
		}

		void IScrollItem.Destroy()
		{
		}

		void IPNGAssetObject.OnPNGAssetObject(Texture2D aTexture2D, object aUserData)
		{
			if (this != null && !inst.IsNullOrDisposed() && !inst.GetComponent<RectTransform>().IsNullOrDisposed())
			{
				if (!mediaIsVideo || File.Exists(filePath))
				{
					loader.SetActive(false);
				}
				SetupContextMenu();
				UpdateImage(aTexture2D);
			}
		}

		void IVideoAssetObject.OnVideoAssetObject(string path, object aUserData)
		{
			if (this != null && !inst.IsNullOrDisposed() && !inst.GetComponent<RectTransform>().IsNullOrDisposed())
			{
				loader.SetActive(false);
				if (path != null)
				{
					filePath = path;
				}
			}
		}

		public override void UpdateClientMessage(IChatMessage aClientMessage)
		{
		}

		private void OnClicked()
		{
			if (inst.GetComponent<SaveMediaController>().ContextMenu.activeSelf)
			{
				return;
			}
			if (mediaIsVideo && File.Exists(filePath))
			{
			}
			else
			{
				if (mediaIsVideo || !(inst != null))
				{
					return;
				}
				Texture2D texture2D = inst.transform.Find("Content/MediaTarget").GetComponent<RawImage>().texture as Texture2D;
				if (texture2D != null)
				{
					MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.Hide();
					Sprite aSprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
					FullScreenPicturePanel panel = (FullScreenPicturePanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.FULL_SCREEN_MEDIA);
					panel.Init(aSprite);
					inst.GetComponent<SaveMediaController>().AllowShowingOfMenu = false;
					panel.PanelClosedEvent += OnFullScreenPhotoClosed;
					panel.MediaController.SaveButton.onClick.AddListener(delegate
					{
						panel.MediaController.OnSavePhotoClicked(cacheUrl, true);
					});
				}
			}
		}

		private void OnFullScreenPhotoClosed(BasePanel aPanel)
		{
			inst.GetComponent<SaveMediaController>().AllowShowingOfMenu = true;
			aPanel.PanelClosedEvent -= OnFullScreenPhotoClosed;
		}

		private void UpdateImage(Texture2D aTexture)
		{
			if (!(aTexture == null))
			{
				if (messageHeight <= 0f)
				{
					messageHeight = aTexture.height;
					messageWidth = aTexture.width;
				}
				RectTransform component = inst.GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(messageWidth + ((!(thread is IGroupChatThread)) ? 0f : AVATAR_OFFSET), messageHeight);
				inst.transform.Find("Content/MediaTarget").GetComponent<RawImage>().texture = aTexture;
				if (base.instance != null && base.instance.transform.GetComponent<RectTransform>() != null)
				{
					SaveHeight(base.instance.transform.GetComponent<RectTransform>().sizeDelta.y);
				}
				ScrollView.Reposition();
			}
		}

		private void SetupContextMenu()
		{
			SaveMediaController controller = inst.GetComponent<SaveMediaController>();
			controller.SaveButton.onClick.AddListener(delegate
			{
				controller.OnSavePhotoClicked(cacheUrl);
			});
		}
	}
}
