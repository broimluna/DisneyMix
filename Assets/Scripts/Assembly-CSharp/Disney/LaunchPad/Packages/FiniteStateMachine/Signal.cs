using UnityEngine;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public abstract class Signal : MonoBehaviour, ISignal
	{
		[SerializeField]
		protected State mStartState;

		[SerializeField]
		protected State mEndState;

		[SerializeField]
		protected Transition mTransition;

		public State StartState
		{
			get
			{
				return mStartState;
			}
			set
			{
				SetState(ref mStartState, value);
			}
		}

		public State EndState
		{
			get
			{
				return mEndState;
			}
			set
			{
				SetState(ref mEndState, value);
			}
		}

		public Transition Transition
		{
			get
			{
				return mTransition;
			}
			set
			{
				mTransition = value;
			}
		}

		 string ISignal.name
		{
			get
			{
				return base.name;
			}
			set
			{
				base.name = value;
			}
		}

		public virtual void Awake()
		{
			Reset();
		}

		public abstract bool ActivateSignal();

		public bool OnActivateSignal<EventT>(EventT evt)
		{
			ActivateSignal();
			return false;
		}

		public abstract bool IsSignaled();

		public abstract void Reset();

		public void PerformTransition(StateTraverser traverser)
		{
			if (mTransition != null)
			{
				mTransition.Perform(GetStateChangeArgs(traverser));
			}
		}

		public StateChangeArgs GetStateChangeArgs(StateTraverser traverser)
		{
			return new StateChangeArgs(traverser, mStartState, mEndState, this, mTransition);
		}

		private void SetState(ref State state, State value)
		{
			state = value;
		}
	}
}
