namespace Fabric.ModularSynth
{
	public class SamplePlayer : Module
	{
		private AudioOutputPin audioOutput = new AudioOutputPin();

		private ControlInputPin volume = new ControlInputPin(1f, 0f, 1f);

		private ControlInputPin pitch = new ControlInputPin(1f, 0f, 3f);

		private ControlInputPin loop = new ControlInputPin(false);

		public SampleFileInstance sampleFileInstance;

		private string sampleFileName = "";

		public SamplePlayer()
		{
			RegisterPin(audioOutput, "Output");
			RegisterPin(volume, "Volume");
			RegisterPin(pitch, "Pitch");
			RegisterPin(loop, "Loop");
			RegisterParameter(sampleFileName, "SampleInstance");
		}

		public override void OnCreate()
		{
			string text = (string)GetParameter("SampleInstance");
			int sampleFileIndexByName = SampleManager.Instance.GetSampleFileIndexByName(text);
			if (sampleFileIndexByName >= 0)
			{
				SampleFile sampleFileByIndex = SampleManager.Instance.GetSampleFileByIndex(sampleFileIndexByName);
				if (sampleFileByIndex != null)
				{
					sampleFileInstance = sampleFileByIndex.GetInstance();
				}
			}
		}

		public override void OnControlPinsUpdated()
		{
			if (sampleFileInstance != null)
			{
				sampleFileInstance.looped = (bool)loop.GetValue();
			}
		}

		public override void OnAudioPinsUpdate()
		{
			if (audioOutput.HasBuffer() && sampleFileInstance != null)
			{
				AudioBuffer audioBuffer = audioOutput.GetBuffer();
				sampleFileInstance.FillAudioBuffer(ref audioBuffer, (float)volume.GetValue(), (float)pitch.GetValue());
			}
		}
	}
}
