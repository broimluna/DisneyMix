using System;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.FortuneCookie
{
	public class Cookie : MonoBehaviour
	{
		public Camera GameCamera;

		public float SelectionRise;

		public float SelectionScale;

		public float SelectionTime;

		protected CookieState mCurrentState = CookieState.NotSelected;

		protected CookieState mTargetState;

		private Vector3 mInitialPos;

		private Quaternion mInitialRot;

		private Rigidbody mRigidbody;

		private Animator mAnimator;

		private bool mIsOnPlate;

		public CookieState State
		{
			get
			{
				return mCurrentState;
			}
			protected set
			{
				mCurrentState = value;
			}
		}

		public event EventHandler<CookieCollisionEnterEventArgs> CookieCollisionWithTrayEnter;

		public event EventHandler<CookieCollisionEnterEventArgs> CookieCollisionWithCookieEnter;

		private void Start()
		{
			mRigidbody = GetComponent<Rigidbody>();
			mAnimator = GetComponent<Animator>();
		}

		private void OnCollisionEnter(Collision collision)
		{
			EventHandler<CookieCollisionEnterEventArgs> eventHandler = null;
			if (collision.gameObject.GetComponent<Cookie>() == null)
			{
				eventHandler = this.CookieCollisionWithTrayEnter;
				if (eventHandler != null)
				{
					eventHandler(this, new CookieCollisionEnterEventArgs(collision));
				}
			}
			else
			{
				eventHandler = this.CookieCollisionWithCookieEnter;
				if (eventHandler != null)
				{
					eventHandler(this, new CookieCollisionEnterEventArgs(collision));
				}
			}
		}

		public void UpdateState(CookieState state)
		{
			if (mCurrentState != CookieState.Transition)
			{
				mTargetState = state;
				if (mTargetState == CookieState.NotSelected && mCurrentState == CookieState.Selected)
				{
					State = CookieState.Transition;
					SetNotSelectedState();
				}
				if (mTargetState == CookieState.Selected && mCurrentState == CookieState.NotSelected)
				{
					State = CookieState.Transition;
					SetSelectedState();
				}
			}
		}

		protected void SetSelectedState()
		{
			mInitialPos = base.transform.position;
			mInitialRot = base.transform.rotation;
			Sequence s = DOTween.Sequence();
			s.Append(base.transform.DOScale(base.transform.localScale * SelectionScale, SelectionTime));
			s.Join(base.transform.DOMove(GameCamera.transform.position + SelectionRise * GameCamera.transform.forward, SelectionTime).SetEase(Ease.OutBounce));
			s.Join(base.transform.DORotate((base.transform.parent.parent.rotation * Quaternion.Euler(new Vector3(150f, -30f, 180f))).eulerAngles, SelectionTime));
			s.AppendCallback(delegate
			{
				mAnimator.enabled = true;
				mAnimator.applyRootMotion = true;
				mRigidbody.linearVelocity = Vector3.zero;
				mRigidbody.angularVelocity = Vector3.zero;
				mRigidbody.constraints = RigidbodyConstraints.FreezePosition;
				mRigidbody.freezeRotation = true;
				mRigidbody.useGravity = false;
				State = mTargetState;
			});
		}

		protected void SetNotSelectedState()
		{
			mAnimator.enabled = false;
			base.transform.GetChild(0).gameObject.SetActive(false);
			mAnimator.applyRootMotion = false;
			Sequence s = DOTween.Sequence();
			s.Append(base.transform.DOMove(mInitialPos, SelectionTime));
			s.Join(base.transform.DORotate(mInitialRot.eulerAngles, SelectionTime));
			s.Join(base.transform.DOScale(base.transform.localScale / SelectionScale, SelectionTime));
			s.AppendCallback(delegate
			{
				mRigidbody.constraints = RigidbodyConstraints.None;
				mRigidbody.freezeRotation = false;
				mRigidbody.useGravity = true;
				State = mTargetState;
			});
		}
	}
}
