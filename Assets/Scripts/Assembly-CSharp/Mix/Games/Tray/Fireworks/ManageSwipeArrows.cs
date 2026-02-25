using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Fireworks
{
	public class ManageSwipeArrows : MonoBehaviour
	{
		public Animator selectionArrowsAnim;

		private GameObject mLeftArrow;

		private GameObject mRightArrow;

		private Button mLeftArrowButton;

		private Button mRightArrowButton;

		private void Start()
		{
			mLeftArrow = base.gameObject.transform.GetChild(0).gameObject;
			mRightArrow = base.gameObject.transform.GetChild(2).gameObject;
			if (mLeftArrow != null)
			{
				mLeftArrowButton = mLeftArrow.transform.GetChild(0).GetComponent<Button>();
			}
			if (mRightArrow != null)
			{
				mRightArrowButton = mRightArrow.transform.GetChild(0).GetComponent<Button>();
			}
		}

		public void ToggleLeftArrow(bool isActive)
		{
			if (mLeftArrow != null)
			{
				mLeftArrow.SetActive(isActive);
			}
		}

		public void ToggleRightArrow(bool isActive)
		{
			if (mRightArrow != null)
			{
				mRightArrow.SetActive(isActive);
			}
		}

		public void PlayArrowExitAnimation()
		{
			if (mLeftArrowButton != null)
			{
				mLeftArrowButton.interactable = false;
			}
			if (mRightArrowButton != null)
			{
				mRightArrowButton.interactable = false;
			}
		}
	}
}
