using ZXing.Common;

namespace ZXing.Rendering
{
	public interface IBarcodeRenderer<out TOutput>
	{
		TOutput Render(BitMatrix matrix, BarcodeFormat format, string content);

		TOutput Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options);
	}
}
