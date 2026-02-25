namespace Fabric.ModularSynth
{
	public class ADSR : Module
	{
		private ControlInputPin gate = new ControlInputPin(false);

		private ControlInputPin attack = new ControlInputPin(0.2f, 0f, 1f);

		private ControlInputPin decay = new ControlInputPin(0.4f, 0f, 1f);

		private ControlInputPin release = new ControlInputPin(0.8f, 0f, 1f);

		private ControlInputPin sustain = new ControlInputPin(0.3f, 0f, 1f);

		private AudioOutputPin audioOutput = new AudioOutputPin();

		private InterpolatedParameter interpolatedParameter = new InterpolatedParameter();

		public Envelope envelope = new Envelope();

		public float currentPosition;

		private int timerIndex;

		private bool trigger;

		public ADSR()
		{
			RegisterPin(gate, "Gate");
			RegisterPin(attack, "Attack");
			RegisterPin(decay, "Decay");
			RegisterPin(release, "Release");
			RegisterPin(sustain, "Sustain Level");
			RegisterPin(audioOutput, "Output");
			envelope._points = new Point[5];
			envelope._points[0] = new Point(0f, 0f, CurveTypes.Linear);
			envelope._points[1] = new Point(0.2f, 1f, CurveTypes.Linear);
			envelope._points[2] = new Point(0.4f, 0.5f, CurveTypes.Linear);
			envelope._points[3] = new Point(0.8f, 0.5f, CurveTypes.Linear);
			envelope._points[4] = new Point(1f, 0f, CurveTypes.Linear);
		}

		public override void OnCreate()
		{
			timerIndex = 0;
		}

		public override void OnPlay()
		{
			trigger = (bool)gate.GetValue();
		}

		public override void OnControlPinsUpdated()
		{
			trigger = (bool)gate.GetValue();
			float x = (float)attack.GetValue();
			float x2 = (float)decay.GetValue();
			float x3 = (float)release.GetValue();
			float y = (float)sustain.GetValue();
			envelope._points[1]._x = x;
			envelope._points[2]._x = x2;
			envelope._points[2]._y = y;
			envelope._points[3]._x = x3;
			envelope._points[3]._y = y;
		}

		public override void OnAudioPinsUpdate()
		{
			if (!audioOutput.HasBuffer())
			{
				return;
			}
			AudioBuffer buffer = audioOutput.GetBuffer();
			for (int i = 0; i < buffer.Size; i++)
			{
				float timeMS = GetTimeMS(timerIndex++, buffer.SampleRate);
				if (trigger)
				{
					interpolatedParameter.Reset(0f);
					interpolatedParameter.SetTarget(timeMS, 1f, 5000f, 0.5f);
					trigger = false;
				}
				currentPosition = interpolatedParameter.Get(timeMS);
				buffer[i] = envelope.Calculate_y(currentPosition);
			}
		}

		private float GetTimeMS(float samples, float sampleRate)
		{
			return samples * 1000f / sampleRate;
		}
	}
}
