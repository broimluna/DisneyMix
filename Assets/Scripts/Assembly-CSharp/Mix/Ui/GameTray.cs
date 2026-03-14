using Mix.AssetBundles;
using Mix.Assets;
using Mix.Games;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class GameTray : MonoBehaviour, IBundleObject
	{
		public enum GameTrayState
		{
			NONE = 0,
			GAME = 1,
			PAUSE = 2,
			ERROR = 3
		}

		public ChatBar ChatBar;

		public GameObject LoaderPanel;

		public GameObject PausedPanel;

		public GameObject ScreenshotPanel;

		public Image LoaderImage;

		public Image PauseImage;

		public GameObject ContinueMessage;

		public GameObject QuitMessage;

		public GameObject NetworkErrorMessage;

		private float minimumHeight;

		private string loaderURL;

		private Object loaderObject;

		private string pauseURL;

		private Object pauseObject;

		private string PAUSE_BUNDLE = "pause";

		private string LOADING_BUNDLE = "loading";

		private float defaultHeight;

		void IBundleObject.OnBundleAssetObject(Object aGameObject, object aUserData)
		{
			Object bundleInstance = MonoSingleton<AssetManager>.Instance.GetBundleInstance((!aUserData.Equals(PAUSE_BUNDLE)) ? loaderURL : pauseURL);
			Sprite sprite = null;
			if (bundleInstance is Texture2D)
			{
				Texture2D texture2D = (Texture2D)bundleInstance;
				sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0f, 0f));
			}
			else if (bundleInstance is GameObject)
			{
				GameObject gameObject = (GameObject)bundleInstance;
				sprite = gameObject.GetComponent<Image>().sprite;
				if (aUserData.Equals(PAUSE_BUNDLE))
				{
					pauseObject = gameObject;
				}
				else
				{
					loaderObject = gameObject;
				}
			}
			if (sprite != null)
			{
				if (aUserData.Equals(PAUSE_BUNDLE))
				{
					PauseImage.sprite = sprite;
					PauseImage.gameObject.SetActive(true);
				}
				else
				{
					LoaderImage.sprite = sprite;
					LoaderImage.gameObject.SetActive(true);
				}
			}
		}

		private void Start()
		{
			defaultHeight = GetComponent<LayoutElement>().minHeight;
		}

		private void OnDestroy()
		{
			if (loaderObject != null && MonoSingleton<AssetManager>.Instance != null)
			{
				MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(loaderURL, loaderObject);
				loaderObject = null;
			}
			if (pauseObject != null && MonoSingleton<AssetManager>.Instance != null)
			{
				MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(pauseURL, pauseObject);
				pauseObject = null;
			}
		}

		public bool AnimateDown()
		{
			LayoutElement component = GetComponent<LayoutElement>();
			component.minHeight = Util.Vector2Update(new Vector2(0f, component.minHeight), Vector2.zero, 10f).y;
			MonoSingleton<GameManager>.Instance.SetGameExitHeight(component.minHeight);
			if (component.minHeight <= 5f)
			{
				component.minHeight = 0f;
				MonoSingleton<GameManager>.Instance.SetGameExitHeight(component.minHeight);
				return false;
			}
			return true;
		}

		public void UpdateState(ChatController.UIState aState)
		{
			if (!this.IsNullOrDisposed() && !ChatBar.IsNullOrDisposed())
			{
				bool flag = aState.Equals(ChatController.UIState.GameTray);
				base.gameObject.SetActive(flag);
				ChatBar.ToggleListeners(!flag);
			}
		}

		public void SetSize(float aHeight)
		{
			GetComponent<LayoutElement>().minHeight = aHeight;
		}

		public void OnAnimateDownComplete()
		{
			OnGamePause(null);
			if (ScreenshotPanel.activeSelf)
			{
				Image component = ScreenshotPanel.GetComponent<Image>();
				component.sprite = null;
				ScreenshotPanel.SetActive(false);
			}
		}

		public void OnQuitGame()
		{
			MonoSingleton<GameManager>.Instance.QuitGameSession();
		}

		public void OnResumeGame()
		{
			MonoSingleton<GameManager>.Instance.ResumeGameSession();
		}

		public void OnGamePause(string aLogo)
		{
			PausedPanel.SetActive(aLogo != null);
			if (aLogo != null)
			{
				pauseURL = aLogo;
				MonoSingleton<AssetManager>.Instance.LoadABundle(this, aLogo, PAUSE_BUNDLE, string.Empty, false, false, true);
				ContinueMessage.SetActive(true);
				QuitMessage.SetActive(true);
				NetworkErrorMessage.SetActive(false);
				return;
			}
			PauseImage.sprite = null;
			LoaderImage.sprite = null;
			LoaderImage.gameObject.SetActive(false);
			if (!string.IsNullOrEmpty(pauseURL) && pauseObject != null)
			{
				MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(pauseURL, pauseObject);
				pauseObject = null;
			}
			if (!string.IsNullOrEmpty(loaderURL) && loaderObject != null)
			{
				MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(loaderURL, loaderObject);
				loaderObject = null;
			}
		}

		public void OnGameError(string aLogo)
		{
			PausedPanel.SetActive(aLogo != null);
			if (aLogo != null)
			{
				pauseURL = aLogo;
				MonoSingleton<AssetManager>.Instance.LoadABundle(this, aLogo, PAUSE_BUNDLE, string.Empty, false, false, true);
				ContinueMessage.SetActive(false);
				QuitMessage.SetActive(false);
				NetworkErrorMessage.SetActive(true);
			}
		}

		public void UpdateGameLoader(string aLogo)
		{
			if (aLogo != null)
			{
				SetSize(defaultHeight);
			}
			LoaderPanel.SetActive(aLogo != null);
			if (aLogo != null && MonoSingleton<AssetManager>.Instance != null)
			{
				loaderURL = aLogo;
				MonoSingleton<AssetManager>.Instance.LoadABundle(this, aLogo, LOADING_BUNDLE, string.Empty, false, false, true);
			}
			else
			{
				LoaderImage.sprite = null;
				LoaderImage.gameObject.SetActive(false);
			}
		}

		public void UpdateGameScreenshot(Texture2D aGameScreenshot)
		{
			ScreenshotPanel.gameObject.SetActive(true);
			Image component = ScreenshotPanel.GetComponent<Image>();
			Sprite sprite = Sprite.Create(aGameScreenshot, new Rect(0f, 0f, aGameScreenshot.width, aGameScreenshot.height), new Vector2(0f, 0f));
			component.sprite = sprite;
		}
	}
}
