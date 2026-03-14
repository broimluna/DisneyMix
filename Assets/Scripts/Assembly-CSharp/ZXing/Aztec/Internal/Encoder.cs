using System;
using ZXing.Common;
using ZXing.Common.ReedSolomon;

namespace ZXing.Aztec.Internal
{
	public static class Encoder
	{
		public const int DEFAULT_EC_PERCENT = 33;

		public const int DEFAULT_AZTEC_LAYERS = 0;

		private const int MAX_NB_BITS = 32;

		private const int MAX_NB_BITS_COMPACT = 4;

		private static readonly int[] WORD_SIZE = new int[33]
		{
			4, 6, 6, 8, 8, 8, 8, 8, 8, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 12, 12, 12, 12, 12, 12, 12,
			12, 12, 12
		};

		public static AztecCode encode(byte[] data)
		{
			return encode(data, 33, 0);
		}

		public static AztecCode encode(byte[] data, int minECCPercent, int userSpecifiedLayers)
		{
			BitArray bitArray = new HighLevelEncoder(data).encode();
			int num = bitArray.Size * minECCPercent / 100 + 11;
			int num2 = bitArray.Size + num;
			bool flag;
			int num3;
			int num4;
			int num5;
			BitArray bitArray2;
			if (userSpecifiedLayers != 0)
			{
				flag = userSpecifiedLayers < 0;
				num3 = Math.Abs(userSpecifiedLayers);
				if (num3 > ((!flag) ? 32 : 4))
				{
					throw new ArgumentException(string.Format("Illegal value {0} for layers", userSpecifiedLayers));
				}
				num4 = TotalBitsInLayer(num3, flag);
				num5 = WORD_SIZE[num3];
				int num6 = num4 - num4 % num5;
				bitArray2 = stuffBits(bitArray, num5);
				if (bitArray2.Size + num > num6)
				{
					throw new ArgumentException("Data to large for user specified layer");
				}
				if (flag && bitArray2.Size > num5 * 64)
				{
					throw new ArgumentException("Data to large for user specified layer");
				}
			}
			else
			{
				num5 = 0;
				bitArray2 = null;
				int num7 = 0;
				while (true)
				{
					if (num7 > 32)
					{
						throw new ArgumentException("Data too large for an Aztec code");
					}
					flag = num7 <= 3;
					num3 = ((!flag) ? num7 : (num7 + 1));
					num4 = TotalBitsInLayer(num3, flag);
					if (num2 <= num4)
					{
						if (num5 != WORD_SIZE[num3])
						{
							num5 = WORD_SIZE[num3];
							bitArray2 = stuffBits(bitArray, num5);
						}
						if (bitArray2 != null)
						{
							int num8 = num4 - num4 % num5;
							if ((!flag || bitArray2.Size <= num5 * 64) && bitArray2.Size + num <= num8)
							{
								break;
							}
						}
					}
					num7++;
				}
			}
			BitArray bitArray3 = generateCheckWords(bitArray2, num4, num5);
			int num9 = bitArray2.Size / num5;
			BitArray modeMessage = generateModeMessage(flag, num3, num9);
			int num10 = ((!flag) ? (14 + num3 * 4) : (11 + num3 * 4));
			int[] array = new int[num10];
			int num11;
			if (flag)
			{
				num11 = num10;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = i;
				}
			}
			else
			{
				num11 = num10 + 1 + 2 * ((num10 / 2 - 1) / 15);
				int num12 = num10 / 2;
				int num13 = num11 / 2;
				for (int j = 0; j < num12; j++)
				{
					int num14 = j + j / 15;
					array[num12 - j - 1] = num13 - num14 - 1;
					array[num12 + j] = num13 + num14 + 1;
				}
			}
			BitMatrix bitMatrix = new BitMatrix(num11);
			int k = 0;
			int num15 = 0;
			for (; k < num3; k++)
			{
				int num16 = ((!flag) ? ((num3 - k) * 4 + 12) : ((num3 - k) * 4 + 9));
				for (int l = 0; l < num16; l++)
				{
					int num17 = l * 2;
					for (int m = 0; m < 2; m++)
					{
						if (bitArray3[num15 + num17 + m])
						{
							bitMatrix[array[k * 2 + m], array[k * 2 + l]] = true;
						}
						if (bitArray3[num15 + num16 * 2 + num17 + m])
						{
							bitMatrix[array[k * 2 + l], array[num10 - 1 - k * 2 - m]] = true;
						}
						if (bitArray3[num15 + num16 * 4 + num17 + m])
						{
							bitMatrix[array[num10 - 1 - k * 2 - m], array[num10 - 1 - k * 2 - l]] = true;
						}
						if (bitArray3[num15 + num16 * 6 + num17 + m])
						{
							bitMatrix[array[num10 - 1 - k * 2 - l], array[k * 2 + m]] = true;
						}
					}
				}
				num15 += num16 * 8;
			}
			drawModeMessage(bitMatrix, flag, num11, modeMessage);
			if (flag)
			{
				drawBullsEye(bitMatrix, num11 / 2, 5);
			}
			else
			{
				drawBullsEye(bitMatrix, num11 / 2, 7);
				int num18 = 0;
				int num19 = 0;
				while (num18 < num10 / 2 - 1)
				{
					for (int n = (num11 / 2) & 1; n < num11; n += 2)
					{
						bitMatrix[num11 / 2 - num19, n] = true;
						bitMatrix[num11 / 2 + num19, n] = true;
						bitMatrix[n, num11 / 2 - num19] = true;
						bitMatrix[n, num11 / 2 + num19] = true;
					}
					num18 += 15;
					num19 += 16;
				}
			}
			AztecCode aztecCode = new AztecCode();
			aztecCode.isCompact = flag;
			aztecCode.Size = num11;
			aztecCode.Layers = num3;
			aztecCode.CodeWords = num9;
			aztecCode.Matrix = bitMatrix;
			return aztecCode;
		}

