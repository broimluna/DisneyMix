using System.Text;
using ZXing.Common;

namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal abstract class AI01weightDecoder : AI01decoder
	{
		internal AI01weightDecoder(BitArray information)
			: base(information)
		{
		}

		protected void encodeCompressedWeight(StringBuilder buf, int currentPos, int weightSize)
		{
			int weight = getGeneralDecoder().extractNumericValueFromBitArray(currentPos, weightSize);
			addWeightCode(buf, weight);
			int num = checkWeight(weight);
			int num2 = 100000;
			for (int i = 0; i < 5; i++)
			{
				if (num / num2 == 0)
				{
					buf.Append('0');
				}
				num2 /= 10;
			}
			buf.Append(num);
		}

		protected abstract void addWeightCode(StringBuilder buf, int weight);

		protected abstract int checkWeight(int weight);
	}
}
