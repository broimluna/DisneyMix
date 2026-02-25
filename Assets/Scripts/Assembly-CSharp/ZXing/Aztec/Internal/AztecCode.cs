using ZXing.Common;

namespace ZXing.Aztec.Internal
{
	public sealed class AztecCode
	{
		public bool isCompact { get; set; }

		public int Size { get; set; }

		public int Layers { get; set; }

		public int CodeWords { get; set; }

		public BitMatrix Matrix { get; set; }
	}
}
