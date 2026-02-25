using System;

namespace ZXing
{
	public class RGBLuminanceSource : BaseLuminanceSource
	{
		public enum BitmapFormat
		{
			Unknown = 0,
			Gray8 = 1,
			RGB24 = 2,
			RGB32 = 3,
			ARGB32 = 4,
			BGR24 = 5,
			BGR32 = 6,
			BGRA32 = 7,
			RGB565 = 8,
			RGBA32 = 9
		}

		protected RGBLuminanceSource(int width, int height)
			: base(width, height)
		{
		}

		public RGBLuminanceSource(byte[] rgbRawBytes, int width, int height)
			: this(rgbRawBytes, width, height, BitmapFormat.RGB24)
		{
		}

		[Obsolete("Use RGBLuminanceSource(luminanceArray, width, height, BitmapFormat.Gray8)")]
		public RGBLuminanceSource(byte[] luminanceArray, int width, int height, bool is8Bit)
			: this(luminanceArray, width, height, BitmapFormat.Gray8)
		{
		}

		public RGBLuminanceSource(byte[] rgbRawBytes, int width, int height, BitmapFormat bitmapFormat)
			: base(width, height)
		{
			CalculateLuminance(rgbRawBytes, bitmapFormat);
		}

		protected override LuminanceSource CreateLuminanceSource(byte[] newLuminances, int width, int height)
		{
			RGBLuminanceSource rGBLuminanceSource = new RGBLuminanceSource(width, height);
			rGBLuminanceSource.luminances = newLuminances;
			return rGBLuminanceSource;
		}

		private static BitmapFormat DetermineBitmapFormat(byte[] rgbRawBytes, int width, int height)
		{
			int num = width * height;
			switch (rgbRawBytes.Length / num)
			{
			case 1:
				return BitmapFormat.Gray8;
			case 2:
				return BitmapFormat.RGB565;
			case 3:
				return BitmapFormat.RGB24;
			case 4:
				return BitmapFormat.RGB32;
			default:
				throw new ArgumentException("The bitmap format could not be determined. Please specify the correct value.");
			}
		}

		protected void CalculateLuminance(byte[] rgbRawBytes, BitmapFormat bitmapFormat)
		{
			if (bitmapFormat == BitmapFormat.Unknown)
			{
				bitmapFormat = DetermineBitmapFormat(rgbRawBytes, Width, Height);
			}
			switch (bitmapFormat)
			{
			case BitmapFormat.Gray8:
				Buffer.BlockCopy(rgbRawBytes, 0, luminances, 0, (rgbRawBytes.Length >= luminances.Length) ? luminances.Length : rgbRawBytes.Length);
				break;
			case BitmapFormat.RGB24:
				CalculateLuminanceRGB24(rgbRawBytes);
				break;
			case BitmapFormat.BGR24:
				CalculateLuminanceBGR24(rgbRawBytes);
				break;
			case BitmapFormat.RGB32:
				CalculateLuminanceRGB32(rgbRawBytes);
				break;
			case BitmapFormat.BGR32:
				CalculateLuminanceBGR32(rgbRawBytes);
				break;
			case BitmapFormat.RGBA32:
				CalculateLuminanceRGBA32(rgbRawBytes);
				break;
			case BitmapFormat.ARGB32:
				CalculateLuminanceARGB32(rgbRawBytes);
				break;
			case BitmapFormat.BGRA32:
				CalculateLuminanceBGRA32(rgbRawBytes);
				break;
			case BitmapFormat.RGB565:
				CalculateLuminanceRGB565(rgbRawBytes);
				break;
			default:
				throw new ArgumentException("The bitmap format isn't supported.", bitmapFormat.ToString());
			}
		}

		private void CalculateLuminanceRGB565(byte[] rgb565RawData)
		{
			int num = 0;
			int num2 = 0;
			while (num2 < rgb565RawData.Length && num < luminances.Length)
			{
				byte b = rgb565RawData[num2];
				byte b2 = rgb565RawData[num2 + 1];
				int num3 = b & 0x1F;
				int num4 = (((b & 0xE0) >> 5) | ((b2 & 3) << 3)) & 0x1F;
				int num5 = (b2 >> 2) & 0x1F;
				int num6 = num5 * 527 + 23 >> 6;
				int num7 = num4 * 527 + 23 >> 6;
				int num8 = num3 * 527 + 23 >> 6;
				luminances[num] = (byte)(19562 * num6 + 38550 * num7 + 7424 * num8 >> 16);
				num2 += 2;
				num++;
			}
		}

