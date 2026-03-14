using System;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class LambdaSignal : Signal
	{
		private Func<bool> mShouldActivateDelegate = () => false;

		public void SetShouldActivateDelegate(Func<bool> shouldActivateDelegate)
		{
			mShouldActivateDelegate = shouldActivateDelegate;
		}

		public override bool ActivateSignal()
		{
			return IsSignaled();
		}

		public override void Reset()
		{
		}

		public override bool IsSignaled()
		{
			return mShouldActivateDelegate();
		}
	}
}
