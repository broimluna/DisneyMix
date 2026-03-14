using System;
using System.Text;

namespace ZXing.Common
{
	public sealed class BitMatrix
	{
		private readonly int width;

		private readonly int height;

		private readonly int rowSize;

		private readonly int[] bits;

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

		public int Dimension
		{
			get
			{
				if (width != height)
				{
					throw new ArgumentException("Can't call getDimension() on a non-square matrix");
				}
				return width;
			}
		}

		public bool this[int x, int y]
		{
			get
			{
				int num = y * rowSize + (x >> 5);
				return (((uint)bits[num] >> (x & 0x1F)) & 1) != 0;
			}
			set
			{
				if (value)
				{
					int num = y * rowSize + (x >> 5);
					bits[num] |= 1 << (x & 0x1F);
				}
			}
		}

		public BitMatrix(int dimension)
			: this(dimension, dimension)
		{
		}

		public BitMatrix(int width, int height)
		{
			if (width < 1 || height < 1)
			{
				throw new ArgumentException("Both dimensions must be greater than 0");
			}
			this.width = width;
			this.height = height;
			rowSize = width + 31 >> 5;
			bits = new int[rowSize * height];
		}

		private BitMatrix(int width, int height, int rowSize, int[] bits)
		{
			this.width = width;
			this.height = height;
			this.rowSize = rowSize;
			this.bits = bits;
		}

		public void flip(int x, int y)
		{
			int num = y * rowSize + (x >> 5);
			bits[num] ^= 1 << (x & 0x1F);
		}

		public void clear()
		{
			int num = bits.Length;
			for (int i = 0; i < num; i++)
			{
				bits[i] = 0;
			}
		}

		public void setRegion(int left, int top, int width, int height)
		{
			if (top < 0 || left < 0)
			{
				throw new ArgumentException("Left and top must be nonnegative");
			}
			if (height < 1 || width < 1)
			{
				throw new ArgumentException("Height and width must be at least 1");
			}
			int num = left + width;
			int num2 = top + height;
			if (num2 > this.height || num > this.width)
			{
				throw new ArgumentException("The region must fit inside the matrix");
			}
			for (int i = top; i < num2; i++)
			{
				int num3 = i * rowSize;
				for (int j = left; j < num; j++)
				{
					bits[num3 + (j >> 5)] |= 1 << j;
				}
			}
		}

		public BitArray getRow(int y, BitArray row)
		{
			if (row == null || row.Size < width)
			{
				row = new BitArray(width);
			}
			else
			{
				row.clear();
			}
			int num = y * rowSize;
			for (int i = 0; i < rowSize; i++)
			{
				row.setBulk(i << 5, bits[num + i]);
			}
			return row;
		}

		public void setRow(int y, BitArray row)
		{
			Array.Copy(row.Array, 0, bits, y * rowSize, rowSize);
		}

		public void rotate180()
		{
			int size = Width;
			int num = Height;
			BitArray bitArray = new BitArray(size);
			BitArray bitArray2 = new BitArray(size);
			for (int i = 0; i < (num + 1) / 2; i++)
			{
				bitArray = getRow(i, bitArray);
				bitArray2 = getRow(num - 1 - i, bitArray2);
				bitArray.reverse();
				bitArray2.reverse();
				setRow(i, bitArray2);
				setRow(num - 1 - i, bitArray);
			}
		}

		public int[] getEnclosingRectangle()
		{
			int num = width;
			int num2 = height;
			int num3 = -1;
			int num4 = -1;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < rowSize; j++)
				{
					int num5 = bits[i * rowSize + j];
					if (num5 == 0)
					{
						continue;
					}
					if (i < num2)
					{
						num2 = i;
					}
					if (i > num4)
					{
						num4 = i;
					}
					if (j * 32 < num)
					{
						int k;
						for (k = 0; num5 << 31 - k == 0; k++)
						{
						}
						if (j * 32 + k < num)
						{
							num = j * 32 + k;
						}
					}
					if (j * 32 + 31 > num3)
					{
						int num6 = 31;
						while ((uint)num5 >> num6 == 0)
						{
							num6--;
						}
						if (j * 32 + num6 > num3)
						{
							num3 = j * 32 + num6;
						}
					}
				}
			}
			int num7 = num3 - num;
			int num8 = num4 - num2;
			if (num7 < 0 || num8 < 0)
			{
				return null;
			}
			return new int[4] { num, num2, num7, num8 };
		}

		public int[] getTopLeftOnBit()
		{
			int i;
			for (i = 0; i < bits.Length && bits[i] == 0; i++)
			{
			}
			if (i == bits.Length)
			{
				return null;
			}
			int num = i / rowSize;
			int num2 = i % rowSize << 5;
			int num3 = bits[i];
			int j;
			for (j = 0; num3 << 31 - j == 0; j++)
			{
			}
			num2 += j;
			return new int[2] { num2, num };
		}

		public int[] getBottomRightOnBit()
		{
			int num = bits.Length - 1;
			while (num >= 0 && bits[num] == 0)
			{
				num--;
			}
			if (num < 0)
			{
				return null;
			}
			int num2 = num / rowSize;
			int num3 = num % rowSize << 5;
			int num4 = bits[num];
			int num5 = 31;
			while ((uint)num4 >> num5 == 0)
			{
				num5--;
			}
			num3 += num5;
			return new int[2] { num3, num2 };
		}

		public override bool Equals(object obj)
		{
			if (!(obj is BitMatrix))
			{
				return false;
			}
			BitMatrix bitMatrix = (BitMatrix)obj;
			if (width != bitMatrix.width || height != bitMatrix.height || rowSize != bitMatrix.rowSize || bits.Length != bitMatrix.bits.Length)
			{
				return false;
			}
			for (int i = 0; i < bits.Length; i++)
			{
				if (bits[i] != bitMatrix.bits[i])
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			int num = width;
			num = 31 * num + width;
			num = 31 * num + height;
			num = 31 * num + rowSize;
			int[] array = bits;
			foreach (int num2 in array)
			{
				num = 31 * num + num2.GetHashCode();
			}
			return num;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(height * (width + 1));
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					stringBuilder.Append((!this[j, i]) ? "  " : "X ");
				}
				stringBuilder.AppendLine(string.Empty);
			}
			return stringBuilder.ToString();
		}

		public object Clone()
		{
			return new BitMatrix(width, height, rowSize, (int[])bits.Clone());
		}
	}
}
