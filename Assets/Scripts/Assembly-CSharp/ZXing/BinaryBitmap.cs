using System;
using ZXing.Common;

namespace ZXing
{
	public sealed class BinaryBitmap
	{
		private Binarizer binarizer;

		private BitMatrix matrix;

		public int Width
		{
			get
			{
				return binarizer.Width;
			}
		}

		public int Height
		{
			get
			{
				return binarizer.Height;
			}
		}

		public BitMatrix BlackMatrix
		{
			get
			{
				if (matrix == null)
				{
					matrix = binarizer.BlackMatrix;
				}
				return matrix;
			}
		}

		public bool CropSupported
		{
			get
			{
				return binarizer.LuminanceSource.CropSupported;
			}
		}

		public bool RotateSupported
		{
			get
			{
				return binarizer.LuminanceSource.RotateSupported;
			}
		}

		public BinaryBitmap(Binarizer binarizer)
		{
			if (binarizer == null)
			{
				throw new ArgumentException("Binarizer must be non-null.");
			}
			this.binarizer = binarizer;
		}

		public BitArray getBlackRow(int y, BitArray row)
		{
			return binarizer.getBlackRow(y, row);
		}

		public BinaryBitmap crop(int left, int top, int width, int height)
		{
			LuminanceSource source = binarizer.LuminanceSource.crop(left, top, width, height);
			return new BinaryBitmap(binarizer.createBinarizer(source));
		}

		public BinaryBitmap rotateCounterClockwise()
		{
			LuminanceSource source = binarizer.LuminanceSource.rotateCounterClockwise();
			return new BinaryBitmap(binarizer.createBinarizer(source));
		}

		public BinaryBitmap rotateCounterClockwise45()
		{
			LuminanceSource source = binarizer.LuminanceSource.rotateCounterClockwise45();
			return new BinaryBitmap(binarizer.createBinarizer(source));
		}

		public override string ToString()
		{
			BitMatrix blackMatrix = BlackMatrix;
			return (blackMatrix == null) ? string.Empty : blackMatrix.ToString();
		}
	}
}
