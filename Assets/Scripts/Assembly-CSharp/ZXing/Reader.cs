using System.Collections.Generic;

namespace ZXing
{
	public interface Reader
	{
		Result decode(BinaryBitmap image);

		Result decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints);

		void reset();
	}
}
