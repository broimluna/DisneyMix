using System.Collections.Generic;
using Mix.AvatarInternal;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class AvatarColorPicker : MonoBehaviour
	{
		private Transform contentHolder;

		private IAvatarColorListener listener;

		private List<AvatarColorChip> colors;

		private bool IsInitialized;

		private int currentSelection = -1;

		public GameObject colorInst;

		private void Start()
		{
		}

		private void Update()
		{
		}

		public void Init(IAvatarColorListener aListener)
		{
			contentHolder = base.gameObject.transform.Find("ColorPickerContent");
			colors = new List<AvatarColorChip>();
			listener = aListener;
			IsInitialized = true;
		}

		public bool HasInitialized()
		{
			return IsInitialized;
		}

		private void ClearColorContents()
		{
			if (contentHolder == null)
			{
			}
			for (int i = 0; i < contentHolder.childCount; i++)
			{
				if (contentHolder.GetChild(i).gameObject.activeSelf)
				{
					Object.Destroy(contentHolder.GetChild(i).gameObject);
				}
			}
			colors.Clear();
		}

		public void SetSelected(int index)
		{
			if (colors != null && index < colors.Count && currentSelection != index)
			{
				HorizontalLayoutGroup component = contentHolder.gameObject.GetComponent<HorizontalLayoutGroup>();
				float num = colors[index].GetButtonWidth() + (float)component.padding.left + (float)component.padding.right;
				RectTransform component2 = contentHolder.gameObject.GetComponent<RectTransform>();
				RectTransform component3 = base.gameObject.GetComponent<RectTransform>();
				Vector2 anchoredPosition = new Vector2(-1f * num * (float)index + component3.rect.width / 2f - num / 2f, component2.anchoredPosition.y);
				component2.anchoredPosition = anchoredPosition;
				colors[index].SetToggleState(true);
				currentSelection = index;
			}
		}

		public void SetContent(string categoryName)
		{
			Color[] colorsByCategoryName = AvatarColorTints.GetColorsByCategoryName(categoryName);
			ClearColorContents();
			currentSelection = -1;
			if (colorsByCategoryName == null)
			{
				base.gameObject.SetActive(false);
				return;
			}
			for (int i = 0; i < colorsByCategoryName.Length; i++)
			{
				colors.Add(new AvatarColorChip(contentHolder, i, listener, colorInst, colorsByCategoryName[i]));
			}
		}
	}
}
