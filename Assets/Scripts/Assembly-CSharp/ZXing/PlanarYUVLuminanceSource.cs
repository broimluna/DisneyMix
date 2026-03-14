using System;

namespace ZXing
{
	public sealed class PlanarYUVLuminanceSource : BaseLuminanceSource
	{
		private const int THUMBNAIL_SCALE_FACTOR = 2;

		private readonly byte[] yuvData;

		private readonly int dataWidth;

		private readonly int dataHeight;

		private readonly int left;

		private readonly int top;

		public override byte[] Matrix
		{
			get
			{
				int num = Width;
				int num2 = Height;
				if (num == dataWidth && num2 == dataHeight)
				{
					return yuvData;
				}
				int num3 = num * num2;
				byte[] array = new byte[num3];
				int num4 = top * dataWidth + left;
				if (num == dataWidth)
				{
					Array.Copy(yuvData, num4, array, 0, num3);
					return array;
				}
				byte[] sourceArray = yuvData;
				for (int i = 0; i < num2; i++)
				{
					int destinationIndex = i * num;
					Array.Copy(sourceArray, num4, array, destinationIndex, num);
					num4 += dataWidth;
				}
				return array;
			}
		}

		public override bool CropSupported
		{
			get
			{
				return true;
			}
		}

		public int ThumbnailWidth
		{
			get
			{
				return Width / 2;
			}
		}

		public int ThumbnailHeight
		{
			get
			{
				return Height / 2;
			}
		}

		public PlanarYUVLuminanceSource(byte[] yuvData, int dataWidth, int dataHeight, int left, int top, int width, int height, bool reverseHoriz)
			: base(width, height)
		{
			if (left + width > dataWidth || top + height > dataHeight)
			{
				throw new ArgumentException("Crop rectangle does not fit within image data.");
			}
			this.yuvData = yuvData;
			this.dataWidth = dataWidth;
			this.dataHeight = dataHeight;
			this.left = left;
			this.top = top;
			if (reverseHoriz)
			{
				reverseHorizontal(width, height);
			}
		}

		private PlanarYUVLuminanceSource(byte[] luminances, int width, int height)
			: base(width, height)
		{
			yuvData = luminances;
			base.luminances = luminances;
			dataWidth = width;
			dataHeight = height;
			left = 0;
			top = 0;
		}

		public override byte[] getRow(int y, byte[] row)
		{
			if (y < 0 || y >= Height)
			{
				throw new ArgumentException("Requested row is outside the image: " + y);
			}
			int num = Width;
			if (row == null || row.Length < num)
			{
				row = new byte[num];
			}
			int sourceIndex = (y + top) * dataWidth + left;
			Array.Copy(yuvData, sourceIndex, row, 0, num);
			return row;
		}

		public override LuminanceSource crop(int left, int top, int width, int height)
		{
			return new PlanarYUVLuminanceSource(yuvData, dataWidth, dataHeight, this.left + left, this.top + top, width, height, false);
		}

		public int[] renderThumbnail()
		{
			int num = Width / 2;
			int num2 = Height / 2;
			int[] array = new int[num * num2];
			byte[] array2 = yuvData;
			int num3 = top * dataWidth + left;
			for (int i = 0; i < num2; i++)
			{
				int num4 = i * num;
				for (int j = 0; j < num; j++)
				{
					int num5 = array2[num3 + j * 2] & 0xFF;
					array[num4 + j] = -16777216 | (num5 * 65793);
				}
				num3 += dataWidth * 2;
			}
			return array;
		}

		private void reverseHorizontal(int width, int height)
		{
			byte[] array = yuvData;
			int num = 0;
			int num2 = top * dataWidth + left;
			while (num < height)
			{
				int num3 = num2 + width / 2;
				int num4 = num2;
				int num5 = num2 + width - 1;
				while (num4 < num3)
				{
					byte b = array[num4];
					array[num4] = array[num5];
					array[num5] = b;
					num4++;
					num5--;
				}
				num++;
				num2 += dataWidth;
			}
		}

		protected override LuminanceSource CreateLuminanceSource(byte[] newLuminances, int width, int height)
		{
			return new PlanarYUVLuminanceSource(newLuminances, width, height);
		}
	}
}
