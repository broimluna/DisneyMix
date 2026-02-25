using System;
using System.Text;

namespace ZXing.Datamatrix.Encoder
{
	internal static class HighLevelEncoder
	{
		public const char PAD = '\u0081';

		public const char LATCH_TO_C40 = 'æ';

		public const char LATCH_TO_BASE256 = 'ç';

		public const char FNC1 = 'è';

		public const char STRUCTURED_APPEND = 'é';

		public const char READER_PROGRAMMING = 'ê';

		public const char UPPER_SHIFT = 'ë';

		public const char MACRO_05 = 'ì';

		public const char MACRO_06 = 'í';

		public const char LATCH_TO_ANSIX12 = 'î';

		public const char LATCH_TO_TEXT = 'ï';

		public const char LATCH_TO_EDIFACT = 'ð';

		public const char ECI = 'ñ';

		public const char C40_UNLATCH = 'þ';

		public const char X12_UNLATCH = 'þ';

		public const string MACRO_05_HEADER = "[)>\u001e05\u001d";

		public const string MACRO_06_HEADER = "[)>\u001e06\u001d";

		public const string MACRO_TRAILER = "\u001e\u0004";

		private static char randomize253State(char ch, int codewordPosition)
		{
			int num = 149 * codewordPosition % 253 + 1;
			int num2 = ch + num;
			return (num2 > 254) ? ((char)(num2 - 254)) : ((char)num2);
		}

		public static string encodeHighLevel(string msg)
		{
			return encodeHighLevel(msg, SymbolShapeHint.FORCE_NONE, null, null, 0);
		}

		public static string encodeHighLevel(string msg, SymbolShapeHint shape, Dimension minSize, Dimension maxSize, int defaultEncodation)
		{
			Encoder[] array = new Encoder[6]
			{
				new ASCIIEncoder(),
				new C40Encoder(),
				new TextEncoder(),
				new X12Encoder(),
				new EdifactEncoder(),
				new Base256Encoder()
			};
			EncoderContext encoderContext = new EncoderContext(msg);
			encoderContext.setSymbolShape(shape);
			encoderContext.setSizeConstraints(minSize, maxSize);
			if (msg.StartsWith("[)>\u001e05\u001d") && msg.EndsWith("\u001e\u0004"))
			{
				encoderContext.writeCodeword('ì');
				encoderContext.setSkipAtEnd(2);
				encoderContext.Pos += "[)>\u001e05\u001d".Length;
			}
			else if (msg.StartsWith("[)>\u001e06\u001d") && msg.EndsWith("\u001e\u0004"))
			{
				encoderContext.writeCodeword('í');
				encoderContext.setSkipAtEnd(2);
				encoderContext.Pos += "[)>\u001e06\u001d".Length;
			}
			int num = defaultEncodation;
			switch (num)
			{
			case 5:
				encoderContext.writeCodeword('ç');
				break;
			case 1:
				encoderContext.writeCodeword('æ');
				break;
			case 3:
				encoderContext.writeCodeword('î');
				break;
			case 2:
				encoderContext.writeCodeword('ï');
				break;
			case 4:
				encoderContext.writeCodeword('ð');
				break;
			default:
				throw new InvalidOperationException("Illegal mode: " + num);
			case 0:
				break;
			}
			while (encoderContext.HasMoreCharacters)
			{
				array[num].encode(encoderContext);
				if (encoderContext.NewEncoding >= 0)
				{
					num = encoderContext.NewEncoding;
					encoderContext.resetEncoderSignal();
				}
			}
			int length = encoderContext.Codewords.Length;
			encoderContext.updateSymbolInfo();
			int dataCapacity = encoderContext.SymbolInfo.dataCapacity;
			if (length < dataCapacity && num != 0 && num != 5)
			{
				encoderContext.writeCodeword('þ');
			}
			StringBuilder codewords = encoderContext.Codewords;
			if (codewords.Length < dataCapacity)
			{
				codewords.Append('\u0081');
			}
			while (codewords.Length < dataCapacity)
			{
				codewords.Append(randomize253State('\u0081', codewords.Length + 1));
			}
			return encoderContext.Codewords.ToString();
		}

