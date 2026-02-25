using System.Text;
using ZXing.Common;

namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal abstract class AI013x0xDecoder : AI01weightDecoder
	{
		private static int HEADER_SIZE = 5;

		private static int WEIGHT_SIZE = 15;

		internal AI013x0xDecoder(BitArray information)
			: base(information)
		{
		}

		public override string parseInformation()
		{
			if (getInformation().Size != HEADER_SIZE + AI01decoder.GTIN_SIZE + WEIGHT_SIZE)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			encodeCompressedGtin(stringBuilder, HEADER_SIZE);
			encodeCompressedWeight(stringBuilder, HEADER_SIZE + AI01decoder.GTIN_SIZE, WEIGHT_SIZE);
			return stringBuilder.ToString();
		}
	}
}
