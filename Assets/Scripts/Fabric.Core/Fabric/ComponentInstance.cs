using System.Runtime.CompilerServices;
using UnityEngine;

namespace Fabric
{
	public class ComponentInstance
	{
		public Vector3 _randomisePosition;

		[CompilerGenerated]
		private Component _003C_componentInstanceHolder_003Ek__BackingField;

		public GameObject _parentGameObject { get; set; }

		public GameObject _componentGameObject { get; set; }

		public Component _componentInstanceHolder
		{
			[CompilerGenerated]
			set
			{
				_003C_componentInstanceHolder_003Ek__BackingField = value;
			}
		}

		public Component _instance { get; set; }

		public Transform _transform { get; set; }

		public float _triggerTime { get; set; }
	}
}
