using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fabric.ModularSynth
{
	[Serializable]
	public class SampleManager
	{
		private static SampleManager _instance;

		[SerializeField]
		private List<SampleFile> _sampleFileList = new List<SampleFile>();

		public float _outputSampleRate { get; private set; }

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

		public void Start()
		{
			_outputSampleRate = AudioSettings.outputSampleRate;
			for (int i = 0; i < _sampleFileList.Count; i++)
			{
				_sampleFileList[i].Start();
			}
		}

		public void Destroy()
		{
			for (int i = 0; i < _sampleFileList.Count; i++)
			{
				_sampleFileList[i].Destroy();
			}
		}

		public int GetSampleFileIndexByName(string name)
		{
			for (int i = 0; i < _sampleFileList.Count; i++)
			{
				if (_sampleFileList[i]._audioClip != null && _sampleFileList[i]._audioClip.name == name)
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
