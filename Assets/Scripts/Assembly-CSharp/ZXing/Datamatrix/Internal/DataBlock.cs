using System;

namespace ZXing.Datamatrix.Internal
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

		internal static DataBlock[] getDataBlocks(byte[] rawCodewords, Version version)
		{
			Version.ECBlocks eCBlocks = version.getECBlocks();
			int num = 0;
			Version.ECB[] eCBlocksValue = eCBlocks.ECBlocksValue;
			Version.ECB[] array = eCBlocksValue;
			foreach (Version.ECB eCB in array)
			{
				num += eCB.Count;
			}
			DataBlock[] array2 = new DataBlock[num];
			int num2 = 0;
			Version.ECB[] array3 = eCBlocksValue;
			foreach (Version.ECB eCB2 in array3)
			{
				for (int k = 0; k < eCB2.Count; k++)
				{
					int dataCodewords = eCB2.DataCodewords;
					int num3 = eCBlocks.ECCodewords + dataCodewords;
					array2[num2++] = new DataBlock(dataCodewords, new byte[num3]);
				}
			}
			int num4 = array2[0].codewords.Length;
			int num5 = num4 - eCBlocks.ECCodewords;
			int num6 = num5 - 1;
			int num7 = 0;
			for (int l = 0; l < num6; l++)
			{
				for (int m = 0; m < num2; m++)
				{
					array2[m].codewords[l] = rawCodewords[num7++];
				}
			}
			bool flag = version.getVersionNumber() == 24;
			int num8 = ((!flag) ? num2 : 8);
			for (int n = 0; n < num8; n++)
			{
				array2[n].codewords[num5 - 1] = rawCodewords[num7++];
			}
			int num9 = array2[0].codewords.Length;
			for (int num10 = num5; num10 < num9; num10++)
			{
				for (int num11 = 0; num11 < num2; num11++)
				{
					int num12 = ((!flag || num11 <= 7) ? num10 : (num10 - 1));
					array2[num11].codewords[num12] = rawCodewords[num7++];
				}
			}
			if (num7 != rawCodewords.Length)
			{
				throw new ArgumentException();
			}
			return array2;
		}
	}
}
