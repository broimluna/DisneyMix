using System;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class Event
	{
		[SerializeField]
		public string _eventName = "";

		[SerializeField]
		public int _eventID;

		[SerializeField]
		public EventAction EventAction;

		[NonSerialized]
		private EventStatus _status;

		[SerializeField]
		public GameObject parentGameObject;

		[SerializeField]
		public object _parameter;

		[SerializeField]
		public float _delay;

		[SerializeField]
		public float _delayTimer;

		[SerializeField]
		public InitialiseParameters _initialiseParameters;

		[SerializeField]
		public OnEventNotify _onEventNotify;

		[SerializeField]
		public string _eventCategory = "";

		[SerializeField]
		public int _priority;

		public EventStatus eventStatus
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
			}
		}

		public void Copy(Event fromEvent)
		{
			_eventName = fromEvent._eventName;
			EventAction = fromEvent.EventAction;
			eventStatus = fromEvent.eventStatus;
			parentGameObject = fromEvent.parentGameObject;
			_parameter = fromEvent._parameter;
			_delay = fromEvent._delay;
			_delayTimer = fromEvent._delayTimer;
			_initialiseParameters = fromEvent._initialiseParameters;
		}
	}
}
