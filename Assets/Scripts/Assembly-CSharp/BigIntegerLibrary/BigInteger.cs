using System;
using System.Runtime.Serialization;

namespace BigIntegerLibrary
{
	[Serializable]
	internal sealed class BigInteger : IComparable, ISerializable, IEquatable<BigInteger>, IComparable<BigInteger>
	{
		private class DigitContainer
		{
			private const int ChunkSize = 16;

			private const int ChunkSizeDivisionShift = 4;

			private const int ChunkCount = 80;

			private readonly long[][] digits;

			public long this[int index]
			{
				get
				{
					int num = index >> 4;
					long[] array = digits[num];
					return (array != null) ? array[index % 16] : 0;
				}
				set
				{
					int num = index >> 4;
					long[] array = digits[num] ?? (digits[num] = new long[16]);
					array[index % 16] = value;
				}
			}

			public DigitContainer()
			{
				digits = new long[80][];
			}
		}

		private const long NumberBase = 65536L;

		internal const int MaxSize = 1280;

		private const int RatioToBinaryDigits = 16;

		public static readonly BigInteger Zero = new BigInteger();

		public static readonly BigInteger One = new BigInteger(1L);

		public static readonly BigInteger Two = new BigInteger(2L);

		public static readonly BigInteger Ten = new BigInteger(10L);

		private DigitContainer digits;

		private int size;

		private Sign sign;

		public BigInteger()
		{
			digits = new DigitContainer();
			size = 1;
			digits[size] = 0L;
			sign = Sign.Positive;
		}

