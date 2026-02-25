using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class AudioBusManager
	{
		[SerializeField]
		public List<AudioBus> _audioBuses = new List<AudioBus>();

		public AudioBus FindAudioBusByName(string name)
		{
			if (name.Length == 0)
			{
				return null;
			}
			for (int i = 0; i < _audioBuses.Count; i++)
			{
				if (_audioBuses[i]._name == name)
				{
					return _audioBuses[i];
				}
			}
			return null;
		}
	}
}
