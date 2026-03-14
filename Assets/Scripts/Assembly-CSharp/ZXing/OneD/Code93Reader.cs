using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class Code93Reader : OneDReader
	{
		private const string ALPHABET_STRING = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%abcd*";

		private static readonly char[] ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%abcd*".ToCharArray();

		private static readonly int[] CHARACTER_ENCODINGS = new int[48]
		{
			276, 328, 324, 322, 296, 292, 290, 336, 274, 266,
			424, 420, 418, 404, 402, 394, 360, 356, 354, 308,
			282, 344, 332, 326, 300, 278, 436, 434, 428, 422,
			406, 410, 364, 358, 310, 314, 302, 468, 466, 458,
			366, 374, 430, 294, 474, 470, 306, 350
		};

		private static readonly int ASTERISK_ENCODING = CHARACTER_ENCODINGS[47];

		private readonly StringBuilder decodeRowResult;

		private readonly int[] counters;

		public Code93Reader()
		{
			decodeRowResult = new StringBuilder(20);
			counters = new int[6];
		}

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			for (int i = 0; i < counters.Length; i++)
			{
				counters[i] = 0;
			}
			decodeRowResult.Length = 0;
			int[] array = findAsteriskPattern(row);
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
				int num2 = toPattern(counters);
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
			decodeRowResult.Remove(decodeRowResult.Length - 1, 1);
			int num5 = 0;
			int[] array3 = counters;
			foreach (int num6 in array3)
			{
				num5 += num6;
			}
			if (num == size || !row[num])
			{
				return null;
			}
			if (decodeRowResult.Length < 2)
			{
				return null;
			}
			if (!checkChecksums(decodeRowResult))
			{
				return null;
			}
			decodeRowResult.Length -= 2;
			string text = decodeExtended(decodeRowResult);
			if (text == null)
			{
				return null;
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
			}, BarcodeFormat.CODE_93);
		}

		private int[] findAsteriskPattern(BitArray row)
		{
			int size = row.Size;
			int nextSet = row.getNextSet(0);
			for (int i = 0; i < counters.Length; i++)
			{
				counters[i] = 0;
			}
			int num = 0;
			int num2 = nextSet;
			bool flag = false;
			int num3 = counters.Length;
			for (int j = nextSet; j < size; j++)
			{
				if (row[j] ^ flag)
				{
					counters[num]++;
					continue;
				}
				if (num == num3 - 1)
				{
					if (toPattern(counters) == ASTERISK_ENCODING)
					{
						return new int[2] { num2, j };
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

		private static int toPattern(int[] counters)
		{
			int num = counters.Length;
			int num2 = 0;
			foreach (int num3 in counters)
			{
				num2 += num3;
			}
			int num4 = 0;
			for (int j = 0; j < num; j++)
			{
				int num5 = (counters[j] << OneDReader.INTEGER_MATH_SHIFT) * 9 / num2;
				int num6 = num5 >> OneDReader.INTEGER_MATH_SHIFT;
				if ((num5 & 0xFF) > 127)
				{
					num6++;
				}
				if (num6 < 1 || num6 > 4)
				{
					return -1;
				}
				if ((j & 1) == 0)
				{
					for (int k = 0; k < num6; k++)
					{
						num4 = (num4 << 1) | 1;
					}
				}
				else
				{
					num4 <<= num6;
				}
			}
			return num4;
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

		private static string decodeExtended(StringBuilder encoded)
		{
			int length = encoded.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				char c = encoded[i];
				if (c >= 'a' && c <= 'd')
				{
					if (i >= length - 1)
					{
						return null;
					}
					char c2 = encoded[i + 1];
					char value = '\0';
					switch (c)
					{
					case 'd':
						if (c2 >= 'A' && c2 <= 'Z')
						{
							value = (char)(c2 + 32);
							break;
						}
						return null;
					case 'a':
						if (c2 >= 'A' && c2 <= 'Z')
						{
							value = (char)(c2 - 64);
							break;
						}
						return null;
					case 'b':
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
					case 'c':
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

		private static bool checkChecksums(StringBuilder result)
		{
			int length = result.Length;
			if (!checkOneChecksum(result, length - 2, 20))
			{
				return false;
			}
			if (!checkOneChecksum(result, length - 1, 15))
			{
				return false;
			}
			return true;
		}

		private static bool checkOneChecksum(StringBuilder result, int checkPosition, int weightMax)
		{
			int num = 1;
			int num2 = 0;
			for (int num3 = checkPosition - 1; num3 >= 0; num3--)
			{
				num2 += num * "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%abcd*".IndexOf(result[num3]);
				if (++num > weightMax)
				{
					num = 1;
				}
			}
			if (result[checkPosition] != ALPHABET[num2 % 47])
			{
				return false;
			}
			return true;
		}
	}
}
