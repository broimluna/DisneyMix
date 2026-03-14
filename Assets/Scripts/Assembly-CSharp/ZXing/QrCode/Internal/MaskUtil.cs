using System;

namespace ZXing.QrCode.Internal
{
	public static class MaskUtil
	{
		private const int N1 = 3;

		private const int N2 = 3;

		private const int N3 = 40;

		private const int N4 = 10;

		public static int applyMaskPenaltyRule1(ByteMatrix matrix)
		{
			return applyMaskPenaltyRule1Internal(matrix, true) + applyMaskPenaltyRule1Internal(matrix, false);
		}

		public static int applyMaskPenaltyRule2(ByteMatrix matrix)
		{
			int num = 0;
			byte[][] array = matrix.Array;
			int width = matrix.Width;
			int height = matrix.Height;
			for (int i = 0; i < height - 1; i++)
			{
				for (int j = 0; j < width - 1; j++)
				{
					int num2 = array[i][j];
					if (num2 == array[i][j + 1] && num2 == array[i + 1][j] && num2 == array[i + 1][j + 1])
					{
						num++;
					}
				}
			}
			return 3 * num;
		}

		public static int applyMaskPenaltyRule3(ByteMatrix matrix)
		{
			int num = 0;
			byte[][] array = matrix.Array;
			int width = matrix.Width;
			int height = matrix.Height;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					byte[] array2 = array[i];
					if (j + 6 < width && array2[j] == 1 && array2[j + 1] == 0 && array2[j + 2] == 1 && array2[j + 3] == 1 && array2[j + 4] == 1 && array2[j + 5] == 0 && array2[j + 6] == 1 && (isWhiteHorizontal(array2, j - 4, j) || isWhiteHorizontal(array2, j + 7, j + 11)))
					{
						num++;
					}
					if (i + 6 < height && array[i][j] == 1 && array[i + 1][j] == 0 && array[i + 2][j] == 1 && array[i + 3][j] == 1 && array[i + 4][j] == 1 && array[i + 5][j] == 0 && array[i + 6][j] == 1 && (isWhiteVertical(array, j, i - 4, i) || isWhiteVertical(array, j, i + 7, i + 11)))
					{
						num++;
					}
				}
			}
			return num * 40;
		}

		private static bool isWhiteHorizontal(byte[] rowArray, int from, int to)
		{
			for (int i = from; i < to; i++)
			{
				if (i >= 0 && i < rowArray.Length && rowArray[i] == 1)
				{
					return false;
				}
			}
			return true;
		}

		private static bool isWhiteVertical(byte[][] array, int col, int from, int to)
		{
			for (int i = from; i < to; i++)
			{
				if (i >= 0 && i < array.Length && array[i][col] == 1)
				{
					return false;
				}
			}
			return true;
		}

		public static int applyMaskPenaltyRule4(ByteMatrix matrix)
		{
			int num = 0;
			byte[][] array = matrix.Array;
			int width = matrix.Width;
			int height = matrix.Height;
			for (int i = 0; i < height; i++)
			{
				byte[] array2 = array[i];
				for (int j = 0; j < width; j++)
				{
					if (array2[j] == 1)
					{
						num++;
					}
				}
			}
			int num2 = matrix.Height * matrix.Width;
			double num3 = (double)num / (double)num2;
			int num4 = (int)(Math.Abs(num3 - 0.5) * 20.0);
			return num4 * 10;
		}

		public static bool getDataMaskBit(int maskPattern, int x, int y)
		{
			int num2;
			switch (maskPattern)
			{
			case 0:
				num2 = (y + x) & 1;
				break;
			case 1:
				num2 = y & 1;
				break;
			case 2:
				num2 = x % 3;
				break;
			case 3:
				num2 = (y + x) % 3;
				break;
			case 4:
				num2 = ((int)((uint)y >> 1) + x / 3) & 1;
				break;
			case 5:
			{
				int num = y * x;
				num2 = (num & 1) + num % 3;
				break;
			}
			case 6:
			{
				int num = y * x;
				num2 = ((num & 1) + num % 3) & 1;
				break;
			}
			case 7:
			{
				int num = y * x;
				num2 = (num % 3 + ((y + x) & 1)) & 1;
				break;
			}
			default:
				throw new ArgumentException("Invalid mask pattern: " + maskPattern);
			}
			return num2 == 0;
		}

		private static int applyMaskPenaltyRule1Internal(ByteMatrix matrix, bool isHorizontal)
		{
			int num = 0;
			int num2 = ((!isHorizontal) ? matrix.Width : matrix.Height);
			int num3 = ((!isHorizontal) ? matrix.Height : matrix.Width);
			byte[][] array = matrix.Array;
			for (int i = 0; i < num2; i++)
			{
				int num4 = 0;
				int num5 = -1;
				for (int j = 0; j < num3; j++)
				{
					int num6 = ((!isHorizontal) ? array[j][i] : array[i][j]);
					if (num6 == num5)
					{
						num4++;
						continue;
					}
					if (num4 >= 5)
					{
						num += 3 + (num4 - 5);
					}
					num4 = 1;
					num5 = num6;
				}
				if (num4 >= 5)
				{
					num += 3 + (num4 - 5);
				}
			}
			return num;
		}
	}
}
