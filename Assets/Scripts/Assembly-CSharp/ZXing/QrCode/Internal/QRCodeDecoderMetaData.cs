namespace ZXing.QrCode.Internal
{
	public sealed class QRCodeDecoderMetaData
	{
		private readonly bool mirrored;

		public bool IsMirrored
		{
			get
			{
				return mirrored;
			}
		}

		public QRCodeDecoderMetaData(bool mirrored)
		{
			this.mirrored = mirrored;
		}

		public void applyMirroredCorrection(ResultPoint[] points)
		{
			if (mirrored && points != null && points.Length >= 3)
			{
				ResultPoint resultPoint = points[0];
				points[0] = points[2];
				points[2] = resultPoint;
			}
		}
	}
}
