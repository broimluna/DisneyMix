using System;
using System.Text;
using BigIntegerLibrary;
using ZXing.Common;

namespace ZXing.PDF417.Internal
{
	internal static class DecodedBitStreamParser
	{
		private enum Mode
		{
			ALPHA = 0,
			LOWER = 1,
			MIXED = 2,
			PUNCT = 3,
			ALPHA_SHIFT = 4,
			PUNCT_SHIFT = 5
		}

		private const int TEXT_COMPACTION_MODE_LATCH = 900;

		private const int BYTE_COMPACTION_MODE_LATCH = 901;

		private const int NUMERIC_COMPACTION_MODE_LATCH = 902;

		private const int BYTE_COMPACTION_MODE_LATCH_6 = 924;

		private const int BEGIN_MACRO_PDF417_CONTROL_BLOCK = 928;

		private const int BEGIN_MACRO_PDF417_OPTIONAL_FIELD = 923;

		private const int MACRO_PDF417_TERMINATOR = 922;

		private const int MODE_SHIFT_TO_BYTE_COMPACTION_MODE = 913;

		private const int MAX_NUMERIC_CODEWORDS = 15;

		private const int PL = 25;

		private const int LL = 27;

		private const int AS = 27;

		private const int ML = 28;

		private const int AL = 28;

		private const int PS = 29;

		private const int PAL = 29;

		private const int NUMBER_OF_SEQUENCE_CODEWORDS = 2;

		private static readonly char[] PUNCT_CHARS;

		private static readonly char[] MIXED_CHARS;

		private static readonly BigInteger[] EXP900;

		static DecodedBitStreamParser()
		{
			PUNCT_CHARS = new char[29]
			{
				';', '<', '>', '@', '[', '\\', '}', '_', '`', '~',
				'!', '\r', '\t', ',', ':', '\n', '-', '.', '$', '/',
				'"', '|', '*', '(', ')', '?', '{', '}', '\''
			};
			MIXED_CHARS = new char[25]
			{
				'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
				'&', '\r', '\t', ',', ':', '#', '-', '.', '$', '/',
				'+', '%', '*', '=', '^'
			};
			EXP900 = new BigInteger[16];
			EXP900[0] = BigInteger.One;
			BigInteger bigInteger = new BigInteger(900L);
			EXP900[1] = bigInteger;
			for (int i = 2; i < EXP900.Length; i++)
			{
				EXP900[i] = BigInteger.Multiplication(EXP900[i - 1], bigInteger);
			}
		}

		internal static DecoderResult decode(int[] codewords, string ecLevel)
		{
			StringBuilder stringBuilder = new StringBuilder(codewords.Length * 2);
			int num = 1;
			int num2 = codewords[num++];
			PDF417ResultMetadata pDF417ResultMetadata = new PDF417ResultMetadata();
			while (num < codewords[0])
			{
				switch (num2)
				{
				case 900:
					num = textCompaction(codewords, num, stringBuilder);
					break;
				case 901:
				case 913:
				case 924:
					num = byteCompaction(num2, codewords, num, stringBuilder);
					break;
				case 902:
					num = numericCompaction(codewords, num, stringBuilder);
					break;
				case 928:
					num = decodeMacroBlock(codewords, num, pDF417ResultMetadata);
					break;
				case 922:
				case 923:
					return null;
				default:
					num--;
					num = textCompaction(codewords, num, stringBuilder);
					break;
				}
				if (num < 0)
				{
					return null;
				}
				if (num < codewords.Length)
				{
					num2 = codewords[num++];
					continue;
				}
				return null;
			}
			if (stringBuilder.Length == 0)
			{
				return null;
			}
			DecoderResult decoderResult = new DecoderResult(null, stringBuilder.ToString(), null, ecLevel);
			decoderResult.Other = pDF417ResultMetadata;
			return decoderResult;
		}

