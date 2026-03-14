using System;

namespace ZXing.PDF417.Internal.EC
{
	internal sealed class ModulusGF
	{
		public static ModulusGF PDF417_GF = new ModulusGF(PDF417Common.NUMBER_OF_CODEWORDS, 3);

		private readonly int[] expTable;

		private readonly int[] logTable;

		private readonly int modulus;

		public ModulusPoly Zero { get; private set; }

		public ModulusPoly One { get; private set; }

		internal int Size
		{
			get
			{
				return modulus;
			}
		}

		public ModulusGF(int modulus, int generator)
		{
			this.modulus = modulus;
			expTable = new int[modulus];
			logTable = new int[modulus];
			int num = 1;
			for (int i = 0; i < modulus; i++)
			{
				expTable[i] = num;
				num = num * generator % modulus;
			}
			for (int j = 0; j < modulus - 1; j++)
			{
				logTable[expTable[j]] = j;
			}
			Zero = new ModulusPoly(this, new int[1]);
			One = new ModulusPoly(this, new int[1] { 1 });
		}

		internal ModulusPoly buildMonomial(int degree, int coefficient)
		{
			if (degree < 0)
			{
				throw new ArgumentException();
			}
			if (coefficient == 0)
			{
				return Zero;
			}
			int[] array = new int[degree + 1];
			array[0] = coefficient;
			return new ModulusPoly(this, array);
		}

		internal int add(int a, int b)
		{
			return (a + b) % modulus;
		}

		internal int subtract(int a, int b)
		{
			return (modulus + a - b) % modulus;
		}

		internal int exp(int a)
		{
			return expTable[a];
		}

		internal int log(int a)
		{
			if (a == 0)
			{
				throw new ArgumentException();
			}
			return logTable[a];
		}

		internal int inverse(int a)
		{
			if (a == 0)
			{
				throw new ArithmeticException();
			}
			return expTable[modulus - logTable[a] - 1];
		}

		internal int multiply(int a, int b)
		{
			if (a == 0 || b == 0)
			{
				return 0;
			}
			return expTable[(logTable[a] + logTable[b]) % (modulus - 1)];
		}
	}
}
