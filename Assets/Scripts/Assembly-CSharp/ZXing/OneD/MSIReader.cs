using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class MSIReader : OneDReader
	{
		private const int START_ENCODING = 6;

		private const int END_ENCODING = 9;

		internal static string ALPHABET_STRING = "0123456789";

		private static readonly char[] ALPHABET = ALPHABET_STRING.ToCharArray();

		internal static int[] CHARACTER_ENCODINGS = new int[10] { 2340, 2342, 2356, 2358, 2468, 2470, 2484, 2486, 3364, 3366 };

		private readonly bool usingCheckDigit;

		private readonly StringBuilder decodeRowResult;

		private readonly int[] counters;

		private int averageCounterWidth;

		private static readonly int[] doubleAndCrossSum = new int[10] { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };

		public MSIReader()
			: this(false)
		{
		}

		public MSIReader(bool usingCheckDigit)
		{
			this.usingCheckDigit = usingCheckDigit;
			decodeRowResult = new StringBuilder(20);
			counters = new int[8];
		}

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			for (int i = 0; i < counters.Length; i++)
			{
				counters[i] = 0;
			}
			decodeRowResult.Length = 0;
			int[] array = findStartPattern(row, counters);
			if (array == null)
			{
				return null;
			}
			int num = row.getNextSet(array[1]);
			int num2 = num;
			char c;
			do
			{
				if (!OneDReader.recordPattern(row, num, counters, 8))
				{
					int[] array2 = findEndPattern(row, num, counters);
					if (array2 == null)
					{
						return null;
					}
					num2 = num;
					num = array2[1];
					break;
				}
				int pattern = toPattern(counters, 8);
				if (!patternToChar(pattern, out c))
				{
					int[] array3 = findEndPattern(row, num, counters);
					if (array3 == null)
					{
						return null;
					}
					num2 = num;
					num = array3[1];
					break;
				}
				decodeRowResult.Append(c);
				num2 = num;
				int[] array4 = counters;
				foreach (int num3 in array4)
				{
					num += num3;
				}
				num = row.getNextSet(num);
			}
			while (c != '*');
			if (decodeRowResult.Length < 3)
			{
				return null;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(decodeRowResult.ToString());
			string text = decodeRowResult.ToString();
			if (usingCheckDigit)
			{
				string text2 = text.Substring(0, text.Length - 1);
				int num4 = CalculateChecksumLuhn(text2);
				if ((ushort)(num4 + 48) != text[text2.Length])
				{
					return null;
				}
			}
			float x = (float)(array[1] + array[0]) / 2f;
			float x2 = (float)(num + num2) / 2f;
			ResultPointCallback resultPointCallback = ((hints != null && hints.ContainsKey(DecodeHintType.NEED_RESULT_POINT_CALLBACK)) ? ((ResultPointCallback)hints[DecodeHintType.NEED_RESULT_POINT_CALLBACK]) : null);
			if (resultPointCallback != null)
			{
				resultPointCallback(new ResultPoint(x, rowNumber));
				resultPointCallback(new ResultPoint(x2, rowNumber));
			}
			return new Result(text, bytes, new ResultPoint[2]
			{
				new ResultPoint(x, rowNumber),
				new ResultPoint(x2, rowNumber)
			}, BarcodeFormat.MSI);
		}

		private int[] findStartPattern(BitArray row, int[] counters)
		{
			int size = row.Size;
			int nextSet = row.getNextSet(0);
			int num = 0;
			int num2 = nextSet;
			bool flag = false;
			counters[0] = 0;
			counters[1] = 0;
			for (int i = nextSet; i < size; i++)
			{
				if (row[i] ^ flag)
				{
					counters[num]++;
					continue;
				}
				if (num == 1)
				{
					float num3 = (float)counters[0] / (float)counters[1];
					if ((double)num3 >= 1.5 && num3 <= 5f)
					{
						calculateAverageCounterWidth(counters, 2);
						if (toPattern(counters, 2) == 6 && row.isRange(Math.Max(0, num2 - (i - num2 >> 1)), num2, false))
						{
							return new int[2] { num2, i };
						}
					}
					num2 += counters[0] + counters[1];
					Array.Copy(counters, 2, counters, 0, 0);
					counters[0] = 0;
					counters[1] = 0;
					num--;
				}
				else
				{
					num++;
				}
				counters[num] = 1;
				flag = !flag;
			}
			return null;
		}

		private int[] findEndPattern(BitArray row, int rowOffset, int[] counters)
		{
			int size = row.Size;
			int num = 0;
			bool flag = false;
			counters[0] = 0;
			counters[1] = 0;
			counters[2] = 0;
			for (int i = rowOffset; i < size; i++)
			{
				if (row[i] ^ flag)
				{
					counters[num]++;
					continue;
				}
				if (num == 2)
				{
					float num2 = (float)counters[1] / (float)counters[0];
					if ((double)num2 >= 1.5 && num2 <= 5f && toPattern(counters, 3) == 9)
					{
						int end = Math.Min(row.Size - 1, i + (i - rowOffset >> 1));
						if (row.isRange(i, end, false))
						{
							return new int[2] { rowOffset, i };
						}
					}
					return null;
				}
				num++;
				counters[num] = 1;
				flag = !flag;
			}
			return null;
		}

		private void calculateAverageCounterWidth(int[] counters, int patternLength)
		{
			int num = int.MaxValue;
			int num2 = 0;
			for (int i = 0; i < patternLength; i++)
			{
				int num3 = counters[i];
				if (num3 < num)
				{
					num = num3;
				}
				if (num3 > num2)
				{
					num2 = num3;
				}
			}
			averageCounterWidth = ((num2 << 8) + (num << 8)) / 2;
		}

		private int toPattern(int[] counters, int patternLength)
		{
			int num = 0;
			int num2 = 1;
			int num3 = 3;
			for (int i = 0; i < patternLength; i++)
			{
				int num4 = counters[i];
				num = ((num4 << 8 >= averageCounterWidth) ? ((num << 2) | num3) : ((num << 1) | num2));
				num2 ^= 1;
				num3 ^= 3;
			}
			return num;
		}

		private static bool patternToChar(int pattern, out char c)
		{
			for (int i = 0; i < CHARACTER_ENCODINGS.Length; i++)
			{
				if (CHARACTER_ENCODINGS[i] == pattern)
				{
					c = ALPHABET[i];
					return true;
				}
			}
			c = '*';
			return false;
		}

		private static int CalculateChecksumLuhn(string number)
		{
			int num = 0;
			for (int num2 = number.Length - 2; num2 >= 0; num2 -= 2)
			{
				int num3 = number[num2] - 48;
				num += num3;
			}
			for (int num4 = number.Length - 1; num4 >= 0; num4 -= 2)
			{
				int num5 = doubleAndCrossSum[number[num4] - 48];
				num += num5;
			}
			return (10 - num % 10) % 10;
		}
	}
}
