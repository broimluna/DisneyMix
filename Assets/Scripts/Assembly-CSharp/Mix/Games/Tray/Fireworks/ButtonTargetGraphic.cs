using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Fireworks
{
	public class ButtonTargetGraphic : MonoBehaviour
	{
		public Button button;

		public Image icon;

		public Text text;

		public bool targetIsImage;

		public Color normalColor;

		public Color highlightedColor;

		public Color pressedColor;

		public Color disabledColor;

		private bool mIsPressed;

		public void SetNormal()
		{
			mIsPressed = false;
			if (targetIsImage)
			{
				if (icon != null)
				{
					icon.color = normalColor;
				}
			}
			else if (text != null)
			{
				text.color = normalColor;
			}
		}

		public void SetPressed()
		{
			if (!(button != null) || !button.IsInteractable())
			{
				return;
			}
			mIsPressed = true;
			if (targetIsImage)
			{
				if (icon != null)
				{
					icon.color = pressedColor;
				}
			}
			else if (text != null)
			{
				text.color = pressedColor;
			}
		}

		private void Update()
		{
			if (button != null && !button.IsInteractable())
			{
				if (targetIsImage)
				{
					if (icon != null)
					{
						icon.color = disabledColor;
					}
				}
				else if (text != null)
				{
					text.color = disabledColor;
				}
			}
			else
			{
				if (!button.IsInteractable() || mIsPressed)
				{
					return;
				}
				if (targetIsImage)
				{
					if (icon != null)
					{
						icon.color = normalColor;
					}
				}
				else if (text != null)
				{
					text.color = normalColor;
				}
			}
		}
	}
}
