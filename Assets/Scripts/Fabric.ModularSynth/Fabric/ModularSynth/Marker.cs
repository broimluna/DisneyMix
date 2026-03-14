using System;
using UnityEngine;

namespace Fabric.ModularSynth
{
	[Serializable]
	public class Marker
	{
		[SerializeField]
		public string name = "";

		[SerializeField]
		public int offsetSamples;
	}
}
