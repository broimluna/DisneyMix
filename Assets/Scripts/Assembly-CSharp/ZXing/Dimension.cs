using System;

namespace ZXing
{
	public sealed class Dimension
	{
		private readonly int width;

		private readonly int height;

		public int Width
		{
			get
			{
				return width;
			}
		}

		public int Height
		{
			get
			{
				return height;
			}
		}

		public Dimension(int width, int height)
		{
			if (width < 0 || height < 0)
			{
				throw new ArgumentException();
			}
			this.width = width;
			this.height = height;
		}

		public override bool Equals(object other)
		{
			if (other is Dimension)
			{
				Dimension dimension = (Dimension)other;
				return width == dimension.width && height == dimension.height;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return width * 32713 + height;
		}

		public override string ToString()
		{
			return width + "x" + height;
		}
	}
}
