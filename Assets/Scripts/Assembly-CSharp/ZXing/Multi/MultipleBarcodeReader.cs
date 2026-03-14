using System.Collections.Generic;

namespace ZXing.Multi
{
	public interface MultipleBarcodeReader
	{
		Result[] decodeMultiple(BinaryBitmap image);

		Result[] decodeMultiple(BinaryBitmap image, IDictionary<DecodeHintType, object> hints);
	}
}
