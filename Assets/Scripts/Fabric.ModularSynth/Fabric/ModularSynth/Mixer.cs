namespace Fabric.ModularSynth
{
	internal class Mixer : Module
	{
		private AudioInputPin audioInput1 = new AudioInputPin();

		private AudioInputPin audioInput2 = new AudioInputPin();

		private AudioOutputPin audioOutput = new AudioOutputPin();

		public Mixer()
		{
			RegisterPin(audioOutput, "Output");
			RegisterPin(audioInput1, "In1");
			RegisterPin(audioInput2, "In2");
		}

		public override void OnAudioPinsUpdate()
		{
			if (audioOutput.HasBuffer())
			{
				AudioBuffer buffer = audioOutput.GetBuffer();
				buffer.Clear();
				if (audioInput1.IsConnected())
				{
					AudioBuffer buffer2 = audioInput1.GetBuffer();
					buffer.AddBuffer(buffer2);
				}
				if (audioInput2.IsConnected())
				{
					AudioBuffer buffer3 = audioInput2.GetBuffer();
					buffer.AddBuffer(buffer3);
				}
			}
		}
	}
}
