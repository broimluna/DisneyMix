using System.Text;
using ZXing.Common;

namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal sealed class AI01AndOtherAIs : AI01decoder
	{
		private static int HEADER_SIZE = 4;

		internal AI01AndOtherAIs(BitArray information)
			: base(information)
		{
		}

		public override string parseInformation()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(01)");
			int length = stringBuilder.Length;
			int value = getGeneralDecoder().extractNumericValueFromBitArray(HEADER_SIZE, 4);
			stringBuilder.Append(value);
			encodeCompressedGtinWithoutAI(stringBuilder, HEADER_SIZE + 4, length);
			return getGeneralDecoder().decodeAllCodes(stringBuilder, HEADER_SIZE + 44);
		}
	}
}
