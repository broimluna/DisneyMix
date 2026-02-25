using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class EAN13Writer : UPCEANWriter
	{
		private const int CODE_WIDTH = 95;

		public override BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (format != BarcodeFormat.EAN_13)
			{
				throw new ArgumentException("Can only encode EAN_13, but got " + format);
			}
			return base.encode(contents, format, width, height, hints);
		}

		public override bool[] encode(string contents)
		{
			if (contents.Length < 12 || contents.Length > 13)
			{
				throw new ArgumentException("Requested contents should be 12 (without checksum digit) or 13 digits long, but got " + contents.Length);
			}
			string text = contents;
			foreach (char c in text)
			{
				if (!char.IsDigit(c))
				{
					throw new ArgumentException("Requested contents should only contain digits, but got '" + c + "'");
				}
			}
			if (contents.Length == 12)
			{
				contents = OneDimensionalCodeWriter.CalculateChecksumDigitModulo10(contents);
			}
			else if (!UPCEANReader.checkStandardUPCEANChecksum(contents))
			{
				throw new ArgumentException("Contents do not pass checksum");
			}
			int num = int.Parse(contents.Substring(0, 1));
			int num2 = EAN13Reader.FIRST_DIGIT_ENCODINGS[num];
			bool[] array = new bool[95];
			int num3 = 0;
			num3 += OneDimensionalCodeWriter.appendPattern(array, num3, UPCEANReader.START_END_PATTERN, true);
			for (int j = 1; j <= 6; j++)
			{
				int num4 = int.Parse(contents.Substring(j, 1));
				if (((num2 >> 6 - j) & 1) == 1)
				{
					num4 += 10;
				}
				num3 += OneDimensionalCodeWriter.appendPattern(array, num3, UPCEANReader.L_AND_G_PATTERNS[num4], false);
			}
			num3 += OneDimensionalCodeWriter.appendPattern(array, num3, UPCEANReader.MIDDLE_PATTERN, false);
			for (int k = 7; k <= 12; k++)
			{
				int num5 = int.Parse(contents.Substring(k, 1));
				num3 += OneDimensionalCodeWriter.appendPattern(array, num3, UPCEANReader.L_PATTERNS[num5], true);
			}
			OneDimensionalCodeWriter.appendPattern(array, num3, UPCEANReader.START_END_PATTERN, true);
			return array;
		}
	}
}
