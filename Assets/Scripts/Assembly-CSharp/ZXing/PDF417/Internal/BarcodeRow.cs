namespace ZXing.PDF417.Internal
{
	internal sealed class BarcodeRow
	{
		private readonly sbyte[] row;

		private int currentLocation;

		internal sbyte this[int x]
		{
			get
			{
				return row[x];
			}
			set
			{
				row[x] = value;
			}
		}

		internal BarcodeRow(int width)
		{
			row = new sbyte[width];
			currentLocation = 0;
		}

		internal void set(int x, bool black)
		{
			row[x] = (sbyte)(black ? 1 : 0);
		}

		internal void addBar(bool black, int width)
		{
			for (int i = 0; i < width; i++)
			{
				set(currentLocation++, black);
			}
		}

		internal sbyte[] getScaledRow(int scale)
		{
			sbyte[] array = new sbyte[row.Length * scale];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = row[i / scale];
			}
			return array;
		}
	}
}
