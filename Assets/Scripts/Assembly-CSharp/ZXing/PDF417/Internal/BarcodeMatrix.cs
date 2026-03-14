namespace ZXing.PDF417.Internal
{
	internal sealed class BarcodeMatrix
	{
		private readonly BarcodeRow[] matrix;

		private int currentRow;

		private readonly int height;

		private readonly int width;

		internal BarcodeMatrix(int height, int width)
		{
			matrix = new BarcodeRow[height];
			int i = 0;
			for (int num = matrix.Length; i < num; i++)
			{
				matrix[i] = new BarcodeRow((width + 4) * 17 + 1);
			}
			this.width = width * 17;
			this.height = height;
			currentRow = -1;
		}

		internal void set(int x, int y, sbyte value)
		{
			matrix[y][x] = value;
		}

		internal void startRow()
		{
			currentRow++;
		}

		internal BarcodeRow getCurrentRow()
		{
			return matrix[currentRow];
		}

		internal sbyte[][] getMatrix()
		{
			return getScaledMatrix(1, 1);
		}

		internal sbyte[][] getScaledMatrix(int xScale, int yScale)
		{
			sbyte[][] array = new sbyte[height * yScale][];
			for (int i = 0; i < height * yScale; i++)
			{
				array[i] = new sbyte[width * xScale];
			}
			int num = height * yScale;
			for (int j = 0; j < num; j++)
			{
				array[num - j - 1] = matrix[j / yScale].getScaledRow(xScale);
			}
			return array;
		}
	}
}
