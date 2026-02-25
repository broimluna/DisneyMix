using System;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal sealed class GeneralAppIdDecoder
	{
		private BitArray information;

		private CurrentParsingState current = new CurrentParsingState();

		private StringBuilder buffer = new StringBuilder();

		internal GeneralAppIdDecoder(BitArray information)
		{
			this.information = information;
		}

		internal string decodeAllCodes(StringBuilder buff, int initialPosition)
		{
			int num = initialPosition;
			string remaining = null;
			while (true)
			{
				DecodedInformation decodedInformation = decodeGeneralPurposeField(num, remaining);
				string text = FieldParser.parseFieldsInGeneralPurpose(decodedInformation.getNewString());
				if (text != null)
				{
					buff.Append(text);
				}
				remaining = ((!decodedInformation.isRemaining()) ? null : decodedInformation.getRemainingValue().ToString());
				if (num == decodedInformation.NewPosition)
				{
					break;
				}
				num = decodedInformation.NewPosition;
			}
			return buff.ToString();
		}

		private bool isStillNumeric(int pos)
		{
			if (pos + 7 > information.Size)
			{
				return pos + 4 <= information.Size;
			}
			for (int i = pos; i < pos + 3; i++)
			{
				if (information[i])
				{
					return true;
				}
			}
			return information[pos + 3];
		}

		private DecodedNumeric decodeNumeric(int pos)
		{
			int num;
			if (pos + 7 > information.Size)
			{
				num = extractNumericValueFromBitArray(pos, 4);
				if (num == 0)
				{
					return new DecodedNumeric(information.Size, DecodedNumeric.FNC1, DecodedNumeric.FNC1);
				}
				return new DecodedNumeric(information.Size, num - 1, DecodedNumeric.FNC1);
			}
			num = extractNumericValueFromBitArray(pos, 7);
			int firstDigit = (num - 8) / 11;
			int secondDigit = (num - 8) % 11;
			return new DecodedNumeric(pos + 7, firstDigit, secondDigit);
		}

		internal int extractNumericValueFromBitArray(int pos, int bits)
		{
			return extractNumericValueFromBitArray(information, pos, bits);
		}

		internal static int extractNumericValueFromBitArray(BitArray information, int pos, int bits)
		{
			int num = 0;
			for (int i = 0; i < bits; i++)
			{
				if (information[pos + i])
				{
					num |= 1 << ((bits - i - 1) & 0x1F);
				}
			}
			return num;
		}

		internal DecodedInformation decodeGeneralPurposeField(int pos, string remaining)
		{
			buffer.Length = 0;
			if (remaining != null)
			{
				buffer.Append(remaining);
			}
			current.setPosition(pos);
			DecodedInformation decodedInformation = parseBlocks();
			if (decodedInformation != null && decodedInformation.isRemaining())
			{
				return new DecodedInformation(current.getPosition(), buffer.ToString(), decodedInformation.getRemainingValue());
			}
			return new DecodedInformation(current.getPosition(), buffer.ToString());
		}

		private DecodedInformation parseBlocks()
		{
			int position;
			BlockParsedResult blockParsedResult;
			bool flag;
			do
			{
				position = current.getPosition();
				if (current.isAlpha())
				{
					blockParsedResult = parseAlphaBlock();
					flag = blockParsedResult.isFinished();
				}
				else if (current.isIsoIec646())
				{
					blockParsedResult = parseIsoIec646Block();
					flag = blockParsedResult.isFinished();
				}
				else
				{
					blockParsedResult = parseNumericBlock();
					flag = blockParsedResult.isFinished();
				}
			}
			while ((position != current.getPosition() || flag) && !flag);
			return blockParsedResult.getDecodedInformation();
		}

		private BlockParsedResult parseNumericBlock()
		{
			while (isStillNumeric(current.getPosition()))
			{
				DecodedNumeric decodedNumeric = decodeNumeric(current.getPosition());
				current.setPosition(decodedNumeric.NewPosition);
				if (decodedNumeric.isFirstDigitFNC1())
				{
					DecodedInformation decodedInformation = ((!decodedNumeric.isSecondDigitFNC1()) ? new DecodedInformation(current.getPosition(), buffer.ToString(), decodedNumeric.getSecondDigit()) : new DecodedInformation(current.getPosition(), buffer.ToString()));
					return new BlockParsedResult(decodedInformation, true);
				}
				buffer.Append(decodedNumeric.getFirstDigit());
				if (decodedNumeric.isSecondDigitFNC1())
				{
					DecodedInformation decodedInformation2 = new DecodedInformation(current.getPosition(), buffer.ToString());
					return new BlockParsedResult(decodedInformation2, true);
				}
				buffer.Append(decodedNumeric.getSecondDigit());
			}
			if (isNumericToAlphaNumericLatch(current.getPosition()))
			{
				current.setAlpha();
				current.incrementPosition(4);
			}
			return new BlockParsedResult(false);
		}

		private BlockParsedResult parseIsoIec646Block()
		{
			while (isStillIsoIec646(current.getPosition()))
			{
				DecodedChar decodedChar = decodeIsoIec646(current.getPosition());
				current.setPosition(decodedChar.NewPosition);
				if (decodedChar.isFNC1())
				{
					DecodedInformation decodedInformation = new DecodedInformation(current.getPosition(), buffer.ToString());
					return new BlockParsedResult(decodedInformation, true);
				}
				buffer.Append(decodedChar.getValue());
			}
			if (isAlphaOr646ToNumericLatch(current.getPosition()))
			{
				current.incrementPosition(3);
				current.setNumeric();
			}
			else if (isAlphaTo646ToAlphaLatch(current.getPosition()))
			{
				if (current.getPosition() + 5 < information.Size)
				{
					current.incrementPosition(5);
				}
				else
				{
					current.setPosition(information.Size);
				}
				current.setAlpha();
			}
			return new BlockParsedResult(false);
		}

		private BlockParsedResult parseAlphaBlock()
		{
			while (isStillAlpha(current.getPosition()))
			{
				DecodedChar decodedChar = decodeAlphanumeric(current.getPosition());
				current.setPosition(decodedChar.NewPosition);
				if (decodedChar.isFNC1())
				{
					DecodedInformation decodedInformation = new DecodedInformation(current.getPosition(), buffer.ToString());
					return new BlockParsedResult(decodedInformation, true);
				}
				buffer.Append(decodedChar.getValue());
			}
			if (isAlphaOr646ToNumericLatch(current.getPosition()))
			{
				current.incrementPosition(3);
				current.setNumeric();
			}
			else if (isAlphaTo646ToAlphaLatch(current.getPosition()))
			{
				if (current.getPosition() + 5 < information.Size)
				{
					current.incrementPosition(5);
				}
				else
				{
					current.setPosition(information.Size);
				}
				current.setIsoIec646();
			}
			return new BlockParsedResult(false);
		}

		private bool isStillIsoIec646(int pos)
		{
			if (pos + 5 > information.Size)
			{
				return false;
			}
			int num = extractNumericValueFromBitArray(pos, 5);
			if (num >= 5 && num < 16)
			{
				return true;
			}
			if (pos + 7 > information.Size)
			{
				return false;
			}
			int num2 = extractNumericValueFromBitArray(pos, 7);
			if (num2 >= 64 && num2 < 116)
			{
				return true;
			}
			if (pos + 8 > information.Size)
			{
				return false;
			}
			int num3 = extractNumericValueFromBitArray(pos, 8);
			return num3 >= 232 && num3 < 253;
		}

		private DecodedChar decodeIsoIec646(int pos)
		{
			int num = extractNumericValueFromBitArray(pos, 5);
			switch (num)
			{
			case 15:
				return new DecodedChar(pos + 5, DecodedChar.FNC1);
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
			case 13:
			case 14:
				return new DecodedChar(pos + 5, (char)(48 + num - 5));
			default:
			{
				int num2 = extractNumericValueFromBitArray(pos, 7);
				if (num2 >= 64 && num2 < 90)
				{
					return new DecodedChar(pos + 7, (char)(num2 + 1));
				}
				if (num2 >= 90 && num2 < 116)
				{
					return new DecodedChar(pos + 7, (char)(num2 + 7));
				}
				int num3 = extractNumericValueFromBitArray(pos, 8);
				char value;
				switch (num3)
				{
				case 232:
					value = '!';
					break;
				case 233:
					value = '"';
					break;
				case 234:
					value = '%';
					break;
				case 235:
					value = '&';
					break;
				case 236:
					value = '\'';
					break;
				case 237:
					value = '(';
					break;
				case 238:
					value = ')';
					break;
				case 239:
					value = '*';
					break;
				case 240:
					value = '+';
					break;
				case 241:
					value = ',';
					break;
				case 242:
					value = '-';
					break;
				case 243:
					value = '.';
					break;
				case 244:
					value = '/';
					break;
				case 245:
					value = ':';
					break;
				case 246:
					value = ';';
					break;
				case 247:
					value = '<';
					break;
				case 248:
					value = '=';
					break;
				case 249:
					value = '>';
					break;
				case 250:
					value = '?';
					break;
				case 251:
					value = '_';
					break;
				case 252:
					value = ' ';
					break;
				default:
					throw new ArgumentException("Decoding invalid ISO/IEC 646 value: " + num3);
				}
				return new DecodedChar(pos + 8, value);
			}
			}
		}

		private bool isStillAlpha(int pos)
		{
			if (pos + 5 > information.Size)
			{
				return false;
			}
			int num = extractNumericValueFromBitArray(pos, 5);
			if (num >= 5 && num < 16)
			{
				return true;
			}
			if (pos + 6 > information.Size)
			{
				return false;
			}
			int num2 = extractNumericValueFromBitArray(pos, 6);
			return num2 >= 16 && num2 < 63;
		}

		private DecodedChar decodeAlphanumeric(int pos)
		{
			int num = extractNumericValueFromBitArray(pos, 5);
			switch (num)
			{
			case 15:
				return new DecodedChar(pos + 5, DecodedChar.FNC1);
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
			case 13:
			case 14:
				return new DecodedChar(pos + 5, (char)(48 + num - 5));
			default:
			{
				int num2 = extractNumericValueFromBitArray(pos, 6);
				if (num2 >= 32 && num2 < 58)
				{
					return new DecodedChar(pos + 6, (char)(num2 + 33));
				}
				char value;
				switch (num2)
				{
				case 58:
					value = '*';
					break;
				case 59:
					value = ',';
					break;
				case 60:
					value = '-';
					break;
				case 61:
					value = '.';
					break;
				case 62:
					value = '/';
					break;
				default:
					throw new InvalidOperationException("Decoding invalid alphanumeric value: " + num2);
				}
				return new DecodedChar(pos + 6, value);
			}
			}
		}

		private bool isAlphaTo646ToAlphaLatch(int pos)
		{
			if (pos + 1 > information.Size)
			{
				return false;
			}
			for (int i = 0; i < 5 && i + pos < information.Size; i++)
			{
				if (i == 2)
				{
					if (!information[pos + 2])
					{
						return false;
					}
				}
				else if (information[pos + i])
				{
					return false;
				}
			}
			return true;
		}

		private bool isAlphaOr646ToNumericLatch(int pos)
		{
			if (pos + 3 > information.Size)
			{
				return false;
			}
			for (int i = pos; i < pos + 3; i++)
			{
				if (information[i])
				{
					return false;
				}
			}
			return true;
		}

		private bool isNumericToAlphaNumericLatch(int pos)
		{
			if (pos + 1 > information.Size)
			{
				return false;
			}
			for (int i = 0; i < 4 && i + pos < information.Size; i++)
			{
				if (information[pos + i])
				{
					return false;
				}
			}
			return true;
		}
	}
}
