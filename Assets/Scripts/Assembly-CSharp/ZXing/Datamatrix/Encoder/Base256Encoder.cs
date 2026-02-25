using System;
using System.Text;

namespace ZXing.Datamatrix.Encoder
{
	internal sealed class Base256Encoder : Encoder
	{
		public int EncodingMode
		{
			get
			{
				return 5;
			}
		}

		public void encode(EncoderContext context)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('\0');
			while (context.HasMoreCharacters)
			{
				char currentChar = context.CurrentChar;
				stringBuilder.Append(currentChar);
				context.Pos++;
				int num = HighLevelEncoder.lookAheadTest(context.Message, context.Pos, EncodingMode);
				if (num != EncodingMode)
				{
					context.signalEncoderChange(num);
					break;
				}
			}
			int num2 = stringBuilder.Length - 1;
			int num3 = context.CodewordCount + num2 + 1;
			context.updateSymbolInfo(num3);
			bool flag = context.SymbolInfo.dataCapacity - num3 > 0;
			if (context.HasMoreCharacters || flag)
			{
				if (num2 <= 249)
				{
					stringBuilder[0] = (char)num2;
				}
				else
				{
					if (num2 <= 249 || num2 > 1555)
					{
						throw new InvalidOperationException("Message length not in valid ranges: " + num2);
					}
					stringBuilder[0] = (char)(num2 / 250 + 249);
					stringBuilder.Insert(1, new char[1] { (char)(num2 % 250) });
				}
			}
			int length = stringBuilder.Length;
			for (int i = 0; i < length; i++)
			{
				context.writeCodeword(randomize255State(stringBuilder[i], context.CodewordCount + 1));
			}
		}

		private static char randomize255State(char ch, int codewordPosition)
		{
			int num = 149 * codewordPosition % 255 + 1;
			int num2 = ch + num;
			if (num2 <= 255)
			{
				return (char)num2;
			}
			return (char)(num2 - 256);
		}
	}
}
