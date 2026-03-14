namespace ZXing.QrCode.Internal
{
	internal sealed class BlockPair
	{
		private readonly byte[] dataBytes;

		private readonly byte[] errorCorrectionBytes;

		public byte[] DataBytes
		{
			get
			{
				return dataBytes;
			}
		}

		public byte[] ErrorCorrectionBytes
		{
			get
			{
				return errorCorrectionBytes;
			}
		}

		public BlockPair(byte[] data, byte[] errorCorrection)
		{
			dataBytes = data;
			errorCorrectionBytes = errorCorrection;
		}
	}
}
