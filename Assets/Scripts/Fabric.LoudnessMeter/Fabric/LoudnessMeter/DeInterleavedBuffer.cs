namespace Fabric.LoudnessMeter
{
	internal class DeInterleavedBuffer
	{
		private float[] _buffer = new float[2048];

		private int _numChannels;

		public void SetBuffer(float[] buffer, int numChannels)
		{
			if (_buffer.Length != buffer.Length)
			{
				_buffer = new float[buffer.Length];
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				_buffer[i] = buffer[i];
			}
			_numChannels = numChannels;
		}

		public int GetNumSamples()
		{
			return _buffer.Length / _numChannels;
		}

		public int GetNumChannels()
		{
			return _numChannels;
		}

		public float GetSample(int index, int channel)
		{
			int num = index * _numChannels + channel;
			return _buffer[num];
		}

		public void SetSample(float sample, int index, int channel)
		{
			int num = index * _numChannels + channel;
			_buffer[num] = sample;
		}
	}
}
