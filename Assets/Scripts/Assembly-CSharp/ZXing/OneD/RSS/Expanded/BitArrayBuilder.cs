using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD.RSS.Expanded
{
	internal static class BitArrayBuilder
	{
		internal static BitArray buildBitArray(List<ExpandedPair> pairs)
		{
			int num = (pairs.Count << 1) - 1;
			if (pairs[pairs.Count - 1].RightChar == null)
			{
				num--;
			}
			int size = 12 * num;
			BitArray bitArray = new BitArray(size);
			int num2 = 0;
			ExpandedPair expandedPair = pairs[0];
			int value = expandedPair.RightChar.Value;
			for (int num3 = 11; num3 >= 0; num3--)
			{
				if ((value & (1 << num3)) != 0)
				{
					bitArray[num2] = true;
				}
				num2++;
			}
			for (int i = 1; i < pairs.Count; i++)
			{
				ExpandedPair expandedPair2 = pairs[i];
				int value2 = expandedPair2.LeftChar.Value;
				for (int num4 = 11; num4 >= 0; num4--)
				{
					if ((value2 & (1 << num4)) != 0)
					{
						bitArray[num2] = true;
					}
					num2++;
				}
				if (expandedPair2.RightChar == null)
				{
					continue;
				}
				int value3 = expandedPair2.RightChar.Value;
				for (int num5 = 11; num5 >= 0; num5--)
				{
					if ((value3 & (1 << num5)) != 0)
					{
						bitArray[num2] = true;
					}
					num2++;
				}
			}
			return bitArray;
		}
	}
}
