using UnityEngine;

namespace Fabric
{
	[AddComponentMenu("Fabric/Events/Listener")]
	public class EventListener : MonoBehaviour
	{
		[SerializeField]
		[HideInInspector]
		public string _eventName = "_UnSet_";

		[SerializeField]
		[HideInInspector]
		public int _eventID;

		[SerializeField]
		[HideInInspector]
		public bool _overrideEventTriggerAction;

		[HideInInspector]
		[SerializeField]
		public OverrideParameters _overrideParameters;
	}
}
