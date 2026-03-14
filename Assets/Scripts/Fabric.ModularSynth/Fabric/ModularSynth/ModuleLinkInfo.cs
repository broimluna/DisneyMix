using System;

namespace Fabric.ModularSynth
{
	[Serializable]
	public class ModuleLinkInfo
	{
		public string fromModule;

		public int fromPin;

		public string toModule;

		public int toPin;
	}
}
