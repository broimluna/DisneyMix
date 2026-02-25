using System;

namespace ZXing
{
	public abstract class BaseLuminanceSource : LuminanceSource
	{
		protected const int RChannelWeight = 19562;

		protected const int GChannelWeight = 38550;

		protected const int BChannelWeight = 7424;

		protected const int ChannelWeight = 16;

		protected byte[] luminances;

		public override byte[] Matrix
		{
			get
			{
				return luminances;
			}
		}

		public override bool RotateSupported
		{
			get
			{
				return true;
			}
		}

		public override bool CropSupported
		{
			get
			{
				return true;
			}
		}

		public override bool InversionSupported
		{
			get
			{
				return true;
			}
		}

		protected BaseLuminanceSource(int width, int height)
			: base(width, height)
		{
			luminances = new byte[width * height];
		}

		protected BaseLuminanceSource(byte[] luminanceArray, int width, int height)
			: base(width, height)
		{
			luminances = new byte[width * height];
			Buffer.BlockCopy(luminanceArray, 0, luminances, 0, width * height);
		}

		public override byte[] getRow(int y, byte[] row)
		{
			int num = Width;
			if (row == null || row.Length < num)
			{
				row = new byte[num];
			}
			for (int i = 0; i < num; i++)
			{
				row[i] = luminances[y * num + i];
			}
			return row;
		}

		public override LuminanceSource rotateCounterClockwise()
		{
			byte[] array = new byte[Width * Height];
			int num = Height;
			int num2 = Width;
			byte[] matrix = Matrix;
			for (int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					int num3 = num2 - j - 1;
					int num4 = i;
					array[num3 * num + num4] = matrix[i * Width + j];
				}
			}
			return CreateLuminanceSource(array, num, num2);
		}

		public override LuminanceSource rotateCounterClockwise45()
		{
			return base.rotateCounterClockwise45();
		}

		public override LuminanceSource crop(int left, int top, int width, int height)
		{
			if (left + width > Width || top + height > Height)
			{
				throw new ArgumentException("Crop rectangle does not fit within image data.");
			}
			byte[] array = new byte[width * height];
			byte[] matrix = Matrix;
			int num = Width;
			int num2 = left + width;
			int num3 = top + height;
			int num4 = top;
			int num5 = 0;
			while (num4 < num3)
			{
				int num6 = left;
				int num7 = 0;
				while (num6 < num2)
				{
					array[num5 * width + num7] = matrix[num4 * num + num6];
					num6++;
					num7++;
				}
				num4++;
				num5++;
			}
			return CreateLuminanceSource(array, width, height);
		}

		public override LuminanceSource invert()
		{
			return new InvertedLuminanceSource(this);
		}

		protected abstract LuminanceSource CreateLuminanceSource(byte[] newLuminances, int width, int height);
	}
}
