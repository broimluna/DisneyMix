using Disney.LaunchPad.Packages.EventSystem;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class TransitionCompletedEvent : BaseEvent
	{
		public StateChangeArgs StateChangeDetails;

		public TransitionCompletedEvent(StateChangeArgs stateChangeDetails)
		{
			StateChangeDetails = stateChangeDetails;
		}

		private TransitionCompletedEvent()
		{
		}
	}
}
