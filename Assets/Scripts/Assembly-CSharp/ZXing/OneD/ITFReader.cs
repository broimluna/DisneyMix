using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class ITFReader : OneDReader
	{
		private const int W = 3;

		private const int N = 1;

		private const int LARGEST_DEFAULT_ALLOWED_LENGTH = 14;

		private static readonly int MAX_AVG_VARIANCE = (int)((float)OneDReader.PATTERN_MATCH_RESULT_SCALE_FACTOR * 0.42f);

		private static readonly int MAX_INDIVIDUAL_VARIANCE = (int)((float)OneDReader.PATTERN_MATCH_RESULT_SCALE_FACTOR * 0.78f);

		private static readonly int[] DEFAULT_ALLOWED_LENGTHS = new int[5] { 6, 8, 10, 12, 14 };

		private int narrowLineWidth = -1;

		private static readonly int[] START_PATTERN = new int[4] { 1, 1, 1, 1 };

		private static readonly int[] END_PATTERN_REVERSED = new int[3] { 1, 1, 3 };

		internal static int[][] PATTERNS = new int[10][]
		{
			new int[5] { 1, 1, 3, 3, 1 },
			new int[5] { 3, 1, 1, 1, 3 },
			new int[5] { 1, 3, 1, 1, 3 },
			new int[5] { 3, 3, 1, 1, 1 },
			new int[5] { 1, 1, 3, 1, 3 },
			new int[5] { 3, 1, 3, 1, 1 },
			new int[5] { 1, 3, 3, 1, 1 },
			new int[5] { 1, 1, 1, 3, 3 },
			new int[5] { 3, 1, 1, 3, 1 },
			new int[5] { 1, 3, 1, 3, 1 }
		};

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			int[] array = decodeStart(row);
			if (array == null)
			{
				return null;
			}
			int[] array2 = decodeEnd(row);
			if (array2 == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(20);
			if (!decodeMiddle(row, array[1], array2[0], stringBuilder))
			{
				return null;
			}
			string text = stringBuilder.ToString();
			int[] array3 = null;
			int num = 14;
			if (hints != null && hints.ContainsKey(DecodeHintType.ALLOWED_LENGTHS))
			{
				array3 = (int[])hints[DecodeHintType.ALLOWED_LENGTHS];
				num = 0;
			}
			if (array3 == null)
			{
				array3 = DEFAULT_ALLOWED_LENGTHS;
				num = 14;
			}
			int length = text.Length;
			bool flag = length > 14;
			if (!flag)
			{
				int[] array4 = array3;
				foreach (int num2 in array4)
				{
					if (length == num2)
					{
						flag = true;
						break;
					}
					if (num2 > num)
					{
						num = num2;
					}
				}
				if (!flag && length > num)
				{
					flag = true;
				}
				if (!flag)
				{
					return null;
				}
			}
			ResultPointCallback resultPointCallback = ((hints != null && hints.ContainsKey(DecodeHintType.NEED_RESULT_POINT_CALLBACK)) ? ((ResultPointCallback)hints[DecodeHintType.NEED_RESULT_POINT_CALLBACK]) : null);
			if (resultPointCallback != null)
			{
				resultPointCallback(new ResultPoint(array[1], rowNumber));
				resultPointCallback(new ResultPoint(array2[0], rowNumber));
			}
			return new Result(text, null, new ResultPoint[2]
			{
				new ResultPoint(array[1], rowNumber),
				new ResultPoint(array2[0], rowNumber)
			}, BarcodeFormat.ITF);
		}

		private static bool decodeMiddle(BitArray row, int payloadStart, int payloadEnd, StringBuilder resultString)
		{
			int[] array = new int[10];
			int[] array2 = new int[5];
			int[] array3 = new int[5];
			while (payloadStart < payloadEnd)
			{
				if (!OneDReader.recordPattern(row, payloadStart, array))
				{
					return false;
				}
				for (int i = 0; i < 5; i++)
				{
					int num = i << 1;
					array2[i] = array[num];
					array3[i] = array[num + 1];
				}
				int bestMatch;
				if (!decodeDigit(array2, out bestMatch))
				{
					return false;
				}
				resultString.Append((char)(48 + bestMatch));
				if (!decodeDigit(array3, out bestMatch))
				{
					return false;
				}
				resultString.Append((char)(48 + bestMatch));
				int[] array4 = array;
				foreach (int num2 in array4)
				{
					payloadStart += num2;
				}
			}
			return true;
		}

		private int[] decodeStart(BitArray row)
		{
			int num = skipWhiteSpace(row);
			if (num < 0)
			{
				return null;
			}
			int[] array = findGuardPattern(row, num, START_PATTERN);
			if (array == null)
			{
				return null;
			}
			narrowLineWidth = array[1] - array[0] >> 2;
			if (!validateQuietZone(row, array[0]))
			{
				return null;
			}
			return array;
		}

		private bool validateQuietZone(BitArray row, int startPattern)
		{
			int num = narrowLineWidth * 10;
			num = ((num >= startPattern) ? startPattern : num);
			int num2 = startPattern - 1;
			while (num > 0 && num2 >= 0 && !row[num2])
			{
				num--;
				num2--;
			}
			if (num != 0)
			{
				return false;
			}
			return true;
		}

		private static int skipWhiteSpace(BitArray row)
		{
			int size = row.Size;
			int nextSet = row.getNextSet(0);
			if (nextSet == size)
			{
				return -1;
			}
			return nextSet;
		}

		private int[] decodeEnd(BitArray row)
		{
			row.reverse();
			int num = skipWhiteSpace(row);
			if (num < 0)
			{
				return null;
			}
			int[] array = findGuardPattern(row, num, END_PATTERN_REVERSED);
			if (array == null)
			{
				row.reverse();
				return null;
			}
			if (!validateQuietZone(row, array[0]))
			{
				row.reverse();
				return null;
			}
			int num2 = array[0];
			array[0] = row.Size - array[1];
			array[1] = row.Size - num2;
			row.reverse();
			return array;
		}

		private static int[] findGuardPattern(BitArray row, int rowOffset, int[] pattern)
		{
			int num = pattern.Length;
			int[] array = new int[num];
			int size = row.Size;
			bool flag = false;
			int num2 = 0;
			int num3 = rowOffset;
			for (int i = rowOffset; i < size; i++)
			{
				if (row[i] ^ flag)
				{
					array[num2]++;
					continue;
				}
				if (num2 == num - 1)
				{
					if (OneDReader.patternMatchVariance(array, pattern, MAX_INDIVIDUAL_VARIANCE) < MAX_AVG_VARIANCE)
					{
						return new int[2] { num3, i };
					}
					num3 += array[0] + array[1];
					Array.Copy(array, 2, array, 0, num - 2);
					array[num - 2] = 0;
					array[num - 1] = 0;
					num2--;
				}
				else
				{
					num2++;
				}
				array[num2] = 1;
				flag = !flag;
			}
			return null;
		}

		private static bool decodeDigit(int[] counters, out int bestMatch)
		{
			int num = MAX_AVG_VARIANCE;
			bestMatch = -1;
			int num2 = PATTERNS.Length;
			for (int i = 0; i < num2; i++)
			{
				int[] pattern = PATTERNS[i];
				int num3 = OneDReader.patternMatchVariance(counters, pattern, MAX_INDIVIDUAL_VARIANCE);
				if (num3 < num)
				{
					num = num3;
					bestMatch = i;
				}
			}
			return bestMatch >= 0;
		}
	}
}
