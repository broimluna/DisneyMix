namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal sealed class BlockParsedResult
	{
		private DecodedInformation decodedInformation;

		private bool finished;

		internal BlockParsedResult(bool finished)
			: this(null, finished)
		{
		}

		internal BlockParsedResult(DecodedInformation information, bool finished)
		{
			this.finished = finished;
			decodedInformation = information;
		}

		internal DecodedInformation getDecodedInformation()
		{
			return decodedInformation;
		}

		internal bool isFinished()
		{
			return finished;
		}
	}
}
