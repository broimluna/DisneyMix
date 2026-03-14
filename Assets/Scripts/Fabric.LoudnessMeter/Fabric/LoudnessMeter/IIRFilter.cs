using System;
using System.Collections.Generic;

namespace Fabric.LoudnessMeter
{
	internal class IIRFilter
	{
		protected double var1;

		protected double var2;

		protected double var3;

		protected double var4;

		protected double var5;

		protected double var6;

		protected double var7;

		protected double var8;

		protected double var9;

		protected double var10;

		private double var11;

		private double var12;

		private double var13;

		private double var14;

		private double var15;

		private List<double> list1 = new List<double>();

		private List<double> list2 = new List<double>();

		public IIRFilter(double in1 = 1.0, double in2 = 0.0, double in3 = 0.0, double in4 = 0.0, double in5 = 0.0)
		{
			var1 = in1;
			var2 = in2;
			var3 = in3;
			var4 = in4;
			var5 = in5;
			var6 = in1;
			var7 = in2;
			var8 = in3;
			var9 = in4;
			var10 = in5;
			double num = (2.0 - 2.0 * var5) / (var5 - var4 + 1.0);
			double num2 = Math.Sqrt((var4 + var5 + 1.0) / (var5 - var4 + 1.0));
			var11 = num2 / num;
			var15 = Math.Atan(num2);
			var13 = (var1 - var3) / (1.0 - var5);
			var12 = (var1 - var2 + var3) / (var5 - var4 + 1.0);
			var14 = (var1 + var2 + var3) / (var4 + var5 + 1.0);
		}

		public void Initialise(double in1, int in2)
		{
			list1.Clear();
			list2.Clear();
			for (int i = 0; i != in2; i++)
			{
				list1.Add(0.0);
				list2.Add(0.0);
			}
			double num = 48000.0;
			if (in1 == num)
			{
				var6 = var1;
				var7 = var2;
				var8 = var3;
				var9 = var4;
				var10 = var5;
			}
			else
			{
				double num2 = Math.Tan(var15 * num / in1);
				double num3 = 1.0 / (1.0 + num2 / var11 + num2 * num2);
				var6 = (var12 + var13 * num2 / var11 + var14 * num2 * num2) * num3;
				var7 = 2.0 * (var14 * num2 * num2 - var12) * num3;
				var8 = (var12 - var13 * num2 / var11 + var14 * num2 * num2) * num3;
				var9 = 2.0 * (num2 * num2 - 1.0) * num3;
				var10 = (1.0 - num2 / var11 + num2 * num2) * num3;
			}
		}

		public void process(DeInterleavedBuffer buffer)
		{
			for (int i = 0; i < buffer.GetNumSamples(); i++)
			{
				for (int j = 0; j < buffer.GetNumChannels(); j++)
				{
					float sample = buffer.GetSample(i, j);
					double num = (double)sample - var9 * list1[j] - var10 * list2[j];
					double num2 = var6 * num + var7 * list1[j] + var8 * list2[j];
					list2[j] = list1[j];
					list1[j] = num;
					buffer.SetSample((float)num2, i, j);
				}
			}
		}
	}
}
