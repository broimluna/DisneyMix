using System.Collections.Generic;

namespace Fabric.ModularSynth
{
	internal class Output : Module
	{
		private int frameID;

		private AudioInputPin audioInput = new AudioInputPin();

		public Output()
		{
			RegisterPin(audioInput, "Input");
			frameID = -1;
		}

		public void ProcessModuleList(float[] outputs, int numChannels, int numSamples, ref List<Module> moduleList)
		{
			AudioBuffer buffer = audioInput.GetBuffer();
			if (buffer != null)
			{
				if (buffer.NumChannels != numChannels || buffer.Size != numSamples)
				{
					buffer.Resize(numSamples, numChannels);
				}
				buffer.Clear();
			}
			for (int i = 0; i < moduleList.Count; i++)
			{
				moduleList[i].Update(frameID++, true);
			}
			if (buffer != null && audioInput.IsConnected())
			{
				for (int j = 0; j < numSamples; j++)
				{
					outputs[j] = buffer[j];
				}
			}
		}

		public void Process(float[] outputs, int numChannels, int numSamples)
		{
			AudioBuffer buffer = audioInput.GetBuffer();
			if (buffer != null)
			{
				if (buffer.NumChannels != numChannels || buffer.Size != numSamples)
				{
					buffer.Resize(numSamples, numChannels);
				}
				buffer.Clear();
			}
			Update(frameID++);
			if (buffer != null && audioInput.IsConnected())
			{
				for (int i = 0; i < numSamples; i++)
				{
					outputs[i] = buffer[i];
				}
			}
		}
	}
}
