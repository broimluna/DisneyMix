namespace Fabric.ModularSynth
{
	internal class Multiplier : Module
	{
		private AudioInputPin audioInput = new AudioInputPin();

		private AudioOutputPin audioOutput = new AudioOutputPin();

		private ControlInputPin gain = new ControlInputPin(1f, 0f, 1f);

		public Multiplier()
		{
			RegisterPin(audioInput, "Input");
			RegisterPin(audioOutput, "Output");
			RegisterPin(gain, "Gain");
		}

		public override void OnAudioPinsUpdate()
		{
			if (audioInput.IsConnected() && audioOutput.HasBuffer())
			{
				AudioBuffer buffer = audioInput.GetBuffer();
				AudioBuffer buffer2 = audioOutput.GetBuffer();
				buffer.Scale((float)gain.GetValue());
				buffer.CopyToBuffer(buffer2);
			}
		}
	}
}
