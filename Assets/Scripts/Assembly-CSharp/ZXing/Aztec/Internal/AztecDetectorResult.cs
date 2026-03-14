using ZXing.Common;

namespace ZXing.Aztec.Internal
{
	public class AztecDetectorResult : DetectorResult
	{
		public bool Compact { get; private set; }

		public int NbDatablocks { get; private set; }

		public int NbLayers { get; private set; }

		public AztecDetectorResult(BitMatrix bits, ResultPoint[] points, bool compact, int nbDatablocks, int nbLayers)
			: base(bits, points)
		{
			Compact = compact;
			NbDatablocks = nbDatablocks;
			NbLayers = nbLayers;
		}
	}
}
