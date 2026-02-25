using Disney.LaunchPad.Packages.EventSystem;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class StatePreEnterEvent : BaseEvent
	{
		public StateChangeArgs mArgs;

		public StatePreEnterEvent(StateChangeArgs args)
		{
			mArgs = args;
		}
	}
}
