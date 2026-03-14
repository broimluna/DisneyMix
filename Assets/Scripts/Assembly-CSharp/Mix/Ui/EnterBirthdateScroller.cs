using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class EnterBirthdateScroller : MonoBehaviour
	{
		public enum ScrollState
		{
			Stopped = 0,
			Dragging = 1,
			Moving = 2,
			Snapping = 3
		}

		public delegate void OnDragCallback(bool aIsDragging);

		public delegate void OnValueChangedCallback(int aNewValue);

		private const float SCROLL_THRESHOLD_SCALE_SQ = 1.44f;

		private const float SNAP_SPEED = 15f;

		private const float EPSILON = 0.0005f;

		public OnDragCallback OnDragStateChanged;

		public OnValueChangedCallback OnValueChanged;

		private ScrollRect mScrollRect;

		private ScrollState mState;

		private int mCurValue;

		private float mRange;

		private float mSnapToPos;

		private float mSnapSpeedThresholdSq;

		private string mScrollSfx;

		public int mMinVal { get; private set; }

		public int mMaxVal { get; private set; }

		public int CurValue
		{
			get
			{
				return mCurValue;
			}
			set
			{
				if (value >= mMinVal && value <= mMaxVal)
				{
					mCurValue = value;
					mSnapToPos = CalculateSnapPos(mCurValue);
					mState = ScrollState.Snapping;
					StartCoroutine(EaseToSnap());
				}
			}
		}

		public int MaxValue
		{
			set
			{
				mMaxVal = value;
				mRange = mMaxVal - mMinVal;
				mSnapToPos = CalculateSnapPos(mCurValue);
			}
		}

		public void Init(int aStartingValue, int aMin, int aMax, float aBeginSnapSpeedThreshold, string aScrollSfx)
		{
			mMinVal = aMin;
			MaxValue = aMax;
			mRange = mMaxVal - mMinVal;
			mSnapSpeedThresholdSq = aBeginSnapSpeedThreshold * aBeginSnapSpeedThreshold;
			mScrollSfx = aScrollSfx;
			CurValue = aStartingValue;
			mState = ScrollState.Snapping;
			StartCoroutine(EaseToSnap());
		}

		public void Enable(bool aEnable)
		{
			mScrollRect.enabled = aEnable;
		}

		public void OnDragBegin()
		{
			mState = ScrollState.Dragging;
			StopCoroutine(EaseToSnap());
			if (OnDragStateChanged != null)
			{
				OnDragStateChanged(true);
			}
		}

		public void OnDragEnd()
		{
			mState = ScrollState.Moving;
		}

		private void Update()
		{
			if (mState == ScrollState.Dragging || mState == ScrollState.Moving)
			{
				int num = CalculateValueFromPosition();
				if (num != mCurValue)
				{
					mCurValue = num;
					PlayTickSound();
				}
			}
			if (mState == ScrollState.Moving && mScrollRect.velocity.sqrMagnitude <= mSnapSpeedThresholdSq * 1.44f)
			{
				mState = ScrollState.Snapping;
				StartCoroutine(EaseToSnap());
			}
		}

		private void Awake()
		{
			mScrollRect = GetComponent<ScrollRect>();
		}

		private int CalculateValueFromPosition()
		{
			float f = mScrollRect.verticalNormalizedPosition * mRange;
			float num = Mathf.Round(f);
			mSnapToPos = Mathf.Clamp(num / mRange, 0f, 1f);
			return Convert.ToInt32(Mathf.Lerp(mMaxVal, mMinVal, mSnapToPos));
		}

		private IEnumerator EaseToSnap()
		{
			while (Mathf.Abs(mScrollRect.verticalNormalizedPosition - mSnapToPos) > 0.0005f && mState == ScrollState.Snapping)
			{
				mScrollRect.verticalNormalizedPosition = Mathf.Lerp(mScrollRect.verticalNormalizedPosition, mSnapToPos, Time.deltaTime * 15f);
				yield return null;
			}
			if (mState == ScrollState.Snapping)
			{
				mScrollRect.StopMovement();
				mScrollRect.verticalNormalizedPosition = mSnapToPos;
				mState = ScrollState.Stopped;
				if (OnValueChanged != null)
				{
					OnValueChanged(mCurValue);
				}
				if (OnDragStateChanged != null)
				{
					OnDragStateChanged(false);
				}
			}
		}

		private void PlayTickSound()
		{
			Singleton<SoundManager>.Instance.PlaySoundEvent(mScrollSfx);
		}

		private float CalculateSnapPos(int aCurValue)
		{
			float value = (float)(mMaxVal - aCurValue) / mRange;
			return Mathf.Clamp(value, 0f, 1f);
		}
	}
}
