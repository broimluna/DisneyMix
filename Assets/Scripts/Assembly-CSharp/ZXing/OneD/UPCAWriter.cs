using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD
{
	public class UPCAWriter : Writer
	{
		private readonly EAN13Writer subWriter = new EAN13Writer();

		public BitMatrix encode(string contents, BarcodeFormat format, int width, int height)
		{
			return encode(contents, format, width, height, null);
		}

		public BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (format != BarcodeFormat.UPC_A)
			{
				throw new ArgumentException("Can only encode UPC-A, but got " + format);
			}
			return subWriter.encode(preencode(contents), BarcodeFormat.EAN_13, width, height, hints);
		}

		private static string preencode(string contents)
		{
			switch (contents.Length)
			{
			case 11:
			{
				int num = 0;
				for (int i = 0; i < 11; i++)
				{
					num += (contents[i] - 48) * ((i % 2 != 0) ? 1 : 3);
				}
				contents += (1000 - num) % 10;
				break;
			}
			default:
				throw new ArgumentException("Requested contents should be 11 or 12 digits long, but got " + contents.Length);
			case 12:
				break;
			}
			return '0' + contents;
		}
	}
}
