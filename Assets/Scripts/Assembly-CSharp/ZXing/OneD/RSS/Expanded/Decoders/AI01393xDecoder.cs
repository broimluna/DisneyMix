using System.Text;
using ZXing.Common;

namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal sealed class AI01393xDecoder : AI01decoder
	{
		private static int HEADER_SIZE = 8;

		private static int LAST_DIGIT_SIZE = 2;

		private static int FIRST_THREE_DIGITS_SIZE = 10;

		internal AI01393xDecoder(BitArray information)
			: base(information)
		{
		}

		public override string parseInformation()
		{
			if (getInformation().Size < HEADER_SIZE + AI01decoder.GTIN_SIZE)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			encodeCompressedGtin(stringBuilder, HEADER_SIZE);
			int value = getGeneralDecoder().extractNumericValueFromBitArray(HEADER_SIZE + AI01decoder.GTIN_SIZE, LAST_DIGIT_SIZE);
			stringBuilder.Append("(393");
			stringBuilder.Append(value);
			stringBuilder.Append(')');
			int num = getGeneralDecoder().extractNumericValueFromBitArray(HEADER_SIZE + AI01decoder.GTIN_SIZE + LAST_DIGIT_SIZE, FIRST_THREE_DIGITS_SIZE);
			if (num / 100 == 0)
			{
				stringBuilder.Append('0');
			}
			if (num / 10 == 0)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(num);
			DecodedInformation decodedInformation = getGeneralDecoder().decodeGeneralPurposeField(HEADER_SIZE + AI01decoder.GTIN_SIZE + LAST_DIGIT_SIZE + FIRST_THREE_DIGITS_SIZE, null);
			stringBuilder.Append(decodedInformation.getNewString());
			return stringBuilder.ToString();
		}
	}
}
