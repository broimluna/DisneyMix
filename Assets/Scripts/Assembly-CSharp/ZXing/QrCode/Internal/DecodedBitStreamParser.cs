using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.QrCode.Internal
{
	internal static class DecodedBitStreamParser
	{
		private const int GB2312_SUBSET = 1;

		private static readonly char[] ALPHANUMERIC_CHARS = new char[45]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z', ' ', '$', '%', '*',
			'+', '-', '.', '/', ':'
		};

		internal static DecoderResult decode(byte[] bytes, Version version, ErrorCorrectionLevel ecLevel, IDictionary<DecodeHintType, object> hints)
		{
			BitSource bitSource = new BitSource(bytes);
			StringBuilder stringBuilder = new StringBuilder(50);
			List<byte[]> list = new List<byte[]>(1);
			int saSequence = -1;
			int saParity = -1;
			try
			{
				CharacterSetECI characterSetECI = null;
				bool fc1InEffect = false;
				Mode mode;
				do
				{
					if (bitSource.available() < 4)
					{
						mode = Mode.TERMINATOR;
					}
					else
					{
						try
						{
							mode = Mode.forBits(bitSource.readBits(4));
						}
						catch (ArgumentException)
						{
							return null;
						}
					}
					if (mode == Mode.TERMINATOR)
					{
						continue;
					}
					if (mode == Mode.FNC1_FIRST_POSITION || mode == Mode.FNC1_SECOND_POSITION)
					{
						fc1InEffect = true;
						continue;
					}
					if (mode == Mode.STRUCTURED_APPEND)
					{
						if (bitSource.available() < 16)
						{
							return null;
						}
						saSequence = bitSource.readBits(8);
						saParity = bitSource.readBits(8);
						continue;
					}
					if (mode == Mode.ECI)
					{
						int value = parseECIValue(bitSource);
						characterSetECI = CharacterSetECI.getCharacterSetECIByValue(value);
						if (characterSetECI == null)
						{
							return null;
						}
						continue;
					}
					if (mode == Mode.HANZI)
					{
						int num = bitSource.readBits(4);
						int count = bitSource.readBits(mode.getCharacterCountBits(version));
						if (num == 1 && !decodeHanziSegment(bitSource, stringBuilder, count))
						{
							return null;
						}
						continue;
					}
					int count2 = bitSource.readBits(mode.getCharacterCountBits(version));
					if (mode == Mode.NUMERIC)
					{
						if (!decodeNumericSegment(bitSource, stringBuilder, count2))
						{
							return null;
						}
						continue;
					}
					if (mode == Mode.ALPHANUMERIC)
					{
						if (!decodeAlphanumericSegment(bitSource, stringBuilder, count2, fc1InEffect))
						{
							return null;
						}
						continue;
					}
					if (mode == Mode.BYTE)
					{
						if (!decodeByteSegment(bitSource, stringBuilder, count2, characterSetECI, list, hints))
						{
							return null;
						}
						continue;
					}
					if (mode == Mode.KANJI)
					{
						if (!decodeKanjiSegment(bitSource, stringBuilder, count2))
						{
							return null;
						}
						continue;
					}
					return null;
				}
				while (mode != Mode.TERMINATOR);
			}
			catch (ArgumentException)
			{
				return null;
			}
			string text = stringBuilder.ToString().Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
			return new DecoderResult(bytes, text, (list.Count != 0) ? list : null, (ecLevel != null) ? ecLevel.ToString() : null, saSequence, saParity);
		}

		private static bool decodeHanziSegment(BitSource bits, StringBuilder result, int count)
		{
			if (count * 13 > bits.available())
			{
				return false;
			}
			byte[] array = new byte[2 * count];
			int num = 0;
			while (count > 0)
			{
				int num2 = bits.readBits(13);
				int num3 = (num2 / 96 << 8) | (num2 % 96);
				num3 = ((num3 >= 959) ? (num3 + 42657) : (num3 + 41377));
				array[num] = (byte)((num3 >> 8) & 0xFF);
				array[num + 1] = (byte)(num3 & 0xFF);
				num += 2;
				count--;
			}
			try
			{
				result.Append(Encoding.GetEncoding(StringUtils.GB2312).GetString(array, 0, array.Length));
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		private static bool decodeKanjiSegment(BitSource bits, StringBuilder result, int count)
		{
			if (count * 13 > bits.available())
			{
				return false;
			}
			byte[] array = new byte[2 * count];
			int num = 0;
			while (count > 0)
			{
				int num2 = bits.readBits(13);
				int num3 = (num2 / 192 << 8) | (num2 % 192);
				num3 = ((num3 >= 7936) ? (num3 + 49472) : (num3 + 33088));
				array[num] = (byte)(num3 >> 8);
				array[num + 1] = (byte)num3;
				num += 2;
				count--;
			}
			try
			{
				result.Append(Encoding.GetEncoding(StringUtils.SHIFT_JIS).GetString(array, 0, array.Length));
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		private static bool decodeByteSegment(BitSource bits, StringBuilder result, int count, CharacterSetECI currentCharacterSetECI, IList<byte[]> byteSegments, IDictionary<DecodeHintType, object> hints)
		{
			if (count << 3 > bits.available())
			{
				return false;
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = (byte)bits.readBits(8);
			}
			string name = ((currentCharacterSetECI != null) ? currentCharacterSetECI.EncodingName : StringUtils.guessEncoding(array, hints));
			try
			{
				result.Append(Encoding.GetEncoding(name).GetString(array, 0, array.Length));
			}
			catch (Exception)
			{
				return false;
			}
			byteSegments.Add(array);
			return true;
		}

		private static char toAlphaNumericChar(int value)
		{
			if (value >= ALPHANUMERIC_CHARS.Length)
			{
				throw FormatException.Instance;
			}
			return ALPHANUMERIC_CHARS[value];
		}

		private static bool decodeAlphanumericSegment(BitSource bits, StringBuilder result, int count, bool fc1InEffect)
		{
			int length = result.Length;
			while (count > 1)
			{
				if (bits.available() < 11)
				{
					return false;
				}
				int num = bits.readBits(11);
				result.Append(toAlphaNumericChar(num / 45));
				result.Append(toAlphaNumericChar(num % 45));
				count -= 2;
			}
			if (count == 1)
			{
				if (bits.available() < 6)
				{
					return false;
				}
				result.Append(toAlphaNumericChar(bits.readBits(6)));
			}
			if (fc1InEffect)
			{
				for (int i = length; i < result.Length; i++)
				{
					if (result[i] == '%')
					{
						if (i < result.Length - 1 && result[i + 1] == '%')
						{
							result.Remove(i + 1, 1);
							continue;
						}
						result.Remove(i, 1);
						result.Insert(i, new char[1] { '\u001d' });
					}
				}
			}
			return true;
		}

		private static bool decodeNumericSegment(BitSource bits, StringBuilder result, int count)
		{
			while (count >= 3)
			{
				if (bits.available() < 10)
				{
					return false;
				}
				int num = bits.readBits(10);
				if (num >= 1000)
				{
					return false;
				}
				result.Append(toAlphaNumericChar(num / 100));
				result.Append(toAlphaNumericChar(num / 10 % 10));
				result.Append(toAlphaNumericChar(num % 10));
				count -= 3;
			}
			switch (count)
			{
			case 2:
			{
				if (bits.available() < 7)
				{
					return false;
				}
				int num3 = bits.readBits(7);
				if (num3 >= 100)
				{
					return false;
				}
				result.Append(toAlphaNumericChar(num3 / 10));
				result.Append(toAlphaNumericChar(num3 % 10));
				break;
			}
			case 1:
			{
				if (bits.available() < 4)
				{
					return false;
				}
				int num2 = bits.readBits(4);
				if (num2 >= 10)
				{
					return false;
				}
				result.Append(toAlphaNumericChar(num2));
				break;
			}
			}
			return true;
		}

		private static int parseECIValue(BitSource bits)
		{
			int num = bits.readBits(8);
			if ((num & 0x80) == 0)
			{
				return num & 0x7F;
			}
			if ((num & 0xC0) == 128)
			{
				int num2 = bits.readBits(8);
				return ((num & 0x3F) << 8) | num2;
			}
			if ((num & 0xE0) == 192)
			{
				int num3 = bits.readBits(16);
				return ((num & 0x1F) << 16) | num3;
			}
			throw new ArgumentException("Bad ECI bits starting with byte " + num);
		}
	}
}
