using ZXing.Common;

namespace ZXing.QrCode.Internal
{
	public static class MatrixUtil
	{
		private const int VERSION_INFO_POLY = 7973;

		private const int TYPE_INFO_POLY = 1335;

		private const int TYPE_INFO_MASK_PATTERN = 21522;

		private static readonly int[][] POSITION_DETECTION_PATTERN = new int[7][]
		{
			new int[7] { 1, 1, 1, 1, 1, 1, 1 },
			new int[7] { 1, 0, 0, 0, 0, 0, 1 },
			new int[7] { 1, 0, 1, 1, 1, 0, 1 },
			new int[7] { 1, 0, 1, 1, 1, 0, 1 },
			new int[7] { 1, 0, 1, 1, 1, 0, 1 },
			new int[7] { 1, 0, 0, 0, 0, 0, 1 },
			new int[7] { 1, 1, 1, 1, 1, 1, 1 }
		};

		private static readonly int[][] POSITION_ADJUSTMENT_PATTERN = new int[5][]
		{
			new int[5] { 1, 1, 1, 1, 1 },
			new int[5] { 1, 0, 0, 0, 1 },
			new int[5] { 1, 0, 1, 0, 1 },
			new int[5] { 1, 0, 0, 0, 1 },
			new int[5] { 1, 1, 1, 1, 1 }
		};

		private static readonly int[][] POSITION_ADJUSTMENT_PATTERN_COORDINATE_TABLE = new int[40][]
		{
			new int[7] { -1, -1, -1, -1, -1, -1, -1 },
			new int[7] { 6, 18, -1, -1, -1, -1, -1 },
			new int[7] { 6, 22, -1, -1, -1, -1, -1 },
			new int[7] { 6, 26, -1, -1, -1, -1, -1 },
			new int[7] { 6, 30, -1, -1, -1, -1, -1 },
			new int[7] { 6, 34, -1, -1, -1, -1, -1 },
			new int[7] { 6, 22, 38, -1, -1, -1, -1 },
			new int[7] { 6, 24, 42, -1, -1, -1, -1 },
			new int[7] { 6, 26, 46, -1, -1, -1, -1 },
			new int[7] { 6, 28, 50, -1, -1, -1, -1 },
			new int[7] { 6, 30, 54, -1, -1, -1, -1 },
			new int[7] { 6, 32, 58, -1, -1, -1, -1 },
			new int[7] { 6, 34, 62, -1, -1, -1, -1 },
			new int[7] { 6, 26, 46, 66, -1, -1, -1 },
			new int[7] { 6, 26, 48, 70, -1, -1, -1 },
			new int[7] { 6, 26, 50, 74, -1, -1, -1 },
			new int[7] { 6, 30, 54, 78, -1, -1, -1 },
			new int[7] { 6, 30, 56, 82, -1, -1, -1 },
			new int[7] { 6, 30, 58, 86, -1, -1, -1 },
			new int[7] { 6, 34, 62, 90, -1, -1, -1 },
			new int[7] { 6, 28, 50, 72, 94, -1, -1 },
			new int[7] { 6, 26, 50, 74, 98, -1, -1 },
			new int[7] { 6, 30, 54, 78, 102, -1, -1 },
			new int[7] { 6, 28, 54, 80, 106, -1, -1 },
			new int[7] { 6, 32, 58, 84, 110, -1, -1 },
			new int[7] { 6, 30, 58, 86, 114, -1, -1 },
			new int[7] { 6, 34, 62, 90, 118, -1, -1 },
			new int[7] { 6, 26, 50, 74, 98, 122, -1 },
			new int[7] { 6, 30, 54, 78, 102, 126, -1 },
			new int[7] { 6, 26, 52, 78, 104, 130, -1 },
			new int[7] { 6, 30, 56, 82, 108, 134, -1 },
			new int[7] { 6, 34, 60, 86, 112, 138, -1 },
			new int[7] { 6, 30, 58, 86, 114, 142, -1 },
			new int[7] { 6, 34, 62, 90, 118, 146, -1 },
			new int[7] { 6, 30, 54, 78, 102, 126, 150 },
			new int[7] { 6, 24, 50, 76, 102, 128, 154 },
			new int[7] { 6, 28, 54, 80, 106, 132, 158 },
			new int[7] { 6, 32, 58, 84, 110, 136, 162 },
			new int[7] { 6, 26, 54, 82, 110, 138, 166 },
			new int[7] { 6, 30, 58, 86, 114, 142, 170 }
		};

