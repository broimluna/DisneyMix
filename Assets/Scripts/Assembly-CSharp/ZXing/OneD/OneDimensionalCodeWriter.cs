using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD
{
	public abstract class OneDimensionalCodeWriter : Writer
	{
		public virtual int DefaultMargin
		{
			get
			{
				return 10;
			}
		}

		public BitMatrix encode(string contents, BarcodeFormat format, int width, int height)
		{
			return encode(contents, format, width, height, null);
		}

		public virtual BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (string.IsNullOrEmpty(contents))
			{
				throw new ArgumentException("Found empty contents");
			}
			if (width < 0 || height < 0)
			{
				throw new ArgumentException("Negative size is not allowed. Input: " + width + 'x' + height);
			}
			int sidesMargin = DefaultMargin;
			if (hints != null)
			{
				int? num = ((!hints.ContainsKey(EncodeHintType.MARGIN)) ? ((int?)null) : new int?((int)hints[EncodeHintType.MARGIN]));
				if (num.HasValue)
				{
					sidesMargin = num.Value;
				}
			}
			bool[] code = encode(contents);
			return renderResult(code, width, height, sidesMargin);
		}

		private static BitMatrix renderResult(bool[] code, int width, int height, int sidesMargin)
		{
			int num = code.Length;
			int num2 = num + sidesMargin;
			int num3 = Math.Max(width, num2);
			int height2 = Math.Max(1, height);
			int num4 = num3 / num2;
			int num5 = (num3 - num * num4) / 2;
			BitMatrix bitMatrix = new BitMatrix(num3, height2);
			int num6 = 0;
			int num7 = num5;
			while (num6 < num)
			{
				if (code[num6])
				{
					bitMatrix.setRegion(num7, 0, num4, height2);
				}
				num6++;
				num7 += num4;
			}
			return bitMatrix;
		}

		protected static int appendPattern(bool[] target, int pos, int[] pattern, bool startColor)
		{
			bool flag = startColor;
			int num = 0;
			foreach (int num2 in pattern)
			{
				for (int j = 0; j < num2; j++)
				{
					target[pos++] = flag;
				}
				num += num2;
				flag = !flag;
			}
			return num;
		}

		public abstract bool[] encode(string contents);

		public static string CalculateChecksumDigitModulo10(string contents)
		{
			int num = 0;
			int num2 = 0;
			for (int num3 = contents.Length - 1; num3 >= 0; num3 -= 2)
			{
				num += contents[num3] - 48;
			}
			for (int num4 = contents.Length - 2; num4 >= 0; num4 -= 2)
			{
				num2 += contents[num4] - 48;
			}
			return contents + (10 - (num * 3 + num2) % 10) % 10;
		}
	}
}