		internal static int lookAheadTest(string msg, int startpos, int currentMode)
		{
			if (startpos >= msg.Length)
			{
				return currentMode;
			}
			float[] array;
			if (currentMode == 0)
			{
				array = new float[6] { 0f, 1f, 1f, 1f, 1f, 1.25f };
			}
			else
			{
				array = new float[6] { 1f, 2f, 2f, 2f, 2f, 2.25f };
				array[currentMode] = 0f;
			}
			int num = 0;
			while (true)
			{
				if (startpos + num == msg.Length)
				{
					int min = int.MaxValue;
					byte[] array2 = new byte[6];
					int[] array3 = new int[6];
					min = findMinimums(array, array3, min, array2);
					int minimumCount = getMinimumCount(array2);
					if (array3[0] == min)
					{
						return 0;
					}
					if (minimumCount == 1 && array2[5] > 0)
					{
						return 5;
					}
					if (minimumCount == 1 && array2[4] > 0)
					{
						return 4;
					}
					if (minimumCount == 1 && array2[2] > 0)
					{
						return 2;
					}
					if (minimumCount == 1 && array2[3] > 0)
					{
						return 3;
					}
					return 1;
				}
				char ch = msg[startpos + num];
				num++;
				if (isDigit(ch))
				{
					array[0] += 0.5f;
				}
				else if (isExtendedASCII(ch))
				{
					array[0] = (int)Math.Ceiling(array[0]);
					array[0] += 2f;
				}
				else
				{
					array[0] = (int)Math.Ceiling(array[0]);
					array[0] += 1f;
				}
				if (isNativeC40(ch))
				{
					array[1] += 2f / 3f;
				}
				else if (isExtendedASCII(ch))
				{
					array[1] += 2.6666667f;
				}
				else
				{
					array[1] += 1.3333334f;
				}
				if (isNativeText(ch))
				{
					array[2] += 2f / 3f;
				}
				else if (isExtendedASCII(ch))
				{
					array[2] += 2.6666667f;
				}
				else
				{
					array[2] += 1.3333334f;
				}
				if (isNativeX12(ch))
				{
					array[3] += 2f / 3f;
				}
				else if (isExtendedASCII(ch))
				{
					array[3] += 4.3333335f;
				}
				else
				{
					array[3] += 3.3333333f;
				}
				if (isNativeEDIFACT(ch))
				{
					array[4] += 0.75f;
				}
				else if (isExtendedASCII(ch))
				{
					array[4] += 4.25f;
				}
				else
				{
					array[4] += 3.25f;
				}
				if (isSpecialB256(ch))
				{
					array[5] += 4f;
				}
				else
				{
					array[5] += 1f;
				}
				if (num < 4)
				{
					continue;
				}
				int[] array4 = new int[6];
				byte[] array5 = new byte[6];
				findMinimums(array, array4, int.MaxValue, array5);
				int minimumCount2 = getMinimumCount(array5);
				if (array4[0] < array4[5] && array4[0] < array4[1] && array4[0] < array4[2] && array4[0] < array4[3] && array4[0] < array4[4])
				{
					return 0;
				}
				if (array4[5] < array4[0] || array5[1] + array5[2] + array5[3] + array5[4] == 0)
				{
					return 5;
				}
				if (minimumCount2 == 1 && array5[4] > 0)
				{
					return 4;
				}
				if (minimumCount2 == 1 && array5[2] > 0)
				{
					return 2;
				}
				if (minimumCount2 == 1 && array5[3] > 0)
				{
					return 3;
				}
				if (array4[1] + 1 < array4[0] && array4[1] + 1 < array4[5] && array4[1] + 1 < array4[4] && array4[1] + 1 < array4[2])
				{
					if (array4[1] < array4[3])
					{
						return 1;
					}
					if (array4[1] == array4[3])
					{
						break;
					}
				}
			}
			for (int i = startpos + num + 1; i < msg.Length; i++)
			{
				char ch2 = msg[i];
				if (isX12TermSep(ch2))
				{
					return 3;
				}
				if (!isNativeX12(ch2))
				{
					break;
				}
			}
			return 1;
		}

		private static int findMinimums(float[] charCounts, int[] intCharCounts, int min, byte[] mins)
		{
			SupportClass.Fill(mins, (byte)0);
			for (int i = 0; i < 6; i++)
			{
				intCharCounts[i] = (int)Math.Ceiling(charCounts[i]);
				int num = intCharCounts[i];
				if (min > num)
				{
					min = num;
					SupportClass.Fill(mins, (byte)0);
				}
				if (min == num)
				{
					mins[i]++;
				}
			}
			return min;
		}

		private static int getMinimumCount(byte[] mins)
		{
			int num = 0;
			for (int i = 0; i < 6; i++)
			{
				num += mins[i];
			}
			return num;
		}

		internal static bool isDigit(char ch)
		{
			return ch >= '0' && ch <= '9';
		}

		internal static bool isExtendedASCII(char ch)
		{
			return ch >= '\u0080' && ch <= 'ÿ';
		}

		internal static bool isNativeC40(char ch)
		{
			int result;
			switch (ch)
			{
			default:
				result = ((ch >= 'A' && ch <= 'Z') ? 1 : 0);
				break;
			case ' ':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				result = 1;
				break;
			}
			return (byte)result != 0;
		}

		internal static bool isNativeText(char ch)
		{
			int result;
			switch (ch)
			{
			default:
				result = ((ch >= 'a' && ch <= 'z') ? 1 : 0);
				break;
			case ' ':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				result = 1;
				break;
			}
			return (byte)result != 0;
		}

		internal static bool isNativeX12(char ch)
		{
			return isX12TermSep(ch) || ch == ' ' || (ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z');
		}

		internal static bool isX12TermSep(char ch)
		{
			return ch == '\r' || ch == '*' || ch == '>';
		}

		internal static bool isNativeEDIFACT(char ch)
		{
			return ch >= ' ' && ch <= '^';
		}

		internal static bool isSpecialB256(char ch)
		{
			return false;
		}

		public static int determineConsecutiveDigitCount(string msg, int startpos)
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

		internal static void illegalCharacter(char c)
		{
			throw new ArgumentException(string.Format("Illegal character: {0} (0x{1:X})", c, (int)c));
		}
	}
}
