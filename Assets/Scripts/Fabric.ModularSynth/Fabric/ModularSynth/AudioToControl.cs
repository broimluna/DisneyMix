namespace Fabric.ModularSynth
{
	internal class AudioToControl : Module
	{
		private AudioInputPin audioInput = new AudioInputPin();

		private ControlOutputPin controlOutput = new ControlOutputPin(eControlType.FloatSlider, false);

		public AudioToControl()
		{
			RegisterPin(controlOutput, "Output");
			RegisterPin(audioInput, "Input");
		}

		public override void OnAudioPinsUpdate()
		{
			if (audioInput.IsConnected())
			{
				AudioBuffer buffer = audioInput.GetBuffer();
				for (int i = 0; i < buffer.Size; i++)
				{
					controlOutput.Value = buffer[i];
				}
			}
		}
	}
}
