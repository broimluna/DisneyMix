using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class CodaBarReader : OneDReader
	{
		private const string ALPHABET_STRING = "0123456789-$:/.+ABCD";

		private const int MIN_CHARACTER_LENGTH = 3;

		private static readonly int MAX_ACCEPTABLE = (int)((float)OneDReader.PATTERN_MATCH_RESULT_SCALE_FACTOR * 2f);

		private static readonly int PADDING = (int)((float)OneDReader.PATTERN_MATCH_RESULT_SCALE_FACTOR * 1.5f);

		internal static readonly char[] ALPHABET = "0123456789-$:/.+ABCD".ToCharArray();

		internal static int[] CHARACTER_ENCODINGS = new int[20]
		{
			3, 6, 9, 96, 18, 66, 33, 36, 48, 72,
			12, 24, 69, 81, 84, 21, 26, 41, 11, 14
		};

		private static readonly char[] STARTEND_ENCODING = new char[4] { 'A', 'B', 'C', 'D' };

		private readonly StringBuilder decodeRowResult;

		private int[] counters;

		private int counterLength;

		public CodaBarReader()
		{
			decodeRowResult = new StringBuilder(20);
			counters = new int[80];
			counterLength = 0;
		}

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			for (int i = 0; i < counters.Length; i++)
			{
				counters[i] = 0;
			}
			if (!setCounters(row))
			{
				return null;
			}
			int num = findStartPattern();
			if (num < 0)
			{
				return null;
			}
			int num2 = num;
			decodeRowResult.Length = 0;
			int num3;
			do
			{
				num3 = toNarrowWidePattern(num2);
				if (num3 == -1)
				{
					return null;
				}
				decodeRowResult.Append((char)num3);
				num2 += 8;
			}
			while ((decodeRowResult.Length <= 1 || !arrayContains(STARTEND_ENCODING, ALPHABET[num3])) && num2 < counterLength);
			int num4 = counters[num2 - 1];
			int num5 = 0;
			for (int j = -8; j < -1; j++)
			{
				num5 += counters[num2 + j];
			}
			if (num2 < counterLength && num4 < num5 / 2)
			{
				return null;
			}
			if (!validatePattern(num))
			{
				return null;
			}
			for (int k = 0; k < decodeRowResult.Length; k++)
			{
				decodeRowResult[k] = ALPHABET[(uint)decodeRowResult[k]];
			}
			char key = decodeRowResult[0];
			if (!arrayContains(STARTEND_ENCODING, key))
			{
				return null;
			}
			char key2 = decodeRowResult[decodeRowResult.Length - 1];
			if (!arrayContains(STARTEND_ENCODING, key2))
			{
				return null;
			}
			if (decodeRowResult.Length <= 3)
			{
				return null;
			}
			if (!SupportClass.GetValue(hints, DecodeHintType.RETURN_CODABAR_START_END, false))
			{
				decodeRowResult.Remove(decodeRowResult.Length - 1, 1);
				decodeRowResult.Remove(0, 1);
			}
			int num6 = 0;
			for (int l = 0; l < num; l++)
			{
				num6 += counters[l];
			}
			float x = num6;
			for (int m = num; m < num2 - 1; m++)
			{
				num6 += counters[m];
			}
			float x2 = num6;
			ResultPointCallback value = SupportClass.GetValue<ResultPointCallback>(hints, DecodeHintType.NEED_RESULT_POINT_CALLBACK, null);
			if (value != null)
			{
				value(new ResultPoint(x, rowNumber));
				value(new ResultPoint(x2, rowNumber));
			}
			return new Result(decodeRowResult.ToString(), null, new ResultPoint[2]
			{
				new ResultPoint(x, rowNumber),
				new ResultPoint(x2, rowNumber)
			}, BarcodeFormat.CODABAR);
		}

		private bool validatePattern(int start)
		{
			int[] array = new int[4];
			int[] array2 = new int[4];
			int num = decodeRowResult.Length - 1;
			int num2 = start;
			int num3 = 0;
			while (true)
			{
				int num4 = CHARACTER_ENCODINGS[(uint)decodeRowResult[num3]];
				for (int num5 = 6; num5 >= 0; num5--)
				{
					int num6 = (num5 & 1) + (num4 & 1) * 2;
					array[num6] += counters[num2 + num5];
					array2[num6]++;
					num4 >>= 1;
				}
				if (num3 >= num)
				{
					break;
				}
				num2 += 8;
				num3++;
			}
			int[] array3 = new int[4];
			int[] array4 = new int[4];
			for (int i = 0; i < 2; i++)
			{
				array4[i] = 0;
				array4[i + 2] = (array[i] << OneDReader.INTEGER_MATH_SHIFT) / array2[i] + (array[i + 2] << OneDReader.INTEGER_MATH_SHIFT) / array2[i + 2] >> 1;
				array3[i] = array4[i + 2];
				array3[i + 2] = (array[i + 2] * MAX_ACCEPTABLE + PADDING) / array2[i + 2];
			}
			num2 = start;
			int num7 = 0;
			while (true)
			{
				int num8 = CHARACTER_ENCODINGS[(uint)decodeRowResult[num7]];
				for (int num9 = 6; num9 >= 0; num9--)
				{
					int num10 = (num9 & 1) + (num8 & 1) * 2;
					int num11 = counters[num2 + num9] << OneDReader.INTEGER_MATH_SHIFT;
					if (num11 < array4[num10] || num11 > array3[num10])
					{
						return false;
					}
					num8 >>= 1;
				}
				if (num7 >= num)
				{
					break;
				}
				num2 += 8;
				num7++;
			}
			return true;
		}

		private bool setCounters(BitArray row)
		{
			counterLength = 0;
			int i = row.getNextUnset(0);
			int size = row.Size;
			if (i >= size)
			{
				return false;
			}
			bool flag = true;
			int num = 0;
			for (; i < size; i++)
			{
				if (row[i] ^ flag)
				{
					num++;
					continue;
				}
				counterAppend(num);
				num = 1;
				flag = !flag;
			}
			counterAppend(num);
			return true;
		}

		private void counterAppend(int e)
		{
			counters[counterLength] = e;
			counterLength++;
			if (counterLength >= counters.Length)
			{
				int[] destinationArray = new int[counterLength * 2];
				Array.Copy(counters, 0, destinationArray, 0, counterLength);
				counters = destinationArray;
			}
		}

		private int findStartPattern()
		{
			for (int i = 1; i < counterLength; i += 2)
			{
				int num = toNarrowWidePattern(i);
				if (num != -1 && arrayContains(STARTEND_ENCODING, ALPHABET[num]))
				{
					int num2 = 0;
					for (int j = i; j < i + 7; j++)
					{
						num2 += counters[j];
					}
					if (i == 1 || counters[i - 1] >= num2 / 2)
					{
						return i;
					}
				}
			}
			return -1;
		}

		internal static bool arrayContains(char[] array, char key)
		{
			if (array != null)
			{
				foreach (char c in array)
				{
					if (c == key)
					{
						return true;
					}
				}
			}
			return false;
		}

		private int toNarrowWidePattern(int position)
		{
			int num = position + 7;
			if (num >= counterLength)
			{
				return -1;
			}
			int[] array = counters;
			int num2 = 0;
			int num3 = int.MaxValue;
			for (int i = position; i < num; i += 2)
			{
				int num4 = array[i];
				if (num4 < num3)
				{
					num3 = num4;
				}
				if (num4 > num2)
				{
					num2 = num4;
				}
			}
			int num5 = (num3 + num2) / 2;
			int num6 = 0;
			int num7 = int.MaxValue;
			for (int j = position + 1; j < num; j += 2)
			{
				int num8 = array[j];
				if (num8 < num7)
				{
					num7 = num8;
				}
				if (num8 > num6)
				{
					num6 = num8;
				}
			}
			int num9 = (num7 + num6) / 2;
			int num10 = 128;
			int num11 = 0;
			for (int k = 0; k < 7; k++)
			{
				int num12 = (((k & 1) != 0) ? num9 : num5);
				num10 >>= 1;
				if (array[position + k] > num12)
				{
					num11 |= num10;
				}
			}
			for (int l = 0; l < CHARACTER_ENCODINGS.Length; l++)
			{
				if (CHARACTER_ENCODINGS[l] == num11)
				{
					return l;
				}
			}
			return -1;
		}
	}
}
