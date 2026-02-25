using Disney.LaunchPad.Packages.EventSystem;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class StateTraverserStateChangeEvent : BaseEvent
	{
		public State mNewState;

		public StateTraverserStateChangeEvent(State newState)
		{
			mNewState = newState;
		}
	}
}
