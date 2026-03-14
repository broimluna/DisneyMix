using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fabric.ModularSynth
{
	[Serializable]
	public class SampleFile
	{
		[SerializeField]
		private List<SampleFileInstance> _sampleFileInstanceList = new List<SampleFileInstance>();

		[SerializeField]
		public AudioClip _audioClip;

		[SerializeField]
		public List<Marker> _markers = new List<Marker>();

		private int _refCount;

		public float _sampleRate { get; private set; }

		public float[] _data { get; private set; }

		public void IncRef()
		{
			_refCount++;
		}

		public void Start()
		{
			if (_audioClip != null)
			{
				_data = new float[_audioClip.samples * _audioClip.channels];
				_audioClip.GetData(_data, 0);
				_sampleRate = _audioClip.frequency;
			}
		}

		public void Destroy()
		{
			_data = null;
		}

		public SampleFileInstance GetInstance()
		{
			IncRef();
			return new SampleFileInstance(this);
		}
	}
}
