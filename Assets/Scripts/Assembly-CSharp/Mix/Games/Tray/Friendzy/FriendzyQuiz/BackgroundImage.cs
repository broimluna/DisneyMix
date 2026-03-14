using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.FriendzyQuiz
{
	public class BackgroundImage : MonoBehaviour
	{
		private Image mBackgroundImage;

		private Color FADE_COLOR = new Color(0.35f, 0.35f, 0.35f, 1f);

		private Color FULL_COLOR = Color.white;

		private void Awake()
		{
			mBackgroundImage = GetComponent<Image>();
		}

		public void ToggleFade(bool aShouldDim, float aDuration)
		{
			Color colorChange = FULL_COLOR;
			if (aShouldDim)
			{
				colorChange = FADE_COLOR;
				mBackgroundImage.color = FULL_COLOR;
			}
			else
			{
				mBackgroundImage.color = FADE_COLOR;
			}
			DOVirtual.DelayedCall(aDuration, delegate
			{
				mBackgroundImage.DOColor(colorChange, aDuration);
			});
		}

		public void ToggleEnable(bool aEnabled)
		{
			mBackgroundImage.enabled = aEnabled;
		}

		public void SetSprite(Sprite aSprite)
		{
			mBackgroundImage.sprite = aSprite;
		}
	}
}
