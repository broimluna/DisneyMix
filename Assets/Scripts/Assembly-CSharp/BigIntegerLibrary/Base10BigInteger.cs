using System.Text;

namespace BigIntegerLibrary
{
	internal sealed class Base10BigInteger
	{
		private class DigitContainer
		{
			private const int ChunkSize = 32;

			private const int ChunkSizeDivisionShift = 5;

			private const int ChunkCount = 200;

			private readonly long[][] digits;

			public long this[int index]
			{
				get
				{
					int num = index >> 5;
					long[] array = digits[num];
					return (array != null) ? array[index % 32] : 0;
				}
				set
				{
					int num = index >> 5;
					long[] array = digits[num] ?? (digits[num] = new long[32]);
					array[index % 32] = value;
				}
			}

			public DigitContainer()
			{
				digits = new long[200][];
			}
		}

		private const long NumberBase = 10L;

		private const int MaxSize = 6400;

		private static readonly Base10BigInteger Zero = new Base10BigInteger();

		private static readonly Base10BigInteger One = new Base10BigInteger(1L);

		private DigitContainer digits;

		private int size;

		private Sign sign;

		internal Sign NumberSign
		{
			set
			{
				sign = value;
			}
		}

		public Base10BigInteger()
		{
			digits = new DigitContainer();
			size = 1;
			digits[size] = 0L;
			sign = Sign.Positive;
		}

		public Base10BigInteger(long n)
		{
			digits = new DigitContainer();
			sign = Sign.Positive;
			if (n == 0L)
			{
				size = 1;
				digits[size] = 0L;
				return;
			}
			if (n < 0)
			{
				n = -n;
				sign = Sign.Negative;
			}
			size = 0;
			while (n > 0)
			{
				digits[size] = n % 10;
				n /= 10;
				size++;
			}
		}

		public Base10BigInteger(Base10BigInteger n)
		{
			digits = new DigitContainer();
			size = n.size;
			sign = n.sign;
			for (int i = 0; i < n.size; i++)
			{
				digits[i] = n.digits[i];
			}
		}

		public bool Equals(Base10BigInteger other)
		{
			if (sign != other.sign)
			{
				return false;
			}
			if (size != other.size)
			{
				return false;
			}
			for (int i = 0; i < size; i++)
			{
				if (digits[i] != other.digits[i])
				{
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object o)
		{
			if (!(o is Base10BigInteger))
			{
				return false;
			}
			return Equals((Base10BigInteger)o);
		}

		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < size; i++)
			{
				num += (int)digits[i];
			}
			return num;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder;
			if (sign == Sign.Negative)
			{
				stringBuilder = new StringBuilder(size + 1);
				stringBuilder.Append('-');
			}
			else
			{
				stringBuilder = new StringBuilder(size);
			}
			for (int num = size - 1; num >= 0; num--)
			{
				stringBuilder.Append(digits[num]);
			}
			return stringBuilder.ToString();
		}

		public static Base10BigInteger Opposite(Base10BigInteger n)
		{
			Base10BigInteger base10BigInteger = new Base10BigInteger(n);
			if (base10BigInteger != Zero)
			{
				if (base10BigInteger.sign == Sign.Positive)
				{
					base10BigInteger.sign = Sign.Negative;
				}
				else
				{
					base10BigInteger.sign = Sign.Positive;
				}
			}
			return base10BigInteger;
		}

		public static bool Greater(Base10BigInteger a, Base10BigInteger b)
		{
			if (a.sign != b.sign)
			{
				if (a.sign == Sign.Negative && b.sign == Sign.Positive)
				{
					return false;
				}
				if (a.sign == Sign.Positive && b.sign == Sign.Negative)
				{
					return true;
				}
			}
			else if (a.sign == Sign.Positive)
			{
				if (a.size > b.size)
				{
					return true;
				}
				if (a.size < b.size)
				{
					return false;
				}
				for (int num = a.size - 1; num >= 0; num--)
				{
					if (a.digits[num] > b.digits[num])
					{
						return true;
					}
					if (a.digits[num] < b.digits[num])
					{
						return false;
					}
				}
			}
			else
			{
				if (a.size < b.size)
				{
					return true;
				}
				if (a.size > b.size)
				{
					return false;
				}
				for (int num2 = a.size - 1; num2 >= 0; num2--)
				{
					if (a.digits[num2] < b.digits[num2])
					{
						return true;
					}
					if (a.digits[num2] > b.digits[num2])
					{
						return false;
					}
				}
			}
			return false;
		}

