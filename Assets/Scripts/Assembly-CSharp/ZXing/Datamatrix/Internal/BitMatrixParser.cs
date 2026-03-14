using System;
using ZXing.Common;

namespace ZXing.Datamatrix.Internal
{
	internal sealed class BitMatrixParser
	{
		private readonly BitMatrix mappingBitMatrix;

		private readonly BitMatrix readMappingMatrix;

		private readonly Version version;

		public Version Version
		{
			get
			{
				return version;
			}
		}

		internal BitMatrixParser(BitMatrix bitMatrix)
		{
			int height = bitMatrix.Height;
			if (height >= 8 && height <= 144 && (height & 1) == 0)
			{
				version = readVersion(bitMatrix);
				if (version != null)
				{
					mappingBitMatrix = extractDataRegion(bitMatrix);
					readMappingMatrix = new BitMatrix(mappingBitMatrix.Width, mappingBitMatrix.Height);
				}
			}
		}

		internal static Version readVersion(BitMatrix bitMatrix)
		{
			int height = bitMatrix.Height;
			int width = bitMatrix.Width;
			return Version.getVersionForDimensions(height, width);
		}

		internal byte[] readCodewords()
		{
			byte[] array = new byte[version.getTotalCodewords()];
			int num = 0;
			int num2 = 4;
			int num3 = 0;
			int height = mappingBitMatrix.Height;
			int width = mappingBitMatrix.Width;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			do
			{
				if (num2 == height && num3 == 0 && !flag)
				{
					array[num++] = (byte)readCorner1(height, width);
					num2 -= 2;
					num3 += 2;
					flag = true;
					continue;
				}
				if (num2 == height - 2 && num3 == 0 && (width & 3) != 0 && !flag2)
				{
					array[num++] = (byte)readCorner2(height, width);
					num2 -= 2;
					num3 += 2;
					flag2 = true;
					continue;
				}
				if (num2 == height + 4 && num3 == 2 && (width & 7) == 0 && !flag3)
				{
					array[num++] = (byte)readCorner3(height, width);
					num2 -= 2;
					num3 += 2;
					flag3 = true;
					continue;
				}
				if (num2 == height - 2 && num3 == 0 && (width & 7) == 4 && !flag4)
				{
					array[num++] = (byte)readCorner4(height, width);
					num2 -= 2;
					num3 += 2;
					flag4 = true;
					continue;
				}
				do
				{
					if (num2 < height && num3 >= 0 && !readMappingMatrix[num3, num2])
					{
						array[num++] = (byte)readUtah(num2, num3, height, width);
					}
					num2 -= 2;
					num3 += 2;
				}
				while (num2 >= 0 && num3 < width);
				num2++;
				num3 += 3;
				do
				{
					if (num2 >= 0 && num3 < width && !readMappingMatrix[num3, num2])
					{
						array[num++] = (byte)readUtah(num2, num3, height, width);
					}
					num2 += 2;
					num3 -= 2;
				}
				while (num2 < height && num3 >= 0);
				num2 += 3;
				num3++;
			}
			while (num2 < height || num3 < width);
			if (num != version.getTotalCodewords())
			{
				return null;
			}
			return array;
		}

		private bool readModule(int row, int column, int numRows, int numColumns)
		{
			if (row < 0)
			{
				row += numRows;
				column += 4 - ((numRows + 4) & 7);
			}
			if (column < 0)
			{
				column += numColumns;
				row += 4 - ((numColumns + 4) & 7);
			}
			readMappingMatrix[column, row] = true;
			return mappingBitMatrix[column, row];
		}

		private int readUtah(int row, int column, int numRows, int numColumns)
		{
			int num = 0;
			if (readModule(row - 2, column - 2, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(row - 2, column - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(row - 1, column - 2, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(row - 1, column - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(row - 1, column, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(row, column - 2, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(row, column - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(row, column, numRows, numColumns))
			{
				num |= 1;
			}
			return num;
		}

		private int readCorner1(int numRows, int numColumns)
		{
			int num = 0;
			if (readModule(numRows - 1, 0, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(numRows - 1, 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(numRows - 1, 2, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(0, numColumns - 2, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(0, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(1, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(2, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(3, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			return num;
		}

		private int readCorner2(int numRows, int numColumns)
		{
			int num = 0;
			if (readModule(numRows - 3, 0, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(numRows - 2, 0, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(numRows - 1, 0, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(0, numColumns - 4, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(0, numColumns - 3, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(0, numColumns - 2, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(0, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(1, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			return num;
		}

		private int readCorner3(int numRows, int numColumns)
		{
			int num = 0;
			if (readModule(numRows - 1, 0, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(numRows - 1, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(0, numColumns - 3, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(0, numColumns - 2, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(0, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(1, numColumns - 3, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(1, numColumns - 2, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(1, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			return num;
		}

		private int readCorner4(int numRows, int numColumns)
		{
			int num = 0;
			if (readModule(numRows - 3, 0, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(numRows - 2, 0, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(numRows - 1, 0, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(0, numColumns - 2, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(0, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(1, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(2, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			num <<= 1;
			if (readModule(3, numColumns - 1, numRows, numColumns))
			{
				num |= 1;
			}
			return num;
		}

		private BitMatrix extractDataRegion(BitMatrix bitMatrix)
		{
			int symbolSizeRows = version.getSymbolSizeRows();
			int symbolSizeColumns = version.getSymbolSizeColumns();
			if (bitMatrix.Height != symbolSizeRows)
			{
				throw new ArgumentException("Dimension of bitMarix must match the version size");
			}
			int dataRegionSizeRows = version.getDataRegionSizeRows();
			int dataRegionSizeColumns = version.getDataRegionSizeColumns();
			int num = symbolSizeRows / dataRegionSizeRows;
			int num2 = symbolSizeColumns / dataRegionSizeColumns;
			int height = num * dataRegionSizeRows;
			int width = num2 * dataRegionSizeColumns;
			BitMatrix bitMatrix2 = new BitMatrix(width, height);
			for (int i = 0; i < num; i++)
			{
				int num3 = i * dataRegionSizeRows;
				for (int j = 0; j < num2; j++)
				{
					int num4 = j * dataRegionSizeColumns;
					for (int k = 0; k < dataRegionSizeRows; k++)
					{
						int y = i * (dataRegionSizeRows + 2) + 1 + k;
						int y2 = num3 + k;
						for (int l = 0; l < dataRegionSizeColumns; l++)
						{
							int x = j * (dataRegionSizeColumns + 2) + 1 + l;
							if (bitMatrix[x, y])
							{
								int x2 = num4 + l;
								bitMatrix2[x2, y2] = true;
							}
						}
					}
				}
			}
			return bitMatrix2;
		}
	}
}
