using System;
using System.Text;
using BigIntegerLibrary;
using ZXing.Common;

namespace ZXing.PDF417.Internal
{
	internal static class PDF417HighLevelEncoder
	{
		private const int TEXT_COMPACTION = 0;

		private const int BYTE_COMPACTION = 1;

		private const int NUMERIC_COMPACTION = 2;

		private const int SUBMODE_ALPHA = 0;

		private const int SUBMODE_LOWER = 1;

		private const int SUBMODE_MIXED = 2;

		private const int SUBMODE_PUNCTUATION = 3;

		private const int LATCH_TO_TEXT = 900;

		private const int LATCH_TO_BYTE_PADDED = 901;

		private const int LATCH_TO_NUMERIC = 902;

		private const int SHIFT_TO_BYTE = 913;

		private const int LATCH_TO_BYTE = 924;

		private const int ECI_USER_DEFINED = 925;

		private const int ECI_GENERAL_PURPOSE = 926;

		private const int ECI_CHARSET = 927;

		private static readonly sbyte[] TEXT_MIXED_RAW;

		private static readonly sbyte[] TEXT_PUNCTUATION_RAW;

		private static readonly sbyte[] MIXED;

		private static readonly sbyte[] PUNCTUATION;

		internal static Encoding DEFAULT_ENCODING;

		static PDF417HighLevelEncoder()
		{
			TEXT_MIXED_RAW = new sbyte[30]
			{
				48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
				38, 13, 9, 44, 58, 35, 45, 46, 36, 47,
				43, 37, 42, 61, 94, 0, 32, 0, 0, 0
			};
			TEXT_PUNCTUATION_RAW = new sbyte[30]
			{
				59, 60, 62, 64, 91, 92, 93, 95, 96, 126,
				33, 13, 9, 44, 58, 10, 45, 46, 36, 47,
				34, 124, 42, 40, 41, 63, 123, 125, 39, 0
			};
			MIXED = new sbyte[128];
			PUNCTUATION = new sbyte[128];
			for (int i = 0; i < MIXED.Length; i++)
			{
				MIXED[i] = -1;
			}
			for (sbyte b = 0; b < TEXT_MIXED_RAW.Length; b++)
			{
				sbyte b2 = TEXT_MIXED_RAW[b];
				if (b2 > 0)
				{
					MIXED[b2] = b;
				}
			}
			for (int j = 0; j < PUNCTUATION.Length; j++)
			{
				PUNCTUATION[j] = -1;
			}
			for (sbyte b3 = 0; b3 < TEXT_PUNCTUATION_RAW.Length; b3++)
			{
				sbyte b4 = TEXT_PUNCTUATION_RAW[b3];
				if (b4 > 0)
				{
					PUNCTUATION[b4] = b3;
				}
			}
			DEFAULT_ENCODING = Encoding.GetEncoding("CP437");
		}

		internal static string encodeHighLevel(string msg, Compaction compaction, Encoding encoding, bool disableEci)
		{
			StringBuilder stringBuilder = new StringBuilder(msg.Length);
			if (!DEFAULT_ENCODING.Equals(encoding) && !disableEci)
			{
				CharacterSetECI characterSetECIByName = CharacterSetECI.getCharacterSetECIByName(encoding.WebName);
				if (characterSetECIByName != null)
				{
					encodingECI(characterSetECIByName.Value, stringBuilder);
				}
			}
			int length = msg.Length;
			int num = 0;
			int initialSubmode = 0;
			byte[] array = null;
			switch (compaction)
			{
			case Compaction.TEXT:
				encodeText(msg, num, length, stringBuilder, initialSubmode);
				break;
			case Compaction.BYTE:
				array = encoding.GetBytes(msg);
				encodeBinary(array, num, array.Length, 1, stringBuilder);
				break;
			case Compaction.NUMERIC:
				stringBuilder.Append('Ά');
				encodeNumeric(msg, num, length, stringBuilder);
				break;
			default:
			{
				int num2 = 0;
				while (num < length)
				{
					int num3 = determineConsecutiveDigitCount(msg, num);
					if (num3 >= 13)
					{
						stringBuilder.Append('Ά');
						num2 = 2;
						initialSubmode = 0;
						encodeNumeric(msg, num, num3, stringBuilder);
						num += num3;
						continue;
					}
					int num4 = determineConsecutiveTextCount(msg, num);
					if (num4 >= 5 || num3 == length)
					{
						if (num2 != 0)
						{
							stringBuilder.Append('\u0384');
							num2 = 0;
							initialSubmode = 0;
						}
						initialSubmode = encodeText(msg, num, num4, stringBuilder, initialSubmode);
						num += num4;
						continue;
					}
					if (array == null)
					{
						array = encoding.GetBytes(msg);
					}
					int num5 = determineConsecutiveBinaryCount(msg, array, num);
					if (num5 == 0)
					{
						num5 = 1;
					}
					if (num5 == 1 && num2 == 0)
					{
						encodeBinary(array, num, 1, 0, stringBuilder);
					}
					else
					{
						encodeBinary(array, num, num5, num2, stringBuilder);
						num2 = 1;
						initialSubmode = 0;
					}
					num += num5;
				}
				break;
			}
			}
			return stringBuilder.ToString();
		}

