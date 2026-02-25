using System.Text;

namespace ZXing.QrCode.Internal
{
	public sealed class ByteMatrix
	{
		private readonly byte[][] bytes;

		private readonly int width;

		private readonly int height;

		public int Height
		{
			get
			{
				return height;
			}
		}

		public int Width
		{
			get
			{
				return width;
			}
		}

		public int this[int x, int y]
		{
			get
			{
				return bytes[y][x];
			}
			set
			{
				bytes[y][x] = (byte)value;
			}
		}

		public byte[][] Array
		{
			get
			{
				return bytes;
			}
		}

		public ByteMatrix(int width, int height)
		{
			bytes = new byte[height][];
			for (int i = 0; i < height; i++)
			{
				bytes[i] = new byte[width];
			}
			this.width = width;
			this.height = height;
		}

		public void set(int x, int y, byte value)
		{
			bytes[y][x] = value;
		}

		public void set(int x, int y, bool value)
		{
			bytes[y][x] = (byte)(value ? 1u : 0u);
		}

		public void clear(byte value)
		{
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					bytes[i][j] = value;
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(2 * width * height + 2);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					switch (bytes[i][j])
					{
					case 0:
						stringBuilder.Append(" 0");
						break;
					case 1:
						stringBuilder.Append(" 1");
						break;
					default:
						stringBuilder.Append("  ");
						break;
					}
				}
				stringBuilder.Append('\n');
			}
			return stringBuilder.ToString();
		}
	}
}
