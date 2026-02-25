using Disney.LaunchPad.Packages.EventSystem;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public interface ITransition
	{
		EventDispatcher EventDispatcher { get; }

		void Perform(StateChangeArgs stateChangeDetails);

		void Reset();
	}
}
