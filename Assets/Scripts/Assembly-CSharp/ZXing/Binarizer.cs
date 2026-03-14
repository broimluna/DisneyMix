using System;
using ZXing.Common;

namespace ZXing
{
	public abstract class Binarizer
	{
		private readonly LuminanceSource source;

		public virtual LuminanceSource LuminanceSource
		{
			get
			{
				return source;
			}
		}

		public abstract BitMatrix BlackMatrix { get; }

		public int Width
		{
			get
			{
				return source.Width;
			}
		}

		public int Height
		{
			get
			{
				return source.Height;
			}
		}

		protected internal Binarizer(LuminanceSource source)
		{
			if (source == null)
			{
				throw new ArgumentException("Source must be non-null.");
			}
			this.source = source;
		}

		public abstract BitArray getBlackRow(int y, BitArray row);

		public abstract Binarizer createBinarizer(LuminanceSource source);
	}
}
