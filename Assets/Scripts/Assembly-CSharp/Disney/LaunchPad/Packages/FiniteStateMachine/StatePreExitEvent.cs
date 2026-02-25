using Disney.LaunchPad.Packages.EventSystem;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class StatePreExitEvent : BaseEvent
	{
		public StateChangeArgs mArgs;

		public StatePreExitEvent(StateChangeArgs args)
		{
			mArgs = args;
		}
	}
}