		private void CalculateLuminanceRGB24(byte[] rgbRawBytes)
		{
			int num = 0;
			int num2 = 0;
			while (num < rgbRawBytes.Length && num2 < luminances.Length)
			{
				int num3 = rgbRawBytes[num++];
				int num4 = rgbRawBytes[num++];
				int num5 = rgbRawBytes[num++];
				luminances[num2] = (byte)(19562 * num3 + 38550 * num4 + 7424 * num5 >> 16);
				num2++;
			}
		}

		private void CalculateLuminanceBGR24(byte[] rgbRawBytes)
		{
			int num = 0;
			int num2 = 0;
			while (num < rgbRawBytes.Length && num2 < luminances.Length)
			{
				int num3 = rgbRawBytes[num++];
				int num4 = rgbRawBytes[num++];
				int num5 = rgbRawBytes[num++];
				luminances[num2] = (byte)(19562 * num5 + 38550 * num4 + 7424 * num3 >> 16);
				num2++;
			}
		}

		private void CalculateLuminanceRGB32(byte[] rgbRawBytes)
		{
			int num = 0;
			int num2 = 0;
			while (num < rgbRawBytes.Length && num2 < luminances.Length)
			{
				int num3 = rgbRawBytes[num++];
				int num4 = rgbRawBytes[num++];
				int num5 = rgbRawBytes[num++];
				num++;
				luminances[num2] = (byte)(19562 * num3 + 38550 * num4 + 7424 * num5 >> 16);
				num2++;
			}
		}

		private void CalculateLuminanceBGR32(byte[] rgbRawBytes)
		{
			int num = 0;
			int num2 = 0;
			while (num < rgbRawBytes.Length && num2 < luminances.Length)
			{
				int num3 = rgbRawBytes[num++];
				int num4 = rgbRawBytes[num++];
				int num5 = rgbRawBytes[num++];
				num++;
				luminances[num2] = (byte)(19562 * num5 + 38550 * num4 + 7424 * num3 >> 16);
				num2++;
			}
		}

		private void CalculateLuminanceBGRA32(byte[] rgbRawBytes)
		{
			int num = 0;
			int num2 = 0;
			while (num < rgbRawBytes.Length && num2 < luminances.Length)
			{
				byte b = rgbRawBytes[num++];
				byte b2 = rgbRawBytes[num++];
				byte b3 = rgbRawBytes[num++];
				byte b4 = rgbRawBytes[num++];
				byte b5 = (byte)(19562 * b3 + 38550 * b2 + 7424 * b >> 16);
				luminances[num2] = (byte)((b5 * b4 >> 8) + (255 * (255 - b4) >> 8));
				num2++;
			}
		}

		private void CalculateLuminanceRGBA32(byte[] rgbRawBytes)
		{
			int num = 0;
			int num2 = 0;
			while (num < rgbRawBytes.Length && num2 < luminances.Length)
			{
				byte b = rgbRawBytes[num++];
				byte b2 = rgbRawBytes[num++];
				byte b3 = rgbRawBytes[num++];
				byte b4 = rgbRawBytes[num++];
				byte b5 = (byte)(19562 * b + 38550 * b2 + 7424 * b3 >> 16);
				luminances[num2] = (byte)((b5 * b4 >> 8) + (255 * (255 - b4) >> 8));
				num2++;
			}
		}

		private void CalculateLuminanceARGB32(byte[] rgbRawBytes)
		{
			int num = 0;
			int num2 = 0;
			while (num < rgbRawBytes.Length && num2 < luminances.Length)
			{
				byte b = rgbRawBytes[num++];
				byte b2 = rgbRawBytes[num++];
				byte b3 = rgbRawBytes[num++];
				byte b4 = rgbRawBytes[num++];
				byte b5 = (byte)(19562 * b2 + 38550 * b3 + 7424 * b4 >> 16);
				luminances[num2] = (byte)((b5 * b >> 8) + (255 * (255 - b) >> 8));
				num2++;
			}
		}
	}
}