		public static bool GreaterOrEqual(Base10BigInteger a, Base10BigInteger b)
		{
			return Greater(a, b) || object.Equals(a, b);
		}

		public static bool Smaller(Base10BigInteger a, Base10BigInteger b)
		{
			return !GreaterOrEqual(a, b);
		}

		public static bool SmallerOrEqual(Base10BigInteger a, Base10BigInteger b)
		{
			return !Greater(a, b);
		}

		public static Base10BigInteger Abs(Base10BigInteger n)
		{
			Base10BigInteger base10BigInteger = new Base10BigInteger(n);
			base10BigInteger.sign = Sign.Positive;
			return base10BigInteger;
		}

		public static Base10BigInteger Addition(Base10BigInteger a, Base10BigInteger b)
		{
			Base10BigInteger base10BigInteger = null;
			if (a.sign == Sign.Positive && b.sign == Sign.Positive)
			{
				base10BigInteger = ((!(a >= b)) ? Add(b, a) : Add(a, b));
				base10BigInteger.sign = Sign.Positive;
			}
			if (a.sign == Sign.Negative && b.sign == Sign.Negative)
			{
				base10BigInteger = ((!(a <= b)) ? Add(-b, -a) : Add(-a, -b));
				base10BigInteger.sign = Sign.Negative;
			}
			if (a.sign == Sign.Positive && b.sign == Sign.Negative)
			{
				if (a >= -b)
				{
					base10BigInteger = Subtract(a, -b);
					base10BigInteger.sign = Sign.Positive;
				}
				else
				{
					base10BigInteger = Subtract(-b, a);
					base10BigInteger.sign = Sign.Negative;
				}
			}
			if (a.sign == Sign.Negative && b.sign == Sign.Positive)
			{
				if (-a <= b)
				{
					base10BigInteger = Subtract(b, -a);
					base10BigInteger.sign = Sign.Positive;
				}
				else
				{
					base10BigInteger = Subtract(-a, b);
					base10BigInteger.sign = Sign.Negative;
				}
			}
			return base10BigInteger;
		}

		public static Base10BigInteger Subtraction(Base10BigInteger a, Base10BigInteger b)
		{
			Base10BigInteger base10BigInteger = null;
			if (a.sign == Sign.Positive && b.sign == Sign.Positive)
			{
				if (a >= b)
				{
					base10BigInteger = Subtract(a, b);
					base10BigInteger.sign = Sign.Positive;
				}
				else
				{
					base10BigInteger = Subtract(b, a);
					base10BigInteger.sign = Sign.Negative;
				}
			}
			if (a.sign == Sign.Negative && b.sign == Sign.Negative)
			{
				if (a <= b)
				{
					base10BigInteger = Subtract(-a, -b);
					base10BigInteger.sign = Sign.Negative;
				}
				else
				{
					base10BigInteger = Subtract(-b, -a);
					base10BigInteger.sign = Sign.Positive;
				}
			}
			if (a.sign == Sign.Positive && b.sign == Sign.Negative)
			{
				base10BigInteger = ((!(a >= -b)) ? Add(-b, a) : Add(a, -b));
				base10BigInteger.sign = Sign.Positive;
			}
			if (a.sign == Sign.Negative && b.sign == Sign.Positive)
			{
				base10BigInteger = ((!(-a >= b)) ? Add(b, -a) : Add(-a, b));
				base10BigInteger.sign = Sign.Negative;
			}
			return base10BigInteger;
		}

		public static Base10BigInteger Multiplication(Base10BigInteger a, Base10BigInteger b)
		{
			if (a == Zero || b == Zero)
			{
				return Zero;
			}
			Base10BigInteger base10BigInteger = Multiply(Abs(a), Abs(b));
			if (a.sign == b.sign)
			{
				base10BigInteger.sign = Sign.Positive;
			}
			else
			{
				base10BigInteger.sign = Sign.Negative;
			}
			return base10BigInteger;
		}

		private static Base10BigInteger Add(Base10BigInteger a, Base10BigInteger b)
		{
			Base10BigInteger base10BigInteger = new Base10BigInteger(a);
			long num = 0L;
			for (int i = 0; i < b.size; i++)
			{
				long num2 = base10BigInteger.digits[i] + b.digits[i] + num;
				base10BigInteger.digits[i] = num2 % 10;
				num = num2 / 10;
			}
			for (int i = b.size; i < a.size; i++)
			{
				if (num <= 0)
				{
					break;
				}
				long num2 = base10BigInteger.digits[i] + num;
				base10BigInteger.digits[i] = num2 % 10;
				num = num2 / 10;
			}
			if (num > 0)
			{
				base10BigInteger.digits[base10BigInteger.size] = num % 10;
				base10BigInteger.size++;
				num /= 10;
			}
			return base10BigInteger;
		}

