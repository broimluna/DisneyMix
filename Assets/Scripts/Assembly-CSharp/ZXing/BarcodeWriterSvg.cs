using ZXing.Rendering;

namespace ZXing
{
	public class BarcodeWriterSvg : BarcodeWriterGeneric<SvgRenderer.SvgImage>
	{
		public BarcodeWriterSvg()
		{
			base.Renderer = new SvgRenderer();
		}
	}
}
