using System;
using System.Text;

namespace ZXing.Datamatrix.Encoder
{
	internal sealed class EdifactEncoder : Encoder
	{
		public int EncodingMode
		{
			get
			{
				return 4;
			}
		}

		public void encode(EncoderContext context)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (context.HasMoreCharacters)
			{
				char currentChar = context.CurrentChar;
				encodeChar(currentChar, stringBuilder);
				context.Pos++;
				int length = stringBuilder.Length;
				if (length >= 4)
				{
					context.writeCodewords(encodeToCodewords(stringBuilder, 0));
					stringBuilder.Remove(0, 4);
					int num = HighLevelEncoder.lookAheadTest(context.Message, context.Pos, EncodingMode);
					if (num != EncodingMode)
					{
						context.signalEncoderChange(0);
						break;
					}
				}
			}
			stringBuilder.Append('\u001f');
			handleEOD(context, stringBuilder);
		}

		private static void handleEOD(EncoderContext context, StringBuilder buffer)
		{
			try
			{
				int length = buffer.Length;
				switch (length)
				{
				case 0:
					return;
				case 1:
				{
					context.updateSymbolInfo();
					int num = context.SymbolInfo.dataCapacity - context.CodewordCount;
					if (context.RemainingCharacters == 0 && num <= 2)
					{
						return;
					}
					break;
				}
				}
				if (length > 4)
				{
					throw new InvalidOperationException("Count must not exceed 4");
				}
				int num2 = length - 1;
				string text = encodeToCodewords(buffer, 0);
				bool flag = !context.HasMoreCharacters && num2 <= 2;
				if (num2 <= 2)
				{
					context.updateSymbolInfo(context.CodewordCount + num2);
					int num3 = context.SymbolInfo.dataCapacity - context.CodewordCount;
					if (num3 >= 3)
					{
						flag = false;
						context.updateSymbolInfo(context.CodewordCount + text.Length);
					}
				}
				if (flag)
				{
					context.resetSymbolInfo();
					context.Pos -= num2;
				}
				else
				{
					context.writeCodewords(text);
				}
			}
			finally
			{
				context.signalEncoderChange(0);
			}
		}

		private static void encodeChar(char c, StringBuilder sb)
		{
			if (c >= ' ' && c <= '?')
			{
				sb.Append(c);
			}
			else if (c >= '@' && c <= '^')
			{
				sb.Append((char)(c - 64));
			}
			else
			{
				HighLevelEncoder.illegalCharacter(c);
			}
		}

		private static string encodeToCodewords(StringBuilder sb, int startPos)
		{
			int num = sb.Length - startPos;
			if (num == 0)
			{
				throw new InvalidOperationException("StringBuilder must not be empty");
			}
			char c = sb[startPos];
			char c2 = ((num >= 2) ? sb[startPos + 1] : '\0');
			char c3 = ((num >= 3) ? sb[startPos + 2] : '\0');
			char c4 = ((num >= 4) ? sb[startPos + 3] : '\0');
			int num2 = (int)(((uint)c << 18) + ((uint)c2 << 12) + ((uint)c3 << 6) + c4);
			char value = (char)((num2 >> 16) & 0xFF);
			char value2 = (char)((num2 >> 8) & 0xFF);
			char value3 = (char)(num2 & 0xFF);
			StringBuilder stringBuilder = new StringBuilder(3);
			stringBuilder.Append(value);
			if (num >= 2)
			{
				stringBuilder.Append(value2);
			}
			if (num >= 3)
			{
				stringBuilder.Append(value3);
			}
			return stringBuilder.ToString();
		}
	}
}
