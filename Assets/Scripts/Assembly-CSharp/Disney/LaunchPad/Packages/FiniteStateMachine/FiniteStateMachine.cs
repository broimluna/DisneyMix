using System;
using Disney.LaunchPad.Packages.EventSystem;
using UnityEngine;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class FiniteStateMachine : MonoBehaviour
	{
		protected EventDispatcher mEventDispatcher;

		protected Transition mDefaultTransition;

		protected StateTraverser mStateTraverser;

		protected GameObject mStateParent;

		protected GameObject mTranstionsParent;

		protected State mInitialState;

		public EventDispatcher eventDispatcher
		{
			get
			{
				return mEventDispatcher;
			}
			set
			{
				mEventDispatcher = value;
			}
		}

		public Transition defaultTransition
		{
			get
			{
				return mDefaultTransition;
			}
			set
			{
				mDefaultTransition = value;
			}
		}

		public StateTraverser traverser
		{
			get
			{
				return mStateTraverser;
			}
			set
			{
				mStateTraverser = value;
			}
		}

		public State initialState
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

		private void Awake()
		{
			mStateParent = new GameObject("States");
			mTranstionsParent = new GameObject("Transitions");
			mStateParent.transform.SetParent(base.transform, false);
			mTranstionsParent.transform.SetParent(base.transform, false);
		}

		public void SetActive(bool isActive)
		{
			if (isActive && mStateTraverser == null)
			{
				traverser = CreateStateTraverser(initialState, string.Empty);
			}
			traverser.SetActive(isActive);
		}

		public State CreateState(string stateName)
		{
			GameObject gameObject = new GameObject(stateName);
			State result = gameObject.AddComponent<State>();
			gameObject.transform.parent = mStateParent.transform;
			if (mInitialState == null)
			{
				initialState = result;
			}
			return result;
		}

		public State CreateState<StateLogicComponentT>(string stateName) where StateLogicComponentT : Component
		{
			State state = CreateState(stateName);
			state.gameObject.AddComponent<StateLogicComponentT>();
			return state;
		}

		public StateTraverser CreateStateTraverser(State initialState, string stateTraverserName = "")
		{
			return CreateStateTraverser<StateTraverser>(initialState, stateTraverserName);
		}

		public StateTraverserT CreateStateTraverser<StateTraverserT>(State initialState, string stateTraverserName = "") where StateTraverserT : StateTraverser
		{
			if (stateTraverserName.Length == 0)
			{
				stateTraverserName = typeof(StateTraverserT).Name;
			}
			GameObject gameObject = new GameObject(stateTraverserName);
			StateTraverserT result = gameObject.AddComponent<StateTraverserT>();
			gameObject.transform.parent = base.transform;
			result.InitialState = initialState;
			return result;
		}

		public SignalT CreateSignal<SignalT>(State startState, State endState, Transition transition = null) where SignalT : Signal
		{
			string arg = "NULL State";
			if ((bool)endState)
			{
				arg = endState.gameObject.name;
			}
			string text = string.Format("Go To {1} Signal", startState.gameObject.name, arg);
			GameObject gameObject = new GameObject(text);
			gameObject.transform.parent = startState.transform;
			SignalT val = gameObject.AddComponent<SignalT>();
			val.StartState = startState;
			val.EndState = endState;
			if (transition != null)
			{
				val.Transition = transition;
			}
			else
			{
				val.Transition = defaultTransition;
			}
			startState.AddSignal(val);
			return val;
		}

		public ManualSignal CreateEventSignal<EventT>(State startState, State endState, Transition transition = null, EventDispatcher.Priority priority = EventDispatcher.Priority.LAST) where EventT : BaseEvent
		{
			if (transition == null)
			{
				transition = defaultTransition;
			}
			ManualSignal manualSignal = startState.CreateSignal<ManualSignal>(endState, transition);
			eventDispatcher.AddListener<EventT>(manualSignal.OnActivateSignal, priority);
			return manualSignal;
		}

		public SignalT CreateEventSignal<SignalT, EventT>(State startState, State endState, Transition transition = null, EventDispatcher.Priority priority = EventDispatcher.Priority.LAST) where SignalT : Signal where EventT : BaseEvent
		{
			if (transition == null)
			{
				transition = defaultTransition;
			}
			SignalT val = startState.CreateSignal<SignalT>(endState, transition);
			eventDispatcher.AddListener<EventT>(val.OnActivateSignal, priority);
			return val;
		}

		public SignalT CreateSignal<SignalT>(State startState, State endState, Transition transition, Func<bool> lambdaOrDelegate) where SignalT : LambdaSignal
		{
			SignalT result = startState.CreateSignal<SignalT>(endState, transition);
			result.SetShouldActivateDelegate(lambdaOrDelegate);
			return result;
		}

		public SignalT CreateSignal<SignalT>(State startState, State endState, Func<bool> lambdaOrDelegate) where SignalT : LambdaSignal
		{
			SignalT result = startState.CreateSignal<SignalT>(endState, defaultTransition);
			result.SetShouldActivateDelegate(lambdaOrDelegate);
			return result;
		}

		public TransitionT CreateTransition<TransitionT>(string transitionName = "") where TransitionT : Transition
		{
			string text = transitionName;
			if (text.Length == 0)
			{
				text = typeof(TransitionT).Name;
			}
			GameObject gameObject = new GameObject(text);
			gameObject.transform.parent = mTranstionsParent.transform;
			return gameObject.AddComponent<TransitionT>();
		}

		public static FiniteStateMachine CreateFiniteStateMachine(GameObject parentGameObject, string name = "")
		{
			return CreateFiniteStateMachine(parentGameObject.transform, name);
		}

		public static FiniteStateMachine CreateFiniteStateMachine(Transform parentTransform, string name = "")
		{
			string text = name;
			if (text.Length == 0)
			{
				text = "Finite State Machine";
			}
			GameObject gameObject = new GameObject(text);
			gameObject.transform.SetParent(parentTransform, false);
			return gameObject.AddComponent<FiniteStateMachine>();
		}
	}
}
