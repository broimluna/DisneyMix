using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveTargetSprite : HighFiveTarget
	{
		public Color GlowColor = Color.yellow;

		public Color InactiveColor = Color.grey;

		public Color ActiveColor = Color.white;

		public SpriteRenderer Visuals;

		public GameObject GlowEffect;

		protected override Sequence ShowSequence()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.AppendCallback(ResetTarget);
			sequence.AppendInterval(mWindupTime);
			sequence.AppendCallback(base.SetHitStateEarly);
			sequence.Append(mTransform.DOScale(mInitialScale, mGrowTime).SetEase(Ease.Linear));
			sequence.Join(mTransform.DOLocalMove(new Vector3(0f, 0f, 3f), mGrowTime).From().SetEase(Ease.InOutCubic));
			sequence.AppendCallback(SetHitStateAwesome);
			sequence.AppendInterval(mHoldTime);
			sequence.AppendCallback(SetHitStateGoodLate);
			Tween t = Visuals.DOColor(ActiveColor, mGrowTime - 0.1f).SetEase(Ease.Linear);
			sequence.Insert(mGrowTime, t);
			sequence.InsertCallback(mGrowTime + mGrowTime - 0.1f, SetHitStateGoodEarly);
			sequence.SetId(base.gameObject);
			return sequence;
		}

		protected override Sequence AutoHideSequence()
		{
			Sequence sequence = DOTween.Sequence();
			Tween t = mTransform.DOScale(Vector3.zero, mShrinkTime).SetEase(Ease.InCubic);
			sequence.Append(t);
			sequence.AppendCallback(base.SetHitStateMissEnd);
			sequence.AppendInterval(0.125f);
			sequence.AppendCallback(base.OnTargetHiddenNoHit);
			t = Visuals.DOColor(InactiveColor, mShrinkTime - 0.1f).SetEase(Ease.Linear);
			sequence.Insert(0.1f, t);
			sequence.InsertCallback(0.1f, base.SetHitStateLate);
			sequence.SetId(base.gameObject);
			return sequence;
		}

		protected override void OnHitHideSequence()
		{
			Sequence sequence = DOTween.Sequence();
			Tween t = mTransform.DOScale(Vector3.zero, mShrinkTime).SetEase(Ease.InCubic);
			sequence.Append(t);
			t = Visuals.DOColor(InactiveColor, mShrinkTime - 0.1f).SetEase(Ease.Linear);
			sequence.Insert(0.1f, t);
			sequence.AppendInterval(0.15f);
			sequence.AppendCallback(Cleanup);
			sequence.SetId(base.gameObject);
		}

		protected override void ResetTarget()
		{
			base.ResetTarget();
			mTransform.localScale = Vector3.zero;
			Visuals.gameObject.SetActive(true);
			Visuals.color = InactiveColor;
			GlowEffect.SetActive(false);
		}

		protected override void SetHitStateGoodEarly()
		{
			base.SetHitStateGoodEarly();
			Visuals.color = ActiveColor;
		}

		protected override void SetHitStateGoodLate()
		{
			base.SetHitStateGoodLate();
			Visuals.color = ActiveColor;
		}

		protected override void Awake()
		{
			base.Awake();
			mTransform = Visuals.transform;
			mInitialScale = mTransform.localScale;
		}

		protected override void Cleanup()
		{
			base.Cleanup();
			Visuals.gameObject.SetActive(false);
		}

		protected override void EnableGlow()
		{
			GlowEffect.SetActive(true);
			Visuals.color = GlowColor;
		}

		protected override void DisableGlow()
		{
			GlowEffect.SetActive(false);
			Visuals.color = ActiveColor;
		}
	}
}
