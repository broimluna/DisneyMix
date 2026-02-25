using Disney.LaunchPad.Packages.EventSystem;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class StateTraverserTransitionEndedEvent : BaseEvent
	{
		public State mPreviousState;

		public State mNewState;

		public Signal mSignal;

		public StateTraverserTransitionEndedEvent(State previousState, State newState, Signal signal)
		{
			mPreviousState = previousState;
			mNewState = newState;
			mSignal = signal;
		}
	}
}
