using System;
using System.Collections.Generic;

namespace ZXing.Common.ReedSolomon
{
	public sealed class ReedSolomonEncoder
	{
		private readonly GenericGF field;

		private readonly IList<GenericGFPoly> cachedGenerators;

		public ReedSolomonEncoder(GenericGF field)
		{
			this.field = field;
			cachedGenerators = new List<GenericGFPoly>();
			cachedGenerators.Add(new GenericGFPoly(field, new int[1] { 1 }));
		}

		private GenericGFPoly buildGenerator(int degree)
		{
			if (degree >= cachedGenerators.Count)
			{
				GenericGFPoly genericGFPoly = cachedGenerators[cachedGenerators.Count - 1];
				for (int i = cachedGenerators.Count; i <= degree; i++)
				{
					GenericGFPoly genericGFPoly2 = genericGFPoly.multiply(new GenericGFPoly(field, new int[2]
					{
						1,
						field.exp(i - 1 + field.GeneratorBase)
					}));
					cachedGenerators.Add(genericGFPoly2);
					genericGFPoly = genericGFPoly2;
				}
			}
			return cachedGenerators[degree];
		}

		public void encode(int[] toEncode, int ecBytes)
		{
			if (ecBytes == 0)
			{
				throw new ArgumentException("No error correction bytes");
			}
			int num = toEncode.Length - ecBytes;
			if (num <= 0)
			{
				throw new ArgumentException("No data bytes provided");
			}
			GenericGFPoly other = buildGenerator(ecBytes);
			int[] array = new int[num];
			Array.Copy(toEncode, 0, array, 0, num);
			GenericGFPoly genericGFPoly = new GenericGFPoly(field, array);
			genericGFPoly = genericGFPoly.multiplyByMonomial(ecBytes, 1);
			GenericGFPoly genericGFPoly2 = genericGFPoly.divide(other)[1];
			int[] coefficients = genericGFPoly2.Coefficients;
			int num2 = ecBytes - coefficients.Length;
			for (int i = 0; i < num2; i++)
			{
				toEncode[num + i] = 0;
			}
			Array.Copy(coefficients, 0, toEncode, num + num2, coefficients.Length);
		}
	}
}
