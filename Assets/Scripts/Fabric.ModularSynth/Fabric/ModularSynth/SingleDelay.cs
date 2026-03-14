namespace Fabric.ModularSynth
{
	internal class SingleDelay : Module
	{
		private AudioInputPin audioInput = new AudioInputPin();

		private AudioOutputPin audioOutput = new AudioOutputPin();

		private ControlInputPin delayPin = new ControlInputPin(200f, 0f, 1000f);

		private ControlInputPin feedbackPin = new ControlInputPin(0.2f, 0f, 1f);

		private float[] _delayBuffer;

		private int _bufferIndex;

		private int _bufferLength;

		public SingleDelay()
		{
			RegisterPin(audioInput, "Input");
			RegisterPin(audioOutput, "Output");
			RegisterPin(delayPin, "Delay");
			RegisterPin(feedbackPin, "Feedback");
		}

		public void SetDelay(int delay)
		{
			AudioBuffer buffer = audioOutput.GetBuffer();
			if (buffer != null)
			{
				if (_delayBuffer == null)
				{
					int num = (int)((float)delayPin.Max * buffer.SampleRate / 1000f);
					_delayBuffer = new float[num];
				}
				_bufferLength = (int)((float)delay * buffer.SampleRate / 1000f);
			}
		}

		public override void OnCreate()
		{
			SetDelay(200);
		}

		public override void OnControlPinsUpdated()
		{
			SetDelay((int)(float)delayPin.GetValue());
		}

		public override void OnAudioPinsUpdate()
		{
			if (!audioInput.IsConnected() || !audioOutput.HasBuffer())
			{
				return;
			}
			AudioBuffer buffer = audioInput.GetBuffer();
			AudioBuffer buffer2 = audioOutput.GetBuffer();
			for (int i = 0; i < buffer.Size; i++)
			{
				if (_delayBuffer != null)
				{
					float num = buffer[i];
					float value = num + _delayBuffer[_bufferIndex];
					_delayBuffer[_bufferIndex] = num + (float)feedbackPin.GetValue() * _delayBuffer[_bufferIndex];
					_bufferIndex++;
					if (_bufferIndex >= _bufferLength)
					{
						_bufferIndex = 0;
					}
					buffer2[i] = value;
				}
				else
				{
					buffer2[i] = buffer[i];
				}
			}
		}
	}
}
