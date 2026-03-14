namespace ZXing.Common.ReedSolomon
{
	public sealed class ReedSolomonDecoder
	{
		private readonly GenericGF field;

		public ReedSolomonDecoder(GenericGF field)
		{
			this.field = field;
		}

		public bool decode(int[] received, int twoS)
		{
			GenericGFPoly genericGFPoly = new GenericGFPoly(field, received);
			int[] array = new int[twoS];
			bool flag = true;
			for (int i = 0; i < twoS; i++)
			{
				int num = genericGFPoly.evaluateAt(field.exp(i + field.GeneratorBase));
				array[array.Length - 1 - i] = num;
				if (num != 0)
				{
					flag = false;
				}
			}
			if (flag)
			{
				return true;
			}
			GenericGFPoly b = new GenericGFPoly(field, array);
			GenericGFPoly[] array2 = runEuclideanAlgorithm(field.buildMonomial(twoS, 1), b, twoS);
			if (array2 == null)
			{
				return false;
			}
			GenericGFPoly errorLocator = array2[0];
			int[] array3 = findErrorLocations(errorLocator);
			if (array3 == null)
			{
				return false;
			}
			GenericGFPoly errorEvaluator = array2[1];
			int[] array4 = findErrorMagnitudes(errorEvaluator, array3);
			for (int j = 0; j < array3.Length; j++)
			{
				int num2 = received.Length - 1 - field.log(array3[j]);
				if (num2 < 0)
				{
					return false;
				}
				received[num2] = GenericGF.addOrSubtract(received[num2], array4[j]);
			}
			return true;
		}

		internal GenericGFPoly[] runEuclideanAlgorithm(GenericGFPoly a, GenericGFPoly b, int R)
		{
			if (a.Degree < b.Degree)
			{
				GenericGFPoly genericGFPoly = a;
				a = b;
				b = genericGFPoly;
			}
			GenericGFPoly genericGFPoly2 = a;
			GenericGFPoly genericGFPoly3 = b;
			GenericGFPoly genericGFPoly4 = field.Zero;
			GenericGFPoly genericGFPoly5 = field.One;
			while (genericGFPoly3.Degree >= R / 2)
			{
				GenericGFPoly genericGFPoly6 = genericGFPoly2;
				GenericGFPoly other = genericGFPoly4;
				genericGFPoly2 = genericGFPoly3;
				genericGFPoly4 = genericGFPoly5;
				if (genericGFPoly2.isZero)
				{
					return null;
				}
				genericGFPoly3 = genericGFPoly6;
				GenericGFPoly genericGFPoly7 = field.Zero;
				int coefficient = genericGFPoly2.getCoefficient(genericGFPoly2.Degree);
				int b2 = field.inverse(coefficient);
				while (genericGFPoly3.Degree >= genericGFPoly2.Degree && !genericGFPoly3.isZero)
				{
					int degree = genericGFPoly3.Degree - genericGFPoly2.Degree;
					int coefficient2 = field.multiply(genericGFPoly3.getCoefficient(genericGFPoly3.Degree), b2);
					genericGFPoly7 = genericGFPoly7.addOrSubtract(field.buildMonomial(degree, coefficient2));
					genericGFPoly3 = genericGFPoly3.addOrSubtract(genericGFPoly2.multiplyByMonomial(degree, coefficient2));
				}
				genericGFPoly5 = genericGFPoly7.multiply(genericGFPoly4).addOrSubtract(other);
				if (genericGFPoly3.Degree >= genericGFPoly2.Degree)
				{
					return null;
				}
			}
			int coefficient3 = genericGFPoly5.getCoefficient(0);
			if (coefficient3 == 0)
			{
				return null;
			}
			int scalar = field.inverse(coefficient3);
			GenericGFPoly genericGFPoly8 = genericGFPoly5.multiply(scalar);
			GenericGFPoly genericGFPoly9 = genericGFPoly3.multiply(scalar);
			return new GenericGFPoly[2] { genericGFPoly8, genericGFPoly9 };
		}

		private int[] findErrorLocations(GenericGFPoly errorLocator)
		{
			int degree = errorLocator.Degree;
			if (degree == 1)
			{
				return new int[1] { errorLocator.getCoefficient(1) };
			}
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

		private int[] findErrorMagnitudes(GenericGFPoly errorEvaluator, int[] errorLocations)
		{
			int num = errorLocations.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				int num2 = field.inverse(errorLocations[i]);
				int a = 1;
				for (int j = 0; j < num; j++)
				{
					if (i != j)
					{
						int num3 = field.multiply(errorLocations[j], num2);
						int b = (((num3 & 1) != 0) ? (num3 & -2) : (num3 | 1));
						a = field.multiply(a, b);
					}
				}
				array[i] = field.multiply(errorEvaluator.evaluateAt(num2), field.inverse(a));
				if (field.GeneratorBase != 0)
				{
					array[i] = field.multiply(array[i], num2);
				}
			}
			return array;
		}
	}
}
