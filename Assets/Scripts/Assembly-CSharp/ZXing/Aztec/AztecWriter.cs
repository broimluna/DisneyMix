using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Aztec.Internal;
using ZXing.Common;

namespace ZXing.Aztec
{
	public sealed class AztecWriter : Writer
	{
		private static readonly Encoding DEFAULT_CHARSET;

		static AztecWriter()
		{
			DEFAULT_CHARSET = Encoding.GetEncoding("ISO-8859-1");
		}

		public BitMatrix encode(string contents, BarcodeFormat format, int width, int height)
		{
			return encode(contents, format, width, height, null);
		}

		public BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			Encoding charset = DEFAULT_CHARSET;
			int eccPercent = 33;
			int layers = 0;
			if (hints != null)
			{
				if (hints.ContainsKey(EncodeHintType.CHARACTER_SET))
				{
					object obj = hints[EncodeHintType.CHARACTER_SET];
					if (obj != null)
					{
						charset = Encoding.GetEncoding(obj.ToString());
					}
				}
				if (hints.ContainsKey(EncodeHintType.ERROR_CORRECTION))
				{
					object obj2 = hints[EncodeHintType.ERROR_CORRECTION];
					if (obj2 != null)
					{
						eccPercent = Convert.ToInt32(obj2);
					}
				}
				if (hints.ContainsKey(EncodeHintType.AZTEC_LAYERS))
				{
					object obj3 = hints[EncodeHintType.AZTEC_LAYERS];
					if (obj3 != null)
					{
						layers = Convert.ToInt32(obj3);
					}
				}
			}
			return encode(contents, format, width, height, charset, eccPercent, layers);
		}

		private static BitMatrix encode(string contents, BarcodeFormat format, int width, int height, Encoding charset, int eccPercent, int layers)
		{
			if (format != BarcodeFormat.AZTEC)
			{
				throw new ArgumentException("Can only encode AZTEC code, but got " + format);
			}
			AztecCode code = ZXing.Aztec.Internal.Encoder.encode(charset.GetBytes(contents), eccPercent, layers);
			return renderResult(code, width, height);
		}

		private static BitMatrix renderResult(AztecCode code, int width, int height)
		{
			BitMatrix matrix = code.Matrix;
			if (matrix == null)
			{
				throw new InvalidOperationException("No input code matrix");
			}
			int width2 = matrix.Width;
			int height2 = matrix.Height;
			int num = Math.Max(width, width2);
			int num2 = Math.Max(height, height2);
			int num3 = Math.Min(num / width2, num2 / height2);
			int num4 = (num - width2 * num3) / 2;
			int num5 = (num2 - height2 * num3) / 2;
			BitMatrix bitMatrix = new BitMatrix(num, num2);
			int num6 = 0;
			int num7 = num5;
			while (num6 < height2)
			{
				int num8 = 0;
				int num9 = num4;
				while (num8 < width2)
				{
					if (matrix[num8, num6])
					{
						bitMatrix.setRegion(num9, num7, num3, num3);
					}
					num8++;
					num9 += num3;
				}
				num6++;
				num7 += num3;
			}
			return bitMatrix;
		}
	}
}
