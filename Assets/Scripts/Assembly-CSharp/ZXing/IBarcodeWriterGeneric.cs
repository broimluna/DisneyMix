using ZXing.Common;

namespace ZXing
{
	public interface IBarcodeWriterGeneric<out TOutput>
	{
		BitMatrix Encode(string contents);

		TOutput Write(string contents);

		TOutput Write(BitMatrix matrix);
	}
}
