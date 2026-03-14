using Mix.AssetBundles;
using Mix.Assets;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games
{
	public class GameLoaderController : MonoBehaviour, IBundleObject
	{
		public GameObject Loader;

		public float MinDelayAmount = 0.5f;

		public Image LoaderBackgroundImage;

		public Image GameLogoImage;

		public string GameLogoURL;

		public Object GameLogoObject;

		void IBundleObject.OnBundleAssetObject(Object aGameObject, object aUserData)
		{
			if (aUserData == null)
			{
				GameLogoObject = MonoSingleton<AssetManager>.Instance.GetBundleInstance(GameLogoURL);
				if (aGameObject is Texture2D)
				{
					Texture2D texture2D = (Texture2D)GameLogoObject;
					Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0f, 0f));
					GameLogoImage.sprite = sprite;
				}
				else if (aGameObject is GameObject)
				{
					GameObject gameObject = (GameObject)GameLogoObject;
					Sprite sprite2 = gameObject.GetComponent<Image>().sprite;
					GameLogoImage.sprite = sprite2;
				}
				GameLogoImage.gameObject.SetActive(true);
			}
		}

		public void Disable()
		{
			GameLogoImage.gameObject.SetActive(false);
		}

		public void Initialize(string aLogo)
		{
			GameLogoURL = aLogo;
			MonoSingleton<AssetManager>.Instance.LoadABundle(this, aLogo, null, string.Empty, false, false, true);
		}

		public void ShowLoader(Transform aParent)
		{
			base.transform.SetParent(aParent);
		}

		private void OnDestroy()
		{
			if (GameLogoObject != null && MonoSingleton<AssetManager>.Instance != null)
			{
				MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(GameLogoURL, GameLogoObject);
				GameLogoObject = null;
			}
		}
	}
}
