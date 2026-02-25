using System.Collections.Generic;
using Disney.LaunchPad.Packages.EventSystem;
using Disney.LaunchPadFramework.Utility;
using UnityEngine;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class State : MonoBehaviour, IState
	{
		[SerializeField]
		protected List<Signal> mSignals = new List<Signal>();

		protected EventDispatcher mEventDispatcher = new EventDispatcher();

		public EventDispatcher EventDispatcher
		{
			get
			{
				return mEventDispatcher;
			}
		}

		 string IState.name
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

		public void Awake()
		{
			base.gameObject.SetActive(false);
		}

		public void OnEnable()
		{
			if (Application.isEditor)
			{
				RefreshChildrenSignalMapping();
			}
		}

		public void AddSignal(Signal signal)
		{
			mSignals.AddIfUnique(signal);
			if (signal.StartState != this)
			{
				signal.StartState = this;
			}
		}

		public void RemoveSignal(Signal signal)
		{
			mSignals.Remove(signal);
			if (signal.StartState == this)
			{
				signal.StartState = null;
			}
		}

		public bool HasSignal(State endState)
		{
			for (int i = 0; i < mSignals.Count; i++)
			{
				Signal signal = mSignals[i];
				if (signal.EndState == endState)
				{
					return true;
				}
			}
			return false;
		}

		public List<Signal> GetSignals()
		{
			return mSignals;
		}

		public Signal GetSignalByName(string name)
		{
			for (int i = 0; i < mSignals.Count; i++)
			{
				Signal signal = mSignals[i];
				if (signal.name == name)
				{
					return signal;
				}
			}
			return null;
		}

		public Signal GetActiveSignal()
		{
			for (int i = 0; i < mSignals.Count; i++)
			{
				Signal signal = mSignals[i];
				if (signal.IsSignaled())
				{
					return signal;
				}
			}
			return null;
		}

		public void ResetSignals()
		{
			for (int i = 0; i < mSignals.Count; i++)
			{
				Signal signal = mSignals[i];
				signal.Reset();
			}
		}

		public void RefreshChildrenSignalMapping()
		{
			GetSignals().Clear();
			Component[] componentsInChildren = GetComponentsInChildren(typeof(Signal));
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Signal signal = componentsInChildren[i] as Signal;
				if (signal != null)
				{
					AddSignal(signal);
				}
			}
		}

		public void RaisePreEnterEvent(StateChangeArgs stateChangeDetails)
		{
			if (base.gameObject != null)
			{
				base.gameObject.SetActive(true);
			}
			EventDispatcher.DispatchEvent(new StatePreEnterEvent(stateChangeDetails));
		}

		public void RaiseEnterEvent(StateChangeArgs stateChangeDetails)
		{
			EventDispatcher.DispatchEvent(new StateEnterEvent(stateChangeDetails));
		}

		public void RaisePostEnterEvent(StateChangeArgs stateChangeDetails)
		{
			EventDispatcher.DispatchEvent(new StatePostEnterEvent(stateChangeDetails));
		}

		public void RaisePreUpdateEvent()
		{
			EventDispatcher.DispatchEvent(new StatePreUpdateEvent());
		}

		public void RaiseUpdateEvent()
		{
			EventDispatcher.DispatchEvent(new StateUpdateEvent());
		}

		public void RaisePostUpdateEvent()
		{
			EventDispatcher.DispatchEvent(new StatePostUpdateEvent());
		}

		public void RaisePreExitEvent(StateChangeArgs stateChangeDetails)
		{
			EventDispatcher.DispatchEvent(new StatePreExitEvent(stateChangeDetails));
		}

		public void RaiseExitEvent(StateChangeArgs stateChangeDetails)
		{
			EventDispatcher.DispatchEvent(new StateExitEvent(stateChangeDetails));
		}

		public void RaisePostExitEvent(StateChangeArgs stateChangeDetails)
		{
			EventDispatcher.DispatchEvent(new StatePostExitEvent(stateChangeDetails));
			if (base.gameObject != null)
			{
				base.gameObject.SetActive(false);
			}
		}
	}
}
