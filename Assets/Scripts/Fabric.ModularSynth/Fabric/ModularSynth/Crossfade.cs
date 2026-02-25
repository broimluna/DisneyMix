namespace Fabric.ModularSynth
{
	internal class Crossfade : Module
	{
		private AudioInputPin audioInput1 = new AudioInputPin();

		private AudioInputPin audioInput2 = new AudioInputPin();

		private AudioOutputPin audioOutput = new AudioOutputPin();

		private Envelope EnvelopeIn1 = new Envelope();

		private Envelope EnvelopeIn2 = new Envelope();

		private ControlInputPin XFade = new ControlInputPin(0.5f, 0f, 1f);

		public Crossfade()
		{
			RegisterPin(audioOutput, "Output");
			RegisterPin(audioInput1, "In1");
			RegisterPin(audioInput2, "In2");
			RegisterPin(XFade, "XFade");
			EnvelopeIn1._points = new Point[2];
			EnvelopeIn1._points[0] = new Point(0f, 1f, CurveTypes.Linear);
			EnvelopeIn1._points[1] = new Point(1f, 0f, CurveTypes.Linear);
			EnvelopeIn2._points = new Point[2];
			EnvelopeIn2._points[0] = new Point(0f, 0f, CurveTypes.Linear);
			EnvelopeIn2._points[1] = new Point(1f, 1f, CurveTypes.Linear);
		}

		public override void OnAudioPinsUpdate()
		{
			if (audioOutput.HasBuffer())
			{
				AudioBuffer buffer = audioOutput.GetBuffer();
				float x = (float)XFade.GetValue();
				buffer.Clear();
				if (audioInput1.IsConnected())
				{
					AudioBuffer buffer2 = audioInput1.GetBuffer();
					buffer.AddBuffer(buffer2, EnvelopeIn1.Calculate_y(x));
				}
				if (audioInput2.IsConnected())
				{
					AudioBuffer buffer3 = audioInput2.GetBuffer();
					buffer.AddBuffer(buffer3, EnvelopeIn2.Calculate_y(x));
				}
			}
		}
	}
}
