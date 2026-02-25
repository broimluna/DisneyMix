using System;
using UnityEngine;

namespace Fabric
{
	[AddComponentMenu("Fabric/Events/Trigger")]
	public class EventTrigger : MonoBehaviour
	{
		private Event _event = new Event();

		private ParameterData parameter = default(ParameterData);

		private DSPParameterData dspParameter = new DSPParameterData();

		[HideInInspector]
		public SwitchPresetData switchPresetData;

		[HideInInspector]
		public GlobalParameterData globalParameterData;

		[HideInInspector]
		public GlobalSwitchParameterData globalSwitchParameterData;

		[HideInInspector]
		public TransitionToSnapshotData transitionToSnapshotData;

		[HideInInspector]
		[SerializeField]
		public string _eventName = "_UnSet_";

		[SerializeField]
		[HideInInspector]
		public EventAction _eventAction;

		[SerializeField]
		[HideInInspector]
		public object _parameter;

		[HideInInspector]
		[SerializeField]
		public bool _trigger;

		[SerializeField]
		[HideInInspector]
		public string _eventValue = "";

		[SerializeField]
		[HideInInspector]
		public float _eventParameter = 1f;

		[HideInInspector]
		[SerializeField]
		public string _eventParameterName = "";

		[SerializeField]
		[HideInInspector]
		public float _delay;

		[HideInInspector]
		[SerializeField]
		public int _probability = 100;

		[HideInInspector]
		[SerializeField]
		public EventTriggerType _eventTriggerType;

		[SerializeField]
		[HideInInspector]
		public bool _ignoreGameObject;

		[HideInInspector]
		[SerializeField]
		public bool _addToQueue;

		[SerializeField]
		[HideInInspector]
		public float _min;

		[SerializeField]
		[HideInInspector]
		public float _max = 1f;

		[SerializeField]
		[HideInInspector]
		public DSPType _dspType;

		[HideInInspector]
		[SerializeField]
		public float _timeToTarget;

		[HideInInspector]
		[SerializeField]
		public float _curve = 0.5f;

		[HideInInspector]
		public GameObject _parentGameObject;

		[SerializeField]
		[HideInInspector]
		public bool _useEventID;

		[HideInInspector]
		private System.Random rnd = new System.Random();

		private void PostEventByName(string name)
		{
			if (_probability < 100)
			{
				int num = (int)(rnd.NextDouble() * 100.0);
				if (num > _probability)
				{
					return;
				}
			}
			_event._eventName = name;
			if (_useEventID)
			{
				_event._eventID = EventManager.GetIDFromEventName(name);
			}
			_event.EventAction = _eventAction;
			_event._delay = _delay;
			if (!_ignoreGameObject)
			{
				if (_parentGameObject == null)
				{
					_parentGameObject = base.gameObject;
				}
				_event.parentGameObject = _parentGameObject;
			}
			else
			{
				_event.parentGameObject = null;
			}
			if (_eventAction == EventAction.SetPitch || _eventAction == EventAction.SetVolume || _eventAction == EventAction.SetPan || _eventAction == EventAction.SetTime || _eventAction == EventAction.SetVolumeProperty || _eventAction == EventAction.SetPitchProperty)
			{
				_event._parameter = _eventParameter;
			}
			else if (_eventAction == EventAction.SetParameter)
			{
				parameter._parameter = _eventParameterName;
				parameter._value = _eventParameter;
				_event._parameter = parameter;
			}
			else if (_eventAction == EventAction.SetDSPParameter)
			{
				dspParameter._dspType = _dspType;
				dspParameter._parameter = _eventParameterName;
				dspParameter._value = _eventParameter;
				dspParameter._time = _timeToTarget;
				dspParameter._curve = _curve;
				_event._parameter = dspParameter;
			}
			else if (_eventAction == EventAction.SwitchPreset)
			{
				if (switchPresetData != null)
				{
					_event._parameter = switchPresetData;
				}
			}
			else if (_eventAction == EventAction.SetGlobalParameter)
			{
				if (globalParameterData != null)
				{
					_event._parameter = globalParameterData;
				}
			}
			else if (_eventAction == EventAction.SetGlobalSwitch)
			{
				if (globalSwitchParameterData != null)
				{
					_event._parameter = globalSwitchParameterData;
				}
			}
			else if (_eventAction == EventAction.TransitionToSnapshot)
			{
				if (switchPresetData != null)
				{
					_event._parameter = transitionToSnapshotData;
				}
			}
			else
			{
				_event._parameter = _eventValue;
			}
			if (EventManager.Instance != null)
			{
				if (_useEventID)
				{
					EventManager.Instance.PostEventID(_event);
				}
				else
				{
					EventManager.Instance.PostEvent(_event);
				}
			}
		}

