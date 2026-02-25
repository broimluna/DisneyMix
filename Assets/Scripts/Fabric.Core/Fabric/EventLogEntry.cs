using UnityEngine;

namespace Fabric
{
	public class EventLogEntry
	{
		public EventAction _eventAction;

		public float _value;

		public float _triggerTime;

		public string _descritpion;

		public GameObject _gameObject;

		public string _gameObjectName;

		public Event _audioEvent;
	}
}