		private static int encodeText(string msg, int startpos, int count, StringBuilder sb, int initialSubmode)
		{
			StringBuilder stringBuilder = new StringBuilder(count);
			int num = initialSubmode;
			int num2 = 0;
			do
			{
				IL_000c:
				char c = msg[startpos + num2];
				switch (num)
				{
				case 0:
					if (isAlphaUpper(c))
					{
						if (c == ' ')
						{
							stringBuilder.Append('\u001a');
						}
						else
						{
							stringBuilder.Append((char)(c - 65));
						}
						break;
					}
					if (isAlphaLower(c))
					{
						num = 1;
						stringBuilder.Append('\u001b');
						goto IL_000c;
					}
					if (isMixed(c))
					{
						num = 2;
						stringBuilder.Append('\u001c');
						goto IL_000c;
					}
					stringBuilder.Append('\u001d');
					stringBuilder.Append((char)PUNCTUATION[(uint)c]);
					break;
				case 1:
					if (isAlphaLower(c))
					{
						if (c == ' ')
						{
							stringBuilder.Append('\u001a');
						}
						else
						{
							stringBuilder.Append((char)(c - 97));
						}
						break;
					}
					if (isAlphaUpper(c))
					{
						stringBuilder.Append('\u001b');
						stringBuilder.Append((char)(c - 65));
						break;
					}
					if (isMixed(c))
					{
						num = 2;
						stringBuilder.Append('\u001c');
						goto IL_000c;
					}
					stringBuilder.Append('\u001d');
					stringBuilder.Append((char)PUNCTUATION[(uint)c]);
					break;
				case 2:
					if (isMixed(c))
					{
						stringBuilder.Append((char)MIXED[(uint)c]);
						break;
					}
					if (isAlphaUpper(c))
					{
						num = 0;
						stringBuilder.Append('\u001c');
						goto IL_000c;
					}
					if (isAlphaLower(c))
					{
						num = 1;
						stringBuilder.Append('\u001b');
						goto IL_000c;
					}
					if (startpos + num2 + 1 < count)
					{
						char ch = msg[startpos + num2 + 1];
						if (isPunctuation(ch))
						{
							num = 3;
							stringBuilder.Append('\u0019');
							goto IL_000c;
						}
					}
					stringBuilder.Append('\u001d');
					stringBuilder.Append((char)PUNCTUATION[(uint)c]);
					break;
				default:
					if (isPunctuation(c))
					{
						stringBuilder.Append((char)PUNCTUATION[(uint)c]);
						break;
					}
					num = 0;
					stringBuilder.Append('\u001d');
					goto IL_000c;
				}
				num2++;
			}
			while (num2 < count);
			char c2 = '\0';
			int length = stringBuilder.Length;
			for (int i = 0; i < length; i++)
			{
				if (i % 2 != 0)
				{
					c2 = (char)(c2 * 30 + stringBuilder[i]);
					sb.Append(c2);
				}
				else
				{
					c2 = stringBuilder[i];
				}
			}
			if (length % 2 != 0)
			{
				sb.Append((char)(c2 * 30 + 29));
			}
			return num;
		}

		private static void encodeBinary(byte[] bytes, int startpos, int count, int startmode, StringBuilder sb)
		{
			if (count == 1 && startmode == 0)
			{
				sb.Append('Α');
			}
			else if (count % 6 == 0)
			{
				sb.Append('Μ');
			}
			else
			{
				sb.Append('\u0385');
			}
			int i = startpos;
			if (count >= 6)
			{
				char[] array = new char[5];
				for (; startpos + count - i >= 6; i += 6)
				{
					long num = 0L;
					for (int j = 0; j < 6; j++)
					{
						num <<= 8;
						num += bytes[i + j] & 0xFF;
					}
					for (int k = 0; k < 5; k++)
					{
						array[k] = (char)(num % 900);
						num /= 900;
					}
					for (int num2 = array.Length - 1; num2 >= 0; num2--)
					{
						sb.Append(array[num2]);
					}
				}
			}
			for (int l = i; l < startpos + count; l++)
			{
				int num3 = bytes[l] & 0xFF;
				sb.Append((char)num3);
			}
		}

