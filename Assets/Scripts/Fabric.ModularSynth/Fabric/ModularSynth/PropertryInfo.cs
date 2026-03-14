using System;

namespace Fabric.ModularSynth
{
	[Serializable]
	public class PropertryInfo
	{
		public int iValue;

		public float fValue;

		public string sValue = "";

		public bool bValue;

		public PropertyType type;

		public ListProperty list = new ListProperty();

		public string name;

		public float min;

		public float max;

		public object GetObjectValue()
		{
			if (type == PropertyType.Int)
			{
				return iValue;
			}
			if (type == PropertyType.Float)
			{
				return fValue;
			}
			if (type == PropertyType.Bool)
			{
				return bValue;
			}
			if (type == PropertyType.String)
			{
				return sValue;
			}
			if (type == PropertyType.List)
			{
				ControlPinList controlPinList = new ControlPinList();
				controlPinList.list = list.list;
				controlPinList.selectedIndex = list.selectedIndex;
				return controlPinList;
			}
			return null;
		}
	}
}
