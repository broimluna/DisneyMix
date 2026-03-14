using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class Code39Writer : OneDimensionalCodeWriter
	{
		public override BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (format != BarcodeFormat.CODE_39)
			{
				throw new ArgumentException("Can only encode CODE_39, but got " + format);
			}
			return base.encode(contents, format, width, height, hints);
		}

		public override bool[] encode(string contents)
		{
			int length = contents.Length;
			if (length > 80)
			{
				throw new ArgumentException("Requested contents should be less than 80 digits long, but got " + length);
			}
			for (int i = 0; i < length; i++)
			{
				int num = Code39Reader.ALPHABET_STRING.IndexOf(contents[i]);
				if (num < 0)
				{
					throw new ArgumentException("Requested contents contains a not encodable character: '" + contents[i] + "'");
				}
			}
			int[] array = new int[9];
			int num2 = 25 + length;
			for (int j = 0; j < length; j++)
			{
				int num3 = Code39Reader.ALPHABET_STRING.IndexOf(contents[j]);
				if (num3 < 0)
				{
					throw new ArgumentException("Bad contents: " + contents);
				}
				toIntArray(Code39Reader.CHARACTER_ENCODINGS[num3], array);
				int[] array2 = array;
				foreach (int num4 in array2)
				{
					num2 += num4;
				}
			}
			bool[] array3 = new bool[num2];
			toIntArray(Code39Reader.CHARACTER_ENCODINGS[39], array);
			int num5 = OneDimensionalCodeWriter.appendPattern(array3, 0, array, true);
			int[] pattern = new int[1] { 1 };
			num5 += OneDimensionalCodeWriter.appendPattern(array3, num5, pattern, false);
			for (int l = 0; l < length; l++)
			{
				int num6 = Code39Reader.ALPHABET_STRING.IndexOf(contents[l]);
				toIntArray(Code39Reader.CHARACTER_ENCODINGS[num6], array);
				num5 += OneDimensionalCodeWriter.appendPattern(array3, num5, array, true);
				num5 += OneDimensionalCodeWriter.appendPattern(array3, num5, pattern, false);
			}
			toIntArray(Code39Reader.CHARACTER_ENCODINGS[39], array);
			OneDimensionalCodeWriter.appendPattern(array3, num5, array, true);
			return array3;
		}

		private static void toIntArray(int a, int[] toReturn)
		{
			for (int i = 0; i < 9; i++)
			{
				int num = a & (1 << 8 - i);
				toReturn[i] = ((num == 0) ? 1 : 2);
			}
		}
	}
}
