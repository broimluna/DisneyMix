using System.Collections.Generic;
using ZXing.Common;

namespace ZXing
{
	public interface Writer
	{
		BitMatrix encode(string contents, BarcodeFormat format, int width, int height);

		BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints);
	}
}
