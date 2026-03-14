using System.Collections.Generic;
using Disney.LaunchPad.Packages.EventSystem;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public interface IState
	{
		EventDispatcher EventDispatcher { get; }

		string name { get; set; }

		void AddSignal(Signal signal);

		void RemoveSignal(Signal signal);

		bool HasSignal(State endState);

		void ResetSignals();

		List<Signal> GetSignals();

		void RaisePreEnterEvent(StateChangeArgs stateChangeDetails);

		void RaiseEnterEvent(StateChangeArgs stateChangeDetails);

		void RaisePostEnterEvent(StateChangeArgs stateChangeDetails);

		void RaisePreUpdateEvent();

		void RaiseUpdateEvent();

		void RaisePostUpdateEvent();

		void RaisePreExitEvent(StateChangeArgs stateChangeDetails);

		void RaiseExitEvent(StateChangeArgs stateChangeDetails);

		void RaisePostExitEvent(StateChangeArgs stateChangeDetails);
	}
}
