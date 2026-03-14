using System;

namespace ZXing.QrCode.Internal
{
	internal sealed class DataBlock
	{
		private readonly int numDataCodewords;

		private readonly byte[] codewords;

		internal int NumDataCodewords
		{
			get
			{
				return numDataCodewords;
			}
		}

		internal byte[] Codewords
		{
			get
			{
				return codewords;
			}
		}

		private DataBlock(int numDataCodewords, byte[] codewords)
		{
			this.numDataCodewords = numDataCodewords;
			this.codewords = codewords;
		}

		internal static DataBlock[] getDataBlocks(byte[] rawCodewords, Version version, ErrorCorrectionLevel ecLevel)
		{
			if (rawCodewords.Length != version.TotalCodewords)
			{
				throw new ArgumentException();
			}
			Version.ECBlocks eCBlocksForLevel = version.getECBlocksForLevel(ecLevel);
			int num = 0;
			Version.ECB[] eCBlocks = eCBlocksForLevel.getECBlocks();
			Version.ECB[] array = eCBlocks;
			foreach (Version.ECB eCB in array)
			{
				num += eCB.Count;
			}
			DataBlock[] array2 = new DataBlock[num];
			int num2 = 0;
			Version.ECB[] array3 = eCBlocks;
			foreach (Version.ECB eCB2 in array3)
			{
				for (int k = 0; k < eCB2.Count; k++)
				{
					int dataCodewords = eCB2.DataCodewords;
					int num3 = eCBlocksForLevel.ECCodewordsPerBlock + dataCodewords;
					array2[num2++] = new DataBlock(dataCodewords, new byte[num3]);
				}
			}
			int num4 = array2[0].codewords.Length;
			int num5;
			for (num5 = array2.Length - 1; num5 >= 0; num5--)
			{
				int num6 = array2[num5].codewords.Length;
				if (num6 == num4)
				{
					break;
				}
			}
			num5++;
			int num7 = num4 - eCBlocksForLevel.ECCodewordsPerBlock;
			int num8 = 0;
			for (int l = 0; l < num7; l++)
			{
				for (int m = 0; m < num2; m++)
				{
					array2[m].codewords[l] = rawCodewords[num8++];
				}
			}
			for (int n = num5; n < num2; n++)
			{
				array2[n].codewords[num7] = rawCodewords[num8++];
			}
			int num9 = array2[0].codewords.Length;
			for (int num10 = num7; num10 < num9; num10++)
			{
				for (int num11 = 0; num11 < num2; num11++)
				{
					int num12 = ((num11 >= num5) ? (num10 + 1) : num10);
					array2[num11].codewords[num12] = rawCodewords[num8++];
				}
			}
			return array2;
		}
	}
}
