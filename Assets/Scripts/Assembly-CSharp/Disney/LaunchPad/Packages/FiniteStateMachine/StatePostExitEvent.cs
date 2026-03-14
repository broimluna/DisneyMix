using Disney.LaunchPad.Packages.EventSystem;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class StatePostExitEvent : BaseEvent
	{
		public StateChangeArgs mArgs;

		public StatePostExitEvent(StateChangeArgs args)
		{
			mArgs = args;
		}
	}
}
