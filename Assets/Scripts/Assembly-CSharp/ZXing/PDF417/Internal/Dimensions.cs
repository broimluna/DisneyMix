namespace ZXing.PDF417.Internal
{
	public sealed class Dimensions
	{
		private readonly int minCols;

		private readonly int maxCols;

		private readonly int minRows;

		private readonly int maxRows;

		public int MinCols
		{
			get
			{
				return minCols;
			}
		}

		public int MaxCols
		{
			get
			{
				return maxCols;
			}
		}

		public int MinRows
		{
			get
			{
				return minRows;
			}
		}

		public int MaxRows
		{
			get
			{
				return maxRows;
			}
		}

		public Dimensions(int minCols, int maxCols, int minRows, int maxRows)
		{
			this.minCols = minCols;
			this.maxCols = maxCols;
			this.minRows = minRows;
			this.maxRows = maxRows;
		}
	}
}
