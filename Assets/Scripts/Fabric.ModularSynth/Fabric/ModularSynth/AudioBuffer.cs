using System.Runtime.CompilerServices;

namespace Fabric.ModularSynth
{
	public class AudioBuffer
	{
		public enum eSignalDomain
		{
			eTimeDomain = 0,
			eFrequencyDomain = 1
		}

		private float[] data;

		private int bufferSize;

		[CompilerGenerated]
		private eSignalDomain _003CSignalDomain_003Ek__BackingField;

		[CompilerGenerated]
		private int _003CFrameID_003Ek__BackingField;

		public float this[int index]
		{
			get
			{
				return data[index];
			}
			set
			{
				data[index] = value;
			}
		}

		public float SampleRate { get; set; }

		public int NumFrames { get; private set; }

		public int NumChannels { get; private set; }

		public int Size { get; private set; }

		private eSignalDomain SignalDomain
		{
			[CompilerGenerated]
			set
			{
				_003CSignalDomain_003Ek__BackingField = value;
			}
		}

		public int FrameID
		{
			[CompilerGenerated]
			set
			{
				_003CFrameID_003Ek__BackingField = value;
			}
		}

		public AudioBuffer(int numFrames, int numChannels)
		{
			NumFrames = numFrames;
			NumChannels = numChannels;
			Size = numFrames * numChannels;
			bufferSize = Size;
			if (Size > 0)
			{
				data = new float[Size];
			}
			else
			{
				data = null;
			}
			SampleRate = 44100f;
			FrameID = -1;
			SignalDomain = eSignalDomain.eTimeDomain;
		}

		public float[] GetBuffer()
		{
			return data;
		}

		public void CopyToBuffer(AudioBuffer buffer)
		{
			if (buffer.NumFrames == NumFrames && buffer.NumChannels == NumChannels)
			{
				for (int i = 0; i < Size; i++)
				{
					buffer[i] = data[i];
				}
			}
		}

		public void AddBuffer(AudioBuffer buffer, float gain = 1f)
		{
			if (buffer.NumFrames == NumFrames && buffer.NumChannels == NumChannels)
			{
				for (int i = 0; i < Size; i++)
				{
					data[i] += buffer[i] * gain;
				}
			}
		}

		public void Clear()
		{
			for (int i = 0; i < Size; i++)
			{
				data[i] = 0f;
			}
		}

		public void Scale(float scale)
		{
			for (int i = 0; i < Size; i++)
			{
				data[i] *= scale;
			}
		}

		public void Resize(int numFrames, int numChannels)
		{
			NumFrames = numFrames;
			NumChannels = numChannels;
			Size = numFrames * numChannels;
			bufferSize = Size;
			if (Size > bufferSize)
			{
				data = new float[Size];
			}
		}
	}
}