		private static readonly int[][] TYPE_INFO_COORDINATES = new int[15][]
		{
			new int[2] { 8, 0 },
			new int[2] { 8, 1 },
			new int[2] { 8, 2 },
			new int[2] { 8, 3 },
			new int[2] { 8, 4 },
			new int[2] { 8, 5 },
			new int[2] { 8, 7 },
			new int[2] { 8, 8 },
			new int[2] { 7, 8 },
			new int[2] { 5, 8 },
			new int[2] { 4, 8 },
			new int[2] { 3, 8 },
			new int[2] { 2, 8 },
			new int[2] { 1, 8 },
			new int[2] { 0, 8 }
		};

		public static void clearMatrix(ByteMatrix matrix)
		{
			matrix.clear(2);
		}

		public static void buildMatrix(BitArray dataBits, ErrorCorrectionLevel ecLevel, Version version, int maskPattern, ByteMatrix matrix)
		{
			clearMatrix(matrix);
			embedBasicPatterns(version, matrix);
			embedTypeInfo(ecLevel, maskPattern, matrix);
			maybeEmbedVersionInfo(version, matrix);
			embedDataBits(dataBits, maskPattern, matrix);
		}

		public static void embedBasicPatterns(Version version, ByteMatrix matrix)
		{
			embedPositionDetectionPatternsAndSeparators(matrix);
			embedDarkDotAtLeftBottomCorner(matrix);
			maybeEmbedPositionAdjustmentPatterns(version, matrix);
			embedTimingPatterns(matrix);
		}

		public static void embedTypeInfo(ErrorCorrectionLevel ecLevel, int maskPattern, ByteMatrix matrix)
		{
			BitArray bitArray = new BitArray();
			makeTypeInfoBits(ecLevel, maskPattern, bitArray);
			for (int i = 0; i < bitArray.Size; i++)
			{
				int value = (bitArray[bitArray.Size - 1 - i] ? 1 : 0);
				int x = TYPE_INFO_COORDINATES[i][0];
				int y = TYPE_INFO_COORDINATES[i][1];
				matrix[x, y] = value;
				if (i < 8)
				{
					int x2 = matrix.Width - i - 1;
					int y2 = 8;
					matrix[x2, y2] = value;
				}
				else
				{
					int x3 = 8;
					int y3 = matrix.Height - 7 + (i - 8);
					matrix[x3, y3] = value;
				}
			}
		}

