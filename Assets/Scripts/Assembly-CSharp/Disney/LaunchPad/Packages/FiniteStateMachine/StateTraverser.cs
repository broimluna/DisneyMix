using Disney.LaunchPad.Packages.EventSystem;
using UnityEngine;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class StateTraverser : MonoBehaviour, IStateTraverser
	{
		[SerializeField]
		private State mInitialState;

		[SerializeField]
		private bool mTransitionToInitialState;

		[SerializeField]
		private Transition mInitialStateTransition;

		private State mCurrentState;

		private State mDefaultInitialState;

		private Signal mActiveSignal;

		private Signal mInternalSignal;

		protected EventDispatcher mEventDispatcher = new EventDispatcher();

		private string mCachedName;

		private bool mIsTransitioning;

		public EventDispatcher EventDispatcher
		{
			get
			{
				return mEventDispatcher;
			}
		}

		public State CurrentState
		{
			get
			{
				return mCurrentState;
			}
		}

		public State InitialState
		{
			get
			{
				return mInitialState;
			}
			set
			{
				mInitialState = value;
			}
		}

		public bool TransitionToInitialState
		{
			get
			{
				return mTransitionToInitialState;
			}
			set
			{
				mTransitionToInitialState = value;
			}
		}

		public Transition InitialStateTransition
		{
			get
			{
				return mInitialStateTransition;
			}
			set
			{
				mInitialStateTransition = value;
			}
		}

		public bool IsTransitioning
		{
			get
			{
				return mIsTransitioning;
			}
			set
			{
				if (value)
				{
					base.gameObject.name = string.Format("{0} (Transitioning)", mCachedName);
				}
				else
				{
					base.gameObject.name = mCachedName;
				}
				mIsTransitioning = value;
			}
		}

		public void Awake()
		{
			base.gameObject.SetActive(false);
			mCachedName = base.gameObject.name;
			mDefaultInitialState = mInitialState;
		}

		public void OnEnable()
		{
			if (mCurrentState != null)
			{
				mCurrentState.gameObject.SetActive(true);
			}
			else
			{
				if (!(InitialState != null))
				{
					return;
				}
				if (TransitionToInitialState)
				{
					if (mInternalSignal == null)
					{
						mInternalSignal = MakeInternalSignal();
					}
					mInternalSignal.StartState = null;
					mInternalSignal.EndState = InitialState;
					mInternalSignal.ActivateSignal();
					mActiveSignal = mInternalSignal;
					IsTransitioning = true;
					Transition transition = mActiveSignal.Transition;
					if (transition != null)
					{
						transition.EventDispatcher.AddListener<TransitionCompletedEvent>(OnCompletedTransition, EventDispatcher.Priority.LAST);
					}
					mActiveSignal.PerformTransition(this);
				}
				else
				{
					SetCurrentState(InitialState);
				}
			}
		}

		public void OnDisable()
		{
			if (!(mCurrentState != null))
			{
				return;
			}
			if (TransitionToInitialState)
			{
				if (mInternalSignal == null)
				{
					mInternalSignal = MakeInternalSignal();
				}
				mInternalSignal.StartState = mCurrentState;
				mInternalSignal.EndState = null;
				mInternalSignal.ActivateSignal();
				mActiveSignal = mInternalSignal;
				IsTransitioning = true;
				Transition transition = mActiveSignal.Transition;
				if (transition != null)
				{
					transition.EventDispatcher.AddListener<TransitionCompletedEvent>(OnCompletedTransition, EventDispatcher.Priority.LAST);
				}
				mActiveSignal.PerformTransition(this);
			}
			else
			{
				mCurrentState.gameObject.SetActive(false);
			}
		}

		public void SetActive(bool active)
		{
			base.gameObject.SetActive(active);
		}

		public void ResetAndDeactivate()
		{
			mActiveSignal = null;
			IsTransitioning = false;
			mEventDispatcher.ClearAll();
			SetCurrentState(null);
			SetActive(false);
		}

		public void SetCurrentState(State state)
		{
			State state2 = mCurrentState;
			mCurrentState = state;
			if (mCurrentState != state2)
			{
				StateChangeArgs stateChangeDetails = new StateChangeArgs(this, state2, mCurrentState, null, null);
				if (state2 != null)
				{
					state2.RaisePreExitEvent(stateChangeDetails);
					state2.RaiseExitEvent(stateChangeDetails);
					state2.RaisePostExitEvent(stateChangeDetails);
				}
				if (mCurrentState != null)
				{
					mCurrentState.RaisePreEnterEvent(stateChangeDetails);
					mCurrentState.RaiseEnterEvent(stateChangeDetails);
					mCurrentState.RaisePostEnterEvent(stateChangeDetails);
				}
				RaiseStateChangedEvent();
			}
		}

		public void ResetTraverser(State newInitialState = null)
		{
			SetActive(false);
			mCurrentState = null;
			if (newInitialState == null)
			{
				mInitialState = mDefaultInitialState;
			}
			else
			{
				mInitialState = newInitialState;
			}
			mActiveSignal = null;
			IsTransitioning = false;
			mEventDispatcher.ClearAll();
		}

		public void Update()
		{
			if (!(mCurrentState != null) || IsTransitioning)
			{
				return;
			}
			mCurrentState.RaisePreUpdateEvent();
			mCurrentState.RaiseUpdateEvent();
			mCurrentState.RaisePostUpdateEvent();
			mActiveSignal = mCurrentState.GetActiveSignal();
			if (mActiveSignal != null)
			{
				mCurrentState.ResetSignals();
				IsTransitioning = true;
				RaiseTransitionBeganEvent(mCurrentState, mActiveSignal.EndState, mActiveSignal);
				Transition transition = mActiveSignal.Transition;
				if (transition != null)
				{
					transition.EventDispatcher.AddListener<TransitionCompletedEvent>(OnCompletedTransition, EventDispatcher.Priority.LAST);
				}
				mActiveSignal.PerformTransition(this);
			}
		}

		protected bool OnCompletedTransition(TransitionCompletedEvent evnt)
		{
			if (mActiveSignal != null)
			{
				if (mActiveSignal.Transition != null)
				{
					mActiveSignal.Transition.EventDispatcher.RemoveListener<TransitionCompletedEvent>(OnCompletedTransition);
				}
				State previousState = null;
				if (evnt.StateChangeDetails.Traverser == this)
				{
					previousState = evnt.StateChangeDetails.StartState;
					mCurrentState = evnt.StateChangeDetails.EndState;
				}
				IsTransitioning = false;
				RaiseTransitionEndedEvent(previousState, mCurrentState, mActiveSignal);
				RaiseStateChangedEvent();
				mActiveSignal = null;
			}
			return false;
		}

		private void RaiseStateChangedEvent()
		{
			if (EventDispatcher != null)
			{
				EventDispatcher.DispatchEvent(new StateTraverserStateChangeEvent(mCurrentState));
			}
		}

		private void RaiseTransitionBeganEvent(State previousState, State newState, Signal signal)
		{
			if (EventDispatcher != null)
			{
				EventDispatcher.DispatchEvent(new StateTraverserTransitionBeganEvent(previousState, newState, signal));
			}
		}

		private void RaiseTransitionEndedEvent(State previousState, State newState, Signal signal)
		{
			if (EventDispatcher != null)
			{
				EventDispatcher.DispatchEvent(new StateTraverserTransitionEndedEvent(previousState, newState, signal));
			}
		}

		private Signal MakeInternalSignal()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "_internalSignal";
			gameObject.transform.parent = base.transform;
			Signal signal = gameObject.AddComponent<ManualSignal>();
			signal.Transition = mInitialStateTransition;
			return signal;
		}
	}
}
