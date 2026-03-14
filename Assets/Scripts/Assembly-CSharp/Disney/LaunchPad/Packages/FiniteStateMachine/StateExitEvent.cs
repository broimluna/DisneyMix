using Disney.LaunchPad.Packages.EventSystem;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class StateExitEvent : BaseEvent
	{
		public StateChangeArgs mArgs;

		public StateExitEvent(StateChangeArgs args)
		{
			mArgs = args;
		}
	}
}
