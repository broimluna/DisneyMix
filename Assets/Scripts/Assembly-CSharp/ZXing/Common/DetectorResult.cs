namespace ZXing.Common
{
	public class DetectorResult
	{
		public BitMatrix Bits { get; private set; }

		public ResultPoint[] Points { get; private set; }

		public DetectorResult(BitMatrix bits, ResultPoint[] points)
		{
			Bits = bits;
			Points = points;
		}
	}
}
