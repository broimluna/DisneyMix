using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Fireworks
{
	[RequireComponent(typeof(LayoutElement))]
	[RequireComponent(typeof(RectTransform))]
	public class SetLayoutToRect : MonoBehaviour
	{
		public RectTransform targetRect;

		public bool isUseLayoutElement = true;

		public bool isSetWidth;

		public bool isSetHeight;

		private void Start()
		{
			RectTransform component = base.gameObject.transform.GetComponent<RectTransform>();
			LayoutElement component2 = base.gameObject.transform.GetComponent<LayoutElement>();
			float width = component.rect.width;
			float height = component.rect.height;
			if (!(targetRect != null))
			{
				return;
			}
			if (isUseLayoutElement)
			{
				if (isSetWidth)
				{
					component2.minWidth = targetRect.rect.width;
				}
				if (isSetHeight)
				{
					component2.minHeight = targetRect.rect.height;
				}
				return;
			}
			if (isSetWidth)
			{
				width = targetRect.rect.width;
			}
			if (isSetHeight)
			{
				height = targetRect.rect.height;
			}
			component.sizeDelta = new Vector2(width, height);
		}
	}
}
