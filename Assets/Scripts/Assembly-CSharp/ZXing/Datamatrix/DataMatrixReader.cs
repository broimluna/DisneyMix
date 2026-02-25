using System.Collections.Generic;
using ZXing.Common;
using ZXing.Datamatrix.Internal;

namespace ZXing.Datamatrix
{
	public sealed class DataMatrixReader : Reader
	{
		private static readonly ResultPoint[] NO_POINTS = new ResultPoint[0];

		private readonly Decoder decoder = new Decoder();

		public Result decode(BinaryBitmap image)
		{
			return decode(image, null);
		}

		public Result decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			DecoderResult decoderResult;
			ResultPoint[] resultPoints;
			if (hints != null && hints.ContainsKey(DecodeHintType.PURE_BARCODE))
			{
				BitMatrix bitMatrix = extractPureBits(image.BlackMatrix);
				if (bitMatrix == null)
				{
					return null;
				}
				decoderResult = decoder.decode(bitMatrix);
				resultPoints = NO_POINTS;
			}
			else
			{
				DetectorResult detectorResult = new Detector(image.BlackMatrix).detect();
				if (detectorResult == null)
				{
					return null;
				}
				decoderResult = decoder.decode(detectorResult.Bits);
				resultPoints = detectorResult.Points;
			}
			if (decoderResult == null)
			{
				return null;
			}
			Result result = new Result(decoderResult.Text, decoderResult.RawBytes, resultPoints, BarcodeFormat.DATA_MATRIX);
			IList<byte[]> byteSegments = decoderResult.ByteSegments;
			if (byteSegments != null)
			{
				result.putMetadata(ResultMetadataType.BYTE_SEGMENTS, byteSegments);
			}
			string eCLevel = decoderResult.ECLevel;
			if (eCLevel != null)
			{
				result.putMetadata(ResultMetadataType.ERROR_CORRECTION_LEVEL, eCLevel);
			}
			return result;
		}

		public void reset()
		{
		}

		private static BitMatrix extractPureBits(BitMatrix image)
		{
			int[] topLeftOnBit = image.getTopLeftOnBit();
			int[] bottomRightOnBit = image.getBottomRightOnBit();
			if (topLeftOnBit == null || bottomRightOnBit == null)
			{
				return null;
			}
			int modulesize;
			if (!moduleSize(topLeftOnBit, image, out modulesize))
			{
				return null;
			}
			int num = topLeftOnBit[1];
			int num2 = bottomRightOnBit[1];
			int num3 = topLeftOnBit[0];
			int num4 = bottomRightOnBit[0];
			int num5 = (num4 - num3 + 1) / modulesize;
			int num6 = (num2 - num + 1) / modulesize;
			if (num5 <= 0 || num6 <= 0)
			{
				return null;
			}
			int num7 = modulesize >> 1;
			num += num7;
			num3 += num7;
			BitMatrix bitMatrix = new BitMatrix(num5, num6);
			for (int i = 0; i < num6; i++)
			{
				int y = num + i * modulesize;
				for (int j = 0; j < num5; j++)
				{
					if (image[num3 + j * modulesize, y])
					{
						bitMatrix[j, i] = true;
					}
				}
			}
			return bitMatrix;
		}

		private static bool moduleSize(int[] leftTopBlack, BitMatrix image, out int modulesize)
		{
			int width = image.Width;
			int i = leftTopBlack[0];
			for (int y = leftTopBlack[1]; i < width && image[i, y]; i++)
			{
			}
			if (i == width)
			{
				modulesize = 0;
				return false;
			}
			modulesize = i - leftTopBlack[0];
			if (modulesize == 0)
			{
				return false;
			}
			return true;
		}
	}
}
