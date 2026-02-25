using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mix.Games.Tray.HighFive
{
	public abstract class HighFiveTarget : MonoBehaviour, IEventSystemHandler, IPointerClickHandler
	{
		protected const float AWESOME_WINDOW = 0.2f;

		protected const float GOOD_WINDOW = 0.1f;

		protected const float HOLD_COLLIDER_AFTER_HIDING_TIME = 0.125f;

		public int ID;

		public Vector3 OnHitPunchPos;

		public Vector3 OnHitPunchRot;

		public ParticleSystem WindupVfx;

		protected GameObject mGameObject;

		protected Transform mTransform;

		protected Collider2D mCollider;

		protected Vector3 mInitialScale;

		protected HitType mCurHitState;

		protected bool mAutoHide;

		protected float mWindupTime;

		protected float mGrowTime;

		protected float mShrinkTime;

		protected float mHoldTime;

		public event Action<HighFiveTarget, HitType, Vector3> OnHit;

		public event Action<HighFiveTarget, HitType> OnComplete;

		public void ShowHand(float aWindupTime, float aGrowTime, float aShrinkTime, bool aAutoHide)
		{
			base.gameObject.SetActive(true);
			DOTween.Kill(base.gameObject, true);
			mWindupTime = aWindupTime;
			mGrowTime = aGrowTime;
			mShrinkTime = aShrinkTime;
			mHoldTime = 0.2f;
			mAutoHide = aAutoHide;
			ShowSequence();
		}

		public virtual void OnPointerClick(PointerEventData aData)
		{
			DOTween.Kill(base.gameObject);
			DisableCollider();
			mTransform.DOPunchPosition(OnHitPunchPos, 0.25f);
			mTransform.DOPunchRotation(OnHitPunchRot, 0.5f).OnComplete(OnHitHideSequence);
			TargetHit(this, mCurHitState, aData.position);
		}

		public Vector3 GetDefaultHitMarkerWorldPos()
		{
			return mTransform.position + mTransform.up * mCollider.offset.y;
		}

		public virtual void Init()
		{
			base.gameObject.SetActive(false);
		}

		protected abstract Sequence ShowSequence();

		protected abstract Sequence AutoHideSequence();

		protected abstract void OnHitHideSequence();

		protected void TargetHit(HighFiveTarget aTarget, HitType aHitType, Vector3 aHitLoc)
		{
			if (this.OnHit != null)
			{
				this.OnHit(aTarget, aHitType, aHitLoc);
			}
		}

		protected void TargetComplete(HighFiveTarget aTarget, HitType aHitType)
		{
			if (this.OnComplete != null)
			{
				this.OnComplete(aTarget, aHitType);
			}
		}

		protected virtual void ResetTarget()
		{
			((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("HandPrep");
			mCollider.enabled = true;
			mCurHitState = HitType.TooFast;
			WindupVfx.Play();
		}

		protected void SetHitStateEarly()
		{
			((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("HandAppears");
			mCurHitState = HitType.TooFast;
			WindupVfx.Stop();
		}

		protected virtual void SetHitStateGoodEarly()
		{
			mCurHitState = HitType.Good;
		}

		protected virtual void SetHitStateAwesome()
		{
			((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("HandReady");
			mCurHitState = HitType.Awesome;
			EnableGlow();
		}

		protected virtual void SetHitStateGoodLate()
		{
			mCurHitState = HitType.Good;
			DisableGlow();
			if (mAutoHide)
			{
				AutoHideSequence();
			}
		}

		protected void SetHitStateLate()
		{
			((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("HandDisappears");
			mCurHitState = HitType.TooSlow;
		}

		protected void SetHitStateMissEnd()
		{
			((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("HandDisappears");
			mCurHitState = HitType.Miss;
		}

		protected virtual void Awake()
		{
			mGameObject = base.gameObject;
			mCollider = GetComponentInChildren<Collider2D>();
			mTransform = base.transform;
			mInitialScale = mTransform.localScale;
		}

		protected void OnTargetHiddenNoHit()
		{
			TargetHit(this, HitType.Miss, GetDefaultHitMarkerWorldPos());
			Cleanup();
		}

		protected virtual void Cleanup()
		{
			TargetComplete(this, mCurHitState);
			DisableCollider();
			base.gameObject.SetActive(false);
		}

		protected void DisableCollider()
		{
			mCollider.enabled = false;
		}

		protected abstract void EnableGlow();

		protected abstract void DisableGlow();
	}
}
