using System;

namespace ZXing.Datamatrix.Encoder
{
	internal sealed class ASCIIEncoder : Encoder
	{
		public int EncodingMode
		{
			get
			{
				return 0;
			}
		}

		public void encode(EncoderContext context)
		{
			int num = HighLevelEncoder.determineConsecutiveDigitCount(context.Message, context.Pos);
			if (num >= 2)
			{
				context.writeCodeword(encodeASCIIDigits(context.Message[context.Pos], context.Message[context.Pos + 1]));
				context.Pos += 2;
				return;
			}
			char currentChar = context.CurrentChar;
			int num2 = HighLevelEncoder.lookAheadTest(context.Message, context.Pos, EncodingMode);
			if (num2 != EncodingMode)
			{
				switch (num2)
				{
				case 5:
					context.writeCodeword('ç');
					context.signalEncoderChange(5);
					break;
				case 1:
					context.writeCodeword('æ');
					context.signalEncoderChange(1);
					break;
				case 3:
					context.writeCodeword('î');
					context.signalEncoderChange(3);
					break;
				case 2:
					context.writeCodeword('ï');
					context.signalEncoderChange(2);
					break;
				case 4:
					context.writeCodeword('ð');
					context.signalEncoderChange(4);
					break;
				default:
					throw new InvalidOperationException("Illegal mode: " + num2);
				}
			}
			else if (HighLevelEncoder.isExtendedASCII(currentChar))
			{
				context.writeCodeword('ë');
				context.writeCodeword((char)(currentChar - 128 + 1));
				context.Pos++;
			}
			else
			{
				context.writeCodeword((char)(currentChar + 1));
				context.Pos++;
			}
		}

		private static char encodeASCIIDigits(char digit1, char digit2)
		{
			if (HighLevelEncoder.isDigit(digit1) && HighLevelEncoder.isDigit(digit2))
			{
				int num = (digit1 - 48) * 10 + (digit2 - 48);
				return (char)(num + 130);
			}
			throw new ArgumentException("not digits: " + digit1 + digit2);
		}
	}
}
