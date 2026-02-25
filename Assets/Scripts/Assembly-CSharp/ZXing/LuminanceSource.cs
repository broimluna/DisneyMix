using System;
using System.Text;

namespace ZXing
{
	public abstract class LuminanceSource
	{
		private int width;

		private int height;

		public abstract byte[] Matrix { get; }

		public virtual int Width
		{
			get
			{
				return width;
			}
			protected set
			{
				width = value;
			}
		}

		public virtual int Height
		{
			get
			{
				return height;
			}
			protected set
			{
				height = value;
			}
		}

		public virtual bool CropSupported
		{
			get
			{
				return false;
			}
		}

		public virtual bool RotateSupported
		{
			get
			{
				return false;
			}
		}

		public virtual bool InversionSupported
		{
			get
			{
				return false;
			}
		}

		protected LuminanceSource(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		public abstract byte[] getRow(int y, byte[] row);

		public virtual LuminanceSource crop(int left, int top, int width, int height)
		{
			throw new NotSupportedException("This luminance source does not support cropping.");
		}

		public virtual LuminanceSource rotateCounterClockwise()
		{
			throw new NotSupportedException("This luminance source does not support rotation.");
		}

		public virtual LuminanceSource rotateCounterClockwise45()
		{
			throw new NotSupportedException("This luminance source does not support rotation by 45 degrees.");
		}

		public virtual LuminanceSource invert()
		{
			throw new NotSupportedException("This luminance source does not support inversion.");
		}

		public override string ToString()
		{
			byte[] array = new byte[width];
			StringBuilder stringBuilder = new StringBuilder(height * (width + 1));
			for (int i = 0; i < height; i++)
			{
				array = getRow(i, array);
				for (int j = 0; j < width; j++)
				{
					int num = array[j] & 0xFF;
					char value = ((num >= 64) ? ((num >= 128) ? ((num >= 192) ? ' ' : '.') : '+') : '#');
					stringBuilder.Append(value);
				}
				stringBuilder.Append('\n');
			}
			return stringBuilder.ToString();
		}
	}
}
