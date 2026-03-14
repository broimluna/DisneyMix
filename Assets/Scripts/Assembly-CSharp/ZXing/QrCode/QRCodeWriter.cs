using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing.QrCode.Internal;

namespace ZXing.QrCode
{
	public sealed class QRCodeWriter : Writer
	{
		private const int QUIET_ZONE_SIZE = 4;

		public BitMatrix encode(string contents, BarcodeFormat format, int width, int height)
		{
			return encode(contents, format, width, height, null);
		}

		public BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (string.IsNullOrEmpty(contents))
			{
				throw new ArgumentException("Found empty contents");
			}
			if (format != BarcodeFormat.QR_CODE)
			{
				throw new ArgumentException("Can only encode QR_CODE, but got " + format);
			}
			if (width < 0 || height < 0)
			{
				throw new ArgumentException("Requested dimensions are too small: " + width + 'x' + height);
			}
			ErrorCorrectionLevel ecLevel = ErrorCorrectionLevel.L;
			int quietZone = 4;
			if (hints != null)
			{
				ErrorCorrectionLevel errorCorrectionLevel = ((!hints.ContainsKey(EncodeHintType.ERROR_CORRECTION)) ? null : ((ErrorCorrectionLevel)hints[EncodeHintType.ERROR_CORRECTION]));
				if (errorCorrectionLevel != null)
				{
					ecLevel = errorCorrectionLevel;
				}
				int? num = ((!hints.ContainsKey(EncodeHintType.MARGIN)) ? ((int?)null) : new int?((int)hints[EncodeHintType.MARGIN]));
				if (num.HasValue)
				{
					quietZone = num.Value;
				}
			}
			QRCode code = Encoder.encode(contents, ecLevel, hints);
			return renderResult(code, width, height, quietZone);
		}

		private static BitMatrix renderResult(QRCode code, int width, int height, int quietZone)
		{
			ByteMatrix matrix = code.Matrix;
			if (matrix == null)
			{
				throw new InvalidOperationException();
			}
			int width2 = matrix.Width;
			int height2 = matrix.Height;
			int num = width2 + (quietZone << 1);
			int num2 = height2 + (quietZone << 1);
			int num3 = Math.Max(width, num);
			int num4 = Math.Max(height, num2);
			int num5 = Math.Min(num3 / num, num4 / num2);
			int num6 = (num3 - width2 * num5) / 2;
			int num7 = (num4 - height2 * num5) / 2;
			BitMatrix bitMatrix = new BitMatrix(num3, num4);
			int num8 = 0;
			int num9 = num7;
			while (num8 < height2)
			{
				int num10 = 0;
				int num11 = num6;
				while (num10 < width2)
				{
					if (matrix[num10, num8] == 1)
					{
						bitMatrix.setRegion(num11, num9, num5, num5);
					}
					num10++;
					num11 += num5;
				}
				num8++;
				num9 += num5;
			}
			return bitMatrix;
		}
	}
}
