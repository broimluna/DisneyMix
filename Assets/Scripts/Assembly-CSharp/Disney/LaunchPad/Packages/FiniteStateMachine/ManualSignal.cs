namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class ManualSignal : Signal
	{
		private bool mSignalActive;

		public override bool ActivateSignal()
		{
			mSignalActive = true;
			return true;
		}

		public override void Reset()
		{
			mSignalActive = false;
		}

		public override bool IsSignaled()
		{
			return mSignalActive;
		}
	}
}
