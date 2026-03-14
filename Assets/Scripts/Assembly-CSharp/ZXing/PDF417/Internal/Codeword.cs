namespace ZXing.PDF417.Internal
{
	public sealed class Codeword
	{
		private static readonly int BARCODE_ROW_UNKNOWN = -1;

		public int StartX { get; private set; }

		public int EndX { get; private set; }

		public int Bucket { get; private set; }

		public int Value { get; private set; }

		public int RowNumber { get; set; }

		public int Width
		{
			get
			{
				return EndX - StartX;
			}
		}

		public bool HasValidRowNumber
		{
			get
			{
				return IsValidRowNumber(RowNumber);
			}
		}

		public Codeword(int startX, int endX, int bucket, int value)
		{
			StartX = startX;
			EndX = endX;
			Bucket = bucket;
			Value = value;
			RowNumber = BARCODE_ROW_UNKNOWN;
		}

		public bool IsValidRowNumber(int rowNumber)
		{
			return rowNumber != BARCODE_ROW_UNKNOWN && Bucket == rowNumber % 3 * 3;
		}

		public void setRowNumberAsRowIndicatorColumn()
		{
			RowNumber = Value / 30 * 3 + Bucket / 3;
		}

		public override string ToString()
		{
			return RowNumber + "|" + Value;
		}
	}
}
