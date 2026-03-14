using System.Text;
using ZXing.Common;

namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal abstract class AI01decoder : AbstractExpandedDecoder
	{
		protected static int GTIN_SIZE = 40;

		internal AI01decoder(BitArray information)
			: base(information)
		{
		}

		protected void encodeCompressedGtin(StringBuilder buf, int currentPos)
		{
			buf.Append("(01)");
			int length = buf.Length;
			buf.Append('9');
			encodeCompressedGtinWithoutAI(buf, currentPos, length);
		}

		protected void encodeCompressedGtinWithoutAI(StringBuilder buf, int currentPos, int initialBufferPosition)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = getGeneralDecoder().extractNumericValueFromBitArray(currentPos + 10 * i, 10);
				if (num / 100 == 0)
				{
					buf.Append('0');
				}
				if (num / 10 == 0)
				{
					buf.Append('0');
				}
				buf.Append(num);
			}
			appendCheckDigit(buf, initialBufferPosition);
		}

		private static void appendCheckDigit(StringBuilder buf, int currentPos)
		{
			int num = 0;
			for (int i = 0; i < 13; i++)
			{
				int num2 = buf[i + currentPos] - 48;
				num += (((i & 1) != 0) ? num2 : (3 * num2));
			}
			num = 10 - num % 10;
			if (num == 10)
			{
				num = 0;
			}
			buf.Append(num);
		}
	}
}
