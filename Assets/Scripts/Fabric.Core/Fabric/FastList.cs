using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class FastList<Key, Data>
	{
		[SerializeField]
		public List<Key> _keys = new List<Key>();

		[SerializeField]
		public List<Data> _data = new List<Data>();

		private Dictionary<Key, Data> _fastList = new Dictionary<Key, Data>();

		public void BuildFast()
		{
			for (int i = 0; i < _keys.Count; i++)
			{
				if (!_fastList.ContainsKey(_keys[i]))
				{
					_fastList.Add(_keys[i], _data[i]);
				}
			}
		}

		public Data FindItem(Key key)
		{
			if (_fastList.ContainsKey(key))
			{
				return _fastList[key];
			}
			for (int i = 0; i < _keys.Count; i++)
			{
				if (_keys[i].Equals(key))
				{
					return _data[i];
				}
			}
			return default(Data);
		}

		public int GetCount()
		{
			return _keys.Count;
		}

		public Data FindItemByIndex(int index)
		{
			if (index < _keys.Count)
			{
				return _data[index];
			}
			return default(Data);
		}
	}
}
