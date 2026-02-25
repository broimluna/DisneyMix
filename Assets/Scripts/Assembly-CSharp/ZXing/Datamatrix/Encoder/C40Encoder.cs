using System;
using System.Text;

namespace ZXing.Datamatrix.Encoder
{
	internal class C40Encoder : Encoder
	{
		public virtual int EncodingMode
		{
			get
			{
				return 1;
			}
		}

		public virtual void encode(EncoderContext context)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (context.HasMoreCharacters)
			{
				char currentChar = context.CurrentChar;
				context.Pos++;
				int num = encodeChar(currentChar, stringBuilder);
				int num2 = stringBuilder.Length / 3 * 2;
				int num3 = context.CodewordCount + num2;
				context.updateSymbolInfo(num3);
				int num4 = context.SymbolInfo.dataCapacity - num3;
				if (!context.HasMoreCharacters)
				{
					StringBuilder removed = new StringBuilder();
					if (stringBuilder.Length % 3 == 2 && (num4 < 2 || num4 > 2))
					{
						num = backtrackOneCharacter(context, stringBuilder, removed, num);
					}
					while (stringBuilder.Length % 3 == 1 && ((num <= 3 && num4 != 1) || num > 3))
					{
						num = backtrackOneCharacter(context, stringBuilder, removed, num);
					}
					break;
				}
				int length = stringBuilder.Length;
				if (length % 3 == 0)
				{
					int num5 = HighLevelEncoder.lookAheadTest(context.Message, context.Pos, EncodingMode);
					if (num5 != EncodingMode)
					{
						context.signalEncoderChange(num5);
						break;
					}
				}
			}
			handleEOD(context, stringBuilder);
		}

		private int backtrackOneCharacter(EncoderContext context, StringBuilder buffer, StringBuilder removed, int lastCharSize)
		{
			int length = buffer.Length;
			buffer.Remove(length - lastCharSize, lastCharSize);
			context.Pos--;
			char currentChar = context.CurrentChar;
			lastCharSize = encodeChar(currentChar, removed);
			context.resetSymbolInfo();
			return lastCharSize;
		}

		internal static void writeNextTriplet(EncoderContext context, StringBuilder buffer)
		{
			context.writeCodewords(encodeToCodewords(buffer, 0));
			buffer.Remove(0, 3);
		}

		protected virtual void handleEOD(EncoderContext context, StringBuilder buffer)
		{
			int num = buffer.Length / 3 * 2;
			int num2 = buffer.Length % 3;
			int num3 = context.CodewordCount + num;
			context.updateSymbolInfo(num3);
			int num4 = context.SymbolInfo.dataCapacity - num3;
			if (num2 == 2)
			{
				buffer.Append('\0');
				while (buffer.Length >= 3)
				{
					writeNextTriplet(context, buffer);
				}
				if (context.HasMoreCharacters)
				{
					context.writeCodeword('þ');
				}
			}
			else if (num4 == 1 && num2 == 1)
			{
				while (buffer.Length >= 3)
				{
					writeNextTriplet(context, buffer);
				}
				if (context.HasMoreCharacters)
				{
					context.writeCodeword('þ');
				}
				context.Pos--;
			}
			else
			{
				if (num2 != 0)
				{
					throw new InvalidOperationException("Unexpected case. Please report!");
				}
				while (buffer.Length >= 3)
				{
					writeNextTriplet(context, buffer);
				}
				if (num4 > 0 || context.HasMoreCharacters)
				{
					context.writeCodeword('þ');
				}
			}
			context.signalEncoderChange(0);
		}

		protected virtual int encodeChar(char c, StringBuilder sb)
		{
			switch (c)
			{
			case ' ':
				sb.Append('\u0003');
				return 1;
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
				return 1;
			default:
				if (c >= 'A' && c <= 'Z')
				{
					sb.Append((char)(c - 65 + 14));
					return 1;
				}
				if (c >= '\0' && c <= '\u001f')
				{
					sb.Append('\0');
					sb.Append(c);
					return 2;
				}
				if (c >= '!' && c <= '/')
				{
					sb.Append('\u0001');
					sb.Append((char)(c - 33));
					return 2;
				}
				if (c >= ':' && c <= '@')
				{
					sb.Append('\u0001');
					sb.Append((char)(c - 58 + 15));
					return 2;
				}
				if (c >= '[' && c <= '_')
				{
					sb.Append('\u0001');
					sb.Append((char)(c - 91 + 22));
					return 2;
				}
				if (c >= '`' && c <= '\u007f')
				{
					sb.Append('\u0002');
					sb.Append((char)(c - 96));
					return 2;
				}
				if (c >= '\u0080')
				{
					sb.Append("\u0001\u001e");
					int num = 2;
					return num + encodeChar((char)(c - 128), sb);
				}
				throw new InvalidOperationException("Illegal character: " + c);
			}
		}

		private static string encodeToCodewords(StringBuilder sb, int startPos)
		{
			char c = sb[startPos];
			char c2 = sb[startPos + 1];
			char c3 = sb[startPos + 2];
			int num = 1600 * c + 40 * c2 + c3 + 1;
			char c4 = (char)(num / 256);
			char c5 = (char)(num % 256);
			return new string(new char[2] { c4, c5 });
		}
	}
}
