using Disney.LaunchPad.Packages.EventSystem;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class StateEnterEvent : BaseEvent
	{
		public StateChangeArgs mArgs;

		public StateEnterEvent(StateChangeArgs args)
		{
			mArgs = args;
		}
	}
}
