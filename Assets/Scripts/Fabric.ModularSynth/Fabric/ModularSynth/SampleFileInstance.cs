using System;
using UnityEngine;

namespace Fabric.ModularSynth
{
	[Serializable]
	public class SampleFileInstance
	{
		public bool looped;

		[SerializeField]
		public int _position;

		private float targetPitch = 1f;

		public SampleFile _sampleFile { get; private set; }

		public SampleFileInstance(SampleFile sampleFile)
		{
			_sampleFile = sampleFile;
		}

		public void FillAudioBuffer(ref AudioBuffer audioBuffer, float volume, float pitch)
		{
			int position = 0;
			int num = _sampleFile._data.Length;
			if (_sampleFile._markers.Count > 1)
			{
				position = _sampleFile._markers[0].offsetSamples;
				num = _sampleFile._markers[1].offsetSamples;
			}
			if (_position >= num)
			{
				if (!looped)
				{
					_position = num;
					audioBuffer.Clear();
					return;
				}
				_position = position;
			}
			if (targetPitch > pitch)
			{
				targetPitch += 0.01f;
			}
			else if (targetPitch < pitch)
			{
				targetPitch -= 0.01f;
			}
			float num2 = _sampleFile._sampleRate / SampleManager.Instance._outputSampleRate;
			int num3 = (int)((float)(audioBuffer.Size / audioBuffer.NumChannels) * num2 * pitch);
			int num4 = audioBuffer.Size / audioBuffer.NumChannels;
			for (int i = 0; i < num4; i++)
			{
				int num5 = _position * num3 / num4;
				if (num5 >= _sampleFile._data.Length)
				{
					num5 = 0;
					_position = 0;
				}
				float value = _sampleFile._data[num5] * volume;
				_position++;
				for (int j = 0; j < audioBuffer.NumChannels; j++)
				{
					audioBuffer[i * audioBuffer.NumChannels + j] = value;
				}
			}
		}
	}
}
