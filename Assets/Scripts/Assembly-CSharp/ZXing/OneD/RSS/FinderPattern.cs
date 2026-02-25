namespace ZXing.OneD.RSS
{
	public sealed class FinderPattern
	{
		public int Value { get; private set; }

		public int[] StartEnd { get; private set; }

		public ResultPoint[] ResultPoints { get; private set; }

		public FinderPattern(int value, int[] startEnd, int start, int end, int rowNumber)
		{
			Value = value;
			StartEnd = startEnd;
			ResultPoints = new ResultPoint[2]
			{
				new ResultPoint(start, rowNumber),
				new ResultPoint(end, rowNumber)
			};
		}

		public override bool Equals(object o)
		{
			if (!(o is FinderPattern))
			{
				return false;
			}
			FinderPattern finderPattern = (FinderPattern)o;
			return Value == finderPattern.Value;
		}

		public override int GetHashCode()
		{
			return Value;
		}
	}
}
