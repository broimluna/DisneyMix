using DG.Tweening;
using Mix.Games.Tray;
using Mix.Native;
using UnityEngine;

namespace Mix.Games
{
	public class ToastPanelController : MonoBehaviour
	{
		public delegate void OnAnimationComplete();

		public delegate void OnToastPanelOnKeyboardReturnPressed(NativeKeyboardReturnKey aKey);

		private const float ANIMATE_TIME = 0.375f;

		private const float PANEL_RETRY_TIME = 0.5f;

		private NativeKeyboard keyboard;

		private float currentKeyboardHeight;

		public GameObject contentObject;

		public GameObject basePanelObject;

		private RectTransform contentRect;

		private RectTransform panelRect;

		private NativeTextView nativeTextView;

		private int statusBarHeight;

		private int screenHeight;

		private float heightScale;

		public event OnAnimationComplete ToastPanelAnimationComplete;

		public event OnAnimationComplete ToastPanelHideComplete;

		public event OnToastPanelOnKeyboardReturnPressed ToastPanelOnKeyboardReturnPressed;

		private void Awake()
		{
			keyboard = MonoSingleton<NativeKeyboardManager>.Instance.Keyboard;
			contentRect = contentObject.GetComponent<RectTransform>();
			panelRect = basePanelObject.GetComponent<RectTransform>();
			nativeTextView = base.gameObject.GetComponentInChildren<NativeTextView>();
		}

		private void OnEnable()
		{
			keyboard.OnNativeKeyboardHeightChanged += ShowToastPanel;
			keyboard.OnNativeKeyboardReturnKeyPressed += ToastPanelOnKeyboardReturn;
			panelRect.sizeDelta = Vector2.zero;
			ShowKeyboard();
		}

		public void ShowKeyboardRetry()
		{
			if (isSelectable(nativeTextView))
			{
				ShowKeyboard();
			}
			else
			{
				HideComplete();
			}
		}

		public void Init(int aStatusBarHeight, int aScreenHeight, float aHeightScale)
		{
			statusBarHeight = aStatusBarHeight;
			screenHeight = aScreenHeight;
			heightScale = aHeightScale;
		}

		public void ShowKeyboard()
		{
			if (!isSelectable(nativeTextView))
			{
				Invoke("ShowKeyboardRetry", 0.5f);
				return;
			}
			nativeTextView.SelectInput();
			nativeTextView.Selected = false;
		}

		private void OnDisable()
		{
			DOTween.Kill(base.gameObject);
		}

		private bool isSelectable(NativeTextView aNativeTextView)
		{
			return !aNativeTextView.Selected && aNativeTextView.interactable;
		}

		private void ShowToastPanel(object sender, NativeKeyboardHeightChangedEventArgs args)
		{
			int num = args.Height;
			MonoSingleton<GameManager>.Instance.IsToastPanelActive = true;
			if (DebugSceneIndicator.IsMainScene)
			{
				num = (int)((float)num * heightScale);
			}
			if (!this.IsNullOrDisposed() && !contentRect.IsNullOrDisposed())
			{
				currentKeyboardHeight = num;
				float num2 = (float)screenHeight * heightScale;
				float num3 = (float)statusBarHeight * heightScale;
				float height = contentRect.rect.height;
				float num4 = (float)num + height;
				float num5 = num2 - num3;
				if (num4 > num5)
				{
					height = num2 - (float)num - num3;
					contentRect.sizeDelta = new Vector2(contentRect.rect.width, height);
				}
				Vector2 endValue = new Vector2(0f, currentKeyboardHeight + contentRect.rect.height);
				panelRect.DOSizeDelta(endValue, 0.375f, true).SetEase(Ease.OutBack).OnComplete(ShowComplete)
					.OnUpdate(RepositionTextView)
					.SetId(base.gameObject);
			}
		}

		public void HideToastPanel()
		{
			MonoSingleton<GameManager>.Instance.IsToastPanelActive = false;
			panelRect.DOSizeDelta(Vector2.zero, 0.375f, true).SetEase(Ease.InQuad).OnComplete(HideComplete)
				.OnUpdate(RepositionTextView)
				.SetId(base.gameObject);
			keyboard.Hide();
		}

		private void RepositionTextView()
		{
			nativeTextView.Reposition();
		}

		private void ShowComplete()
		{
			if (keyboard != null)
			{
				keyboard.OnNativeKeyboardHeightChanged -= ShowToastPanel;
			}
			if (this.ToastPanelAnimationComplete != null)
			{
				this.ToastPanelAnimationComplete();
			}
		}

		private void HideComplete()
		{
			if (keyboard != null)
			{
				keyboard.OnNativeKeyboardHeightChanged -= ShowToastPanel;
				keyboard.OnNativeKeyboardReturnKeyPressed -= ToastPanelOnKeyboardReturn;
			}
			if (this.ToastPanelHideComplete != null)
			{
				this.ToastPanelHideComplete();
			}
		}

		private void ToastPanelOnKeyboardReturn(object sender, NativeKeyboardReturnKeyPressedEventArgs args)
		{
			if (this.ToastPanelOnKeyboardReturnPressed != null)
			{
				this.ToastPanelOnKeyboardReturnPressed(args.ReturnKeyType);
			}
		}
	}
}
