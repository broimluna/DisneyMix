using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class SampleManager
	{
		private static SampleManager _instance;

		[SerializeField]
		private List<SampleFile> _sampleFileList = new List<SampleFile>();

		public static SampleManager Instance
		{
			get
			{
				return _instance;
			}
		}

		public SampleManager()
		{
			_instance = this;
		}

		public int GetSampleFileIndexByName(string name)
		{
			for (int i = 0; i < _sampleFileList.Count; i++)
			{
				if (_sampleFileList[i].Name() == name)
				{
					return i;
				}
			}
			return -1;
		}

		public SampleFile GetSampleFileByIndex(int index)
		{
			return _sampleFileList[index];
		}
	}
}
