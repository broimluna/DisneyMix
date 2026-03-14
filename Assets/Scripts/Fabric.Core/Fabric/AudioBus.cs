using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Fabric
{
	[Serializable]
	public class AudioBus
	{
		[SerializeField]
		public string _name;

		[SerializeField]
		public float _volume = 1f;

		[SerializeField]
		public float _pitch = 1f;

		[SerializeField]
		public bool _limitInstances;

		[SerializeField]
		public int _instanceLimit = 20;

		[NonSerialized]
		private int _instancesActive;

		[SerializeField]
		public bool _limitAudioComponents;

		[SerializeField]
		public int _audioComponentsLimit = 20;

		[NonSerialized]
		private int _audioComponentsActive;

		[SerializeField]
		public AudioMixerGroup _audioMixerGroup;

		public bool IncrementAudioComponent()
		{
			if (!_limitAudioComponents)
			{
				return true;
			}
			_audioComponentsActive++;
			if (_audioComponentsActive <= _audioComponentsLimit)
			{
				return true;
			}
			_audioComponentsActive = _audioComponentsLimit;
			return false;
		}

		public bool DecrementAudioComponent()
		{
			if (_limitAudioComponents)
			{
				_audioComponentsActive--;
				if (_audioComponentsActive >= 0)
				{
					return true;
				}
				_audioComponentsActive = 0;
			}
			return false;
		}
	}
}
