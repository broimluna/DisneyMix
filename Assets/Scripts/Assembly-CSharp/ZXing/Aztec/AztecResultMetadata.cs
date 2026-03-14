namespace ZXing.Aztec
{
	public sealed class AztecResultMetadata
	{
		public bool Compact { get; private set; }

		public int Datablocks { get; private set; }

		public int Layers { get; private set; }

		public AztecResultMetadata(bool compact, int datablocks, int layers)
		{
			Compact = compact;
			Datablocks = datablocks;
			Layers = layers;
		}
	}
}
