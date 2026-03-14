namespace Fabric.ModularSynth
{
	internal class PitchShifter : Module
	{
		private static ControlPinList typeList = new ControlPinList
		{
			selectedIndex = 0,
			list = new string[6] { "256", "512", "1024", "2048", "4096", "8192" }
		};

		private AudioInputPin audioInput = new AudioInputPin();

		private AudioOutputPin audioOutput = new AudioOutputPin();

		private ControlInputPin pitchShift = new ControlInputPin(1f, 0f, 3f);

		private ControlInputPin fftSize = new ControlInputPin(typeList);

		private ControlInputPin oversample = new ControlInputPin(2, 0, 10);

		public PitchShifter()
		{
			RegisterPin(audioInput, "Input");
			RegisterPin(audioOutput, "Output");
			RegisterPin(pitchShift, "PitchShift");
			RegisterPin(fftSize, "FFTSize");
			RegisterPin(oversample, "Oversample");
		}

		public override void OnAudioPinsUpdate()
		{
			if (audioInput.IsConnected() && audioOutput.HasBuffer())
			{
				AudioBuffer buffer = audioInput.GetBuffer();
				AudioBuffer buffer2 = audioOutput.GetBuffer();
				ControlPinList controlPin = (ControlPinList)fftSize.GetValue();
				PitchShifterProcessor.PitchShift((float)pitchShift.GetValue(), buffer.Size, 1024L, (int)oversample.GetValue(), buffer.SampleRate, buffer.GetBuffer());
				buffer.CopyToBuffer(buffer2);
			}
		}
	}
}
