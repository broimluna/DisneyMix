using System;

namespace ZXing
{
	[Obsolete("Use RGBLuminanceSource with the argument BitmapFormat.RGB565")]
	public class RGB565LuminanceSource : RGBLuminanceSource
	{
		protected RGB565LuminanceSource(int width, int height)
			: base(width, height)
		{
		}

		public RGB565LuminanceSource(byte[] rgb565RawData, int width, int height)
			: base(rgb565RawData, width, height, BitmapFormat.RGB565)
		{
		}

		protected override LuminanceSource CreateLuminanceSource(byte[] newLuminances, int width, int height)
		{
			RGB565LuminanceSource rGB565LuminanceSource = new RGB565LuminanceSource(width, height);
			rGB565LuminanceSource.luminances = newLuminances;
			return rGB565LuminanceSource;
		}
	}
}
