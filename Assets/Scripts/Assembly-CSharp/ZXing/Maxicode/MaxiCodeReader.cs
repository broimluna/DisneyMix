using System.Collections.Generic;
using ZXing.Common;
using ZXing.Maxicode.Internal;

namespace ZXing.Maxicode
{
	public sealed class MaxiCodeReader : Reader
	{
		private const int MATRIX_WIDTH = 30;

		private const int MATRIX_HEIGHT = 33;

		private static readonly ResultPoint[] NO_POINTS = new ResultPoint[0];

		private readonly Decoder decoder = new Decoder();

		public Result decode(BinaryBitmap image)
		{
			return decode(image, null);
		}

		public Result decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			if (hints != null && hints.ContainsKey(DecodeHintType.PURE_BARCODE))
			{
				BitMatrix bitMatrix = extractPureBits(image.BlackMatrix);
				if (bitMatrix == null)
				{
					return null;
				}
				DecoderResult decoderResult = decoder.decode(bitMatrix, hints);
				if (decoderResult == null)
				{
					return null;
				}
				ResultPoint[] nO_POINTS = NO_POINTS;
				Result result = new Result(decoderResult.Text, decoderResult.RawBytes, nO_POINTS, BarcodeFormat.MAXICODE);
				string eCLevel = decoderResult.ECLevel;
				if (eCLevel != null)
				{
					result.putMetadata(ResultMetadataType.ERROR_CORRECTION_LEVEL, eCLevel);
				}
				return result;
			}
			return null;
		}

		public void reset()
		{
		}

		private static BitMatrix extractPureBits(BitMatrix image)
		{
			int[] enclosingRectangle = image.getEnclosingRectangle();
			if (enclosingRectangle == null)
			{
				return null;
			}
			int num = enclosingRectangle[0];
			int num2 = enclosingRectangle[1];
			int num3 = enclosingRectangle[2];
			int num4 = enclosingRectangle[3];
			BitMatrix bitMatrix = new BitMatrix(30, 33);
			for (int i = 0; i < 33; i++)
			{
				int y = num2 + (i * num4 + num4 / 2) / 33;
				for (int j = 0; j < 30; j++)
				{
					int x = num + (j * num3 + num3 / 2 + (i & 1) * num3 / 2) / 30;
					if (image[x, y])
					{
						bitMatrix[j, i] = true;
					}
				}
			}
			return bitMatrix;
		}
	}
}
