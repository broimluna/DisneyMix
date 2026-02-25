using UnityEngine;

namespace Mix.Games.Tray.Hints
{
	public class AnchorUIElement : MonoBehaviour
	{
		public enum AnchorStyle
		{
			NONE = 0,
			FILL = 1,
			TOP_LEFT = 2,
			TOP_CENTER = 3,
			TOP_RIGHT = 4,
			BOTTOM_LEFT = 5,
			BOTTOM_CENTER = 6,
			BOTTOM_RIGHT = 7
		}

		public float defaultPadding = 10f;

		[Space(10f)]
		public GameObject leftSpacer;

		public GameObject rightSpacer;

		private RectTransform mRectTransform;

		public void SetAnchor(AnchorStyle style)
		{
			if (mRectTransform == null)
			{
				mRectTransform = GetComponent<RectTransform>();
			}
			switch (style)
			{
			case AnchorStyle.FILL:
				mRectTransform.anchorMin = new Vector2(0f, 0f);
				mRectTransform.anchorMax = new Vector2(1f, 1f);
				mRectTransform.pivot = new Vector2(0.5f, 0.5f);
				mRectTransform.offsetMin = new Vector2(0f, 0f);
				mRectTransform.offsetMax = new Vector2(0f, 0f);
				leftSpacer.SetActive(false);
				rightSpacer.SetActive(false);
				break;
			case AnchorStyle.TOP_LEFT:
				mRectTransform.anchorMin = new Vector2(0f, 1f);
				mRectTransform.anchorMax = new Vector2(1f, 1f);
				mRectTransform.pivot = new Vector2(0.5f, 1f);
				mRectTransform.anchoredPosition = new Vector2(0f, 0f);
				leftSpacer.SetActive(false);
				rightSpacer.SetActive(true);
				break;
			case AnchorStyle.TOP_CENTER:
				mRectTransform.anchorMin = new Vector2(0f, 1f);
				mRectTransform.anchorMax = new Vector2(1f, 1f);
				mRectTransform.pivot = new Vector2(0.5f, 1f);
				mRectTransform.anchoredPosition = new Vector2(0f, 0f);
				leftSpacer.SetActive(true);
				rightSpacer.SetActive(true);
				break;
			case AnchorStyle.TOP_RIGHT:
				mRectTransform.anchorMin = new Vector2(0f, 1f);
				mRectTransform.anchorMax = new Vector2(1f, 1f);
				mRectTransform.pivot = new Vector2(0.5f, 1f);
				mRectTransform.anchoredPosition = new Vector2(0f, 0f);
				leftSpacer.SetActive(true);
				rightSpacer.SetActive(false);
				break;
			case AnchorStyle.BOTTOM_LEFT:
				mRectTransform.anchorMin = new Vector2(0f, 0f);
				mRectTransform.anchorMax = new Vector2(1f, 0f);
				mRectTransform.pivot = new Vector2(0.5f, 0f);
				mRectTransform.anchoredPosition = new Vector2(0f, 0f);
				leftSpacer.SetActive(false);
				rightSpacer.SetActive(true);
				break;
			case AnchorStyle.BOTTOM_CENTER:
				mRectTransform.anchorMin = new Vector2(0f, 0f);
				mRectTransform.anchorMax = new Vector2(1f, 0f);
				mRectTransform.pivot = new Vector2(0.5f, 0f);
				mRectTransform.anchoredPosition = new Vector2(0f, 0f);
				leftSpacer.SetActive(true);
				rightSpacer.SetActive(true);
				break;
			case AnchorStyle.BOTTOM_RIGHT:
				mRectTransform.anchorMin = new Vector2(0f, 0f);
				mRectTransform.anchorMax = new Vector2(1f, 0f);
				mRectTransform.pivot = new Vector2(0.5f, 0f);
				mRectTransform.anchoredPosition = new Vector2(0f, 0f);
				leftSpacer.SetActive(true);
				rightSpacer.SetActive(false);
				break;
			}
		}
	}
}
