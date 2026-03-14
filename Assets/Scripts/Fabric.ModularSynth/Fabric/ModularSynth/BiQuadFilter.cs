using System;

namespace Fabric.ModularSynth
{
	internal class BiQuadFilter
	{
		private double a0;

		private double a1;

		private double a2;

		private double b0;

		private double b1;

		private double b2;

		private double x1;

		private double x2;

		private double y1;

		private double y2;

		public void Transform(AudioBuffer x, AudioBuffer y)
		{
			for (int i = 0; i < x.Size; i++)
			{
				double num = x[i];
				y[i] = (float)(b0 / a0 * num + b1 / a0 * x1 + b2 / a0 * x2 - a1 / a0 * y1 - a2 / a0 * y2);
				x2 = x1;
				x1 = num;
				y2 = y1;
				y1 = y[i];
			}
		}

		public void LowPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			double num = Math.PI * 2.0 * (double)cutoffFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num) / (double)(2f * q);
			b0 = (1.0 - num2) / 2.0;
			b1 = 1.0 - num2;
			b2 = (1.0 - num2) / 2.0;
			a0 = 1.0 + num3;
			a1 = -2.0 * num2;
			a2 = 1.0 - num3;
		}

		public void HighPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			double num = Math.PI * 2.0 * (double)cutoffFrequency / (double)sampleRate;
			Math.Cos(num);
			double num2 = Math.Sin(num) / (double)(2f * q);
			b0 = (1.0 + Math.Cos(num)) / 2.0;
			b1 = 0.0 - (1.0 + Math.Cos(num));
			b2 = (1.0 + Math.Cos(num)) / 2.0;
			a0 = 1.0 + num2;
			a1 = -2.0 * Math.Cos(num);
			a2 = 1.0 - num2;
		}

		public void BandPassFilterConstantPeakGain(float sampleRate, float centreFrequency, float q)
		{
			double num = Math.PI * 2.0 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = (b0 = num3 / (double)(2f * q));
			b1 = 0.0;
			b2 = 0.0 - num4;
			a0 = 1.0 + num4;
			a1 = -2.0 * num2;
			a2 = 1.0 - num4;
		}

		public void NotchFilter(float sampleRate, float centreFrequency, float q)
		{
			double num = Math.PI * 2.0 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			b0 = 1.0;
			b1 = -2.0 * num2;
			b2 = 1.0;
			a0 = 1.0 + num4;
			a1 = -2.0 * num2;
			a2 = 1.0 - num4;
		}

		public void AllPassFilter(float sampleRate, float centreFrequency, float q)
		{
			double num = Math.PI * 2.0 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			b0 = 1.0 - num4;
			b1 = -2.0 * num2;
			b2 = 1.0 + num4;
			a0 = 1.0 + num4;
			a1 = -2.0 * num2;
			a2 = 1.0 - num4;
		}

		public void PeakingEQ(float sampleRate, float centreFrequency, float q, float dbGain)
		{
			double num = Math.PI * 2.0 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			double num5 = Math.Pow(10.0, dbGain / 40f);
			b0 = 1.0 + num4 * num5;
			b1 = -2.0 * num2;
			b2 = 1.0 - num4 * num5;
			a0 = 1.0 + num4 / num5;
			a1 = -2.0 * num2;
			a2 = 1.0 - num4 / num5;
		}

		public void LowShelf(float sampleRate, float cutoffFrequency, float shelfSlope, float dbGain)
		{
			double num = Math.PI * 2.0 * (double)cutoffFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = Math.Pow(10.0, dbGain / 40f);
			double num5 = num3 / 2.0 * Math.Sqrt((num4 + 1.0 / num4) * (double)(1f / shelfSlope - 1f) + 2.0);
			double num6 = 2.0 * Math.Sqrt(num4) * num5;
			b0 = num4 * (num4 + 1.0 - (num4 - 1.0) * num2 + num6);
			b1 = 2.0 * num4 * (num4 - 1.0 - (num4 + 1.0) * num2);
			b2 = num4 * (num4 + 1.0 - (num4 - 1.0) * num2 - num6);
			a0 = num4 + 1.0 + (num4 - 1.0) * num2 + num6;
			a1 = -2.0 * (num4 - 1.0 + (num4 + 1.0) * num2);
			a2 = num4 + 1.0 + (num4 - 1.0) * num2 - num6;
		}

		public void HighShelf(float sampleRate, float cutoffFrequency, float shelfSlope, float dbGain)
		{
			double num = Math.PI * 2.0 * (double)cutoffFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = Math.Pow(10.0, dbGain / 40f);
			double num5 = num3 / 2.0 * Math.Sqrt((num4 + 1.0 / num4) * (double)(1f / shelfSlope - 1f) + 2.0);
			double num6 = 2.0 * Math.Sqrt(num4) * num5;
			b0 = num4 * (num4 + 1.0 + (num4 - 1.0) * num2 + num6);
			b1 = -2.0 * num4 * (num4 - 1.0 + (num4 + 1.0) * num2);
			b2 = num4 * (num4 + 1.0 + (num4 - 1.0) * num2 - num6);
			a0 = num4 + 1.0 - (num4 - 1.0) * num2 + num6;
			a1 = 2.0 * (num4 - 1.0 - (num4 + 1.0) * num2);
			a2 = num4 + 1.0 - (num4 - 1.0) * num2 - num6;
		}
	}
}
