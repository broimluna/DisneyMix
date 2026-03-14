namespace ZXing
{
	public sealed class InvertedLuminanceSource : LuminanceSource
	{
		private readonly LuminanceSource @delegate;

		private byte[] invertedMatrix;

		public override byte[] Matrix
		{
			get
			{
				if (invertedMatrix == null)
				{
					byte[] matrix = @delegate.Matrix;
					int num = Width * Height;
					invertedMatrix = new byte[num];
					for (int i = 0; i < num; i++)
					{
						invertedMatrix[i] = (byte)(255 - (matrix[i] & 0xFF));
					}
				}
				return invertedMatrix;
			}
		}

		public override bool CropSupported
		{
			get
			{
				return @delegate.CropSupported;
			}
		}

		public override bool RotateSupported
		{
			get
			{
				return @delegate.RotateSupported;
			}
		}

		public InvertedLuminanceSource(LuminanceSource @delegate)
			: base(@delegate.Width, @delegate.Height)
		{
			this.@delegate = @delegate;
		}

		public override byte[] getRow(int y, byte[] row)
		{
			row = @delegate.getRow(y, row);
			int num = Width;
			for (int i = 0; i < num; i++)
			{
				row[i] = (byte)(255 - (row[i] & 0xFF));
			}
			return row;
		}

		public override LuminanceSource crop(int left, int top, int width, int height)
		{
			return new InvertedLuminanceSource(@delegate.crop(left, top, width, height));
		}

		public override LuminanceSource invert()
		{
			return @delegate;
		}

		public override LuminanceSource rotateCounterClockwise()
		{
			return new InvertedLuminanceSource(@delegate.rotateCounterClockwise());
		}

		public override LuminanceSource rotateCounterClockwise45()
		{
			return new InvertedLuminanceSource(@delegate.rotateCounterClockwise45());
		}
	}
}
