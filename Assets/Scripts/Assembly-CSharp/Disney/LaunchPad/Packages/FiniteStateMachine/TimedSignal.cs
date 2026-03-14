using Disney.LaunchPad.Packages.MathUtilities;
using UnityEngine;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class TimedSignal : Signal
	{
		private bool mSignalActive;

		private float mElapsedSecs;

		[SerializeField]
		private float mDurationSecs;

		public float Duration
		{
			get
			{
				return mDurationSecs;
			}
			set
			{
				mDurationSecs = value;
			}
		}

		public float SecondsRemaining
		{
			get
			{
				return (mDurationSecs - mElapsedSecs).Clamp(0f, mDurationSecs);
			}
		}

		public float SecondsElapsed
		{
			get
			{
				return mElapsedSecs;
			}
		}

		public override bool ActivateSignal()
		{
			mSignalActive = true;
			return true;
		}

		public override void Reset()
		{
			mSignalActive = false;
			mElapsedSecs = 0f;
		}

		public override bool IsSignaled()
		{
			return mSignalActive;
		}

		public void Update()
		{
			mElapsedSecs += Time.deltaTime;
			if (mElapsedSecs >= mDurationSecs)
			{
				ActivateSignal();
			}
		}
	}
}
