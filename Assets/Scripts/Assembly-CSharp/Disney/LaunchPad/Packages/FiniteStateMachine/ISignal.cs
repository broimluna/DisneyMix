namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public interface ISignal
	{
		State StartState { get; set; }

		State EndState { get; set; }

		Transition Transition { get; set; }

		string name { get; set; }

		bool ActivateSignal();

		bool IsSignaled();

		void Reset();

		void PerformTransition(StateTraverser traverser);

		StateChangeArgs GetStateChangeArgs(StateTraverser traverser);
	}
}