		private static int decodeMacroBlock(int[] codewords, int codeIndex, PDF417ResultMetadata resultMetadata)
		{
			if (codeIndex + 2 > codewords[0])
			{
				return -1;
			}
			int[] array = new int[2];
			int num = 0;
			while (num < 2)
			{
				array[num] = codewords[codeIndex];
				num++;
				codeIndex++;
			}
			string text = decodeBase900toBase10(array, 2);
			if (text == null)
			{
				return -1;
			}
			resultMetadata.SegmentIndex = int.Parse(text);
			StringBuilder stringBuilder = new StringBuilder();
			codeIndex = textCompaction(codewords, codeIndex, stringBuilder);
			resultMetadata.FileId = stringBuilder.ToString();
			if (codewords[codeIndex] == 923)
			{
				codeIndex++;
				int[] array2 = new int[codewords[0] - codeIndex];
				int num2 = 0;
				bool flag = false;
				while (codeIndex < codewords[0] && !flag)
				{
					int num3 = codewords[codeIndex++];
					if (num3 < 900)
					{
						array2[num2++] = num3;
						continue;
					}
					int num4 = num3;
					if (num4 == 922)
					{
						resultMetadata.IsLastSegment = true;
						codeIndex++;
						flag = true;
						continue;
					}
					return -1;
				}
				resultMetadata.OptionalData = new int[num2];
				Array.Copy(array2, resultMetadata.OptionalData, num2);
			}
			else if (codewords[codeIndex] == 922)
			{
				resultMetadata.IsLastSegment = true;
				codeIndex++;
			}
			return codeIndex;
		}

		private static int textCompaction(int[] codewords, int codeIndex, StringBuilder result)
		{
			int[] array = new int[codewords[0] - codeIndex << 1];
			int[] array2 = new int[codewords[0] - codeIndex << 1];
			int num = 0;
			bool flag = false;
			while (codeIndex < codewords[0] && !flag)
			{
				int num2 = codewords[codeIndex++];
				if (num2 < 900)
				{
					array[num] = num2 / 30;
					array[num + 1] = num2 % 30;
					num += 2;
					continue;
				}
				switch (num2)
				{
				case 900:
					array[num++] = 900;
					break;
				case 901:
				case 902:
				case 922:
				case 923:
				case 924:
				case 928:
					codeIndex--;
					flag = true;
					break;
				case 913:
					array[num] = 913;
					num2 = codewords[codeIndex++];
					array2[num] = num2;
					num++;
					break;
				}
			}
			decodeTextCompaction(array, array2, num, result);
			return codeIndex;
		}

		private static void decodeTextCompaction(int[] textCompactionData, int[] byteCompactionData, int length, StringBuilder result)
		{
			Mode mode = Mode.ALPHA;
			Mode mode2 = Mode.ALPHA;
			for (int i = 0; i < length; i++)
			{
				int num = textCompactionData[i];
				char? c = null;
				switch (mode)
				{
				case Mode.ALPHA:
					if (num < 26)
					{
						c = (char)(65 + num);
						break;
					}
					switch (num)
					{
					case 26:
						c = ' ';
						break;
					case 27:
						mode = Mode.LOWER;
						break;
					case 28:
						mode = Mode.MIXED;
						break;
					case 29:
						mode2 = mode;
						mode = Mode.PUNCT_SHIFT;
						break;
					case 913:
						result.Append((char)byteCompactionData[i]);
						break;
					case 900:
						mode = Mode.ALPHA;
						break;
					}
					break;
				case Mode.LOWER:
					if (num < 26)
					{
						c = (char)(97 + num);
						break;
					}
					switch (num)
					{
					case 26:
						c = ' ';
						break;
					case 27:
						mode2 = mode;
						mode = Mode.ALPHA_SHIFT;
						break;
					case 28:
						mode = Mode.MIXED;
						break;
					case 29:
						mode2 = mode;
						mode = Mode.PUNCT_SHIFT;
						break;
					case 913:
						result.Append((char)byteCompactionData[i]);
						break;
					case 900:
						mode = Mode.ALPHA;
						break;
					}
					break;
				case Mode.MIXED:
					if (num < 25)
					{
						c = MIXED_CHARS[num];
						break;
					}
					switch (num)
					{
					case 25:
						mode = Mode.PUNCT;
						break;
					case 26:
						c = ' ';
						break;
					case 27:
						mode = Mode.LOWER;
						break;
					case 28:
						mode = Mode.ALPHA;
						break;
					case 29:
						mode2 = mode;
						mode = Mode.PUNCT_SHIFT;
						break;
					case 913:
						result.Append((char)byteCompactionData[i]);
						break;
					case 900:
						mode = Mode.ALPHA;
						break;
					}
					break;
				case Mode.PUNCT:
					if (num < 29)
					{
						c = PUNCT_CHARS[num];
						break;
					}
					switch (num)
					{
					case 29:
						mode = Mode.ALPHA;
						break;
					case 913:
						result.Append((char)byteCompactionData[i]);
						break;
					case 900:
						mode = Mode.ALPHA;
						break;
					}
					break;
				case Mode.ALPHA_SHIFT:
					mode = mode2;
					if (num < 26)
					{
						c = (char)(65 + num);
						break;
					}
					switch (num)
					{
					case 26:
						c = ' ';
						break;
					case 900:
						mode = Mode.ALPHA;
						break;
					}
					break;
				case Mode.PUNCT_SHIFT:
					mode = mode2;
					if (num < 29)
					{
						c = PUNCT_CHARS[num];
						break;
					}
					switch (num)
					{
					case 29:
						mode = Mode.ALPHA;
						break;
					case 913:
						result.Append((char)byteCompactionData[i]);
						break;
					case 900:
						mode = Mode.ALPHA;
						break;
					}
					break;
				}
				if (c.HasValue)
				{
					result.Append(c.Value);
				}
			}
		}

