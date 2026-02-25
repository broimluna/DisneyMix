namespace Fabric.ModularSynth
{
	public class Scope : Module
	{
		private AudioInputPin audioInput = new AudioInputPin();

		private AudioOutputPin audioOutput = new AudioOutputPin();

		public Scope()
		{
			RegisterPin(audioInput, "Input");
			RegisterPin(audioOutput, "Output");
		}

		public override void OnAudioPinsUpdate()
		{
			if (audioInput.IsConnected() && audioOutput.HasBuffer())
			{
				AudioBuffer buffer = audioInput.GetBuffer();
				AudioBuffer buffer2 = audioOutput.GetBuffer();
				buffer.CopyToBuffer(buffer2);
			}
		}
	}
}
