namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class ImmediateTransition : Transition
	{
		public override void Perform(StateChangeArgs stateChangeDetails)
		{
			m_stateChangeDetails = stateChangeDetails;
			SetTransitionEnabled(true);
			stateChangeDetails.StartState.RaisePreExitEvent(stateChangeDetails);
			stateChangeDetails.StartState.RaiseExitEvent(stateChangeDetails);
			stateChangeDetails.StartState.RaisePostExitEvent(stateChangeDetails);
			stateChangeDetails.EndState.RaisePreEnterEvent(stateChangeDetails);
			stateChangeDetails.EndState.RaiseEnterEvent(stateChangeDetails);
			stateChangeDetails.EndState.RaisePostEnterEvent(stateChangeDetails);
			RaiseTransitionCompletedEvent();
		}

		public override void Reset()
		{
			EventDispatcher.ClearAll();
			SetTransitionEnabled(false);
		}
	}
}
