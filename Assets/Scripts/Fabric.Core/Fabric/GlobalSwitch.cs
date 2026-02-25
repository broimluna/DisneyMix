using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class GlobalSwitch
	{
		public interface IListener
		{
			bool OnSwitch(Switch _switch);
		}

		[Serializable]
		public class Switch
		{
			[SerializeField]
			public string _name;

			[SerializeField]
			public float _RTPParameterMin;

			[SerializeField]
			public float _RTPParameterMax;
		}

		[SerializeField]
		public List<Switch> _switches = new List<Switch>();

		[SerializeField]
		public bool _useGlobalParameter;

		[SerializeField]
		public string _defaultSwitch;

		[NonSerialized]
		private List<IListener> _listeners = new List<IListener>();

		private Switch _activeSwitch;

		public bool SetActiveSwitch(string name)
		{
			for (int i = 0; i < _switches.Count; i++)
			{
				Switch obj = _switches[i];
				if (name == obj._name)
				{
					_activeSwitch = obj;
					NotifyListeners();
					return true;
				}
			}
			return false;
		}

		public Switch GetActiveSwitch()
		{
			if (_activeSwitch == null && _switches.Count > 0)
			{
				for (int i = 0; i < _switches.Count; i++)
				{
					if (_switches[i]._name == _defaultSwitch)
					{
						_activeSwitch = _switches[i];
						break;
					}
				}
			}
			return _activeSwitch;
		}

		public bool RegisterListener(IListener listener)
		{
			if (!_listeners.Contains(listener))
			{
				_listeners.Add(listener);
				return true;
			}
			return false;
		}

		public bool UnregisterListener(IListener listener)
		{
			if (_listeners.Contains(listener))
			{
				_listeners.Remove(listener);
				return true;
			}
			return false;
		}

		private void NotifyListeners()
		{
			for (int i = 0; i < _listeners.Count; i++)
			{
				_listeners[i].OnSwitch(_activeSwitch);
			}
		}
	}
}
