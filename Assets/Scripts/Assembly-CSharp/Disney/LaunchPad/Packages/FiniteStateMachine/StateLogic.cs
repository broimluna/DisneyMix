using Disney.LaunchPad.Packages.EventSystem;
using UnityEngine;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class StateLogic : MonoBehaviour
	{
		private State m_state;

		public State state
		{
			get
			{
				if (m_state == null)
				{
					m_state = base.gameObject.GetComponent<State>();
				}
				return m_state;
			}
		}

		protected void SubscribeToEvent<T>(EventHandlerDelegate<T> callbackMethod) where T : BaseEvent
		{
			if (callbackMethod != null)
			{
				state.EventDispatcher.AddListener(callbackMethod);
			}
		}

		protected void SubscribeToEvent<T>(EventHandlerDelegate<T> callbackMethod, EventDispatcher.Priority priority) where T : BaseEvent
		{
			if (callbackMethod != null)
			{
				state.EventDispatcher.AddListener(callbackMethod, priority);
			}
		}
	}
}
