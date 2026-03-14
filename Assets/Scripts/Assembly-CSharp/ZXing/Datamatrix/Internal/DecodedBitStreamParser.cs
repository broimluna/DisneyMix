using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.Datamatrix.Internal
{
	internal static class DecodedBitStreamParser
	{
		private enum Mode
		{
			PAD_ENCODE = 0,
			ASCII_ENCODE = 1,
			C40_ENCODE = 2,
			TEXT_ENCODE = 3,
			ANSIX12_ENCODE = 4,
			EDIFACT_ENCODE = 5,
			BASE256_ENCODE = 6
		}

		private static readonly char[] C40_BASIC_SET_CHARS = new char[40]
		{
			'*', '*', '*', ' ', '0', '1', '2', '3', '4', '5',
			'6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F',
			'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
			'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
		};

		private static readonly char[] C40_SHIFT2_SET_CHARS = new char[27]
		{
			'!', '"', '#', '$', '%', '&', '\'', '(', ')', '*',
			'+', ',', '-', '.', '/', ':', ';', '<', '=', '>',
			'?', '@', '[', '\\', ']', '^', '_'
		};

		private static readonly char[] TEXT_BASIC_SET_CHARS = new char[40]
		{
			'*', '*', '*', ' ', '0', '1', '2', '3', '4', '5',
			'6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
			'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
			'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
		};

		private static readonly char[] TEXT_SHIFT3_SET_CHARS = new char[32]
		{
			'`', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
			'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
			'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '{', '|', '}',
			'~', '\u007f'
		};

		internal static DecoderResult decode(byte[] bytes)
		{
			BitSource bitSource = new BitSource(bytes);
			StringBuilder stringBuilder = new StringBuilder(100);
			StringBuilder stringBuilder2 = new StringBuilder(0);
			List<byte[]> list = new List<byte[]>(1);
			Mode mode = Mode.ASCII_ENCODE;
			do
			{
				switch (mode)
				{
				case Mode.ASCII_ENCODE:
					if (!decodeAsciiSegment(bitSource, stringBuilder, stringBuilder2, out mode))
					{
						return null;
					}
					break;
				case Mode.C40_ENCODE:
					if (!decodeC40Segment(bitSource, stringBuilder))
					{
						return null;
					}
					goto IL_00c6;
				case Mode.TEXT_ENCODE:
					if (!decodeTextSegment(bitSource, stringBuilder))
					{
						return null;
					}
					goto IL_00c6;
				case Mode.ANSIX12_ENCODE:
					if (!decodeAnsiX12Segment(bitSource, stringBuilder))
					{
						return null;
					}
					goto IL_00c6;
				case Mode.EDIFACT_ENCODE:
					if (!decodeEdifactSegment(bitSource, stringBuilder))
					{
						return null;
					}
					goto IL_00c6;
				case Mode.BASE256_ENCODE:
					if (!decodeBase256Segment(bitSource, stringBuilder, list))
					{
						return null;
					}
					goto IL_00c6;
				default:
					{
						return null;
					}
					IL_00c6:
					mode = Mode.ASCII_ENCODE;
					break;
				}
			}
			while (mode != Mode.PAD_ENCODE && bitSource.available() > 0);
			if (stringBuilder2.Length > 0)
			{
				stringBuilder.Append(stringBuilder2.ToString());
			}
			return new DecoderResult(bytes, stringBuilder.ToString(), (list.Count != 0) ? list : null, null);
		}

		private static bool decodeAsciiSegment(BitSource bits, StringBuilder result, StringBuilder resultTrailer, out Mode mode)
		{
			bool flag = false;
			mode = Mode.ASCII_ENCODE;
			do
			{
				int num = bits.readBits(8);
				if (num == 0)
				{
					return false;
				}
				if (num <= 128)
				{
					if (flag)
					{
						num += 128;
					}
					result.Append((char)(num - 1));
					mode = Mode.ASCII_ENCODE;
					return true;
				}
				if (num == 129)
				{
					mode = Mode.PAD_ENCODE;
					return true;
				}
				if (num <= 229)
				{
					int num2 = num - 130;
					if (num2 < 10)
					{
						result.Append('0');
					}
					result.Append(num2);
					continue;
				}
				if (num == 230)
				{
					mode = Mode.C40_ENCODE;
					return true;
				}
				if (num == 231)
				{
					mode = Mode.BASE256_ENCODE;
					return true;
				}
				if (num == 232)
				{
					result.Append('\u001d');
				}
				else
				{
					if (num == 233 || num == 234)
					{
						continue;
					}
					if (num == 235)
					{
						flag = true;
						continue;
					}
					if (num == 236)
					{
						result.Append("[)>\u001e05\u001d");
						resultTrailer.Insert(0, "\u001e\u0004");
						continue;
					}
					if (num == 237)
					{
						result.Append("[)>\u001e06\u001d");
						resultTrailer.Insert(0, "\u001e\u0004");
						continue;
					}
					if (num == 238)
					{
						mode = Mode.ANSIX12_ENCODE;
						return true;
					}
					if (num == 239)
					{
						mode = Mode.TEXT_ENCODE;
						return true;
					}
					if (num == 240)
					{
						mode = Mode.EDIFACT_ENCODE;
						return true;
					}
					if (num != 241 && num >= 242 && (num != 254 || bits.available() != 0))
					{
						return false;
					}
				}
			}
			while (bits.available() > 0);
			mode = Mode.ASCII_ENCODE;
			return true;
		}

		private static bool decodeC40Segment(BitSource bits, StringBuilder result)
		{
			bool flag = false;
			int[] array = new int[3];
			int num = 0;
			do
			{
				if (bits.available() == 8)
				{
					return true;
				}
				int num2 = bits.readBits(8);
				if (num2 == 254)
				{
					return true;
				}
				parseTwoBytes(num2, bits.readBits(8), array);
				for (int i = 0; i < 3; i++)
				{
					int num3 = array[i];
					switch (num)
					{
					case 0:
						if (num3 < 3)
						{
							num = num3 + 1;
							break;
						}
						if (num3 < C40_BASIC_SET_CHARS.Length)
						{
							char c = C40_BASIC_SET_CHARS[num3];
							if (flag)
							{
								result.Append((char)(c + 128));
								flag = false;
							}
							else
							{
								result.Append(c);
							}
							break;
						}
						return false;
					case 1:
						if (flag)
						{
							result.Append((char)(num3 + 128));
							flag = false;
						}
						else
						{
							result.Append((char)num3);
						}
						num = 0;
						break;
					case 2:
						if (num3 < C40_SHIFT2_SET_CHARS.Length)
						{
							char c2 = C40_SHIFT2_SET_CHARS[num3];
							if (flag)
							{
								result.Append((char)(c2 + 128));
								flag = false;
							}
							else
							{
								result.Append(c2);
							}
						}
						else
						{
							switch (num3)
							{
							case 27:
								result.Append('\u001d');
								break;
							case 30:
								flag = true;
								break;
							default:
								return false;
							}
						}
						num = 0;
						break;
					case 3:
						if (flag)
						{
							result.Append((char)(num3 + 224));
							flag = false;
						}
						else
						{
							result.Append((char)(num3 + 96));
						}
						num = 0;
						break;
					default:
						return false;
					}
				}
			}
			while (bits.available() > 0);
			return true;
		}

		private static bool decodeTextSegment(BitSource bits, StringBuilder result)
		{
			bool flag = false;
			int[] array = new int[3];
			int num = 0;
			do
			{
				if (bits.available() == 8)
				{
					return true;
				}
				int num2 = bits.readBits(8);
				if (num2 == 254)
				{
					return true;
				}
				parseTwoBytes(num2, bits.readBits(8), array);
				for (int i = 0; i < 3; i++)
				{
					int num3 = array[i];
					switch (num)
					{
					case 0:
						if (num3 < 3)
						{
							num = num3 + 1;
							break;
						}
						if (num3 < TEXT_BASIC_SET_CHARS.Length)
						{
							char c3 = TEXT_BASIC_SET_CHARS[num3];
							if (flag)
							{
								result.Append((char)(c3 + 128));
								flag = false;
							}
							else
							{
								result.Append(c3);
							}
							break;
						}
						return false;
					case 1:
						if (flag)
						{
							result.Append((char)(num3 + 128));
							flag = false;
						}
						else
						{
							result.Append((char)num3);
						}
						num = 0;
						break;
					case 2:
						if (num3 < C40_SHIFT2_SET_CHARS.Length)
						{
							char c2 = C40_SHIFT2_SET_CHARS[num3];
							if (flag)
							{
								result.Append((char)(c2 + 128));
								flag = false;
							}
							else
							{
								result.Append(c2);
							}
						}
						else
						{
							switch (num3)
							{
							case 27:
								result.Append('\u001d');
								break;
							case 30:
								flag = true;
								break;
							default:
								return false;
							}
						}
						num = 0;
						break;
					case 3:
						if (num3 < TEXT_SHIFT3_SET_CHARS.Length)
						{
							char c = TEXT_SHIFT3_SET_CHARS[num3];
							if (flag)
							{
								result.Append((char)(c + 128));
								flag = false;
							}
							else
							{
								result.Append(c);
							}
							num = 0;
							break;
						}
						return false;
					default:
						return false;
					}
				}
			}
			while (bits.available() > 0);
			return true;
		}

		private static bool decodeAnsiX12Segment(BitSource bits, StringBuilder result)
		{
			int[] array = new int[3];
			do
			{
				if (bits.available() == 8)
				{
					return true;
				}
				int num = bits.readBits(8);
				if (num == 254)
				{
					return true;
				}
				parseTwoBytes(num, bits.readBits(8), array);
				for (int i = 0; i < 3; i++)
				{
					int num2 = array[i];
					if (num2 == 0)
					{
						result.Append('\r');
						continue;
					}
					if (num2 == 1)
					{
						result.Append('*');
						continue;
					}
					if (num2 == 2)
					{
						result.Append('>');
						continue;
					}
					if (num2 == 3)
					{
						result.Append(' ');
						continue;
					}
					if (num2 < 14)
					{
						result.Append((char)(num2 + 44));
						continue;
					}
					if (num2 < 40)
					{
						result.Append((char)(num2 + 51));
						continue;
					}
					return false;
				}
			}
			while (bits.available() > 0);
			return true;
		}

		private static void parseTwoBytes(int firstByte, int secondByte, int[] result)
		{
			int num = (firstByte << 8) + secondByte - 1;
			num -= (result[0] = num / 1600) * 1600;
			result[2] = num - (result[1] = num / 40) * 40;
		}

		private static bool decodeEdifactSegment(BitSource bits, StringBuilder result)
		{
			do
			{
				if (bits.available() <= 16)
				{
					return true;
				}
				for (int i = 0; i < 4; i++)
				{
					int num = bits.readBits(6);
					if (num == 31)
					{
						int num2 = 8 - bits.BitOffset;
						if (num2 != 8)
						{
							bits.readBits(num2);
						}
						return true;
					}
					if ((num & 0x20) == 0)
					{
						num |= 0x40;
					}
					result.Append((char)num);
				}
			}
			while (bits.available() > 0);
			return true;
		}

		private static bool decodeBase256Segment(BitSource bits, StringBuilder result, IList<byte[]> byteSegments)
		{
			int num = 1 + bits.ByteOffset;
			int num2 = unrandomize255State(bits.readBits(8), num++);
			int num3 = ((num2 == 0) ? (bits.available() / 8) : ((num2 >= 250) ? (250 * (num2 - 249) + unrandomize255State(bits.readBits(8), num++)) : num2));
			if (num3 < 0)
			{
				return false;
			}
			byte[] array = new byte[num3];
			for (int i = 0; i < num3; i++)
			{
				if (bits.available() < 8)
				{
					return false;
				}
				array[i] = (byte)unrandomize255State(bits.readBits(8), num++);
			}
			byteSegments.Add(array);
			try
			{
				result.Append(Encoding.GetEncoding("ISO-8859-1").GetString(array));
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Platform does not support required encoding: " + ex);
			}
			return true;
		}

		private static int unrandomize255State(int randomizedBase256Codeword, int base256CodewordPosition)
		{
			int num = 149 * base256CodewordPosition % 255 + 1;
			int num2 = randomizedBase256Codeword - num;
			return (num2 < 0) ? (num2 + 256) : num2;
		}
	}
}
