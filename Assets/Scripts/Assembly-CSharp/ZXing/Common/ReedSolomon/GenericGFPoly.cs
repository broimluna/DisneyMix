using System;
using System.Text;

namespace ZXing.Common.ReedSolomon
{
	internal sealed class GenericGFPoly
	{
		private readonly GenericGF field;

		private readonly int[] coefficients;

		internal int[] Coefficients
		{
			get
			{
				return coefficients;
			}
		}

		internal int Degree
		{
			get
			{
				return coefficients.Length - 1;
			}
		}

		internal bool isZero
		{
			get
			{
				return coefficients[0] == 0;
			}
		}

		internal GenericGFPoly(GenericGF field, int[] coefficients)
		{
			if (coefficients.Length == 0)
			{
				throw new ArgumentException();
			}
			this.field = field;
			int num = coefficients.Length;
			if (num > 1 && coefficients[0] == 0)
			{
				int i;
				for (i = 1; i < num && coefficients[i] == 0; i++)
				{
				}
				if (i == num)
				{
					this.coefficients = field.Zero.coefficients;
					return;
				}
				this.coefficients = new int[num - i];
				Array.Copy(coefficients, i, this.coefficients, 0, this.coefficients.Length);
			}
			else
			{
				this.coefficients = coefficients;
			}
		}

		internal int getCoefficient(int degree)
		{
			return coefficients[coefficients.Length - 1 - degree];
		}

		internal int evaluateAt(int a)
		{
			int num = 0;
			if (a == 0)
			{
				return getCoefficient(0);
			}
			int num2 = coefficients.Length;
			if (a == 1)
			{
				int[] array = coefficients;
				foreach (int b in array)
				{
					num = GenericGF.addOrSubtract(num, b);
				}
				return num;
			}
			num = coefficients[0];
			for (int j = 1; j < num2; j++)
			{
				num = GenericGF.addOrSubtract(field.multiply(a, num), coefficients[j]);
			}
			return num;
		}

		internal GenericGFPoly addOrSubtract(GenericGFPoly other)
		{
			if (!field.Equals(other.field))
			{
				throw new ArgumentException("GenericGFPolys do not have same GenericGF field");
			}
			if (isZero)
			{
				return other;
			}
			if (other.isZero)
			{
				return this;
			}
			int[] array = coefficients;
			int[] array2 = other.coefficients;
			if (array.Length > array2.Length)
			{
				int[] array3 = array;
				array = array2;
				array2 = array3;
			}
			int[] array4 = new int[array2.Length];
			int num = array2.Length - array.Length;
			Array.Copy(array2, 0, array4, 0, num);
			for (int i = num; i < array2.Length; i++)
			{
				array4[i] = GenericGF.addOrSubtract(array[i - num], array2[i]);
			}
			return new GenericGFPoly(field, array4);
		}

		internal GenericGFPoly multiply(GenericGFPoly other)
		{
			if (!field.Equals(other.field))
			{
				throw new ArgumentException("GenericGFPolys do not have same GenericGF field");
			}
			if (isZero || other.isZero)
			{
				return field.Zero;
			}
			int[] array = coefficients;
			int num = array.Length;
			int[] array2 = other.coefficients;
			int num2 = array2.Length;
			int[] array3 = new int[num + num2 - 1];
			for (int i = 0; i < num; i++)
			{
				int a = array[i];
				for (int j = 0; j < num2; j++)
				{
					array3[i + j] = GenericGF.addOrSubtract(array3[i + j], field.multiply(a, array2[j]));
				}
			}
			return new GenericGFPoly(field, array3);
		}

		internal GenericGFPoly multiply(int scalar)
		{
			switch (scalar)
			{
			case 0:
				return field.Zero;
			case 1:
				return this;
			default:
			{
				int num = coefficients.Length;
				int[] array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = field.multiply(coefficients[i], scalar);
				}
				return new GenericGFPoly(field, array);
			}
			}
		}

		internal GenericGFPoly multiplyByMonomial(int degree, int coefficient)
		{
			if (degree < 0)
			{
				throw new ArgumentException();
			}
			if (coefficient == 0)
			{
				return field.Zero;
			}
			int num = coefficients.Length;
			int[] array = new int[num + degree];
			for (int i = 0; i < num; i++)
			{
				array[i] = field.multiply(coefficients[i], coefficient);
			}
			return new GenericGFPoly(field, array);
		}

		internal GenericGFPoly[] divide(GenericGFPoly other)
		{
			if (!field.Equals(other.field))
			{
				throw new ArgumentException("GenericGFPolys do not have same GenericGF field");
			}
			if (other.isZero)
			{
				throw new ArgumentException("Divide by 0");
			}
			GenericGFPoly genericGFPoly = field.Zero;
			GenericGFPoly genericGFPoly2 = this;
			int coefficient = other.getCoefficient(other.Degree);
			int b = field.inverse(coefficient);
			while (genericGFPoly2.Degree >= other.Degree && !genericGFPoly2.isZero)
			{
				int degree = genericGFPoly2.Degree - other.Degree;
				int coefficient2 = field.multiply(genericGFPoly2.getCoefficient(genericGFPoly2.Degree), b);
				GenericGFPoly other2 = other.multiplyByMonomial(degree, coefficient2);
				GenericGFPoly other3 = field.buildMonomial(degree, coefficient2);
				genericGFPoly = genericGFPoly.addOrSubtract(other3);
				genericGFPoly2 = genericGFPoly2.addOrSubtract(other2);
			}
			return new GenericGFPoly[2] { genericGFPoly, genericGFPoly2 };
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(8 * Degree);
			for (int num = Degree; num >= 0; num--)
			{
				int num2 = getCoefficient(num);
				if (num2 != 0)
				{
					if (num2 < 0)
					{
						stringBuilder.Append(" - ");
						num2 = -num2;
					}
					else if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" + ");
					}
					if (num == 0 || num2 != 1)
					{
						int num3 = field.log(num2);
						switch (num3)
						{
						case 0:
							stringBuilder.Append('1');
							break;
						case 1:
							stringBuilder.Append('a');
							break;
						default:
							stringBuilder.Append("a^");
							stringBuilder.Append(num3);
							break;
						}
					}
					switch (num)
					{
					case 1:
						stringBuilder.Append('x');
						break;
					default:
						stringBuilder.Append("x^");
						stringBuilder.Append(num);
						break;
					case 0:
						break;
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
