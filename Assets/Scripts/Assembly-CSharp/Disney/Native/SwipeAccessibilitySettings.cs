using UnityEngine;

namespace Disney.Native
{
	public class SwipeAccessibilitySettings : AccessibilitySettings
	{
		public GameObject ReferenceSwipeButton;

		public SlideRecognizer slideRecognizer;

		private bool IsOpen;

		private void Start()
		{
			Setup();
		}

		public override void Setup()
		{
			base.Setup();
		}

		public void SetSwipeButton(GameObject aButton)
		{
			ReferenceSwipeButton = aButton;
		}

		public void OnClick()
		{
			if (ReferenceSwipeButton == null)
			{
				ReferenceSwipeButton = base.transform.parent.gameObject;
			}
			if (slideRecognizer == null)
			{
				slideRecognizer = ReferenceSwipeButton.GetComponent<SlideRecognizer>();
			}
			if (slideRecognizer != null)
			{
				if (IsOpen)
				{
					slideRecognizer.SwitchControlSwipeClose();
				}
				else
				{
					slideRecognizer.SwitchControlSwipeOpen();
				}
				IsOpen = !IsOpen;
			}
		}

		public void OnDeleteClicked()
		{
		}

		public void OnDeleteConfirmClicked()
		{
		}

		public void OnDeleteRejectClicked()
		{
		}

		public void OnEditClicked()
		{
		}
	}
}
