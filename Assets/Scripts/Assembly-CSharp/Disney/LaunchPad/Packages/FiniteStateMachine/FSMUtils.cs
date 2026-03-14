using System;
using Disney.LaunchPad.Packages.EventSystem;
using UnityEngine;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public static class FSMUtils
	{
		public static State CreateState(string stateName, Transform parent = null)
		{
			GameObject gameObject = new GameObject(stateName);
			State result = gameObject.AddComponent<State>();
			if (parent != null)
			{
				gameObject.transform.parent = parent;
			}
			return result;
		}

		public static State CreateState(string stateName, GameObject parentObject = null)
		{
			if ((bool)parentObject)
			{
				return CreateState(stateName, parentObject.transform);
			}
			return CreateState(stateName, (Transform)null);
		}

		public static State CreateState<StateLogicComponentT>(string stateName, Transform parent = null) where StateLogicComponentT : Component
		{
			State state = CreateState(stateName, parent);
			state.gameObject.AddComponent<StateLogicComponentT>();
			return state;
		}

		public static State CreateState<StateLogicComponentT>(string stateName, GameObject parentObject = null) where StateLogicComponentT : Component
		{
			if ((bool)parentObject)
			{
				return CreateState<StateLogicComponentT>(stateName, parentObject.transform);
			}
			return FSMUtils.CreateState<StateLogicComponentT>(stateName, (Transform)null);
		}

		public static StateTraverserT CreateStateTraverser<StateTraverserT>(string stateTraverserName, Transform parent, State initialState) where StateTraverserT : StateTraverser
		{
			GameObject gameObject = new GameObject(stateTraverserName);
			StateTraverserT result = gameObject.AddComponent<StateTraverserT>();
			if (parent != null)
			{
				gameObject.transform.parent = parent;
			}
			result.InitialState = initialState;
			return result;
		}

		public static StateTraverserT CreateStateTraverser<StateTraverserT>(string stateTraverserName, GameObject parentObject, State initialState) where StateTraverserT : StateTraverser
		{
			if ((bool)parentObject)
			{
				return CreateStateTraverser<StateTraverserT>(stateTraverserName, parentObject.transform, initialState);
			}
			return CreateStateTraverser<StateTraverserT>(stateTraverserName, (Transform)null, initialState);
		}

		public static StateTraverser CreateStateTraverser(string stateTraverserName, GameObject parentObject, State initialState)
		{
			if ((bool)parentObject)
			{
				return CreateStateTraverser<StateTraverser>(stateTraverserName, parentObject.transform, initialState);
			}
			return CreateStateTraverser<StateTraverser>(stateTraverserName, (Transform)null, initialState);
		}

		public static SignalT CreateSignal<SignalT>(this State startState, State endState, Transition transition) where SignalT : Signal
		{
			string arg = "NULL State";
			if ((bool)endState)
			{
				arg = endState.gameObject.name;
			}
			string name = string.Format("Go To {1} Signal", startState.gameObject.name, arg);
			GameObject gameObject = new GameObject(name);
			gameObject.transform.parent = startState.transform;
			SignalT val = gameObject.AddComponent<SignalT>();
			val.StartState = startState;
			val.EndState = endState;
			val.Transition = transition;
			startState.AddSignal(val);
			return val;
		}

		public static ManualSignal CreateEventSignal<EventT>(this State startState, State endState, Transition transition, EventDispatcher eventDispatcher, EventDispatcher.Priority priority = EventDispatcher.Priority.LAST) where EventT : BaseEvent
		{
			ManualSignal manualSignal = startState.CreateSignal<ManualSignal>(endState, transition);
			eventDispatcher.AddListener<EventT>(manualSignal.OnActivateSignal, priority);
			return manualSignal;
		}

		public static SignalT CreateEventSignal<SignalT, EventT>(this State startState, State endState, Transition transition, EventDispatcher eventDispatcher, EventDispatcher.Priority priority = EventDispatcher.Priority.LAST) where SignalT : Signal where EventT : BaseEvent
		{
			SignalT val = startState.CreateSignal<SignalT>(endState, transition);
			eventDispatcher.AddListener<EventT>(val.OnActivateSignal, priority);
			return val;
		}

		public static SignalT CreateSignal<SignalT>(this State startState, State endState, Transition transition, Func<bool> lambdaOrDelegate) where SignalT : LambdaSignal
		{
			SignalT result = startState.CreateSignal<SignalT>(endState, transition);
			result.SetShouldActivateDelegate(lambdaOrDelegate);
			return result;
		}

		public static TransitionT CreateTransition<TransitionT>(Transform parent) where TransitionT : Transition
		{
			GameObject gameObject = new GameObject(typeof(TransitionT).Name);
			gameObject.transform.parent = parent;
			return gameObject.AddComponent<TransitionT>();
		}

		public static TransitionT CreateTransition<TransitionT>(GameObject parentObject) where TransitionT : Transition
		{
			if ((bool)parentObject)
			{
				return CreateTransition<TransitionT>(parentObject.transform);
			}
			return CreateTransition<TransitionT>((Transform)null);
		}

		public static TransitionT CreateTransition<TransitionT>(string transitionName, Transform parent) where TransitionT : Transition
		{
			GameObject gameObject = new GameObject(transitionName);
			gameObject.transform.parent = parent;
			return gameObject.AddComponent<TransitionT>();
		}

		public static TransitionT CreateTransition<TransitionT>(string transitionName, GameObject parentObject) where TransitionT : Transition
		{
			if ((bool)parentObject)
			{
				return CreateTransition<TransitionT>(transitionName, parentObject.transform);
			}
			return CreateTransition<TransitionT>(transitionName, (Transform)null);
		}
	}
}
