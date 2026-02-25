using System;
using System.Text;

namespace ZXing.PDF417.Internal.EC
{
	internal sealed class ModulusPoly
	{
		private readonly ModulusGF field;

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

		public ModulusPoly(ModulusGF field, int[] coefficients)
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
			if (a == 0)
			{
				return getCoefficient(0);
			}
			int num = coefficients.Length;
			int num2 = 0;
			if (a == 1)
			{
				int[] array = coefficients;
				foreach (int b in array)
				{
					num2 = field.add(num2, b);
				}
				return num2;
			}
			num2 = coefficients[0];
			for (int j = 1; j < num; j++)
			{
				num2 = field.add(field.multiply(a, num2), coefficients[j]);
			}
			return num2;
		}

		internal ModulusPoly add(ModulusPoly other)
		{
			if (!field.Equals(other.field))
			{
				throw new ArgumentException("ModulusPolys do not have same ModulusGF field");
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
				array4[i] = field.add(array[i - num], array2[i]);
			}
			return new ModulusPoly(field, array4);
		}

		internal ModulusPoly subtract(ModulusPoly other)
		{
			if (!field.Equals(other.field))
			{
				throw new ArgumentException("ModulusPolys do not have same ModulusGF field");
			}
			if (other.isZero)
			{
				return this;
			}
			return add(other.getNegative());
		}

		internal ModulusPoly multiply(ModulusPoly other)
		{
			if (!field.Equals(other.field))
			{
				throw new ArgumentException("ModulusPolys do not have same ModulusGF field");
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
					array3[i + j] = field.add(array3[i + j], field.multiply(a, array2[j]));
				}
			}
			return new ModulusPoly(field, array3);
		}

		internal ModulusPoly getNegative()
		{
			int num = coefficients.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = field.subtract(0, coefficients[i]);
			}
			return new ModulusPoly(field, array);
		}

		internal ModulusPoly multiply(int scalar)
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
				return new ModulusPoly(field, array);
			}
			}
		}

		internal ModulusPoly multiplyByMonomial(int degree, int coefficient)
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
			return new ModulusPoly(field, array);
		}

		internal ModulusPoly[] divide(ModulusPoly other)
		{
			if (!field.Equals(other.field))
			{
				throw new ArgumentException("ModulusPolys do not have same ModulusGF field");
			}
			if (other.isZero)
			{
				throw new DivideByZeroException();
			}
			ModulusPoly modulusPoly = field.Zero;
			ModulusPoly modulusPoly2 = this;
			int coefficient = other.getCoefficient(other.Degree);
			int b = field.inverse(coefficient);
			while (modulusPoly2.Degree >= other.Degree && !modulusPoly2.isZero)
			{
				int degree = modulusPoly2.Degree - other.Degree;
				int coefficient2 = field.multiply(modulusPoly2.getCoefficient(modulusPoly2.Degree), b);
				ModulusPoly other2 = other.multiplyByMonomial(degree, coefficient2);
				ModulusPoly other3 = field.buildMonomial(degree, coefficient2);
				modulusPoly = modulusPoly.add(other3);
				modulusPoly2 = modulusPoly2.subtract(other2);
			}
			return new ModulusPoly[2] { modulusPoly, modulusPoly2 };
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
						stringBuilder.Append(num2);
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
