namespace ZXing.Datamatrix.Encoder
{
	public class DefaultPlacement
	{
		private readonly string codewords;

		private readonly int numrows;

		private readonly int numcols;

		private readonly byte[] bits;

		public int Numrows
		{
			get
			{
				return numrows;
			}
		}

		public int Numcols
		{
			get
			{
				return numcols;
			}
		}

		public byte[] Bits
		{
			get
			{
				return bits;
			}
		}

		public DefaultPlacement(string codewords, int numcols, int numrows)
		{
			this.codewords = codewords;
			this.numcols = numcols;
			this.numrows = numrows;
			bits = new byte[numcols * numrows];
			SupportClass.Fill(bits, (byte)2);
		}

		public bool getBit(int col, int row)
		{
			return bits[row * numcols + col] == 1;
		}

		public void setBit(int col, int row, bool bit)
		{
			bits[row * numcols + col] = (byte)(bit ? 1 : 0);
		}

		public bool hasBit(int col, int row)
		{
			return bits[row * numcols + col] < 2;
		}

		public void place()
		{
			int num = 0;
			int num2 = 4;
			int num3 = 0;
			do
			{
				if (num2 == numrows && num3 == 0)
				{
					corner1(num++);
				}
				if (num2 == numrows - 2 && num3 == 0 && numcols % 4 != 0)
				{
					corner2(num++);
				}
				if (num2 == numrows - 2 && num3 == 0 && numcols % 8 == 4)
				{
					corner3(num++);
				}
				if (num2 == numrows + 4 && num3 == 2 && numcols % 8 == 0)
				{
					corner4(num++);
				}
				do
				{
					if (num2 < numrows && num3 >= 0 && !hasBit(num3, num2))
					{
						utah(num2, num3, num++);
					}
					num2 -= 2;
					num3 += 2;
				}
				while (num2 >= 0 && num3 < numcols);
				num2++;
				num3 += 3;
				do
				{
					if (num2 >= 0 && num3 < numcols && !hasBit(num3, num2))
					{
						utah(num2, num3, num++);
					}
					num2 += 2;
					num3 -= 2;
				}
				while (num2 < numrows && num3 >= 0);
				num2 += 3;
				num3++;
			}
			while (num2 < numrows || num3 < numcols);
			if (!hasBit(numcols - 1, numrows - 1))
			{
				setBit(numcols - 1, numrows - 1, true);
				setBit(numcols - 2, numrows - 2, true);
			}
		}

		private void module(int row, int col, int pos, int bit)
		{
			if (row < 0)
			{
				row += numrows;
				col += 4 - (numrows + 4) % 8;
			}
			if (col < 0)
			{
				col += numcols;
				row += 4 - (numcols + 4) % 8;
			}
			int num = codewords[pos];
			num &= 1 << 8 - bit;
			setBit(col, row, num != 0);
		}

		private void utah(int row, int col, int pos)
		{
			module(row - 2, col - 2, pos, 1);
			module(row - 2, col - 1, pos, 2);
			module(row - 1, col - 2, pos, 3);
			module(row - 1, col - 1, pos, 4);
			module(row - 1, col, pos, 5);
			module(row, col - 2, pos, 6);
			module(row, col - 1, pos, 7);
			module(row, col, pos, 8);
		}

		private void corner1(int pos)
		{
			module(numrows - 1, 0, pos, 1);
			module(numrows - 1, 1, pos, 2);
			module(numrows - 1, 2, pos, 3);
			module(0, numcols - 2, pos, 4);
			module(0, numcols - 1, pos, 5);
			module(1, numcols - 1, pos, 6);
			module(2, numcols - 1, pos, 7);
			module(3, numcols - 1, pos, 8);
		}

		private void corner2(int pos)
		{
			module(numrows - 3, 0, pos, 1);
			module(numrows - 2, 0, pos, 2);
			module(numrows - 1, 0, pos, 3);
			module(0, numcols - 4, pos, 4);
			module(0, numcols - 3, pos, 5);
			module(0, numcols - 2, pos, 6);
			module(0, numcols - 1, pos, 7);
			module(1, numcols - 1, pos, 8);
		}

		private void corner3(int pos)
		{
			module(numrows - 3, 0, pos, 1);
			module(numrows - 2, 0, pos, 2);
			module(numrows - 1, 0, pos, 3);
			module(0, numcols - 2, pos, 4);
			module(0, numcols - 1, pos, 5);
			module(1, numcols - 1, pos, 6);
			module(2, numcols - 1, pos, 7);
			module(3, numcols - 1, pos, 8);
		}

		private void corner4(int pos)
		{
			module(numrows - 1, 0, pos, 1);
			module(numrows - 1, numcols - 1, pos, 2);
			module(0, numcols - 3, pos, 3);
			module(0, numcols - 2, pos, 4);
			module(0, numcols - 1, pos, 5);
			module(1, numcols - 3, pos, 6);
			module(1, numcols - 2, pos, 7);
			module(1, numcols - 1, pos, 8);
		}
	}
}