		public static void maybeEmbedVersionInfo(Version version, ByteMatrix matrix)
		{
			if (version.VersionNumber < 7)
			{
				return;
			}
			BitArray bitArray = new BitArray();
			makeVersionInfoBits(version, bitArray);
			int num = 17;
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					int value = (bitArray[num] ? 1 : 0);
					num--;
					matrix[i, matrix.Height - 11 + j] = value;
					matrix[matrix.Height - 11 + j, i] = value;
				}
			}
		}

		public static void embedDataBits(BitArray dataBits, int maskPattern, ByteMatrix matrix)
		{
			int num = 0;
			int num2 = -1;
			int num3 = matrix.Width - 1;
			int i = matrix.Height - 1;
			while (num3 > 0)
			{
				if (num3 == 6)
				{
					num3--;
				}
				for (; i >= 0 && i < matrix.Height; i += num2)
				{
					for (int j = 0; j < 2; j++)
					{
						int x = num3 - j;
						if (isEmpty(matrix[x, i]))
						{
							int num4;
							if (num < dataBits.Size)
							{
								num4 = (dataBits[num] ? 1 : 0);
								num++;
							}
							else
							{
								num4 = 0;
							}
							if (maskPattern != -1 && MaskUtil.getDataMaskBit(maskPattern, x, i))
							{
								num4 ^= 1;
							}
							matrix[x, i] = num4;
						}
					}
				}
				num2 = -num2;
				i += num2;
				num3 -= 2;
			}
			if (num != dataBits.Size)
			{
				throw new WriterException("Not all bits consumed: " + num + '/' + dataBits.Size);
			}
		}

		public static int findMSBSet(int value_Renamed)
		{
			int num = 0;
			while (value_Renamed != 0)
			{
				value_Renamed = (int)((uint)value_Renamed >> 1);
				num++;
			}
			return num;
		}

		public static int calculateBCHCode(int value, int poly)
		{
			int num = findMSBSet(poly);
			value <<= num - 1;
			while (findMSBSet(value) >= num)
			{
				value ^= poly << findMSBSet(value) - num;
			}
			return value;
		}

		public static void makeTypeInfoBits(ErrorCorrectionLevel ecLevel, int maskPattern, BitArray bits)
		{
			if (!QRCode.isValidMaskPattern(maskPattern))
			{
				throw new WriterException("Invalid mask pattern");
			}
			int value = (ecLevel.Bits << 3) | maskPattern;
			bits.appendBits(value, 5);
			int value2 = calculateBCHCode(value, 1335);
			bits.appendBits(value2, 10);
			BitArray bitArray = new BitArray();
			bitArray.appendBits(21522, 15);
			bits.xor(bitArray);
			if (bits.Size != 15)
			{
				throw new WriterException("should not happen but we got: " + bits.Size);
			}
		}

		public static void makeVersionInfoBits(Version version, BitArray bits)
		{
			bits.appendBits(version.VersionNumber, 6);
			int value = calculateBCHCode(version.VersionNumber, 7973);
			bits.appendBits(value, 12);
			if (bits.Size != 18)
			{
				throw new WriterException("should not happen but we got: " + bits.Size);
			}
		}

		private static bool isEmpty(int value)
		{
			return value == 2;
		}

		private static void embedTimingPatterns(ByteMatrix matrix)
		{
			for (int i = 8; i < matrix.Width - 8; i++)
			{
				int value = (i + 1) % 2;
				if (isEmpty(matrix[i, 6]))
				{
					matrix[i, 6] = value;
				}
				if (isEmpty(matrix[6, i]))
				{
					matrix[6, i] = value;
				}
			}
		}

		private static void embedDarkDotAtLeftBottomCorner(ByteMatrix matrix)
		{
			if (matrix[8, matrix.Height - 8] == 0)
			{
				throw new WriterException();
			}
			matrix[8, matrix.Height - 8] = 1;
		}

		private static void embedHorizontalSeparationPattern(int xStart, int yStart, ByteMatrix matrix)
		{
			for (int i = 0; i < 8; i++)
			{
				if (!isEmpty(matrix[xStart + i, yStart]))
				{
					throw new WriterException();
				}
				matrix[xStart + i, yStart] = 0;
			}
		}

		private static void embedVerticalSeparationPattern(int xStart, int yStart, ByteMatrix matrix)
		{
			for (int i = 0; i < 7; i++)
			{
				if (!isEmpty(matrix[xStart, yStart + i]))
				{
					throw new WriterException();
				}
				matrix[xStart, yStart + i] = 0;
			}
		}

		private static void embedPositionAdjustmentPattern(int xStart, int yStart, ByteMatrix matrix)
		{
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					matrix[xStart + j, yStart + i] = POSITION_ADJUSTMENT_PATTERN[i][j];
				}
			}
		}

		private static void embedPositionDetectionPattern(int xStart, int yStart, ByteMatrix matrix)
		{
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					matrix[xStart + j, yStart + i] = POSITION_DETECTION_PATTERN[i][j];
				}
			}
		}

		private static void embedPositionDetectionPatternsAndSeparators(ByteMatrix matrix)
		{
			int num = POSITION_DETECTION_PATTERN[0].Length;
			embedPositionDetectionPattern(0, 0, matrix);
			embedPositionDetectionPattern(matrix.Width - num, 0, matrix);
			embedPositionDetectionPattern(0, matrix.Width - num, matrix);
			embedHorizontalSeparationPattern(0, 7, matrix);
			embedHorizontalSeparationPattern(matrix.Width - 8, 7, matrix);
			embedHorizontalSeparationPattern(0, matrix.Width - 8, matrix);
			embedVerticalSeparationPattern(7, 0, matrix);
			embedVerticalSeparationPattern(matrix.Height - 7 - 1, 0, matrix);
			embedVerticalSeparationPattern(7, matrix.Height - 7, matrix);
		}

		private static void maybeEmbedPositionAdjustmentPatterns(Version version, ByteMatrix matrix)
		{
			if (version.VersionNumber < 2)
			{
				return;
			}
			int num = version.VersionNumber - 1;
			int[] array = POSITION_ADJUSTMENT_PATTERN_COORDINATE_TABLE[num];
			int num2 = POSITION_ADJUSTMENT_PATTERN_COORDINATE_TABLE[num].Length;
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					int num3 = array[i];
					int num4 = array[j];
					if (num4 != -1 && num3 != -1 && isEmpty(matrix[num4, num3]))
					{
						embedPositionAdjustmentPattern(num4 - 2, num3 - 2, matrix);
					}
				}
			}
		}
	}
}
