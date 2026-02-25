using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.FriendzyQuiz
{
	[RequireComponent(typeof(CanvasGroup))]
	public class SplashScreen : MonoBehaviour
	{
		private const float FADE_DURATION = 0.25f;

		private const float DELAY = 0.5f;

		private CanvasGroup mCanvasGroup;

		public Image SplashScreenLogo;

		public Text SplashScreenTitle;

		public bool DataForQuizLoaded { get; set; }

		private void Start()
		{
			mCanvasGroup = GetComponent<CanvasGroup>();
			mCanvasGroup.alpha = 1f;
		}

		public void SetLogoImage(Sprite aLogo)
		{
			SplashScreenLogo.sprite = aLogo;
		}

		public void SetQuizTitle(string aTitle)
		{
			SplashScreenTitle.text = aTitle;
		}

		public Sequence Disappear()
		{
			Sequence sequence = DOTween.Sequence();
			Tween t = mCanvasGroup.DOFade(0f, 0.25f);
			sequence.Append(t);
			sequence.AppendInterval(0.5f);
			return sequence;
		}
	}
}
