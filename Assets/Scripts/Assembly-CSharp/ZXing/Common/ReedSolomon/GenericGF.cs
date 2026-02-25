using System;

namespace ZXing.Common.ReedSolomon
{
	public sealed class GenericGF
	{
		private const int INITIALIZATION_THRESHOLD = 0;

		public static GenericGF AZTEC_DATA_12 = new GenericGF(4201, 4096, 1);

		public static GenericGF AZTEC_DATA_10 = new GenericGF(1033, 1024, 1);

		public static GenericGF AZTEC_DATA_6 = new GenericGF(67, 64, 1);

		public static GenericGF AZTEC_PARAM = new GenericGF(19, 16, 1);

		public static GenericGF QR_CODE_FIELD_256 = new GenericGF(285, 256, 0);

		public static GenericGF DATA_MATRIX_FIELD_256 = new GenericGF(301, 256, 1);

		public static GenericGF AZTEC_DATA_8 = DATA_MATRIX_FIELD_256;

		public static GenericGF MAXICODE_FIELD_64 = AZTEC_DATA_6;

		private int[] expTable;

		private int[] logTable;

		private GenericGFPoly zero;

		private GenericGFPoly one;

		private readonly int size;

		private readonly int primitive;

		private readonly int generatorBase;

		private bool initialized;

		internal GenericGFPoly Zero
		{
			get
			{
				checkInit();
				return zero;
			}
		}

		internal GenericGFPoly One
		{
			get
			{
				checkInit();
				return one;
			}
		}

		public int Size
		{
			get
			{
				return size;
			}
		}

		public int GeneratorBase
		{
			get
			{
				return generatorBase;
			}
		}

		public GenericGF(int primitive, int size, int genBase)
		{
			this.primitive = primitive;
			this.size = size;
			generatorBase = genBase;
			if (size <= 0)
			{
				initialize();
			}
		}

		private void initialize()
		{
			expTable = new int[size];
			logTable = new int[size];
			int num = 1;
			for (int i = 0; i < size; i++)
			{
				expTable[i] = num;
				num <<= 1;
				if (num >= size)
				{
					num ^= primitive;
					num &= size - 1;
				}
			}
			for (int j = 0; j < size - 1; j++)
			{
				logTable[expTable[j]] = j;
			}
			zero = new GenericGFPoly(this, new int[1]);
			one = new GenericGFPoly(this, new int[1] { 1 });
			initialized = true;
		}

		private void checkInit()
		{
			if (!initialized)
			{
				initialize();
			}
		}

		internal GenericGFPoly buildMonomial(int degree, int coefficient)
		{
			checkInit();
			if (degree < 0)
			{
				throw new ArgumentException();
			}
			if (coefficient == 0)
			{
				return zero;
			}
			int[] array = new int[degree + 1];
			array[0] = coefficient;
			return new GenericGFPoly(this, array);
		}

		internal static int addOrSubtract(int a, int b)
		{
			return a ^ b;
		}

		internal int exp(int a)
		{
			checkInit();
			return expTable[a];
		}

		internal int log(int a)
		{
			checkInit();
			if (a == 0)
			{
				throw new ArgumentException();
			}
			return logTable[a];
		}

		internal int inverse(int a)
		{
			checkInit();
			if (a == 0)
			{
				throw new ArithmeticException();
			}
			return expTable[size - logTable[a] - 1];
		}

		internal int multiply(int a, int b)
		{
			checkInit();
			if (a == 0 || b == 0)
			{
				return 0;
			}
			return expTable[(logTable[a] + logTable[b]) % (size - 1)];
		}

		public override string ToString()
		{
			return "GF(0x" + primitive.ToString("X") + ',' + size + ')';
		}
	}
}