		public void PostEvent()
		{
			if (_probability < 100)
			{
				int num = (int)(rnd.NextDouble() * 100.0);
				if (num > _probability)
				{
					return;
				}
			}
			_event._eventName = _eventName;
			if (_useEventID)
			{
				_event._eventID = EventManager.GetIDFromEventName(base.name);
			}
			_event.EventAction = _eventAction;
			_event._delay = _delay;
			if (!_ignoreGameObject)
			{
				if (_parentGameObject == null)
				{
					_parentGameObject = base.gameObject;
				}
				_event.parentGameObject = _parentGameObject;
			}
			else
			{
				_event.parentGameObject = null;
			}
			if (_eventAction == EventAction.SetPitch || _eventAction == EventAction.SetVolume || _eventAction == EventAction.SetPan || _eventAction == EventAction.SetTime || _eventAction == EventAction.SetVolumeProperty || _eventAction == EventAction.SetPitchProperty)
			{
				_event._parameter = _eventParameter;
			}
			else if (_eventAction == EventAction.SetParameter)
			{
				parameter._parameter = _eventParameterName;
				parameter._value = _eventParameter;
				_event._parameter = parameter;
			}
			else if (_eventAction == EventAction.SetDSPParameter)
			{
				dspParameter._dspType = _dspType;
				dspParameter._parameter = _eventParameterName;
				dspParameter._value = _eventParameter;
				dspParameter._time = _timeToTarget;
				dspParameter._curve = _curve;
				_event._parameter = dspParameter;
			}
			else if (_eventAction == EventAction.SwitchPreset)
			{
				_event._parameter = switchPresetData;
			}
			else if (_eventAction == EventAction.SetGlobalParameter)
			{
				if (globalParameterData != null)
				{
					_event._parameter = globalParameterData;
				}
			}
			else if (_eventAction == EventAction.SetGlobalSwitch)
			{
				if (globalSwitchParameterData != null)
				{
					_event._parameter = globalSwitchParameterData;
				}
			}
			else if (_eventAction == EventAction.SetRegion || _eventAction == EventAction.QueueRegion)
			{
				_event._parameter = _eventParameterName;
			}
			else if (_eventAction == EventAction.TransitionToSnapshot)
			{
				_event._parameter = transitionToSnapshotData;
			}
			else
			{
				_event._parameter = _eventValue;
			}
			if (EventManager.Instance != null)
			{
				if (_useEventID)
				{
					EventManager.Instance.PostEventID(_event, _addToQueue);
				}
				else
				{
					EventManager.Instance.PostEvent(_event, _addToQueue);
				}
			}
		}

		private void Start()
		{
			if (_eventTriggerType == EventTriggerType.Start)
			{
				PostEvent();
			}
			if (_eventTriggerType == EventTriggerType.TriggerOnUpdate)
			{
				_trigger = true;
			}
		}

		private void OnDestroy()
		{
			if (_eventTriggerType == EventTriggerType.Destroy)
			{
				PostEvent();
			}
		}

		private void OnTriggerEnter()
		{
			if (_eventTriggerType == EventTriggerType.TriggerEnter)
			{
				PostEvent();
			}
		}

		private void OnTriggerExit()
		{
			if (_eventTriggerType == EventTriggerType.TriggerExit)
			{
				PostEvent();
			}
		}

		private void OnCollisionEnter()
		{
			if (_eventTriggerType == EventTriggerType.CollisionEnter)
			{
				PostEvent();
			}
		}

		private void OnCollisionExit()
		{
			if (_eventTriggerType == EventTriggerType.CollisionExit)
			{
				PostEvent();
			}
		}

		private void OnEnable()
		{
			if (_eventTriggerType == EventTriggerType.Enable)
			{
				PostEvent();
			}
		}

		private void OnDisable()
		{
			if (_eventTriggerType == EventTriggerType.Disable)
			{
				PostEvent();
			}
		}

		private void OnMouseUp()
		{
			if (_eventTriggerType == EventTriggerType.MouseUp)
			{
				PostEvent();
			}
		}

		private void OnMouseDown()
		{
			if (_eventTriggerType == EventTriggerType.MouseDown)
			{
				PostEvent();
			}
		}

		private void OnMouseOver()
		{
			if (_eventTriggerType == EventTriggerType.MouseOver)
			{
				PostEvent();
			}
		}

		private void OnMouseEnter()
		{
			if (_eventTriggerType == EventTriggerType.MouseEnter)
			{
				PostEvent();
			}
		}

		private void OnMouseExit()
		{
			if (_eventTriggerType == EventTriggerType.MouseExit)
			{
				PostEvent();
			}
		}

		private void Update()
		{
			if (_eventTriggerType == EventTriggerType.Update)
			{
				PostEvent();
			}
			if (_trigger)
			{
				PostEvent();
				_trigger = false;
			}
		}
	}
}
