using Disney.LaunchPad.Packages.EventSystem;
using UnityEngine;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public abstract class Transition : MonoBehaviour, ITransition
	{
		protected EventDispatcher m_eventDispatcher = new EventDispatcher();

		protected StateChangeArgs m_stateChangeDetails;

		public EventDispatcher EventDispatcher
		{
			get
			{
				return m_eventDispatcher;
			}
		}

		public virtual void Awake()
		{
			SetTransitionEnabled(false);
		}

		public abstract void Perform(StateChangeArgs stateChangeDetails);

		public abstract void Reset();

		protected void RaiseTransitionCompletedEvent()
		{
			SetTransitionEnabled(false);
			if (m_eventDispatcher != null)
			{
				m_eventDispatcher.DispatchEvent(new TransitionCompletedEvent(m_stateChangeDetails));
			}
			m_stateChangeDetails = null;
		}

		protected void SetTransitionEnabled(bool enable)
		{
			base.enabled = enable;
			base.gameObject.SetActive(enable);
		}
	}
}
