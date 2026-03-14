using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class AvatarColorChip
	{
		private GameObject mainObj;

		private GameObject selectedHighlight;

		private Image colorChip;

		private IAvatarColorListener listener;

		private int index;

		public AvatarColorChip(Transform aParent, int aIndex, IAvatarColorListener aListener, GameObject colorChipObj, Color color)
		{
			listener = aListener;
			mainObj = Object.Instantiate(colorChipObj);
			colorChip = mainObj.transform.Find("ColorChip").GetComponent<Image>();
			selectedHighlight = mainObj.transform.Find("ColorSelected").gameObject;
			colorChip.color = color;
			mainObj.transform.SetParent(aParent, false);
			index = aIndex;
			mainObj.transform.SetSiblingIndex(aIndex);
			mainObj.SetActive(true);
			SetSelectedState(false);
			UnityAction<bool> call = OnClicked;
			mainObj.GetComponent<Toggle>().onValueChanged.AddListener(call);
		}

		public virtual void OnClicked(bool value)
		{
			if (mainObj != null)
			{
				SetSelectedState(value);
				if (value)
				{
					listener.OnColorClicked(index);
				}
			}
		}

		public float GetButtonWidth()
		{
			RectTransform component = mainObj.transform.Find("ColorChip").GetComponent<RectTransform>();
			return component.rect.width;
		}

		public void SetToggleState(bool value)
		{
			Toggle component = mainObj.GetComponent<Toggle>();
			component.isOn = value;
			SetSelectedState(value);
		}

		private void SetSelectedState(bool value)
		{
			selectedHighlight.SetActive(value);
		}
	}
}
