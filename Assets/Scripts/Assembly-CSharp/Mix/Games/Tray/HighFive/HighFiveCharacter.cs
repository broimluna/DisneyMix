using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveCharacter : MonoBehaviour, IEventSystemHandler, IPointerClickHandler
	{
		private const float CONVERSATION_Z_OFFSET = 2f;

		public float BlinkMinTime = 1f;

		public float BlinkMaxTime = 2.5f;

		public float HeadTurnSpeed = 32f;

		public float ResetHeadSpeed = 64f;

		private float mHeadTurnSpeed = 1f;

		private Animator mAnimator;

		private int mAnimBlinkHash;

		private int mAnimHitSelfHash;

		private int mAnimHitTargetHash;

		private int mHappinessHash;

		private int mEmojiCounter;

		private Tween mPunchTween;

		private Tween mOnHitTargetTween;

		private Transform mTransform;

		private Vector3 mTarget;

		private Vector3 mInitalLookAtTarget;

		public void OnPointerClick(PointerEventData aData)
		{
			mAnimator.SetTrigger(mAnimHitSelfHash);
			switch (mEmojiCounter)
			{
			case 0:
				BaseGameController.Instance.AudioManager.PlaySound("EmojiExpression_1");
				break;
			case 1:
				BaseGameController.Instance.AudioManager.PlaySound("EmojiExpression_2");
				break;
			case 2:
				BaseGameController.Instance.AudioManager.PlaySound("EmojiExpression_3");
				break;
			}
			mEmojiCounter = ++mEmojiCounter % 3;
			mPunchTween.Restart(false);
		}

		public void OnHitTarget()
		{
			mAnimator.SetTrigger(mAnimHitTargetHash);
			mOnHitTargetTween.Restart(false);
		}

		public void SetAverageScore(float aAverage)
		{
			mAnimator.SetFloat(mHappinessHash, aAverage);
		}

		public void LookAtTarget(Vector3 aTarget)
		{
			mHeadTurnSpeed = HeadTurnSpeed;
			mTarget = new Vector3(aTarget.x, aTarget.y, aTarget.z - 5f);
		}

		public void ResetLookAt()
		{
			mHeadTurnSpeed = ResetHeadSpeed;
			mTarget = mInitalLookAtTarget;
		}

		public void StartConversation()
		{
			mTransform.DOLocalMoveZ(-2f, 0.25f).SetEase(Ease.OutBack);
		}

		public void StopConversation()
		{
			mTransform.DOLocalMoveZ(0f, 0.15f).SetEase(Ease.OutBack);
		}

		private void Awake()
		{
			mAnimator = GetComponent<Animator>();
			mAnimBlinkHash = Animator.StringToHash("DoBlink");
			mAnimHitSelfHash = Animator.StringToHash("OnSelfHit");
			mAnimHitTargetHash = Animator.StringToHash("OnTargetHit");
			mHappinessHash = Animator.StringToHash("Happiness");
			mEmojiCounter = 0;
			mPunchTween = base.transform.DOPunchPosition(new Vector3(5f, 5f, 0f), 0.5f);
			mPunchTween.Pause();
			mPunchTween.SetAutoKill(false);
			mOnHitTargetTween = base.transform.DOPunchRotation(new Vector3(0f, 0f, 5f), 0.25f, 8);
			mOnHitTargetTween.Pause();
			mOnHitTargetTween.SetAutoKill(false);
			mAnimator.SetFloat(mHappinessHash, 1f);
			mTransform = base.transform.parent;
		}

		private void OnEnable()
		{
			StartCoroutine(WaitForBlink());
		}

		private void Start()
		{
			mInitalLookAtTarget = mTransform.position + 4f * mTransform.forward;
			ResetLookAt();
		}

		private IEnumerator WaitForBlink()
		{
			while (true)
			{
				float delay = Random.Range(BlinkMinTime, BlinkMaxTime);
				yield return new WaitForSeconds(delay);
				DoBlink();
			}
		}

		public void DoBlink()
		{
			mAnimator.SetTrigger(mAnimBlinkHash);
		}

		private void Update()
		{
			Vector3 forward = mTarget - mTransform.position;
			Quaternion to = Quaternion.LookRotation(forward);
			float maxDegreesDelta = mHeadTurnSpeed * Time.deltaTime;
			mTransform.rotation = Quaternion.RotateTowards(mTransform.rotation, to, maxDegreesDelta);
		}

		private void OnDrawGizmos()
		{
			if (mTransform != null)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(mTarget, 0.1f);
				Gizmos.DrawLine(mTransform.position, mTarget);
			}
		}
	}
}
