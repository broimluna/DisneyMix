using System.Collections.Generic;
using Disney.Native;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.Connectivity;
using Mix.Data;
using Mix.Games;
using Mix.Games.Data;
using Mix.Games.Session;
using Mix.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class BaseThumb : ISubSelectorEntry<Sticker_Pack>, IBundleObject
	{
		public BaseContentData entitlement;

		public Transform parent;

		protected IBaseThumb caller;

		protected int index;

		protected object userData;

		protected bool canceled;

		protected Texture2D MissingThumb;

		public static GameObject ThumbHolderGameObject;

		public static GameObject PackHolderGameObject;

		public GameObject thumb;

		protected string CacheKey;

		protected bool Incremented;

		protected TimerThumb timer;

		public BaseThumb(Transform aParent, int aIndex, IBaseThumb aCaller, BaseContentData aEntitlement, Texture2D aMissingThumb = null, object aUserData = null)
		{
			Init(aParent, aIndex, aCaller, aEntitlement, aMissingThumb, aUserData);
		}

		void IBundleObject.OnBundleAssetObject(Object aGameObject, object aUserData)
		{
			DestroySpinner();
			DoButton();
		}

		public virtual void Clean()
		{
			if (thumb != null)
			{
				if (CacheKey != null)
				{
					if (Incremented && MonoSingleton<AssetManager>.Instance != null)
					{
						MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(CacheKey, thumb);
					}
					else
					{
						Object.Destroy(thumb);
					}
				}
				else
				{
					Object.Destroy(thumb);
				}
			}
			if (MonoSingleton<AssetManager>.Instance != null)
			{
				MonoSingleton<AssetManager>.Instance.CancelBundles(this);
			}
			canceled = true;
			entitlement = null;
			userData = null;
			thumb = null;
			ThumbHolderGameObject = null;
			PackHolderGameObject = null;
			parent = null;
			caller = null;
		}

		public virtual void Init(Transform aParent, int aIndex, IBaseThumb aCaller, BaseContentData aEntitlement, Texture2D aMissingThumb, object aUserData)
		{
			if (aEntitlement is Sticker_Pack && PackHolderGameObject == null)
			{
				PackHolderGameObject = (GameObject)Resources.Load("stickerPack_itemName_thumb", typeof(GameObject));
			}
			else if (ThumbHolderGameObject == null)
			{
				ThumbHolderGameObject = (GameObject)Resources.Load("brand_stickerName_thumb", typeof(GameObject));
			}
			parent = aParent;
			caller = aCaller;
			entitlement = aEntitlement;
			index = aIndex;
			userData = aUserData;
			MissingThumb = aMissingThumb;
			if (aEntitlement == null)
			{
				return;
			}
			thumb = Object.Instantiate((!(aEntitlement is Sticker_Pack)) ? ThumbHolderGameObject : PackHolderGameObject);
			thumb.transform.SetParent(parent, false);
			if (aEntitlement is Sticker_Pack)
			{
				thumb.GetComponent<Toggle>().group = parent.GetComponent<ToggleGroup>();
				thumb.transform.Find("ImageTarget").GetComponent<Image>().color = Color.clear;
			}
			else
			{
				if (aEntitlement is Game)
				{
					string duration = (aEntitlement as IEntitlementGameData).GetDuration();
					if (!string.Equals(duration, "0.00:00:00") && !string.IsNullOrEmpty(duration))
					{
						IGameStatistics instance = MonoSingleton<GameManager>.Instance;
						timer = thumb.transform.Find("Timer").GetComponent<TimerThumb>();
						if (!timer.IsNullOrDisposed())
						{
							timer.Initialize(aEntitlement.GetUid(), duration, instance);
						}
					}
				}
				Image component = thumb.GetComponent<Image>();
				component.sprite = null;
				component.color = Color.clear;
			}
			CacheKey = aEntitlement.GetThumb();
			MonoSingleton<AssetManager>.Instance.LoadABundle(this, entitlement.GetThumb(), null, string.Empty, false, false, true);
		}

		public virtual void SetThumbParent(Transform aParent)
		{
			parent = aParent;
			if (thumb != null)
			{
				thumb.transform.SetParent(aParent, false);
			}
		}

		private void SetTextureAsThumb(Texture2D aTexture)
		{
			Sprite sprite = Sprite.Create(aTexture, new Rect(0f, 0f, aTexture.width, aTexture.height), new Vector2(0f, 0f));
			Image image = null;
			if (entitlement is Sticker_Pack)
			{
				image = thumb.transform.Find("ImageTarget").GetComponent<Image>();
				image.color = Color.white;
			}
			else
			{
				image = thumb.GetComponent<Image>();
			}
			image.sprite = sprite;
		}

		private void SetGameObjectAsThumb(GameObject aGameObject)
		{
			parent = thumb.transform.parent;
			Object.Destroy(thumb);
			thumb = aGameObject;
			thumb.transform.SetParent(parent, false);
		}

		private void AddClick()
		{
			if (entitlement is Sticker_Pack)
			{
				thumb.GetComponent<Toggle>().onValueChanged.AddListener(OnToggled);
			}
			else
			{
				thumb.GetComponent<Button>().onClick.AddListener(OnClicked);
			}
			SendToCaller();
		}

		private void SendToCaller(bool IsError = false)
		{
			if (caller != null)
			{
				caller.OnBaseThumbLoaded(false);
			}
		}

		private void DestroySpinner()
		{
			if (!(thumb == null))
			{
				if (!(entitlement is Sticker_Pack))
				{
					Image component = thumb.GetComponent<Image>();
					component.color = Color.white;
				}
				thumb.transform.Find("ContextualLoader").gameObject.SetActive(false);
			}
		}

		public void DoButton()
		{
			Object bundleInstance = MonoSingleton<AssetManager>.Instance.GetBundleInstance(CacheKey);
			if (bundleInstance == null || thumb == null || canceled)
			{
				if (bundleInstance == null && thumb != null)
				{
					thumb.transform.Find("Error").gameObject.SetActive(true);
				}
				SendToCaller(true);
			}
			else if (!(bundleInstance == null))
			{
				Incremented = true;
				if (bundleInstance is Texture2D)
				{
					SetTextureAsThumb((Texture2D)bundleInstance);
				}
				else
				{
					SetGameObjectAsThumb((GameObject)bundleInstance);
				}
				AddClick();
				if (thumb != null && thumb.GetComponent<AccessibilitySettings>() == null)
				{
					thumb.AddComponent<AccessibilitySettings>();
				}
			}
		}

		public virtual void OnToggled(bool aState)
		{
			if (aState)
			{
				OnClicked();
			}
		}

		public Sticker_Pack GetContent()
		{
			return entitlement as Sticker_Pack;
		}

		public T GetThumbComponent<T>() where T : Component
		{
			return thumb.GetComponent<T>();
		}

		public virtual void OnClicked()
		{
			if (entitlement is BaseGameData)
			{
				if (!MonoSingleton<ConnectionManager>.Instance.IsConnected && !MonoSingleton<AssetManager>.Instance.IsBundleCached(entitlement.GetHd()))
				{
					thumb.transform.Find("Error").gameObject.SetActive(true);
					return;
				}
				thumb.transform.Find("Error").gameObject.SetActive(false);
			}
			if (entitlement is Sticker || entitlement is Gag || entitlement is BaseGameData)
			{
				if (Singleton<SettingsManager>.Instance.RecentItems.Contains(entitlement.GetUid()))
				{
					Singleton<SettingsManager>.Instance.RecentItems.Remove(entitlement.GetUid());
				}
				if (Singleton<SettingsManager>.Instance.RecentItems.Count >= 12)
				{
					Singleton<SettingsManager>.Instance.RecentItems.RemoveAt(0);
				}
				Singleton<SettingsManager>.Instance.RecentItems.Add(entitlement.GetUid());
				Singleton<SettingsManager>.Instance.SaveRecentItems();
			}
			if (caller != null)
			{
				if (!timer.IsNullOrDisposed() && timer.IsTimerRunning)
				{
					Dictionary<TimerStatus, string> dictionary = new Dictionary<TimerStatus, string>();
					dictionary.Add(TimerStatus.Running, Singleton<Localizer>.Instance.getString("customtokens.game.fortunecookie_comebacklater"));
					caller.OnBaseThumbClicked(entitlement, dictionary);
				}
				else
				{
					caller.OnBaseThumbClicked(entitlement, userData);
				}
			}
		}

		public void Cancel()
		{
			canceled = true;
		}
	}
}