		private static Base10BigInteger Subtract(Base10BigInteger a, Base10BigInteger b)
		{
			Base10BigInteger base10BigInteger = new Base10BigInteger(a);
			long num = 0L;
			bool flag = true;
			for (int i = 0; i < b.size; i++)
			{
				long num2 = base10BigInteger.digits[i] - b.digits[i] - num;
				if (num2 < 0)
				{
					num = 1L;
					num2 += 10;
				}
				else
				{
					num = 0L;
				}
				base10BigInteger.digits[i] = num2;
			}
			for (int i = b.size; i < a.size; i++)
			{
				if (num <= 0)
				{
					break;
				}
				long num2 = base10BigInteger.digits[i] - num;
				if (num2 < 0)
				{
					num = 1L;
					num2 += 10;
				}
				else
				{
					num = 0L;
				}
				base10BigInteger.digits[i] = num2;
			}
			while (base10BigInteger.size - 1 > 0 && flag)
			{
				if (base10BigInteger.digits[base10BigInteger.size - 1] == 0L)
				{
					base10BigInteger.size--;
				}
				else
				{
					flag = false;
				}
			}
			return base10BigInteger;
		}

		private static Base10BigInteger Multiply(Base10BigInteger a, Base10BigInteger b)
		{
			long num = 0L;
			Base10BigInteger base10BigInteger = new Base10BigInteger();
			base10BigInteger.size = a.size + b.size - 1;
			for (int i = 0; i < base10BigInteger.size + 1; i++)
			{
				base10BigInteger.digits[i] = 0L;
			}
			for (int i = 0; i < a.size; i++)
			{
				if (a.digits[i] == 0L)
				{
					continue;
				}
				for (int j = 0; j < b.size; j++)
				{
					if (b.digits[j] != 0L)
					{
						DigitContainer digitContainer2;
						DigitContainer digitContainer = (digitContainer2 = base10BigInteger.digits);
						int index2;
						int index = (index2 = i + j);
						long num2 = digitContainer2[index2];
						digitContainer[index] = num2 + a.digits[i] * b.digits[j];
					}
				}
			}
			for (int i = 0; i < base10BigInteger.size; i++)
			{
				long num3 = base10BigInteger.digits[i] + num;
				base10BigInteger.digits[i] = num3 % 10;
				num = num3 / 10;
			}
			if (num > 0)
			{
				base10BigInteger.digits[base10BigInteger.size] = num % 10;
				base10BigInteger.size++;
				num /= 10;
			}
			return base10BigInteger;
		}

		public static implicit operator Base10BigInteger(long n)
		{
			return new Base10BigInteger(n);
		}

		public static bool operator ==(Base10BigInteger a, Base10BigInteger b)
		{
			return object.Equals(a, b);
		}

		public static bool operator !=(Base10BigInteger a, Base10BigInteger b)
		{
			return !object.Equals(a, b);
		}

		public static bool operator >(Base10BigInteger a, Base10BigInteger b)
		{
			return Greater(a, b);
		}

		public static bool operator <(Base10BigInteger a, Base10BigInteger b)
		{
			return Smaller(a, b);
		}

		public static bool operator >=(Base10BigInteger a, Base10BigInteger b)
		{
			return GreaterOrEqual(a, b);
		}

		public static bool operator <=(Base10BigInteger a, Base10BigInteger b)
		{
			return SmallerOrEqual(a, b);
		}

		public static Base10BigInteger operator -(Base10BigInteger n)
		{
			return Opposite(n);
		}

		public static Base10BigInteger operator +(Base10BigInteger a, Base10BigInteger b)
		{
			return Addition(a, b);
		}

		public static Base10BigInteger operator -(Base10BigInteger a, Base10BigInteger b)
		{
			return Subtraction(a, b);
		}

		public static Base10BigInteger operator *(Base10BigInteger a, Base10BigInteger b)
		{
			return Multiplication(a, b);
		}

		public static Base10BigInteger operator ++(Base10BigInteger n)
		{
			return n + One;
		}

		public static Base10BigInteger operator --(Base10BigInteger n)
		{
			return n - One;
		}
	}
}
