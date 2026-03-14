using System;

namespace Fabric.ModularSynth
{
	internal class Oscillator : Module
	{
		private enum OscillatorTypes
		{
			Sine = 0,
			Square = 1,
			Sawtooth = 2,
			Noise = 3
		}

		private static ControlPinList typeList = new ControlPinList
		{
			selectedIndex = 0,
			list = new string[4]
			{
				OscillatorTypes.Sine.ToString(),
				OscillatorTypes.Square.ToString(),
				OscillatorTypes.Sawtooth.ToString(),
				OscillatorTypes.Noise.ToString()
			}
		};

		private AudioOutputPin audioOutput = new AudioOutputPin();

		private ControlInputPin rateHz = new ControlInputPin(440f, 0f, 6000f);

		private ControlInputPin ampl = new ControlInputPin(1f, 0f, 1f);

		private ControlInputPin oscType = new ControlInputPin(typeList);

		private float[] wavetable = new float[44100];

		private float amplitude = 1f;

		private float targetAmplitude = 1f;

		private float frequency = 440f;

		private float targetFrequency = 440f;

		private OscillatorTypes oscillatorType;

		private int pos;

		private Random rnd = new Random();

		public Oscillator()
		{
			RegisterPin(audioOutput, "Output");
			RegisterPin(ampl, "Amplitude");
			RegisterPin(rateHz, "RateHz");
			RegisterPin(oscType, "Type");
		}

		public override void OnCreate()
		{
			double num = 0.00014247585730565955;
			for (int i = 0; i < wavetable.Length; i++)
			{
				wavetable[i] = (float)Math.Sin((double)i * num);
			}
			oscillatorType = OscillatorTypes.Sine;
		}

		public override void OnAudioPinsUpdate()
		{
			if (!audioOutput.HasBuffer() || wavetable == null)
			{
				return;
			}
			AudioBuffer buffer = audioOutput.GetBuffer();
			for (int i = 0; i < buffer.Size; i += buffer.NumChannels)
			{
				if (amplitude > targetAmplitude)
				{
					amplitude -= 0.001f;
				}
				else if (amplitude < targetAmplitude)
				{
					amplitude += 0.001f;
				}
				if (oscillatorType == OscillatorTypes.Noise)
				{
					float num = 2f * (float)rnd.NextDouble() / 1f;
					num -= 1f;
					buffer[i] = num;
					continue;
				}
				buffer[i] = wavetable[pos] * amplitude;
				if (frequency > targetFrequency)
				{
					frequency -= 0.3f;
				}
				else if (frequency < targetFrequency)
				{
					frequency += 0.3f;
				}
				pos += (int)frequency;
				if (pos >= wavetable.Length)
				{
					pos -= wavetable.Length;
				}
			}
		}

		public override void OnControlPinsUpdated()
		{
			targetFrequency = (float)rateHz.GetValue();
			targetAmplitude = (float)ampl.GetValue();
			AudioBuffer buffer = audioOutput.GetBuffer();
			if (wavetable == null || (float)wavetable.Length != buffer.SampleRate)
			{
				wavetable = new float[(int)buffer.SampleRate];
			}
			ControlPinList controlPinList = (ControlPinList)oscType.GetValue();
			OscillatorTypes selectedIndex = (OscillatorTypes)controlPinList.selectedIndex;
			if (oscillatorType == selectedIndex)
			{
				return;
			}
			switch (selectedIndex)
			{
			case OscillatorTypes.Sine:
			{
				double num6 = Math.PI * 2.0 / (double)buffer.SampleRate;
				for (int j = 0; j < wavetable.Length; j++)
				{
					wavetable[j] = (float)Math.Sin((double)j * num6);
				}
				break;
			}
			case OscillatorTypes.Square:
			{
				for (int k = 0; k < wavetable.Length; k++)
				{
					if (k < wavetable.Length / 2)
					{
						wavetable[k] = 1f;
					}
					else
					{
						wavetable[k] = -1f;
					}
				}
				break;
			}
			case OscillatorTypes.Sawtooth:
			{
				float num = 1f;
				float num2 = 0f;
				int num3 = (int)buffer.SampleRate / 4;
				int num4 = 3 * (int)buffer.SampleRate / 4;
				float num5 = 1f / (float)num3;
				for (int i = 0; i < wavetable.Length; i++)
				{
					num = ((i <= num3 || i > num4) ? 1f : (-1f));
					num2 += num * num5;
					wavetable[i] = num2;
				}
				break;
			}
			}
			oscillatorType = selectedIndex;
		}
	}
}
