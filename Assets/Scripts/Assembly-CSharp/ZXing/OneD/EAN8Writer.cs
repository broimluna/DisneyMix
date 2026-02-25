using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class EAN8Writer : UPCEANWriter
	{
		private const int CODE_WIDTH = 67;

		public override BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (format != BarcodeFormat.EAN_8)
			{
				throw new ArgumentException("Can only encode EAN_8, but got " + format);
			}
			return base.encode(contents, format, width, height, hints);
		}

		public override bool[] encode(string contents)
		{
			if (contents.Length < 7 || contents.Length > 8)
			{
				throw new ArgumentException("Requested contents should be 7 (without checksum digit) or 8 digits long, but got " + contents.Length);
			}
			string text = contents;
			foreach (char c in text)
			{
				if (!char.IsDigit(c))
				{
					throw new ArgumentException("Requested contents should only contain digits, but got '" + c + "'");
				}
			}
			if (contents.Length == 7)
			{
				contents = OneDimensionalCodeWriter.CalculateChecksumDigitModulo10(contents);
			}
			bool[] array = new bool[67];
			int num = 0;
			num += OneDimensionalCodeWriter.appendPattern(array, num, UPCEANReader.START_END_PATTERN, true);
			for (int j = 0; j <= 3; j++)
			{
				int num2 = int.Parse(contents.Substring(j, 1));
				num += OneDimensionalCodeWriter.appendPattern(array, num, UPCEANReader.L_PATTERNS[num2], false);
			}
			num += OneDimensionalCodeWriter.appendPattern(array, num, UPCEANReader.MIDDLE_PATTERN, false);
			for (int k = 4; k <= 7; k++)
			{
				int num3 = int.Parse(contents.Substring(k, 1));
				num += OneDimensionalCodeWriter.appendPattern(array, num, UPCEANReader.L_PATTERNS[num3], true);
			}
			OneDimensionalCodeWriter.appendPattern(array, num, UPCEANReader.START_END_PATTERN, true);
			return array;
		}
	}
}
