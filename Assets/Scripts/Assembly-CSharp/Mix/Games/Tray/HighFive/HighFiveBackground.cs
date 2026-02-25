using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveBackground : MonoBehaviour
	{
		public float InTime = 0.15f;

		public float HoldTime = 0.05f;

		public float OutTime = 0.3f;

		private SpriteRenderer mSprite;

		private void Awake()
		{
			mSprite = GetComponent<SpriteRenderer>();
		}

		public void DoFlash(Color aTargetColor)
		{
			mSprite.DOKill();
			mSprite.DOColor(aTargetColor, InTime).SetEase(Ease.OutCubic).OnComplete(FadeOut);
		}

		private void FadeOut()
		{
			mSprite.DOColor(Color.clear, OutTime).SetDelay(HoldTime).SetEase(Ease.InCubic);
		}
	}
}
