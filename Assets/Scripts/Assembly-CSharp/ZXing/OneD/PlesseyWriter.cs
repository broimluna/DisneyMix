using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class PlesseyWriter : OneDimensionalCodeWriter
	{
		private const string ALPHABET_STRING = "0123456789ABCDEF";

		private static readonly int[] startWidths = new int[8] { 14, 11, 14, 11, 5, 20, 14, 11 };

		private static readonly int[] terminationWidths = new int[1] { 25 };

		private static readonly int[] endWidths = new int[8] { 20, 5, 20, 5, 14, 11, 14, 11 };

		private static readonly int[][] numberWidths = new int[16][]
		{
			new int[8] { 5, 20, 5, 20, 5, 20, 5, 20 },
			new int[8] { 14, 11, 5, 20, 5, 20, 5, 20 },
			new int[8] { 5, 20, 14, 11, 5, 20, 5, 20 },
			new int[8] { 14, 11, 14, 11, 5, 20, 5, 20 },
			new int[8] { 5, 20, 5, 20, 14, 11, 5, 20 },
			new int[8] { 14, 11, 5, 20, 14, 11, 5, 20 },
			new int[8] { 5, 20, 14, 11, 14, 11, 5, 20 },
			new int[8] { 14, 11, 14, 11, 14, 11, 5, 20 },
			new int[8] { 5, 20, 5, 20, 5, 20, 14, 11 },
			new int[8] { 14, 11, 5, 20, 5, 20, 14, 11 },
			new int[8] { 5, 20, 14, 11, 5, 20, 14, 11 },
			new int[8] { 14, 11, 14, 11, 5, 20, 14, 11 },
			new int[8] { 5, 20, 5, 20, 14, 11, 14, 11 },
			new int[8] { 14, 11, 5, 20, 14, 11, 14, 11 },
			new int[8] { 5, 20, 14, 11, 14, 11, 14, 11 },
			new int[8] { 14, 11, 14, 11, 14, 11, 14, 11 }
		};

		private static readonly byte[] crcGrid = new byte[9] { 1, 1, 1, 1, 0, 1, 0, 0, 1 };

		private static readonly int[] crc0Widths = new int[2] { 5, 20 };

		private static readonly int[] crc1Widths = new int[2] { 14, 11 };

		public override BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (format != BarcodeFormat.PLESSEY)
			{
				throw new ArgumentException("Can only encode Plessey, but got " + format);
			}
			return base.encode(contents, format, width, height, hints);
		}

		public override bool[] encode(string contents)
		{
			int length = contents.Length;
			for (int i = 0; i < length; i++)
			{
				int num = "0123456789ABCDEF".IndexOf(contents[i]);
				if (num < 0)
				{
					throw new ArgumentException("Requested contents contains a not encodable character: '" + contents[i] + "'");
				}
			}
			int num2 = 200 + length * 100 + 200 + 25 + 100 + 100;
			bool[] array = new bool[num2];
			byte[] array2 = new byte[4 * length + 8];
			int num3 = 0;
			int num4 = 100;
			num4 += OneDimensionalCodeWriter.appendPattern(array, num4, startWidths, true);
			for (int j = 0; j < length; j++)
			{
				int num5 = "0123456789ABCDEF".IndexOf(contents[j]);
				int[] pattern = numberWidths[num5];
				num4 += OneDimensionalCodeWriter.appendPattern(array, num4, pattern, true);
				array2[num3++] = (byte)(num5 & 1);
				array2[num3++] = (byte)((num5 >> 1) & 1);
				array2[num3++] = (byte)((num5 >> 2) & 1);
				array2[num3++] = (byte)((num5 >> 3) & 1);
			}
			for (int k = 0; k < 4 * length; k++)
			{
				if (array2[k] != 0)
				{
					for (int l = 0; l < 9; l++)
					{
						array2[k + l] ^= crcGrid[l];
					}
				}
			}
			for (int m = 0; m < 8; m++)
			{
				switch (array2[length * 4 + m])
				{
				case 0:
					num4 += OneDimensionalCodeWriter.appendPattern(array, num4, crc0Widths, true);
					break;
				case 1:
					num4 += OneDimensionalCodeWriter.appendPattern(array, num4, crc1Widths, true);
					break;
				}
			}
			num4 += OneDimensionalCodeWriter.appendPattern(array, num4, terminationWidths, true);
			OneDimensionalCodeWriter.appendPattern(array, num4, endWidths, false);
			return array;
		}
	}
}
