using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveTargetTentacle : HighFiveTarget
	{
		protected enum AnimState
		{
			Hidden = 0,
			Raise = 1,
			Lower = 2,
			OnHit = 3
		}

		public GameObject MeshObj;

		public Vector3 OnHitRotation = Vector3.zero;

		public Vector3 OnHitPosition = Vector3.zero;

		public float OnHitRotateTime = 0.25f;

		protected Animator mAnimator;

		protected int mRaiseTriggernHash;

		protected int mLowerTriggerHash;

		protected int mOnHitTriggerHash;

		protected int mHitTypeHash;

		protected int mRaiseSpeedHash;

		protected int mLowerSpeedHash;

		protected float mRaiseAnimBaseSpeed;

		protected float mLowerAnimBaseSpeed;

		protected Vector3 mInitialPosition;

		protected Quaternion mInitialRotation;

		protected SkinnedMeshRenderer mMeshRenderer;

		protected AnimState mAnimState;

		public override void Init()
		{
			base.Init();
			WindupVfx.Stop();
			mAnimState = AnimState.Hidden;
		}

		protected override void Awake()
		{
			base.Awake();
			mAnimator = MeshObj.GetComponentInChildren<Animator>();
			mRaiseTriggernHash = Animator.StringToHash("Raise");
			mLowerTriggerHash = Animator.StringToHash("Lower");
			mOnHitTriggerHash = Animator.StringToHash("OnHit");
			mHitTypeHash = Animator.StringToHash("HitType");
			mRaiseSpeedHash = Animator.StringToHash("RaiseSpeed");
			mLowerSpeedHash = Animator.StringToHash("LowerSpeed");
			mMeshRenderer = MeshObj.GetComponentInChildren<SkinnedMeshRenderer>();
			mMeshRenderer.sortingOrder = -1;
			mInitialPosition = mTransform.localPosition;
			mInitialRotation = mTransform.localRotation;
		}

		public override void OnPointerClick(PointerEventData aData)
		{
			DOTween.Kill(base.gameObject);
			DisableCollider();
			MeshObj.transform.DOPunchPosition(OnHitPunchPos, 0.25f);
			MeshObj.transform.DOPunchRotation(OnHitPunchRot, 0.5f);
			OnHitHideSequence();
			TargetHit(this, mCurHitState, aData.position);
		}

		protected override Sequence ShowSequence()
		{
			mTransform.localPosition = mInitialPosition;
			mTransform.localRotation = mInitialRotation;
			Sequence sequence = DOTween.Sequence();
			sequence.AppendCallback(ResetTarget);
			sequence.AppendInterval(mWindupTime);
			sequence.AppendCallback(base.SetHitStateEarly);
			sequence.AppendCallback(PlayRaiseAnim);
			sequence.SetId(base.gameObject);
			return sequence;
		}

		protected override Sequence AutoHideSequence()
		{
			mAnimState = AnimState.Lower;
			mAnimator.SetTrigger(mLowerTriggerHash);
			AdjustAnimatorSpeedToTargetTime(mShrinkTime, mLowerSpeedHash, 0.8333f);
			return null;
		}

		protected override void OnHitHideSequence()
		{
			mAnimState = AnimState.OnHit;
			DOTween.Kill(base.gameObject);
			mAnimator.SetInteger(mHitTypeHash, (int)mCurHitState);
			mAnimator.SetTrigger(mOnHitTriggerHash);
			mAnimator.SetFloat(mLowerSpeedHash, 1.5f);
			DOVirtual.DelayedCall(0.5f, DisableGlow);
			if (OnHitRotateTime > 0f)
			{
				mTransform.DOLocalRotate(OnHitRotation, OnHitRotateTime).SetEase(Ease.InCubic);
				mTransform.DOLocalMove(OnHitPosition, OnHitRotateTime).SetEase(Ease.InOutCubic);
			}
			DisableCollider();
		}

		protected override void EnableGlow()
		{
			GameUtil.SetLayerRecursively(mMeshRenderer.gameObject, 15);
		}

		protected override void DisableGlow()
		{
			GameUtil.SetLayerRecursively(mMeshRenderer.gameObject, 16);
		}

		protected void PlayRaiseAnim()
		{
			mAnimState = AnimState.Raise;
			mAnimator.SetTrigger(mRaiseTriggernHash);
			AdjustAnimatorSpeedToTargetTime(mGrowTime, mRaiseSpeedHash, 1.41667f);
		}

		protected void AdjustAnimatorSpeedToTargetTime(float aTargetTime, int aAnimParameter, float animLengthSeconds)
		{
			float value = animLengthSeconds / aTargetTime;
			mAnimator.SetFloat(aAnimParameter, value);
		}

		public void StartSweetSpot()
		{
			SetHitStateAwesome();
		}

		public void EndSweetSpot()
		{
			SetHitStateGoodLate();
		}

		public void EndGoodWindow()
		{
			SetHitStateLate();
		}

		public void EndLateWindow()
		{
			SetHitStateMissEnd();
		}

		public void EndAnimation()
		{
			if (mAnimState != AnimState.Hidden)
			{
				if (!mCollider.enabled)
				{
					Cleanup();
				}
				else
				{
					OnTargetHiddenNoHit();
				}
				mAnimState = AnimState.Hidden;
			}
		}
	}
}
