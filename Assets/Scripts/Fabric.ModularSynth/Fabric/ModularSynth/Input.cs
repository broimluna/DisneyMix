namespace Fabric.ModularSynth
{
	internal class Input : Module
	{
		private int frameID;

		private AudioOutputPin audioOutput = new AudioOutputPin();

		public Input()
		{
			RegisterPin(audioOutput, "Output");
			frameID = -1;
		}

		public void Process(float[] inputs, int numChannels, int numSamples)
		{
			Update(frameID++);
			AudioBuffer buffer = audioOutput.GetBuffer();
			if (buffer != null)
			{
				if (buffer.NumChannels != numChannels || buffer.Size != numSamples)
				{
					buffer.Resize(numSamples, numChannels);
				}
				for (int i = 0; i < numSamples; i++)
				{
					buffer[i] = inputs[i];
				}
			}
		}
	}
}
