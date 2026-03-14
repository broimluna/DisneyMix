using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games
{
	public class FSM<T> where T : struct, IConvertible, IComparable
	{
		private List<FSMState> m_states;

		private float m_timeInCurrentState;

		private T m_currentState;

		private T m_previousState;

		public T state
		{
			get
			{
				return m_currentState;
			}
			set
			{
				if (m_previousState.CompareTo(m_currentState) == 0)
				{
					m_previousState = m_currentState;
				}
				m_currentState = value;
			}
		}

		public float timeInCurrentState
		{
			get
			{
				return m_timeInCurrentState;
			}
		}

		public FSM()
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException("T must be enumerated type");
			}
			m_states = new List<FSMState>();
			int num = Enum.GetNames(typeof(T)).Length;
			for (int i = 0; i < num; i++)
			{
				m_states.Add(new FSMState(null, null, null));
			}
		}

		public void AddState(T stateId, FSMState.EnterFunction enter_func, FSMState.UpdateFunction update_func, FSMState.ExitFunction exit_func)
		{
			m_states[Convert.ToInt16(stateId)] = new FSMState(enter_func, update_func, exit_func);
		}

		public void Start()
		{
			int num = Convert.ToInt16(m_currentState);
			if (num < m_states.Count && m_states[num].Enter != null)
			{
				m_states[num].Enter();
			}
		}

		public void Update()
		{
			int num = Convert.ToInt16(m_previousState);
			int num2 = Convert.ToInt16(m_currentState);
			m_timeInCurrentState += Time.deltaTime;
			T currentState = m_currentState;
			if (m_previousState.CompareTo(m_currentState) != 0)
			{
				if (num < m_states.Count && m_states[num] != null && m_states[num].Exit != null)
				{
					m_states[num].Exit();
				}
				m_previousState = m_currentState;
				if (num2 < m_states.Count && m_states[num2] != null && m_states[num2].Enter != null)
				{
					m_states[num2].Enter();
				}
				m_previousState = currentState;
				m_timeInCurrentState = 0f;
			}
			if (currentState.CompareTo(m_currentState) == 0 && num2 < m_states.Count && m_states[num2] != null && m_states[num2].Update != null)
			{
				m_states[num2].Update();
			}
		}
	}
}
