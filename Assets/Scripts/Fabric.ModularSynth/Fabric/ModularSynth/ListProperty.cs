using System;

namespace Fabric.ModularSynth
{
	[Serializable]
	public class ListProperty
	{
		public string[] list;

		public int selectedIndex = -1;
	}
}
