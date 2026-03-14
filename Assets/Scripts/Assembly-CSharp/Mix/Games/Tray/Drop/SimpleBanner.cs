using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class SimpleBanner : MonoBehaviour
	{
		public Animator BannerAnimator;

		private int bannerRandomizationAnimatorId;

		private float lastRandomizationTime;

		private void Start()
		{
			bannerRandomizationAnimatorId = Animator.StringToHash("Random");
		}

		private void Update()
		{
			if (Time.time - lastRandomizationTime > 0.5f)
			{
				BannerAnimator.SetFloat(bannerRandomizationAnimatorId, Random.value);
				lastRandomizationTime = Time.time;
			}
		}

		public void Show()
		{
			BannerAnimator.SetTrigger("Unfurl");
		}

		public void Hide()
		{
			BannerAnimator.SetTrigger("Furl");
		}
	}
}
