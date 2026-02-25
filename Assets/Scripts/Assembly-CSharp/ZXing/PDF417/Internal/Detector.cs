using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.PDF417.Internal
{
	public sealed class Detector
	{
		private const int INTEGER_MATH_SHIFT = 8;

		private const int PATTERN_MATCH_RESULT_SCALE_FACTOR = 256;

		private const int MAX_AVG_VARIANCE = 107;

		private const int MAX_INDIVIDUAL_VARIANCE = 204;

		private const int MAX_PIXEL_DRIFT = 3;

		private const int MAX_PATTERN_DRIFT = 5;

		private const int SKIPPED_ROW_COUNT_MAX = 25;

		private const int ROW_STEP = 5;

		private const int BARCODE_MIN_HEIGHT = 10;

		private static readonly int[] INDEXES_START_PATTERN = new int[4] { 0, 4, 1, 5 };

		private static readonly int[] INDEXES_STOP_PATTERN = new int[4] { 6, 2, 7, 3 };

		private static readonly int[] START_PATTERN = new int[8] { 8, 1, 1, 1, 1, 1, 1, 3 };

		private static readonly int[] STOP_PATTERN = new int[9] { 7, 1, 1, 3, 1, 1, 1, 2, 1 };

		public static PDF417DetectorResult detect(BinaryBitmap image, IDictionary<DecodeHintType, object> hints, bool multiple)
		{
			BitMatrix bitMatrix = image.BlackMatrix;
			List<ResultPoint[]> list = detect(multiple, bitMatrix);
			if (list.Count == 0)
			{
				bitMatrix = (BitMatrix)bitMatrix.Clone();
				bitMatrix.rotate180();
				list = detect(multiple, bitMatrix);
			}
			return new PDF417DetectorResult(bitMatrix, list);
		}

		private static List<ResultPoint[]> detect(bool multiple, BitMatrix bitMatrix)
		{
			List<ResultPoint[]> list = new List<ResultPoint[]>();
			int num = 0;
			int startColumn = 0;
			bool flag = false;
			while (num < bitMatrix.Height)
			{
				ResultPoint[] array = findVertices(bitMatrix, num, startColumn);
				if (array[0] == null && array[3] == null)
				{
					if (!flag)
					{
						break;
					}
					flag = false;
					startColumn = 0;
					foreach (ResultPoint[] item in list)
					{
						if (item[1] != null)
						{
							num = (int)Math.Max(num, item[1].Y);
						}
						if (item[3] != null)
						{
							num = Math.Max(num, (int)item[3].Y);
						}
					}
					num += 5;
				}
				else
				{
					flag = true;
					list.Add(array);
					if (!multiple)
					{
						break;
					}
					if (array[2] != null)
					{
						startColumn = (int)array[2].X;
						num = (int)array[2].Y;
					}
					else
					{
						startColumn = (int)array[4].X;
						num = (int)array[4].Y;
					}
				}
			}
			return list;
		}

		private static ResultPoint[] findVertices(BitMatrix matrix, int startRow, int startColumn)
		{
			int height = matrix.Height;
			int width = matrix.Width;
			ResultPoint[] array = new ResultPoint[8];
			copyToResult(array, findRowsWithPattern(matrix, height, width, startRow, startColumn, START_PATTERN), INDEXES_START_PATTERN);
			if (array[4] != null)
			{
				startColumn = (int)array[4].X;
				startRow = (int)array[4].Y;
			}
			copyToResult(array, findRowsWithPattern(matrix, height, width, startRow, startColumn, STOP_PATTERN), INDEXES_STOP_PATTERN);
			return array;
		}

		private static void copyToResult(ResultPoint[] result, ResultPoint[] tmpResult, int[] destinationIndexes)
		{
			for (int i = 0; i < destinationIndexes.Length; i++)
			{
				result[destinationIndexes[i]] = tmpResult[i];
			}
		}

		private static ResultPoint[] findRowsWithPattern(BitMatrix matrix, int height, int width, int startRow, int startColumn, int[] pattern)
		{
			ResultPoint[] array = new ResultPoint[4];
			bool flag = false;
			int[] counters = new int[pattern.Length];
			while (startRow < height)
			{
				int[] array2 = findGuardPattern(matrix, startColumn, startRow, width, false, pattern, counters);
				if (array2 != null)
				{
					while (startRow > 0)
					{
						int[] array3 = findGuardPattern(matrix, startColumn, --startRow, width, false, pattern, counters);
						if (array3 != null)
						{
							array2 = array3;
							continue;
						}
						startRow++;
						break;
					}
					array[0] = new ResultPoint(array2[0], startRow);
					array[1] = new ResultPoint(array2[1], startRow);
					flag = true;
					break;
				}
				startRow += 5;
			}
			int i = startRow + 1;
			if (flag)
			{
				int num = 0;
				int[] array4 = new int[2]
				{
					(int)array[0].X,
					(int)array[1].X
				};
				for (; i < height; i++)
				{
					int[] array5 = findGuardPattern(matrix, array4[0], i, width, false, pattern, counters);
					if (array5 != null && Math.Abs(array4[0] - array5[0]) < 5 && Math.Abs(array4[1] - array5[1]) < 5)
					{
						array4 = array5;
						num = 0;
						continue;
					}
					if (num > 25)
					{
						break;
					}
					num++;
				}
				i -= num + 1;
				array[2] = new ResultPoint(array4[0], i);
				array[3] = new ResultPoint(array4[1], i);
			}
			if (i - startRow < 10)
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = null;
				}
			}
			return array;
		}

		private static int[] findGuardPattern(BitMatrix matrix, int column, int row, int width, bool whiteFirst, int[] pattern, int[] counters)
		{
			SupportClass.Fill(counters, 0);
			int num = pattern.Length;
			bool flag = whiteFirst;
			int num2 = column;
			int num3 = 0;
			while (matrix[num2, row] && num2 > 0 && num3++ < 3)
			{
				num2--;
			}
			int i = num2;
			int num4 = 0;
			for (; i < width; i++)
			{
				bool flag2 = matrix[i, row];
				if (flag2 ^ flag)
				{
					counters[num4]++;
					continue;
				}
				if (num4 == num - 1)
				{
					if (patternMatchVariance(counters, pattern, 204) < 107)
					{
						return new int[2] { num2, i };
					}
					num2 += counters[0] + counters[1];
					Array.Copy(counters, 2, counters, 0, num - 2);
					counters[num - 2] = 0;
					counters[num - 1] = 0;
					num4--;
				}
				else
				{
					num4++;
				}
				counters[num4] = 1;
				flag = !flag;
			}
			if (num4 == num - 1 && patternMatchVariance(counters, pattern, 204) < 107)
			{
				return new int[2]
				{
					num2,
					i - 1
				};
			}
			return null;
		}

		private static int patternMatchVariance(int[] counters, int[] pattern, int maxIndividualVariance)
		{
			int num = counters.Length;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < num; i++)
			{
				num2 += counters[i];
				num3 += pattern[i];
			}
			if (num2 < num3)
			{
				return int.MaxValue;
			}
			int num4 = (num2 << 8) / num3;
			maxIndividualVariance = maxIndividualVariance * num4 >> 8;
			int num5 = 0;
			for (int j = 0; j < num; j++)
			{
				int num6 = counters[j] << 8;
				int num7 = pattern[j] * num4;
				int num8 = ((num6 <= num7) ? (num7 - num6) : (num6 - num7));
				if (num8 > maxIndividualVariance)
				{
					return int.MaxValue;
				}
				num5 += num8;
			}
			return num5 / num2;
		}
	}
}
