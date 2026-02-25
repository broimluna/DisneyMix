using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class MSIWriter : OneDimensionalCodeWriter
	{
		private static readonly int[] startWidths = new int[2] { 2, 1 };

		private static readonly int[] endWidths = new int[3] { 1, 2, 1 };

		private static readonly int[][] numberWidths = new int[10][]
		{
			new int[8] { 1, 2, 1, 2, 1, 2, 1, 2 },
			new int[8] { 1, 2, 1, 2, 1, 2, 2, 1 },
			new int[8] { 1, 2, 1, 2, 2, 1, 1, 2 },
			new int[8] { 1, 2, 1, 2, 2, 1, 2, 1 },
			new int[8] { 1, 2, 2, 1, 1, 2, 1, 2 },
			new int[8] { 1, 2, 2, 1, 1, 2, 2, 1 },
			new int[8] { 1, 2, 2, 1, 2, 1, 1, 2 },
			new int[8] { 1, 2, 2, 1, 2, 1, 2, 1 },
			new int[8] { 2, 1, 1, 2, 1, 2, 1, 2 },
			new int[8] { 2, 1, 1, 2, 1, 2, 2, 1 }
		};

		public override BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (format != BarcodeFormat.MSI)
			{
				throw new ArgumentException("Can only encode MSI, but got " + format);
			}
			return base.encode(contents, format, width, height, hints);
		}

		public override bool[] encode(string contents)
		{
			int length = contents.Length;
			for (int i = 0; i < length; i++)
			{
				int num = MSIReader.ALPHABET_STRING.IndexOf(contents[i]);
				if (num < 0)
				{
					throw new ArgumentException("Requested contents contains a not encodable character: '" + contents[i] + "'");
				}
			}
			int num2 = 3 + length * 12 + 4;
			bool[] array = new bool[num2];
			int num3 = OneDimensionalCodeWriter.appendPattern(array, 0, startWidths, true);
			for (int j = 0; j < length; j++)
			{
				int num4 = MSIReader.ALPHABET_STRING.IndexOf(contents[j]);
				int[] pattern = numberWidths[num4];
				num3 += OneDimensionalCodeWriter.appendPattern(array, num3, pattern, true);
			}
			OneDimensionalCodeWriter.appendPattern(array, num3, endWidths, true);
			return array;
		}
	}
}
