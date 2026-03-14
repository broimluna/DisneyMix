using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray
{
	[RequireComponent(typeof(Text))]
	public class UICounter : MonoBehaviour
	{
		public enum CounterPunchType
		{
			None = 0,
			Scale = 1,
			Position = 2
		}

		protected const float PUNCH_TIME = 0.2f;

		public Ease CountEase = Ease.OutQuad;

		public string FormatString = "{0}";

		public CounterPunchType PunchType;

		public Vector3 PunchAmount = Vector3.one;

		protected Text mCounterText;

		protected int mCurValue;

		protected int mTargetValue;

		protected Tween mCounterTween;

		protected Tween mPunchTween;

		protected RectTransform mTextTransform;

		protected Color mInitialColor;

		private bool isAnimationActive;

		public int CurValue
		{
			get
			{
				return mCurValue;
			}
		}

		public event Action OnAnimationStarted;

		public event Action OnAnimationFinished;

		public event Action<int> OnCountFinished;

		public event Action<int> OnCountUpdated;

		public void JumpToValue(int value, bool doPunch = true)
		{
			if (!isAnimationActive)
			{
				isAnimationActive = true;
				if (this.OnAnimationStarted != null)
				{
					this.OnAnimationStarted();
				}
			}
			mCurValue = value;
			mCounterText.text = string.Format(FormatString, value);
			if (doPunch && mPunchTween != null)
			{
				mPunchTween.Restart(false);
				return;
			}
			DOVirtual.DelayedCall(0.01f, delegate
			{
				if (isAnimationActive)
				{
					isAnimationActive = false;
					if (this.OnAnimationFinished != null)
					{
						this.OnAnimationFinished();
					}
				}
			});
		}

		public void CountToTargetValue(int aNewTarget, float aDuration = 1f)
		{
			if (mCounterTween != null)
			{
				mCounterTween.Kill();
			}
			mTargetValue = aNewTarget;
			if (aDuration > 0f)
			{
				if (!isAnimationActive)
				{
					isAnimationActive = true;
					if (this.OnAnimationStarted != null)
					{
						this.OnAnimationStarted();
					}
				}
				mCounterTween = DOVirtual.Float(mCurValue, aNewTarget, aDuration, UpdateCounter);
			}
			else
			{
				JumpToValue(aNewTarget);
				if (this.OnCountFinished != null)
				{
					this.OnCountFinished(CurValue);
				}
			}
		}

		public void ForceText(string aString)
		{
			mCounterText.text = aString;
		}

		protected void Awake()
		{
			mCounterText = GetComponent<Text>();
			mTextTransform = mCounterText.GetComponent<RectTransform>();
			if (PunchType == CounterPunchType.Scale)
			{
				mPunchTween = mTextTransform.DOPunchScale(PunchAmount, 0.2f, 5, 0f).OnComplete(OnPunchComplete);
			}
			else if (PunchType == CounterPunchType.Position)
			{
				mPunchTween = mTextTransform.DOPunchAnchorPos(PunchAmount, 0.2f, 2, 0f).OnComplete(OnPunchComplete);
			}
			if (mPunchTween != null)
			{
				mPunchTween.Pause();
				mPunchTween.SetAutoKill(false);
			}
			mInitialColor = mCounterText.color;
		}

		protected void UpdateCounter(float aCurValue)
		{
			int num = Mathf.FloorToInt(aCurValue);
			if (num == mCurValue)
			{
				return;
			}
			mCurValue = num;
			mCounterText.text = string.Format(FormatString, CurValue);
			if (mPunchTween != null)
			{
				mPunchTween.Restart(false);
			}
			if (mCurValue == mTargetValue)
			{
				if (this.OnCountFinished != null)
				{
					this.OnCountFinished(num);
				}
				if (mPunchTween == null && isAnimationActive)
				{
					isAnimationActive = false;
					if (this.OnAnimationFinished != null)
					{
						this.OnAnimationFinished();
					}
				}
			}
			else if (this.OnCountUpdated != null)
			{
				this.OnCountUpdated(num);
			}
		}

		private void OnPunchComplete()
		{
			if (CurValue == mTargetValue && isAnimationActive)
			{
				isAnimationActive = false;
				if (this.OnAnimationFinished != null)
				{
					this.OnAnimationFinished();
				}
			}
		}
	}
}