		private static void encodeNumeric(string msg, int startpos, int count, StringBuilder sb)
		{
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder(count / 3 + 1);
			BigInteger b = new BigInteger(900L);
			BigInteger other = new BigInteger(0L);
			int num;
			for (; i < count - 1; i += num)
			{
				stringBuilder.Length = 0;
				num = Math.Min(44, count - i);
				string str = '1' + msg.Substring(startpos + i, num);
				BigInteger bigInteger = BigInteger.Parse(str);
				do
				{
					BigInteger bigInteger2 = BigInteger.Modulo(bigInteger, b);
					stringBuilder.Append((char)bigInteger2.GetHashCode());
					bigInteger = BigInteger.Division(bigInteger, b);
				}
				while (!bigInteger.Equals(other));
				for (int num2 = stringBuilder.Length - 1; num2 >= 0; num2--)
				{
					sb.Append(stringBuilder[num2]);
				}
			}
		}

		private static bool isDigit(char ch)
		{
			return ch >= '0' && ch <= '9';
		}

		private static bool isAlphaUpper(char ch)
		{
			return ch == ' ' || (ch >= 'A' && ch <= 'Z');
		}

		private static bool isAlphaLower(char ch)
		{
			return ch == ' ' || (ch >= 'a' && ch <= 'z');
		}

		private static bool isMixed(char ch)
		{
			return MIXED[(uint)ch] != -1;
		}

		private static bool isPunctuation(char ch)
		{
			return PUNCTUATION[(uint)ch] != -1;
		}

		private static bool isText(char ch)
		{
			return ch == '\t' || ch == '\n' || ch == '\r' || (ch >= ' ' && ch <= '~');
		}

		private static int determineConsecutiveDigitCount(string msg, int startpos)
		{
			int num = 0;
			int length = msg.Length;
			int num2 = startpos;
			if (num2 < length)
			{
				char ch = msg[num2];
				while (isDigit(ch) && num2 < length)
				{
					num++;
					num2++;
					if (num2 < length)
					{
						ch = msg[num2];
					}
				}
			}
			return num;
		}

		private static int determineConsecutiveTextCount(string msg, int startpos)
		{
			int length = msg.Length;
			int num = startpos;
			while (num < length)
			{
				char ch = msg[num];
				int num2 = 0;
				while (num2 < 13 && isDigit(ch) && num < length)
				{
					num2++;
					num++;
					if (num < length)
					{
						ch = msg[num];
					}
				}
				if (num2 >= 13)
				{
					return num - startpos - num2;
				}
				if (num2 <= 0)
				{
					ch = msg[num];
					if (!isText(ch))
					{
						break;
					}
					num++;
				}
			}
			return num - startpos;
		}

		private static int determineConsecutiveBinaryCount(string msg, byte[] bytes, int startpos)
		{
			int length = msg.Length;
			int i;
			for (i = startpos; i < length; i++)
			{
				char ch = msg[i];
				int num = 0;
				while (num < 13 && isDigit(ch))
				{
					num++;
					int num2 = i + num;
					if (num2 >= length)
					{
						break;
					}
					ch = msg[num2];
				}
				if (num >= 13)
				{
					return i - startpos;
				}
				int num3 = 0;
				while (num3 < 5 && isText(ch))
				{
					num3++;
					int num4 = i + num3;
					if (num4 >= length)
					{
						break;
					}
					ch = msg[num4];
				}
				if (num3 >= 5)
				{
					return i - startpos;
				}
				ch = msg[i];
				if (bytes[i] == 63 && ch != '?')
				{
					throw new WriterException("Non-encodable character detected: " + ch + " (Unicode: " + (int)ch + ')');
				}
			}
			return i - startpos;
		}

		private static void encodingECI(int eci, StringBuilder sb)
		{
			if (eci >= 0 && eci < 900)
			{
				sb.Append('Ο');
				sb.Append((char)eci);
				return;
			}
			if (eci < 810900)
			{
				sb.Append('Ξ');
				sb.Append((char)(eci / 900 - 1));
				sb.Append((char)(eci % 900));
				return;
			}
			if (eci < 811800)
			{
				sb.Append('Ν');
				sb.Append((char)(810900 - eci));
				return;
			}
			throw new WriterException("ECI number not in valid range from 0..811799, but was " + eci);
		}
	}
}
