using System;
using System.Collections.Generic;

namespace Fabric.ModularSynth
{
	[Serializable]
	public class ModuleInfo
	{
		public string name;

		public string guid;

		public float posX;

		public float posY;

		public List<PropertryInfo> propertiesInfo = new List<PropertryInfo>();

		public List<ParameterInfo> parametersInfo = new List<ParameterInfo>();
	}
}
