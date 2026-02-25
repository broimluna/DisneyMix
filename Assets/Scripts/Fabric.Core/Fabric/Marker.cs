using System;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class Marker
	{
		[SerializeField]
		public string name = "";

		[SerializeField]
		public int offsetSamples;

		[SerializeField]
		public float offsetTime;

		[NonSerialized]
		public float frequency;
	}
}
