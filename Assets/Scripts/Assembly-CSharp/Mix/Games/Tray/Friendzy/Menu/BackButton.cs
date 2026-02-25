using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.Menu
{
	public class BackButton : MonoBehaviour
	{
		public Image Background;

		public Image Arrow;

		public Image Border;

		private RectTransform mTransform;

		private Button mButton;

		public Vector2 OFFSCREEN_BACK_BUTTON_POS = new Vector2(-80f, 0f);

		public Vector2 OVERSHOOT_BACK_BUTTON_POS = new Vector2(10f, 0f);

		public Vector2 ONSCREEN_BACK_BUTTON_POS = Vector2.zero;

		public void SetColors(Color aMainColor, Color aAccentColor)
		{
			aAccentColor.a = 1f;
			Background.color = aAccentColor;
			Arrow.color = aMainColor;
			Border.color = aMainColor;
		}

		public void Show()
		{
			base.gameObject.SetActive(true);
			mTransform.DOAnchorPos(ONSCREEN_BACK_BUTTON_POS, 0.35f).SetEase(Ease.OutBack).OnComplete(delegate
			{
				mButton.interactable = true;
			});
		}

		public void Hide()
		{
			mButton.interactable = false;
			mTransform.DOAnchorPos(OFFSCREEN_BACK_BUTTON_POS, 0.3f).SetEase(Ease.InCubic).OnComplete(delegate
			{
				base.gameObject.SetActive(false);
			});
		}

		private void Awake()
		{
			mButton = GetComponent<Button>();
			mTransform = base.transform as RectTransform;
		}
	}
}
