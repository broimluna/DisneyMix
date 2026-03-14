using System.Text;

namespace ZXing.Datamatrix.Encoder
{
	internal sealed class X12Encoder : C40Encoder
	{
		public override int EncodingMode
		{
			get
			{
				return 3;
			}
		}

		public override void encode(EncoderContext context)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (context.HasMoreCharacters)
			{
				char currentChar = context.CurrentChar;
				context.Pos++;
				encodeChar(currentChar, stringBuilder);
				int length = stringBuilder.Length;
				if (length % 3 == 0)
				{
					C40Encoder.writeNextTriplet(context, stringBuilder);
					int num = HighLevelEncoder.lookAheadTest(context.Message, context.Pos, EncodingMode);
					if (num != EncodingMode)
					{
						context.signalEncoderChange(num);
						break;
					}
				}
			}
			handleEOD(context, stringBuilder);
		}

		protected override int encodeChar(char c, StringBuilder sb)
		{
			switch (c)
			{
			case '\r':
				sb.Append('\0');
				break;
			case '*':
				sb.Append('\u0001');
				break;
			case '>':
				sb.Append('\u0002');
				break;
			case ' ':
				sb.Append('\u0003');
				break;
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
				sb.Append((char)(c - 48 + 4));
				break;
			default:
				if (c >= 'A' && c <= 'Z')
				{
					sb.Append((char)(c - 65 + 14));
				}
				else
				{
					HighLevelEncoder.illegalCharacter(c);
				}
				break;
			}
			return 1;
		}

		protected override void handleEOD(EncoderContext context, StringBuilder buffer)
		{
			context.updateSymbolInfo();
			int num = context.SymbolInfo.dataCapacity - context.CodewordCount;
			switch (buffer.Length)
			{
			case 2:
				context.writeCodeword('þ');
				context.Pos -= 2;
				context.signalEncoderChange(0);
				break;
			case 1:
				context.Pos--;
				if (num > 1)
				{
					context.writeCodeword('þ');
				}
				context.signalEncoderChange(0);
				break;
			}
		}
	}
}