		public BigInteger(long n)
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
				digits[size] = n % 65536;
				n /= 65536;
				size++;
			}
		}

		public BigInteger(BigInteger n)
		{
			digits = new DigitContainer();
			size = n.size;
			sign = n.sign;
			for (int i = 0; i < n.size; i++)
			{
				digits[i] = n.digits[i];
			}
		}

		public BigInteger(string numberString)
		{
			BigInteger bigInteger = new BigInteger();
			Sign sign = Sign.Positive;
			for (int i = 0; i < numberString.Length; i++)
			{
				if (numberString[i] < '0' || numberString[i] > '9')
				{
					if (i != 0 || numberString[i] != '-')
					{
						throw new BigIntegerException("Invalid numeric string.", null);
					}
					sign = Sign.Negative;
				}
				else
				{
					bigInteger = bigInteger * Ten + long.Parse(numberString[i].ToString());
				}
			}
			this.sign = sign;
			digits = new DigitContainer();
			size = bigInteger.size;
			for (int i = 0; i < bigInteger.size; i++)
			{
				digits[i] = bigInteger.digits[i];
			}
		}

		public BigInteger(byte[] byteArray)
		{
			if (byteArray.Length / 4 > 1280)
			{
				throw new BigIntegerException("The byte array's content exceeds the maximum size of a BigInteger.", null);
			}
			digits = new DigitContainer();
			sign = Sign.Positive;
			for (int i = 0; i < byteArray.Length; i += 2)
			{
				int num = byteArray[i];
				if (i + 1 < byteArray.Length)
				{
					num <<= 8;
					num += byteArray[i + 1];
				}
				digits[size++] = num;
			}
			bool flag = true;
			while (size - 1 > 0 && flag)
			{
				if (digits[size - 1] == 0L)
				{
					size--;
				}
				else
				{
					flag = false;
				}
			}
		}

		private BigInteger(SerializationInfo info, StreamingContext context)
		{
			if ((bool)info.GetValue("sign", typeof(bool)))
			{
				sign = Sign.Positive;
			}
			else
			{
				sign = Sign.Negative;
			}
			size = (int)info.GetValue("size", typeof(short));
			digits = new DigitContainer();
			for (int i = 0; i < size; i++)
			{
				digits[i] = (long)info.GetValue(string.Format("{0}{1}", "d_", i.ToString()), typeof(ushort));
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (sign == Sign.Positive)
			{
				info.AddValue("sign", true);
			}
			else
			{
				info.AddValue("sign", false);
			}
			info.AddValue("size", (short)size);
			for (int i = 0; i < size; i++)
			{
				info.AddValue(string.Format("{0}{1}", "d_", i.ToString()), (ushort)digits[i]);
			}
		}

		public bool Equals(BigInteger other)
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
			if (!(o is BigInteger))
			{
				return false;
			}
			return Equals((BigInteger)o);
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
			Base10BigInteger base10BigInteger = new Base10BigInteger();
			Base10BigInteger base10BigInteger2 = new Base10BigInteger(1L);
			for (int i = 0; i < size; i++)
			{
				base10BigInteger += digits[i] * base10BigInteger2;
				base10BigInteger2 *= (Base10BigInteger)65536L;
			}
			base10BigInteger.NumberSign = sign;
			return base10BigInteger.ToString();
		}

		public static BigInteger Parse(string str)
		{
			return new BigInteger(str);
		}

		public int CompareTo(BigInteger other)
		{
			if (Greater(this, other))
			{
				return 1;
			}
			if (object.Equals(this, other))
			{
				return 0;
			}
			return -1;
		}

		public int CompareTo(object obj)
		{
			if (!(obj is BigInteger))
			{
				throw new ArgumentException("obj is not a BigInteger.");
			}
			return CompareTo((BigInteger)obj);
		}

		public static int SizeInBinaryDigits(BigInteger n)
		{
			return n.size * 16;
		}

		public static BigInteger Opposite(BigInteger n)
		{
			BigInteger bigInteger = new BigInteger(n);
			if (bigInteger != Zero)
			{
				if (bigInteger.sign == Sign.Positive)
				{
					bigInteger.sign = Sign.Negative;
				}
				else
				{
					bigInteger.sign = Sign.Positive;
				}
			}
			return bigInteger;
		}

		public static bool Greater(BigInteger a, BigInteger b)
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

		public static bool GreaterOrEqual(BigInteger a, BigInteger b)
		{
			return Greater(a, b) || object.Equals(a, b);
		}

		public static bool Smaller(BigInteger a, BigInteger b)
		{
			return !GreaterOrEqual(a, b);
		}

		public static bool SmallerOrEqual(BigInteger a, BigInteger b)
		{
			return !Greater(a, b);
		}

		public static BigInteger Abs(BigInteger n)
		{
			BigInteger bigInteger = new BigInteger(n);
			bigInteger.sign = Sign.Positive;
			return bigInteger;
		}

		public static BigInteger Addition(BigInteger a, BigInteger b)
		{
			BigInteger bigInteger = null;
			if (a.sign == Sign.Positive && b.sign == Sign.Positive)
			{
				bigInteger = ((!(a >= b)) ? Add(b, a) : Add(a, b));
				bigInteger.sign = Sign.Positive;
			}
			if (a.sign == Sign.Negative && b.sign == Sign.Negative)
			{
				bigInteger = ((!(a <= b)) ? Add(-b, -a) : Add(-a, -b));
				bigInteger.sign = Sign.Negative;
			}
			if (a.sign == Sign.Positive && b.sign == Sign.Negative)
			{
				if (a >= -b)
				{
					bigInteger = Subtract(a, -b);
					bigInteger.sign = Sign.Positive;
				}
				else
				{
					bigInteger = Subtract(-b, a);
					bigInteger.sign = Sign.Negative;
				}
			}
			if (a.sign == Sign.Negative && b.sign == Sign.Positive)
			{
				if (-a <= b)
				{
					bigInteger = Subtract(b, -a);
					bigInteger.sign = Sign.Positive;
				}
				else
				{
					bigInteger = Subtract(-a, b);
					bigInteger.sign = Sign.Negative;
				}
			}
			return bigInteger;
		}

		public static BigInteger Subtraction(BigInteger a, BigInteger b)
		{
			BigInteger bigInteger = null;
			if (a.sign == Sign.Positive && b.sign == Sign.Positive)
			{
				if (a >= b)
				{
					bigInteger = Subtract(a, b);
					bigInteger.sign = Sign.Positive;
				}
				else
				{
					bigInteger = Subtract(b, a);
					bigInteger.sign = Sign.Negative;
				}
			}
			if (a.sign == Sign.Negative && b.sign == Sign.Negative)
			{
				if (a <= b)
				{
					bigInteger = Subtract(-a, -b);
					bigInteger.sign = Sign.Negative;
				}
				else
				{
					bigInteger = Subtract(-b, -a);
					bigInteger.sign = Sign.Positive;
				}
			}
			if (a.sign == Sign.Positive && b.sign == Sign.Negative)
			{
				bigInteger = ((!(a >= -b)) ? Add(-b, a) : Add(a, -b));
				bigInteger.sign = Sign.Positive;
			}
			if (a.sign == Sign.Negative && b.sign == Sign.Positive)
			{
				bigInteger = ((!(-a >= b)) ? Add(b, -a) : Add(-a, b));
				bigInteger.sign = Sign.Negative;
			}
			return bigInteger;
		}

		public static BigInteger Multiplication(BigInteger a, BigInteger b)
		{
			if (a == Zero || b == Zero)
			{
				return Zero;
			}
			BigInteger bigInteger = Multiply(Abs(a), Abs(b));
			if (a.sign == b.sign)
			{
				bigInteger.sign = Sign.Positive;
			}
			else
			{
				bigInteger.sign = Sign.Negative;
			}
			return bigInteger;
		}

		public static BigInteger Division(BigInteger a, BigInteger b)
		{
			if (b == Zero)
			{
				throw new BigIntegerException("Cannot divide by zero.", new DivideByZeroException());
			}
			if (a == Zero)
			{
				return Zero;
			}
			if (Abs(a) < Abs(b))
			{
				return Zero;
			}
			BigInteger bigInteger = ((b.size != 1) ? DivideByBigNumber(Abs(a), Abs(b)) : DivideByOneDigitNumber(Abs(a), b.digits[0]));
			if (a.sign == b.sign)
			{
				bigInteger.sign = Sign.Positive;
			}
			else
			{
				bigInteger.sign = Sign.Negative;
			}
			return bigInteger;
		}

		public static BigInteger Modulo(BigInteger a, BigInteger b)
		{
			if (b == Zero)
			{
				throw new BigIntegerException("Cannot divide by zero.", new DivideByZeroException());
			}
			if (Abs(a) < Abs(b))
			{
				return new BigInteger(a);
			}
			return a - a / b * b;
		}

		public static BigInteger Power(BigInteger a, int exponent)
		{
			if (exponent < 0)
			{
				throw new BigIntegerException("Cannot raise an BigInteger to a negative power.", null);
			}
			if (a == Zero)
			{
				return new BigInteger();
			}
			BigInteger result = new BigInteger(1L);
			if (exponent == 0)
			{
				return result;
			}
			BigInteger bigInteger = new BigInteger(a);
			while (exponent > 0)
			{
				if (exponent % 2 == 1)
				{
					result *= bigInteger;
				}
				exponent /= 2;
				if (exponent > 0)
				{
					bigInteger *= bigInteger;
				}
			}
			return result;
		}

		public static BigInteger IntegerSqrt(BigInteger n)
		{
			if (n.sign == Sign.Negative)
			{
				throw new BigIntegerException("Cannot compute the integer square root of a negative number.", null);
			}
			BigInteger bigInteger = new BigInteger(0L);
			BigInteger bigInteger2 = new BigInteger(n);
			while (Abs(bigInteger2 - bigInteger) >= One)
			{
				bigInteger = bigInteger2;
				bigInteger2 = (bigInteger + n / bigInteger) / Two;
			}
			return bigInteger2;
		}

		public static BigInteger Gcd(BigInteger a, BigInteger b)
		{
			if (a.sign == Sign.Negative || b.sign == Sign.Negative)
			{
				throw new BigIntegerException("Cannot compute the Gcd of negative BigIntegers.", null);
			}
			while (b > Zero)
			{
				BigInteger bigInteger = a % b;
				a = b;
				b = bigInteger;
			}
			return a;
		}

		public static BigInteger ExtendedEuclidGcd(BigInteger a, BigInteger b, out BigInteger u, out BigInteger v)
		{
			if (a.sign == Sign.Negative || b.sign == Sign.Negative)
			{
				throw new BigIntegerException("Cannot compute the Gcd of negative BigIntegers.", null);
			}
			BigInteger bigInteger = new BigInteger();
			BigInteger bigInteger2 = new BigInteger(1L);
			BigInteger bigInteger3 = new BigInteger(1L);
			BigInteger bigInteger4 = new BigInteger();
			u = new BigInteger();
			v = new BigInteger();
			while (b > Zero)
			{
				BigInteger bigInteger5 = a / b;
				BigInteger n = a - bigInteger5 * b;
				u = bigInteger2 - bigInteger5 * bigInteger;
				v = bigInteger4 - bigInteger5 * bigInteger3;
				a = new BigInteger(b);
				b = new BigInteger(n);
				bigInteger2 = new BigInteger(bigInteger);
				bigInteger = new BigInteger(u);
				bigInteger4 = new BigInteger(bigInteger3);
				bigInteger3 = new BigInteger(v);
				u = new BigInteger(bigInteger2);
				v = new BigInteger(bigInteger4);
			}
			return a;
		}

		public static BigInteger ModularInverse(BigInteger a, BigInteger n)
		{
			if (n < Two)
			{
				throw new BigIntegerException("Cannot perform a modulo operation against a BigInteger less than 2.", null);
			}
			if (Abs(a) >= n)
			{
				a %= n;
			}
			if (a.sign == Sign.Negative)
			{
				a += n;
			}
			if (a == Zero)
			{
				throw new BigIntegerException("Cannot obtain the modular inverse of 0.", null);
			}
			if (Gcd(a, n) != One)
			{
				throw new BigIntegerException("Cannot obtain the modular inverse of a number that is not coprime with the modulus.", null);
			}
			BigInteger u;
			BigInteger v;
			ExtendedEuclidGcd(n, a, out u, out v);
			if (v.sign == Sign.Negative)
			{
				return v + n;
			}
			return v;
		}

		public static BigInteger ModularExponentiation(BigInteger a, BigInteger exponent, BigInteger n)
		{
			if (exponent < 0L)
			{
				throw new BigIntegerException("Cannot raise a BigInteger to a negative power.", null);
			}
			if (n < Two)
			{
				throw new BigIntegerException("Cannot perform a modulo operation against a BigInteger less than 2.", null);
			}
			if (Abs(a) >= n)
			{
				a %= n;
			}
			if (a.sign == Sign.Negative)
			{
				a += n;
			}
			if (a == Zero)
			{
				return new BigInteger();
			}
			BigInteger bigInteger = new BigInteger(1L);
			BigInteger bigInteger2 = new BigInteger(a);
			while (exponent > Zero)
			{
				if (exponent % Two == One)
				{
					bigInteger = bigInteger * bigInteger2 % n;
				}
				exponent /= Two;
				bigInteger2 = bigInteger2 * bigInteger2 % n;
			}
			return bigInteger;
		}

		private static BigInteger Add(BigInteger a, BigInteger b)
		{
			BigInteger bigInteger = new BigInteger(a);
			long num = 0L;
			for (int i = 0; i < b.size; i++)
			{
				long num2 = bigInteger.digits[i] + b.digits[i] + num;
				bigInteger.digits[i] = num2 % 65536;
				num = num2 / 65536;
			}
			for (int i = b.size; i < a.size; i++)
			{
				if (num <= 0)
				{
					break;
				}
				long num2 = bigInteger.digits[i] + num;
				bigInteger.digits[i] = num2 % 65536;
				num = num2 / 65536;
			}
			if (num > 0)
			{
				bigInteger.digits[bigInteger.size] = num % 65536;
				bigInteger.size++;
				num /= 65536;
			}
			return bigInteger;
		}

		private static BigInteger Subtract(BigInteger a, BigInteger b)
		{
			BigInteger bigInteger = new BigInteger(a);
			long num = 0L;
			bool flag = true;
			for (int i = 0; i < b.size; i++)
			{
				long num2 = bigInteger.digits[i] - b.digits[i] - num;
				if (num2 < 0)
				{
					num = 1L;
					num2 += 65536;
				}
				else
				{
					num = 0L;
				}
				bigInteger.digits[i] = num2;
			}
			for (int i = b.size; i < a.size; i++)
			{
				if (num <= 0)
				{
					break;
				}
				long num2 = bigInteger.digits[i] - num;
				if (num2 < 0)
				{
					num = 1L;
					num2 += 65536;
				}
				else
				{
					num = 0L;
				}
				bigInteger.digits[i] = num2;
			}
			while (bigInteger.size - 1 > 0 && flag)
			{
				if (bigInteger.digits[bigInteger.size - 1] == 0L)
				{
					bigInteger.size--;
				}
				else
				{
					flag = false;
				}
			}
			return bigInteger;
		}

		private static BigInteger Multiply(BigInteger a, BigInteger b)
		{
			long num = 0L;
			BigInteger bigInteger = new BigInteger();
			bigInteger.size = a.size + b.size - 1;
			for (int i = 0; i < bigInteger.size + 1; i++)
			{
				bigInteger.digits[i] = 0L;
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
						DigitContainer digitContainer = (digitContainer2 = bigInteger.digits);
						int index2;
						int index = (index2 = i + j);
						long num2 = digitContainer2[index2];
						digitContainer[index] = num2 + a.digits[i] * b.digits[j];
					}
				}
			}
			for (int i = 0; i < bigInteger.size; i++)
			{
				long num3 = bigInteger.digits[i] + num;
				bigInteger.digits[i] = num3 % 65536;
				num = num3 / 65536;
			}
			if (num > 0)
			{
				bigInteger.digits[bigInteger.size] = num % 65536;
				bigInteger.size++;
				num /= 65536;
			}
			return bigInteger;
		}

		private static BigInteger DivideByOneDigitNumber(BigInteger a, long b)
		{
			BigInteger bigInteger = new BigInteger();
			int num = a.size - 1;
			bigInteger.size = a.size;
			long num2 = a.digits[num];
			while (num >= 0)
			{
				bigInteger.digits[num] = num2 / b;
				num2 %= b;
				num--;
				if (num >= 0)
				{
					num2 = num2 * 65536 + a.digits[num];
				}
			}
			if (bigInteger.digits[bigInteger.size - 1] == 0L && bigInteger.size != 1)
			{
				bigInteger.size--;
			}
			return bigInteger;
		}

		private static BigInteger DivideByBigNumber(BigInteger a, BigInteger b)
		{
			int num = a.size;
			int num2 = b.size;
			long num3 = 65536 / (b.digits[num2 - 1] + 1);
			BigInteger bigInteger = new BigInteger();
			BigInteger r = a * num3;
			BigInteger bigInteger2 = b * num3;
			for (int num4 = num - num2; num4 >= 0; num4--)
			{
				long num5 = Trial(r, bigInteger2, num4, num2);
				BigInteger dq = bigInteger2 * num5;
				if (DivideByBigNumberSmaller(r, dq, num4, num2))
				{
					num5--;
					dq = bigInteger2 * num5;
				}
				bigInteger.digits[num4] = num5;
				Difference(r, dq, num4, num2);
			}
			bigInteger.size = num - num2 + 1;
			if (bigInteger.size != 1 && bigInteger.digits[bigInteger.size - 1] == 0L)
			{
				bigInteger.size--;
			}
			return bigInteger;
		}

		private static bool DivideByBigNumberSmaller(BigInteger r, BigInteger dq, int k, int m)
		{
			int num = m;
			int num2 = 0;
			while (num != num2)
			{
				if (r.digits[num + k] != dq.digits[num])
				{
					num2 = num;
				}
				else
				{
					num--;
				}
			}
			if (r.digits[num + k] < dq.digits[num])
			{
				return true;
			}
			return false;
		}

		private static void Difference(BigInteger r, BigInteger dq, int k, int m)
		{
			long num = 0L;
			for (int i = 0; i <= m; i++)
			{
				long num2 = r.digits[i + k] - dq.digits[i] - num + 65536;
				r.digits[i + k] = num2 % 65536;
				num = 1 - num2 / 65536;
			}
		}

		private static long Trial(BigInteger r, BigInteger d, int k, int m)
		{
			int num = k + m;
			long num2 = (r.digits[num] * 65536 + r.digits[num - 1]) * 65536 + r.digits[num - 2];
			long num3 = d.digits[m - 1] * 65536 + d.digits[m - 2];
			long num4 = num2 / num3;
			if (num4 < 65535)
			{
				return (int)num4;
			}
			return 65535L;
		}

		public static implicit operator BigInteger(long n)
		{
			return new BigInteger(n);
		}

		public static bool operator ==(BigInteger a, BigInteger b)
		{
			return object.Equals(a, b);
		}

		public static bool operator !=(BigInteger a, BigInteger b)
		{
			return !object.Equals(a, b);
		}

		public static bool operator >(BigInteger a, BigInteger b)
		{
			return Greater(a, b);
		}

		public static bool operator <(BigInteger a, BigInteger b)
		{
			return Smaller(a, b);
		}

		public static bool operator >=(BigInteger a, BigInteger b)
		{
			return GreaterOrEqual(a, b);
		}

		public static bool operator <=(BigInteger a, BigInteger b)
		{
			return SmallerOrEqual(a, b);
		}

		public static BigInteger operator -(BigInteger n)
		{
			return Opposite(n);
		}

		public static BigInteger operator +(BigInteger a, BigInteger b)
		{
			return Addition(a, b);
		}

		public static BigInteger operator -(BigInteger a, BigInteger b)
		{
			return Subtraction(a, b);
		}

		public static BigInteger operator *(BigInteger a, BigInteger b)
		{
			return Multiplication(a, b);
		}

		public static BigInteger operator /(BigInteger a, BigInteger b)
		{
			return Division(a, b);
		}

		public static BigInteger operator %(BigInteger a, BigInteger b)
		{
			return Modulo(a, b);
		}

		public static BigInteger operator ++(BigInteger n)
		{
			return n + One;
		}

		public static BigInteger operator --(BigInteger n)
		{
			return n - One;
		}
	}
}
