using Disney.LaunchPad.Packages.EventSystem;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class StateTraverserTransitionBeganEvent : BaseEvent
	{
		public State mPreviousState;

		public State mNewState;

		public Signal mSignal;

		public StateTraverserTransitionBeganEvent(State previousState, State newState, Signal signal)
		{
			mPreviousState = previousState;
			mNewState = newState;
			mSignal = signal;
		}
	}
}
