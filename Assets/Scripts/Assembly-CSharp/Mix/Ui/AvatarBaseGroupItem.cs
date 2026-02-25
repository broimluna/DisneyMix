using Disney.Native;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.Connectivity;
using Mix.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class AvatarBaseGroupItem : BaseThumb, IBundleObject
	{
		private GameObject ParentObject;

		private GameObject LoadingHolder;

		private GameObject ActiveStateRemove;

		private GameObject ActiveStateSelected;

		private bool isEyesComponent;

		private Image image;

		private UnityAction<bool> callbackRef;

		private bool lockedInput;

		private bool alreadyLoading;

		private AvatarThumbQueuer instantiator;

		private bool pendingSelected;

		private bool pendingInstantiation;

		public static GameObject categoryItemLoader;

		public AvatarBaseGroupItem(Transform aParent, int aIndex, IBaseThumb aCaller, BaseContentData aEntitlement, Texture2D aMissingTexture, AvatarThumbQueuer aInstantiator, object aUserData = null)
			: base(aParent, aIndex, aCaller, aEntitlement, aMissingTexture, aUserData)
		{
			instantiator = aInstantiator;
			pendingInstantiation = true;
			instantiator.InvokeWhenAvailable(InstantiateGameObject);
		}

		void IBundleObject.OnBundleAssetObject(Object aGameObject, object aUserData)
		{
			alreadyLoading = false;
			if (ParentObject == null)
			{
				return;
			}
			if (thumb != null && thumb != ParentObject && entitlement != null && Incremented && MonoSingleton<AssetManager>.Instance != null)
			{
				MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(entitlement.GetThumb(), thumb);
				Incremented = false;
			}
			if (canceled)
			{
				SendLoadedToCaller(true);
				return;
			}
			Object bundleInstance = MonoSingleton<AssetManager>.Instance.GetBundleInstance(entitlement.GetThumb());
			if (bundleInstance == null || image == null)
			{
				thumb = ParentObject;
				thumb.transform.Find("Error").gameObject.SetActive(true);
			}
			else
			{
				CacheKey = entitlement.GetThumb();
				Incremented = true;
				if (bundleInstance is Texture2D)
				{
					thumb = ParentObject;
					SetImageFromTex2D((Texture2D)bundleInstance);
				}
				else if (bundleInstance is GameObject)
				{
					parent = ParentObject.transform.parent;
					((GameObject)bundleInstance).transform.SetParent(parent, false);
				}
				else
				{
					thumb = ParentObject;
					thumb.transform.Find("Error").gameObject.SetActive(true);
				}
			}
			SendLoadedToCaller(false);
			if (thumb != null)
			{
				Toggle component = thumb.GetComponent<Toggle>();
				if (component != null)
				{
					if (MonoSingleton<ConnectionManager>.Instance.IsConnected || MonoSingleton<AssetManager>.Instance.IsBundleCached(entitlement.GetHd()))
					{
						callbackRef = OnValueChanged;
						component.onValueChanged.AddListener(callbackRef);
					}
					else
					{
						thumb.transform.Find("Error").gameObject.SetActive(true);
						component.interactable = false;
					}
					SetToggleState(component.isOn);
				}
				if (thumb != null && thumb.GetComponent<AccessibilitySettings>() == null)
				{
					thumb.AddComponent<AccessibilitySettings>();
				}
			}
			if (LoadingHolder != null)
			{
				image.color = Color.white;
				Object.Destroy(LoadingHolder);
			}
		}

		private void InstantiateGameObject()
		{
			pendingInstantiation = false;
			if (canceled)
			{
				return;
			}
			if (categoryItemLoader == null)
			{
				categoryItemLoader = (GameObject)Resources.Load("category_itemName_thumb", typeof(GameObject));
			}
			if (entitlement != null && !alreadyLoading)
			{
				ParentObject = Object.Instantiate(categoryItemLoader);
				LoadingHolder = ParentObject.transform.Find("ContextualLoader").gameObject;
				ActiveStateRemove = ParentObject.transform.Find("ContainerRemove").gameObject;
				ActiveStateSelected = ParentObject.transform.Find("ContainerSelected").gameObject;
				image = ParentObject.transform.Find("ImageTarget").gameObject.GetComponent<Image>();
				LoadingHolder.SetActive(true);
				ActiveStateRemove.SetActive(false);
				ActiveStateSelected.SetActive(false);
				image.gameObject.SetActive(false);
				CacheKey = entitlement.GetThumb();
				ParentObject.transform.SetParent(parent, false);
				MonoSingleton<AssetManager>.Instance.LoadABundle(this, entitlement.GetThumb(), null, string.Empty, false, false, true);
				alreadyLoading = true;
				if (pendingSelected)
				{
					SetToggleState(true);
					pendingSelected = false;
				}
			}
		}

		public override void SetThumbParent(Transform aParent)
		{
			parent = aParent;
			if (ParentObject != null)
			{
				ParentObject.transform.SetParent(aParent, false);
			}
		}

		public override void Clean()
		{
			if (pendingInstantiation)
			{
				instantiator.Cancel(InstantiateGameObject);
			}
			base.Clean();
			if (!(ParentObject == null))
			{
				Toggle component = ParentObject.GetComponent<Toggle>();
				if (component != null && callbackRef != null)
				{
					component.onValueChanged.RemoveListener(callbackRef);
				}
				if (image != null && (bool)image && (bool)image.sprite)
				{
					image.sprite = null;
				}
			}
		}

		public float GetButtonWidth()
		{
			if (ActiveStateRemove != null)
			{
				RectTransform component = ActiveStateRemove.GetComponent<RectTransform>();
				return component.rect.width;
			}
			return 0f;
		}

		public override void Init(Transform aParent, int aIndex, IBaseThumb aCaller, BaseContentData aEntitlement, Texture2D aMissingThumb, object aUserData)
		{
			parent = aParent;
			caller = aCaller;
			entitlement = aEntitlement;
			index = aIndex;
			userData = aUserData;
			MissingThumb = aMissingThumb;
			Avatar_Multiplane avatar_Multiplane = (Avatar_Multiplane)entitlement;
			isEyesComponent = avatar_Multiplane != null && avatar_Multiplane.GetCategory() == "Eyes";
		}

		public void SetToggleState(bool value)
		{
			if (ParentObject != null)
			{
				Toggle component = ParentObject.GetComponent<Toggle>();
				if (component != null)
				{
					lockedInput = true;
					component.isOn = value;
					SetSelector(value);
					lockedInput = false;
				}
			}
			else
			{
				pendingSelected = value;
			}
		}

		public void OnValueChanged(bool value)
		{
			if (ParentObject != null && ParentObject.gameObject != null && !lockedInput)
			{
				SendClickToCaller(entitlement, userData);
			}
		}

		private void SendClickToCaller(BaseContentData entitlement, object userData)
		{
			if (caller != null && thumb != null)
			{
				caller.OnBaseThumbClicked(entitlement, userData);
			}
		}

		private void SendLoadedToCaller(bool hasErrored)
		{
			if (caller != null)
			{
				caller.OnBaseThumbLoaded(hasErrored);
			}
		}

		private void SetImageFromTex2D(Texture2D tex)
		{
			if (!(tex == null) && image != null)
			{
				Sprite sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0f, 0f));
				if (image != null)
				{
					image.sprite = sprite;
				}
				image.gameObject.SetActive(true);
			}
		}

		private void SetSelector(bool isOn)
		{
			if (isEyesComponent)
			{
				ActiveStateSelected.SetActive(isOn);
			}
			else
			{
				ActiveStateRemove.SetActive(isOn);
			}
		}
	}
}
