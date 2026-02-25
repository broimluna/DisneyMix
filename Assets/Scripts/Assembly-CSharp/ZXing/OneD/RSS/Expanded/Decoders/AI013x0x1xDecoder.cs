using System.Text;
using ZXing.Common;

namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal sealed class AI013x0x1xDecoder : AI01weightDecoder
	{
		private static int HEADER_SIZE = 8;

		private static int WEIGHT_SIZE = 20;

		private static int DATE_SIZE = 16;

		private string dateCode;

		private string firstAIdigits;

		internal AI013x0x1xDecoder(BitArray information, string firstAIdigits, string dateCode)
			: base(information)
		{
			this.dateCode = dateCode;
			this.firstAIdigits = firstAIdigits;
		}

		public override string parseInformation()
		{
			if (getInformation().Size != HEADER_SIZE + AI01decoder.GTIN_SIZE + WEIGHT_SIZE + DATE_SIZE)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			encodeCompressedGtin(stringBuilder, HEADER_SIZE);
			encodeCompressedWeight(stringBuilder, HEADER_SIZE + AI01decoder.GTIN_SIZE, WEIGHT_SIZE);
			encodeCompressedDate(stringBuilder, HEADER_SIZE + AI01decoder.GTIN_SIZE + WEIGHT_SIZE);
			return stringBuilder.ToString();
		}

		private void encodeCompressedDate(StringBuilder buf, int currentPos)
		{
			int num = getGeneralDecoder().extractNumericValueFromBitArray(currentPos, DATE_SIZE);
			if (num != 38400)
			{
				buf.Append('(');
				buf.Append(dateCode);
				buf.Append(')');
				int num2 = num % 32;
				num /= 32;
				int num3 = num % 12 + 1;
				num /= 12;
				int num4 = num;
				if (num4 / 10 == 0)
				{
					buf.Append('0');
				}
				buf.Append(num4);
				if (num3 / 10 == 0)
				{
					buf.Append('0');
				}
				buf.Append(num3);
				if (num2 / 10 == 0)
				{
					buf.Append('0');
				}
				buf.Append(num2);
			}
		}

		protected override void addWeightCode(StringBuilder buf, int weight)
		{
			int value = weight / 100000;
			buf.Append('(');
			buf.Append(firstAIdigits);
			buf.Append(value);
			buf.Append(')');
		}

		protected override int checkWeight(int weight)
		{
			return weight % 100000;
		}
	}
}
