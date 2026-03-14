using System;
using ZXing.Common;

namespace ZXing.QrCode.Internal
{
	internal abstract class DataMask
	{
		private sealed class DataMask000 : DataMask
		{
			internal override bool isMasked(int i, int j)
			{
				return ((i + j) & 1) == 0;
			}
		}

		private sealed class DataMask001 : DataMask
		{
			internal override bool isMasked(int i, int j)
			{
				return (i & 1) == 0;
			}
		}

		private sealed class DataMask010 : DataMask
		{
			internal override bool isMasked(int i, int j)
			{
				return j % 3 == 0;
			}
		}

		private sealed class DataMask011 : DataMask
		{
			internal override bool isMasked(int i, int j)
			{
				return (i + j) % 3 == 0;
			}
		}

		private sealed class DataMask100 : DataMask
		{
			internal override bool isMasked(int i, int j)
			{
				return (((int)((uint)i >> 1) + j / 3) & 1) == 0;
			}
		}

		private sealed class DataMask101 : DataMask
		{
			internal override bool isMasked(int i, int j)
			{
				int num = i * j;
				return (num & 1) + num % 3 == 0;
			}
		}

		private sealed class DataMask110 : DataMask
		{
			internal override bool isMasked(int i, int j)
			{
				int num = i * j;
				return (((num & 1) + num % 3) & 1) == 0;
			}
		}

		private sealed class DataMask111 : DataMask
		{
			internal override bool isMasked(int i, int j)
			{
				return ((((i + j) & 1) + i * j % 3) & 1) == 0;
			}
		}

		private static readonly DataMask[] DATA_MASKS = new DataMask[8]
		{
			new DataMask000(),
			new DataMask001(),
			new DataMask010(),
			new DataMask011(),
			new DataMask100(),
			new DataMask101(),
			new DataMask110(),
			new DataMask111()
		};

		private DataMask()
		{
		}

		internal void unmaskBitMatrix(BitMatrix bits, int dimension)
		{
			for (int i = 0; i < dimension; i++)
			{
				for (int j = 0; j < dimension; j++)
				{
					if (isMasked(i, j))
					{
						bits.flip(j, i);
					}
				}
			}
		}

		internal abstract bool isMasked(int i, int j);

		internal static DataMask forReference(int reference)
		{
			if (reference < 0 || reference > 7)
			{
				throw new ArgumentException();
			}
			return DATA_MASKS[reference];
		}
	}
}
