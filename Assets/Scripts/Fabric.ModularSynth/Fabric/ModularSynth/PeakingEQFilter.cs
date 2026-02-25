namespace Fabric.ModularSynth
{
	internal class PeakingEQFilter : Module
	{
		private BiQuadFilter biQuadFilter = new BiQuadFilter();

		private AudioInputPin audioInput = new AudioInputPin();

		private AudioOutputPin audioOutput = new AudioOutputPin();

		private ControlInputPin q = new ControlInputPin(1f, 0f, 5f);

		private ControlInputPin dbGain = new ControlInputPin(1f, 0f, 5f);

		private ControlInputPin cutoffFrequency = new ControlInputPin(4000f, 10f, 22000f);

		public PeakingEQFilter()
		{
			RegisterPin(audioInput, "Input");
			RegisterPin(audioOutput, "Output");
			RegisterPin(cutoffFrequency, "CutoffFreq");
			RegisterPin(q, "Q");
			RegisterPin(dbGain, "dbGain");
		}

		public override void OnControlPinsUpdated()
		{
			float sampleRate = 44100f;
			if (audioOutput.HasBuffer())
			{
				sampleRate = audioOutput.GetBuffer().SampleRate;
			}
			biQuadFilter.PeakingEQ(sampleRate, (float)cutoffFrequency.GetValue(), (float)q.GetValue(), (float)dbGain.GetValue());
		}

		public override void OnAudioPinsUpdate()
		{
			if (audioInput.IsConnected() && audioOutput.HasBuffer())
			{
				AudioBuffer buffer = audioInput.GetBuffer();
				AudioBuffer buffer2 = audioOutput.GetBuffer();
				biQuadFilter.Transform(buffer, buffer2);
			}
		}
	}
}
