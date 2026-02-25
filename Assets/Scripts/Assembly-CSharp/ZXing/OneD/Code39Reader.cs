using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class Code39Reader : OneDReader
	{
		internal static string ALPHABET_STRING = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. *$/+%";

		private static readonly char[] ALPHABET = ALPHABET_STRING.ToCharArray();

		internal static int[] CHARACTER_ENCODINGS = new int[44]
		{
			52, 289, 97, 352, 49, 304, 112, 37, 292, 100,
			265, 73, 328, 25, 280, 88, 13, 268, 76, 28,
			259, 67, 322, 19, 274, 82, 7, 262, 70, 22,
			385, 193, 448, 145, 400, 208, 133, 388, 196, 148,
			168, 162, 138, 42
		};

		private static readonly int ASTERISK_ENCODING = CHARACTER_ENCODINGS[39];

		private readonly bool usingCheckDigit;

		private readonly bool extendedMode;

		private readonly StringBuilder decodeRowResult;

		private readonly int[] counters;

		public Code39Reader()
			: this(false)
		{
		}

		public Code39Reader(bool usingCheckDigit)
			: this(usingCheckDigit, false)
		{
		}

		public Code39Reader(bool usingCheckDigit, bool extendedMode)
		{
			this.usingCheckDigit = usingCheckDigit;
			this.extendedMode = extendedMode;
			decodeRowResult = new StringBuilder(20);
			counters = new int[9];
		}

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			for (int i = 0; i < counters.Length; i++)
			{
				counters[i] = 0;
			}
			decodeRowResult.Length = 0;
			int[] array = findAsteriskPattern(row, counters);
			if (array == null)
			{
				return null;
			}
			int num = row.getNextSet(array[1]);
			int size = row.Size;
			char c;
			int num3;
			do
			{
				if (!OneDReader.recordPattern(row, num, counters))
				{
					return null;
				}
				int num2 = toNarrowWidePattern(counters);
				if (num2 < 0)
				{
					return null;
				}
				if (!patternToChar(num2, out c))
				{
					return null;
				}
				decodeRowResult.Append(c);
				num3 = num;
				int[] array2 = counters;
				foreach (int num4 in array2)
				{
					num += num4;
				}
				num = row.getNextSet(num);
			}
			while (c != '*');
			decodeRowResult.Length -= 1;
			int num5 = 0;
			int[] array3 = counters;
			foreach (int num6 in array3)
			{
				num5 += num6;
			}
			int num7 = num - num3 - num5;
			if (num != size && num7 << 1 < num5)
			{
				return null;
			}
			bool flag = usingCheckDigit;
			if (hints != null && hints.ContainsKey(DecodeHintType.ASSUME_CODE_39_CHECK_DIGIT))
			{
				flag = (bool)hints[DecodeHintType.ASSUME_CODE_39_CHECK_DIGIT];
			}
			if (flag)
			{
				int num8 = decodeRowResult.Length - 1;
				int num9 = 0;
				for (int l = 0; l < num8; l++)
				{
					num9 += ALPHABET_STRING.IndexOf(decodeRowResult[l]);
				}
				if (decodeRowResult[num8] != ALPHABET[num9 % 43])
				{
					return null;
				}
				decodeRowResult.Length = num8;
			}
			if (decodeRowResult.Length == 0)
			{
				return null;
			}
			bool flag2 = extendedMode;
			if (hints != null && hints.ContainsKey(DecodeHintType.USE_CODE_39_EXTENDED_MODE))
			{
				flag2 = (bool)hints[DecodeHintType.USE_CODE_39_EXTENDED_MODE];
			}
			string text;
			if (flag2)
			{
				text = decodeExtended(decodeRowResult.ToString());
				if (text == null)
				{
					if (hints == null || !hints.ContainsKey(DecodeHintType.RELAXED_CODE_39_EXTENDED_MODE) || !Convert.ToBoolean(hints[DecodeHintType.RELAXED_CODE_39_EXTENDED_MODE]))
					{
						return null;
					}
					text = decodeRowResult.ToString();
				}
			}
			else
			{
				text = decodeRowResult.ToString();
			}
			float x = (float)(array[1] + array[0]) / 2f;
			float x2 = (float)num3 + (float)num5 / 2f;
			ResultPointCallback resultPointCallback = ((hints != null && hints.ContainsKey(DecodeHintType.NEED_RESULT_POINT_CALLBACK)) ? ((ResultPointCallback)hints[DecodeHintType.NEED_RESULT_POINT_CALLBACK]) : null);
			if (resultPointCallback != null)
			{
				resultPointCallback(new ResultPoint(x, rowNumber));
				resultPointCallback(new ResultPoint(x2, rowNumber));
			}
			return new Result(text, null, new ResultPoint[2]
			{
				new ResultPoint(x, rowNumber),
				new ResultPoint(x2, rowNumber)
			}, BarcodeFormat.CODE_39);
		}

		private static int[] findAsteriskPattern(BitArray row, int[] counters)
		{
			int size = row.Size;
			int nextSet = row.getNextSet(0);
			int num = 0;
			int num2 = nextSet;
			bool flag = false;
			int num3 = counters.Length;
			for (int i = nextSet; i < size; i++)
			{
				if (row[i] ^ flag)
				{
					counters[num]++;
					continue;
				}
				if (num == num3 - 1)
				{
					if (toNarrowWidePattern(counters) == ASTERISK_ENCODING && row.isRange(Math.Max(0, num2 - (i - num2 >> 1)), num2, false))
					{
						return new int[2] { num2, i };
					}
					num2 += counters[0] + counters[1];
					Array.Copy(counters, 2, counters, 0, num3 - 2);
					counters[num3 - 2] = 0;
					counters[num3 - 1] = 0;
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

		private static int toNarrowWidePattern(int[] counters)
		{
			int num = counters.Length;
			int num2 = 0;
			int num5;
			do
			{
				int num3 = int.MaxValue;
				foreach (int num4 in counters)
				{
					if (num4 < num3 && num4 > num2)
					{
						num3 = num4;
					}
				}
				num2 = num3;
				num5 = 0;
				int num6 = 0;
				int num7 = 0;
				for (int j = 0; j < num; j++)
				{
					int num8 = counters[j];
					if (num8 > num2)
					{
						num7 |= 1 << ((num - 1 - j) & 0x1F);
						num5++;
						num6 += num8;
					}
				}
				if (num5 != 3)
				{
					continue;
				}
				for (int k = 0; k < num; k++)
				{
					if (num5 <= 0)
					{
						break;
					}
					int num9 = counters[k];
					if (num9 > num2)
					{
						num5--;
						if (num9 << 1 >= num6)
						{
							return -1;
						}
					}
				}
				return num7;
			}
			while (num5 > 3);
			return -1;
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

		private static string decodeExtended(string encoded)
		{
			int length = encoded.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				char c = encoded[i];
				if (c == '+' || c == '$' || c == '%' || c == '/')
				{
					if (i + 1 >= encoded.Length)
					{
						return null;
					}
					char c2 = encoded[i + 1];
					char value = '\0';
					switch (c)
					{
					case '+':
						if (c2 >= 'A' && c2 <= 'Z')
						{
							value = (char)(c2 + 32);
							break;
						}
						return null;
					case '$':
						if (c2 >= 'A' && c2 <= 'Z')
						{
							value = (char)(c2 - 64);
							break;
						}
						return null;
					case '%':
						if (c2 >= 'A' && c2 <= 'E')
						{
							value = (char)(c2 - 38);
							break;
						}
						if (c2 >= 'F' && c2 <= 'W')
						{
							value = (char)(c2 - 11);
							break;
						}
						return null;
					case '/':
						if (c2 >= 'A' && c2 <= 'O')
						{
							value = (char)(c2 - 32);
							break;
						}
						if (c2 == 'Z')
						{
							value = ':';
							break;
						}
						return null;
					}
					stringBuilder.Append(value);
					i++;
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