		private static void drawBullsEye(BitMatrix matrix, int center, int size)
		{
			for (int i = 0; i < size; i += 2)
			{
				for (int j = center - i; j <= center + i; j++)
				{
					matrix[j, center - i] = true;
					matrix[j, center + i] = true;
					matrix[center - i, j] = true;
					matrix[center + i, j] = true;
				}
			}
			matrix[center - size, center - size] = true;
			matrix[center - size + 1, center - size] = true;
			matrix[center - size, center - size + 1] = true;
			matrix[center + size, center - size] = true;
			matrix[center + size, center - size + 1] = true;
			matrix[center + size, center + size - 1] = true;
		}

		internal static BitArray generateModeMessage(bool compact, int layers, int messageSizeInWords)
		{
			BitArray bitArray = new BitArray();
			if (compact)
			{
				bitArray.appendBits(layers - 1, 2);
				bitArray.appendBits(messageSizeInWords - 1, 6);
				return generateCheckWords(bitArray, 28, 4);
			}
			bitArray.appendBits(layers - 1, 5);
			bitArray.appendBits(messageSizeInWords - 1, 11);
			return generateCheckWords(bitArray, 40, 4);
		}

		private static void drawModeMessage(BitMatrix matrix, bool compact, int matrixSize, BitArray modeMessage)
		{
			int num = matrixSize / 2;
			if (compact)
			{
				for (int i = 0; i < 7; i++)
				{
					int num2 = num - 3 + i;
					if (modeMessage[i])
					{
						matrix[num2, num - 5] = true;
					}
					if (modeMessage[i + 7])
					{
						matrix[num + 5, num2] = true;
					}
					if (modeMessage[20 - i])
					{
						matrix[num2, num + 5] = true;
					}
					if (modeMessage[27 - i])
					{
						matrix[num - 5, num2] = true;
					}
				}
				return;
			}
			for (int j = 0; j < 10; j++)
			{
				int num3 = num - 5 + j + j / 5;
				if (modeMessage[j])
				{
					matrix[num3, num - 7] = true;
				}
				if (modeMessage[j + 10])
				{
					matrix[num + 7, num3] = true;
				}
				if (modeMessage[29 - j])
				{
					matrix[num3, num + 7] = true;
				}
				if (modeMessage[39 - j])
				{
					matrix[num - 7, num3] = true;
				}
			}
		}

		private static BitArray generateCheckWords(BitArray bitArray, int totalBits, int wordSize)
		{
			if (bitArray.Size % wordSize != 0)
			{
				throw new InvalidOperationException("size of bit array is not a multiple of the word size");
			}
			int num = bitArray.Size / wordSize;
			ReedSolomonEncoder reedSolomonEncoder = new ReedSolomonEncoder(getGF(wordSize));
			int num2 = totalBits / wordSize;
			int[] array = bitsToWords(bitArray, wordSize, num2);
			reedSolomonEncoder.encode(array, num2 - num);
			int numBits = totalBits % wordSize;
			BitArray bitArray2 = new BitArray();
			bitArray2.appendBits(0, numBits);
			int[] array2 = array;
			foreach (int value in array2)
			{
				bitArray2.appendBits(value, wordSize);
			}
			return bitArray2;
		}

		private static int[] bitsToWords(BitArray stuffedBits, int wordSize, int totalWords)
		{
			int[] array = new int[totalWords];
			int i = 0;
			for (int num = stuffedBits.Size / wordSize; i < num; i++)
			{
				int num2 = 0;
				for (int j = 0; j < wordSize; j++)
				{
					num2 |= (stuffedBits[i * wordSize + j] ? (1 << wordSize - j - 1) : 0);
				}
				array[i] = num2;
			}
			return array;
		}

		private static GenericGF getGF(int wordSize)
		{
			switch (wordSize)
			{
			case 4:
				return GenericGF.AZTEC_PARAM;
			case 6:
				return GenericGF.AZTEC_DATA_6;
			case 8:
				return GenericGF.AZTEC_DATA_8;
			case 10:
				return GenericGF.AZTEC_DATA_10;
			case 12:
				return GenericGF.AZTEC_DATA_12;
			default:
				return null;
			}
		}

		internal static BitArray stuffBits(BitArray bits, int wordSize)
		{
			BitArray bitArray = new BitArray();
			int size = bits.Size;
			int num = (1 << wordSize) - 2;
			for (int i = 0; i < size; i += wordSize)
			{
				int num2 = 0;
				for (int j = 0; j < wordSize; j++)
				{
					if (i + j >= size || bits[i + j])
					{
						num2 |= 1 << ((wordSize - 1 - j) & 0x1F);
					}
				}
				if ((num2 & num) == num)
				{
					bitArray.appendBits(num2 & num, wordSize);
					i--;
				}
				else if ((num2 & num) == 0)
				{
					bitArray.appendBits(num2 | 1, wordSize);
					i--;
				}
				else
				{
					bitArray.appendBits(num2, wordSize);
				}
			}
			return bitArray;
		}

		private static int TotalBitsInLayer(int layers, bool compact)
		{
			return (((!compact) ? 112 : 88) + 16 * layers) * layers;
		}
	}
}
