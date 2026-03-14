using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveUiMultiplier : MonoBehaviour
	{
		[Serializable]
		public class LevelSettings
		{
			public float Scale = 1f;

			public Color BaseColor = Color.white;

			public float Period = 0.5f;
		}

		private const float SEGMENT_ANIM_TIME = 0.125f;

		private const float MULTIPLIER_ANIM_TIME = 0.25f;

		public Image Progress;

		public Image Glow;

		public UICounter Counter;

		public LevelSettings[] GlowSettings;

		private Tween mProgressTween;

		public void Show()
		{
			base.gameObject.SetActive(true);
			SetGlowEffect(0, 0);
			Counter.JumpToValue(1, false);
			SetProgress(0f);
		}

		public void Hide()
		{
			base.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InCubic).OnComplete(delegate
			{
				base.gameObject.SetActive(false);
			});
			if (mProgressTween != null)
			{
				mProgressTween.Kill();
				mProgressTween = null;
			}
			DOTween.Kill(base.gameObject);
		}

		public void UpdateProgress(float aTargetValue)
		{
			if (mProgressTween != null)
			{
				mProgressTween.Kill();
			}
			float fillAmount = Progress.fillAmount;
			if (fillAmount != aTargetValue)
			{
				Ease ease = Ease.OutBack;
				if (fillAmount < aTargetValue)
				{
					ease = Ease.OutCubic;
				}
				mProgressTween = DOVirtual.Float(fillAmount, aTargetValue, 0.125f, SetProgress).SetEase(ease);
			}
		}

		public void UpdateMultiplier(int aNewVal)
		{
			int curValue = Counter.CurValue;
			Counter.CountToTargetValue(aNewVal, 0.25f);
			SetGlowEffect(aNewVal, curValue);
		}

		private void SetProgress(float aProgress)
		{
			Progress.fillAmount = aProgress;
		}

		private void SetGlowEffect(int newLevel, int oldLevel)
		{
			DOTween.Kill(base.gameObject);
			newLevel--;
			oldLevel--;
			Glow.gameObject.SetActive(newLevel > 0);
			if (newLevel >= 0)
			{
				LevelSettings levelSettings = GlowSettings[oldLevel];
				LevelSettings levelSettings2 = GlowSettings[newLevel];
				Glow.color = levelSettings.BaseColor;
				Glow.transform.localScale = levelSettings.Scale * Vector3.one;
				Glow.DOColor(levelSettings2.BaseColor, levelSettings2.Period).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo)
					.SetId(base.gameObject);
				Glow.transform.DOScale(levelSettings2.Scale, levelSettings2.Period).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo)
					.SetId(base.gameObject);
			}
		}
	}
}
