using System;
using Disney.MobileNetwork;
using Mix;
using Mix.Localization;
using Mix.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Disney.Native
{
	public class ScrollerAccessibilitySettings : AccessibilitySettings
	{
		public bool ScrollBarOnly;

		public ScrollView ReferenceScrollView;

		public ScrollRect ReferenceScrollRect;

		private Scrollbar scrollbar;

		[HideInInspector]
		public EnterBirthdateScroller birthdateScroller { get; private set; }

		private void Start()
		{
			Setup();
		}

		public override void Setup()
		{
			if (ReferenceScrollRect != null)
			{
				scrollbar = ReferenceScrollRect.GetComponent<Scrollbar>();
				if (scrollbar == null)
				{
					scrollbar = ReferenceScrollRect.gameObject.GetComponentInChildren<Scrollbar>();
				}
				birthdateScroller = ReferenceScrollRect.GetComponent<EnterBirthdateScroller>();
				if (birthdateScroller == null)
				{
					birthdateScroller = ReferenceScrollRect.gameObject.GetComponentInChildren<EnterBirthdateScroller>();
				}
			}
			base.Setup();
		}

		public string GetScrollText(int aIncrement)
		{
			if (birthdateScroller != null)
			{
				GameObject gameObject = ReferenceScrollRect.content.gameObject;
				int num = 0;
				if (birthdateScroller.mMinVal < 1)
				{
					num = Math.Abs(birthdateScroller.mMinVal) + 1;
				}
				if (birthdateScroller.mMinVal > 1)
				{
					num = -(birthdateScroller.mMinVal - 1);
				}
				int index = birthdateScroller.CurValue + aIncrement + num;
				Transform child = gameObject.transform.GetChild(index);
				Text componentInChildren = child.gameObject.GetComponentInChildren<Text>();
				if (componentInChildren != null)
				{
					return componentInChildren.text;
				}
			}
			return string.Empty;
		}

		public void SayScrollText(int aIncrement)
		{
			string scrollText = GetScrollText(aIncrement);
			string text = Singleton<Localizer>.Instance.getString(CustomToken);
			text = text.Replace("#age#", scrollText);
			text = text.Replace("#month#", scrollText);
			text = text.Replace("#day#", scrollText);
			AccessibilityManager.Instance.Speak(text);
		}

		public string GetSayScrollText(int aIncrement)
		{
			string scrollText = GetScrollText(aIncrement);
			string text = Singleton<Localizer>.Instance.getString(CustomToken);
			text = text.Replace("#age#", scrollText);
			text = text.Replace("#month#", scrollText);
			return text.Replace("#day#", scrollText);
		}

		public void Scroll(float aDistance)
		{
			if (ReferenceScrollView != null && !ScrollBarOnly)
			{
				Rect rectInPhysicalScreenSpace = Util.GetRectInPhysicalScreenSpace(ReferenceScrollRect.gameObject.GetComponent<RectTransform>());
				Rect rectInPhysicalScreenSpace2 = Util.GetRectInPhysicalScreenSpace(ReferenceScrollRect.content);
				float height = rectInPhysicalScreenSpace.height;
				float height2 = rectInPhysicalScreenSpace2.height;
				float num = aDistance * (height / height2);
				ReferenceScrollView.ModifyScrollPosition(num * height);
			}
			else if (birthdateScroller != null && !ScrollBarOnly)
			{
				int num2 = 1;
				if (aDistance < 0f)
				{
					num2 = -1;
				}
				birthdateScroller.CurValue += num2;
			}
			else if (scrollbar != null)
			{
				Rect rectInPhysicalScreenSpace3 = Util.GetRectInPhysicalScreenSpace(ReferenceScrollRect.gameObject.GetComponent<RectTransform>());
				Rect rectInPhysicalScreenSpace4 = Util.GetRectInPhysicalScreenSpace(ReferenceScrollRect.content);
				float height3 = rectInPhysicalScreenSpace3.height;
				float height4 = rectInPhysicalScreenSpace4.height;
				float num3 = aDistance * (height3 / height4);
				scrollbar.value += num3;
			}
		}
	}
}