		private static int byteCompaction(int mode, int[] codewords, int codeIndex, StringBuilder result)
		{
			switch (mode)
			{
			case 901:
			{
				int num4 = 0;
				long num5 = 0L;
				char[] array2 = new char[6];
				int[] array3 = new int[6];
				bool flag2 = false;
				int num6 = codewords[codeIndex++];
				while (codeIndex < codewords[0] && !flag2)
				{
					array3[num4++] = num6;
					num5 = 900 * num5 + num6;
					num6 = codewords[codeIndex++];
					if (num6 == 900 || num6 == 901 || num6 == 902 || num6 == 924 || num6 == 928 || num6 == 923 || num6 == 922)
					{
						codeIndex--;
						flag2 = true;
					}
					else if (num4 % 5 == 0 && num4 > 0)
					{
						for (int j = 0; j < 6; j++)
						{
							array2[5 - j] = (char)(num5 % 256);
							num5 >>= 8;
						}
						result.Append(array2);
						num4 = 0;
					}
				}
				if (codeIndex == codewords[0] && num6 < 900)
				{
					array3[num4++] = num6;
				}
				for (int k = 0; k < num4; k++)
				{
					result.Append((char)array3[k]);
				}
				break;
			}
			case 924:
			{
				int num = 0;
				long num2 = 0L;
				bool flag = false;
				while (codeIndex < codewords[0] && !flag)
				{
					int num3 = codewords[codeIndex++];
					if (num3 < 900)
					{
						num++;
						num2 = 900 * num2 + num3;
					}
					else if (num3 == 900 || num3 == 901 || num3 == 902 || num3 == 924 || num3 == 928 || num3 == 923 || num3 == 922)
					{
						codeIndex--;
						flag = true;
					}
					if (num % 5 == 0 && num > 0)
					{
						char[] array = new char[6];
						for (int i = 0; i < 6; i++)
						{
							array[5 - i] = (char)(num2 & 0xFF);
							num2 >>= 8;
						}
						result.Append(array);
						num = 0;
					}
				}
				break;
			}
			}
			return codeIndex;
		}

		private static int numericCompaction(int[] codewords, int codeIndex, StringBuilder result)
		{
			int num = 0;
			bool flag = false;
			int[] array = new int[15];
			while (codeIndex < codewords[0] && !flag)
			{
				int num2 = codewords[codeIndex++];
				if (codeIndex == codewords[0])
				{
					flag = true;
				}
				if (num2 < 900)
				{
					array[num] = num2;
					num++;
				}
				else if (num2 == 900 || num2 == 901 || num2 == 924 || num2 == 928 || num2 == 923 || num2 == 922)
				{
					codeIndex--;
					flag = true;
				}
				if (num % 15 == 0 || num2 == 902 || flag)
				{
					string text = decodeBase900toBase10(array, num);
					if (text == null)
					{
						return -1;
					}
					result.Append(text);
					num = 0;
				}
			}
			return codeIndex;
		}

		private static string decodeBase900toBase10(int[] codewords, int count)
		{
			BigInteger bigInteger = BigInteger.Zero;
			for (int i = 0; i < count; i++)
			{
				bigInteger = BigInteger.Addition(bigInteger, BigInteger.Multiplication(EXP900[count - i - 1], new BigInteger(codewords[i])));
			}
			string text = bigInteger.ToString();
			if (text[0] != '1')
			{
				return null;
			}
			return text.Substring(1);
		}
	}
}
