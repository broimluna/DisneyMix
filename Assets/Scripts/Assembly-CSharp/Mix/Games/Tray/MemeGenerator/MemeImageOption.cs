using Mix.Games.Session;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mix.Games.Tray.MemeGenerator
{
	public class MemeImageOption : MonoBehaviour, IGameAsset
	{
		public GameObject ContextualLoader;

		protected Image mImage;

		protected Button mButton;

		protected IMemeImageOptionSelected mHandler;

		protected string mImageUrl;

		protected Object mImageObject;

		public string ImageUrl
		{
			get
			{
				return mImageUrl;
			}
		}

		public string ThumbnailUrl { get; private set; }

		public Sprite MemeImage
		{
			get
			{
				return mImage.sprite;
			}
		}

		void IGameAsset.OnGameSessionAssetLoaded(object aUserData)
		{
			if (this.IsNullOrDisposed() || mHandler == null || !(ContextualLoader != null))
			{
				return;
			}
			ContextualLoader.SetActive(false);
			mImageObject = mHandler.GetImage(ThumbnailUrl);
			if (mImageObject != null)
			{
				if (mImageObject is Texture2D)
				{
					Texture2D texture2D = (Texture2D)mImageObject;
					Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0f, 0f));
					mImage.sprite = sprite;
				}
				else if (mImageObject is GameObject)
				{
					GameObject gameObject = (GameObject)mImageObject;
					Sprite sprite2 = gameObject.GetComponent<Image>().sprite;
					mImage.sprite = sprite2;
				}
			}
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		private void OnDestroy()
		{
			if (!string.IsNullOrEmpty(ThumbnailUrl) && mImageObject != null)
			{
				mHandler.DestroyImageOption(ThumbnailUrl, mImageObject);
			}
			mImageObject = null;
		}

		public void Initialize(IMemeImageOptionSelected aHandler, string aImageUrl, string aThumbUrl, Meme_Generator_Data aData)
		{
			mImage = GetComponent<Image>();
			mHandler = aHandler;
			mImageUrl = aImageUrl;
			ThumbnailUrl = aThumbUrl;
			mButton = GetComponent<Button>();
			UnityAction call = OnMemeImageOptionSelected;
			mButton.onClick.AddListener(call);
			mHandler.LoadImage(this, ThumbnailUrl);
		}

		protected void OnMemeImageOptionSelected()
		{
			mHandler.OnImageSelected(this);
		}
	}
}
