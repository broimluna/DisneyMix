using System.Text;
using ZXing.Common;

namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal sealed class AI01392xDecoder : AI01decoder
	{
		private const int HEADER_SIZE = 8;

		private const int LAST_DIGIT_SIZE = 2;

		internal AI01392xDecoder(BitArray information)
			: base(information)
		{
		}

		public override string parseInformation()
		{
			if (getInformation().Size < 8 + AI01decoder.GTIN_SIZE)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			encodeCompressedGtin(stringBuilder, 8);
			int value = getGeneralDecoder().extractNumericValueFromBitArray(8 + AI01decoder.GTIN_SIZE, 2);
			stringBuilder.Append("(392");
			stringBuilder.Append(value);
			stringBuilder.Append(')');
			DecodedInformation decodedInformation = getGeneralDecoder().decodeGeneralPurposeField(8 + AI01decoder.GTIN_SIZE + 2, null);
			stringBuilder.Append(decodedInformation.getNewString());
			return stringBuilder.ToString();
		}
	}
}
