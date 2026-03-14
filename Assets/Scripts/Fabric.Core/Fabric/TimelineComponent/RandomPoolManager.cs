using System.Collections.Generic;
using UnityEngine;

namespace Fabric.TimelineComponent
{
	[AddComponentMenu("")]
	public class RandomPoolManager : Component
	{
		[SerializeField]
		[HideInInspector]
		public Dictionary<string, RandomComponent> _definitionsTable = new Dictionary<string, RandomComponent>();

		[SerializeField]
		[HideInInspector]
		public string projectName;
	}
}
