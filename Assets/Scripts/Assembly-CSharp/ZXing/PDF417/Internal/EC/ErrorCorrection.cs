namespace ZXing.PDF417.Internal.EC
{
	public sealed class ErrorCorrection
	{
		private readonly ModulusGF field;

		public ErrorCorrection()
		{
			field = ModulusGF.PDF417_GF;
		}

		public bool decode(int[] received, int numECCodewords, int[] erasures, out int errorLocationsCount)
		{
			ModulusPoly modulusPoly = new ModulusPoly(field, received);
			int[] array = new int[numECCodewords];
			bool flag = false;
			errorLocationsCount = 0;
			for (int num = numECCodewords; num > 0; num--)
			{
				if ((array[numECCodewords - num] = modulusPoly.evaluateAt(field.exp(num))) != 0)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return true;
			}
			ModulusPoly modulusPoly2 = field.One;
			foreach (int num2 in erasures)
			{
				int b = field.exp(received.Length - 1 - num2);
				ModulusPoly other = new ModulusPoly(field, new int[2]
				{
					field.subtract(0, b),
					1
				});
				modulusPoly2 = modulusPoly2.multiply(other);
			}
			ModulusPoly b2 = new ModulusPoly(field, array);
			ModulusPoly[] array2 = runEuclideanAlgorithm(field.buildMonomial(numECCodewords, 1), b2, numECCodewords);
			if (array2 == null)
			{
				return false;
			}
			ModulusPoly modulusPoly3 = array2[0];
			ModulusPoly modulusPoly4 = array2[1];
			if (modulusPoly3 == null || modulusPoly4 == null)
			{
				return false;
			}
			int[] array3 = findErrorLocations(modulusPoly3);
			if (array3 == null)
			{
				return false;
			}
			int[] array4 = findErrorMagnitudes(modulusPoly4, modulusPoly3, array3);
			for (int j = 0; j < array3.Length; j++)
			{
				int num3 = received.Length - 1 - field.log(array3[j]);
				if (num3 < 0)
				{
					return false;
				}
				received[num3] = field.subtract(received[num3], array4[j]);
			}
			errorLocationsCount = array3.Length;
			return true;
		}

		private ModulusPoly[] runEuclideanAlgorithm(ModulusPoly a, ModulusPoly b, int R)
		{
			if (a.Degree < b.Degree)
			{
				ModulusPoly modulusPoly = a;
				a = b;
				b = modulusPoly;
			}
			ModulusPoly modulusPoly2 = a;
			ModulusPoly modulusPoly3 = b;
			ModulusPoly modulusPoly4 = field.Zero;
			ModulusPoly modulusPoly5 = field.One;
			while (modulusPoly3.Degree >= R / 2)
			{
				ModulusPoly modulusPoly6 = modulusPoly2;
				ModulusPoly other = modulusPoly4;
				modulusPoly2 = modulusPoly3;
				modulusPoly4 = modulusPoly5;
				if (modulusPoly2.isZero)
				{
					return null;
				}
				modulusPoly3 = modulusPoly6;
				ModulusPoly modulusPoly7 = field.Zero;
				int coefficient = modulusPoly2.getCoefficient(modulusPoly2.Degree);
				int b2 = field.inverse(coefficient);
				while (modulusPoly3.Degree >= modulusPoly2.Degree && !modulusPoly3.isZero)
				{
					int degree = modulusPoly3.Degree - modulusPoly2.Degree;
					int coefficient2 = field.multiply(modulusPoly3.getCoefficient(modulusPoly3.Degree), b2);
					modulusPoly7 = modulusPoly7.add(field.buildMonomial(degree, coefficient2));
					modulusPoly3 = modulusPoly3.subtract(modulusPoly2.multiplyByMonomial(degree, coefficient2));
				}
				modulusPoly5 = modulusPoly7.multiply(modulusPoly4).subtract(other).getNegative();
			}
			int coefficient3 = modulusPoly5.getCoefficient(0);
			if (coefficient3 == 0)
			{
				return null;
			}
			int scalar = field.inverse(coefficient3);
			ModulusPoly modulusPoly8 = modulusPoly5.multiply(scalar);
			ModulusPoly modulusPoly9 = modulusPoly3.multiply(scalar);
			return new ModulusPoly[2] { modulusPoly8, modulusPoly9 };
		}

		private int[] findErrorLocations(ModulusPoly errorLocator)
		{
			int degree = errorLocator.Degree;
			int[] array = new int[degree];
			int num = 0;
			for (int i = 1; i < field.Size; i++)
			{
				if (num >= degree)
				{
					break;
				}
				if (errorLocator.evaluateAt(i) == 0)
				{
					array[num] = field.inverse(i);
					num++;
				}
			}
			if (num != degree)
			{
				return null;
			}
			return array;
		}

		private int[] findErrorMagnitudes(ModulusPoly errorEvaluator, ModulusPoly errorLocator, int[] errorLocations)
		{
			int degree = errorLocator.Degree;
			int[] array = new int[degree];
			for (int i = 1; i <= degree; i++)
			{
				array[degree - i] = field.multiply(i, errorLocator.getCoefficient(i));
			}
			ModulusPoly modulusPoly = new ModulusPoly(field, array);
			int num = errorLocations.Length;
			int[] array2 = new int[num];
			for (int j = 0; j < num; j++)
			{
				int a = field.inverse(errorLocations[j]);
				int a2 = field.subtract(0, errorEvaluator.evaluateAt(a));
				int b = field.inverse(modulusPoly.evaluateAt(a));
				array2[j] = field.multiply(a2, b);
			}
			return array2;
		}
	}
}
