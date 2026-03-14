using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public abstract class BaseSubSelector<T, U> : MonoBehaviour where U : ISubSelectorEntry<T>
	{
		public Transform ContentHolder;

		protected ISubSelector<T> listener;

		protected List<U> thumbItems = new List<U>();

		protected bool scroll;

		protected Vector2 scrollTarget;

		protected T currentContent;

		public T CurrentContent
		{
			get
			{
				return currentContent;
			}
			set
			{
				currentContent = value;
				UpdateToggleGroup();
			}
		}

		private void FixedUpdate()
		{
			if (scroll)
			{
				ContentHolder.GetComponent<RectTransform>().anchoredPosition = Util.Vector2Update(ContentHolder.GetComponent<RectTransform>().anchoredPosition, scrollTarget, 5f);
				if (Vector2.Distance(ContentHolder.GetComponent<RectTransform>().anchoredPosition, scrollTarget) < 5f)
				{
					ContentHolder.GetComponent<RectTransform>().anchoredPosition = scrollTarget;
					scroll = false;
				}
			}
		}

		private void OnDestroy()
		{
			Clear();
		}

		public abstract void Setup(ISubSelector<T> aListener);

		public abstract T GetContent(int aPackIndex);

		public abstract void ToggleHighlight(U aThumb);

		protected void UpdateToggleGroup()
		{
			foreach (U thumbItem in thumbItems)
			{
				if (thumbItem.GetContent().Equals(CurrentContent))
				{
					Toggle thumbComponent = thumbItem.GetThumbComponent<Toggle>();
					thumbComponent.isOn = true;
					Rect rectInScreenSpace = Util.GetRectInScreenSpace(GetComponent<RectTransform>());
					Rect rectInScreenSpace2 = Util.GetRectInScreenSpace(thumbItem.GetThumbComponent<RectTransform>());
					rectInScreenSpace.x += rectInScreenSpace2.width / 2f;
					rectInScreenSpace.width -= rectInScreenSpace2.width;
					if (!rectInScreenSpace.Contains(rectInScreenSpace2.center))
					{
						float num = (rectInScreenSpace.center.x - rectInScreenSpace2.center.x) * Singleton<SettingsManager>.Instance.GetWidthScale();
						scroll = true;
						scrollTarget = new Vector2(ContentHolder.GetComponent<RectTransform>().anchoredPosition.x + num, ContentHolder.GetComponent<RectTransform>().anchoredPosition.y);
						scrollTarget.x = Mathf.Clamp(scrollTarget.x, GetComponent<RectTransform>().sizeDelta.x - ContentHolder.GetComponent<RectTransform>().sizeDelta.x, 0f);
					}
					ToggleHighlight(thumbItem);
				}
			}
		}

		protected void Clear()
		{
			if (thumbItems != null)
			{
				foreach (U thumbItem in thumbItems)
				{
					if (thumbItem != null)
					{
						thumbItem.Clean();
					}
				}
				thumbItems.Clear();
			}
			if (ContentHolder != null && ContentHolder.GetComponent<RectTransform>() != null)
			{
				ContentHolder.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
				for (int i = 0; i < ContentHolder.childCount; i++)
				{
					Object.Destroy(ContentHolder.GetChild(i).gameObject);
				}
				ClearInvokes();
			}
		}

		public virtual void ClearInvokes()
		{
		}
	}
}
