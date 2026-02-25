using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.PDF417.Internal
{
	public sealed class PDF417DetectorResult
	{
		public BitMatrix Bits { get; private set; }

		public List<ResultPoint[]> Points { get; private set; }

		public PDF417DetectorResult(BitMatrix bits, List<ResultPoint[]> points)
		{
			Bits = bits;
			Points = points;
		}
	}
}
