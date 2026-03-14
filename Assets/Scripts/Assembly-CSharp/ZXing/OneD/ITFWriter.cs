using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class ITFWriter : OneDimensionalCodeWriter
	{
		private static readonly int[] START_PATTERN = new int[4] { 1, 1, 1, 1 };

		private static readonly int[] END_PATTERN = new int[3] { 3, 1, 1 };

		public override BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (format != BarcodeFormat.ITF)
			{
				throw new ArgumentException("Can only encode ITF, but got " + format);
			}
			return base.encode(contents, format, width, height, hints);
		}

		public override bool[] encode(string contents)
		{
			int length = contents.Length;
			if (length % 2 != 0)
			{
				throw new ArgumentException("The lenght of the input should be even");
			}
			if (length > 80)
			{
				throw new ArgumentException("Requested contents should be less than 80 digits long, but got " + length);
			}
			for (int i = 0; i < length; i++)
			{
				if (!char.IsDigit(contents[i]))
				{
					throw new ArgumentException("Requested contents should only contain digits, but got '" + contents[i] + "'");
				}
			}
			bool[] array = new bool[9 + 9 * length];
			int num = OneDimensionalCodeWriter.appendPattern(array, 0, START_PATTERN, true);
			for (int j = 0; j < length; j += 2)
			{
				int num2 = Convert.ToInt32(contents[j].ToString(), 10);
				int num3 = Convert.ToInt32(contents[j + 1].ToString(), 10);
				int[] array2 = new int[18];
				for (int k = 0; k < 5; k++)
				{
					array2[k << 1] = ITFReader.PATTERNS[num2][k];
					array2[(k << 1) + 1] = ITFReader.PATTERNS[num3][k];
				}
				num += OneDimensionalCodeWriter.appendPattern(array, num, array2, true);
			}
			OneDimensionalCodeWriter.appendPattern(array, num, END_PATTERN, true);
			return array;
		}
	}
}
