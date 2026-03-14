using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Hints
{
	public class GameTooltip : MonoBehaviour
	{
		public enum TooltipAnchorPosition
		{
			NONE = 0,
			TOP = 1,
			CENTER = 2,
			BOTTOM = 3
		}

		public float edgePadding = 20f;

		public float tooltipHeight = 50f;

		[Header("Show Animation")]
		public float showAnimationTime = 0.3f;

		public Ease showAnimationEaseType = Ease.OutBack;

		[Header("Hide Animation")]
		public float hideAnimationTime = 0.3f;

		public Ease hideAnimationEaseType = Ease.InBack;

		[Header("Update Bounce")]
		public float hintUpdateBounceAmount = 0.5f;

		public float hintUpdateBounceTime = 1f;

		[Header("Internal References")]
		public Text hintText;

		public RectTransform background;

		private RectTransform mRectTransform;

		private bool mIsVisible;

		public string text
		{
			get
			{
				return hintText.text;
			}
			set
			{
				if (!string.Equals(hintText.text, value))
				{
					hintText.text = value;
				}
			}
		}

		public TooltipAnchorPosition anchorPosition
		{
			set
			{
				switch (value)
				{
				case TooltipAnchorPosition.TOP:
					mRectTransform.anchorMin = new Vector2(0f, 1f);
					mRectTransform.anchorMax = new Vector2(1f, 1f);
					mRectTransform.offsetMin = new Vector2(0f, 0f);
					mRectTransform.offsetMax = new Vector2(1f, 0f);
					background.anchorMin = new Vector2(0f, 0f);
					background.anchorMax = new Vector2(1f, 1f);
					background.offsetMin = new Vector2(edgePadding, 0f - (edgePadding + tooltipHeight));
					background.offsetMax = new Vector2(0f - edgePadding, 0f - edgePadding);
					break;
				case TooltipAnchorPosition.BOTTOM:
					mRectTransform.anchorMin = new Vector2(0f, 0f);
					mRectTransform.anchorMax = new Vector2(1f, 0f);
					mRectTransform.offsetMin = new Vector2(0f, 0f);
					mRectTransform.offsetMax = new Vector2(1f, 0f);
					background.anchorMin = new Vector2(0f, 0f);
					background.anchorMax = new Vector2(1f, 1f);
					background.offsetMin = new Vector2(edgePadding, edgePadding);
					background.offsetMax = new Vector2(0f - edgePadding, edgePadding + tooltipHeight);
					break;
				case TooltipAnchorPosition.CENTER:
					mRectTransform.anchorMin = new Vector2(0f, 0.5f);
					mRectTransform.anchorMax = new Vector2(1f, 0.5f);
					mRectTransform.offsetMin = new Vector2(0f, 0f);
					mRectTransform.offsetMax = new Vector2(1f, 0f);
					background.anchorMin = new Vector2(0f, 0f);
					background.anchorMax = new Vector2(1f, 1f);
					background.offsetMin = new Vector2(edgePadding, (0f - tooltipHeight) / 2f);
					background.offsetMax = new Vector2(0f - edgePadding, tooltipHeight / 2f);
					break;
				}
			}
		}

		private void Awake()
		{
			mRectTransform = GetComponent<RectTransform>();
			Hide(true);
		}

		public void SetTextWithBounce(string text, bool onlyBounceIfUpdated = true)
		{
			bool flag = false;
			if (!string.Equals(hintText.text, text))
			{
				hintText.text = text;
				flag = true;
			}
			if (mIsVisible && (!onlyBounceIfUpdated || flag))
			{
				background.DOPunchScale(hintUpdateBounceAmount * Vector3.one, hintUpdateBounceTime, 5, 0f);
			}
		}

		public void Show(bool instant = false)
		{
			if (instant)
			{
				background.localScale = Vector3.one;
			}
			else
			{
				background.DOScale(Vector3.one, showAnimationTime).SetEase(showAnimationEaseType);
			}
			mIsVisible = true;
		}

		public void Hide(bool instant = false)
		{
			if (instant)
			{
				background.localScale = Vector3.zero;
			}
			else
			{
				background.DOScale(Vector3.zero, hideAnimationTime).SetEase(hideAnimationEaseType);
			}
			mIsVisible = false;
		}
	}
}
