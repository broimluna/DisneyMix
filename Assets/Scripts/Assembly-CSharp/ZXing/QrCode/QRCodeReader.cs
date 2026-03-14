using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing.QrCode.Internal;

namespace ZXing.QrCode
{
	public class QRCodeReader : Reader
	{
		private static readonly ResultPoint[] NO_POINTS = new ResultPoint[0];

		private readonly Decoder decoder = new Decoder();

		protected Decoder getDecoder()
		{
			return decoder;
		}

		public Result decode(BinaryBitmap image)
		{
			return decode(image, null);
		}

		public Result decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			if (image == null || image.BlackMatrix == null)
			{
				return null;
			}
			DecoderResult decoderResult;
			ResultPoint[] array;
			if (hints != null && hints.ContainsKey(DecodeHintType.PURE_BARCODE))
			{
				BitMatrix bitMatrix = extractPureBits(image.BlackMatrix);
				if (bitMatrix == null)
				{
					return null;
				}
				decoderResult = decoder.decode(bitMatrix, hints);
				array = NO_POINTS;
			}
			else
			{
				DetectorResult detectorResult = new Detector(image.BlackMatrix).detect(hints);
				if (detectorResult == null)
				{
					return null;
				}
				decoderResult = decoder.decode(detectorResult.Bits, hints);
				array = detectorResult.Points;
			}
			if (decoderResult == null)
			{
				return null;
			}
			QRCodeDecoderMetaData qRCodeDecoderMetaData = decoderResult.Other as QRCodeDecoderMetaData;
			if (qRCodeDecoderMetaData != null)
			{
				qRCodeDecoderMetaData.applyMirroredCorrection(array);
			}
			Result result = new Result(decoderResult.Text, decoderResult.RawBytes, array, BarcodeFormat.QR_CODE);
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
			if (decoderResult.StructuredAppend)
			{
				result.putMetadata(ResultMetadataType.STRUCTURED_APPEND_SEQUENCE, decoderResult.StructuredAppendSequenceNumber);
				result.putMetadata(ResultMetadataType.STRUCTURED_APPEND_PARITY, decoderResult.StructuredAppendParity);
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
			float msize;
			if (!moduleSize(topLeftOnBit, image, out msize))
			{
				return null;
			}
			int num = topLeftOnBit[1];
			int num2 = bottomRightOnBit[1];
			int num3 = topLeftOnBit[0];
			int num4 = bottomRightOnBit[0];
			if (num3 >= num4 || num >= num2)
			{
				return null;
			}
			if (num2 - num != num4 - num3)
			{
				num4 = num3 + (num2 - num);
			}
			int num5 = (int)Math.Round((float)(num4 - num3 + 1) / msize);
			int num6 = (int)Math.Round((float)(num2 - num + 1) / msize);
			if (num5 <= 0 || num6 <= 0)
			{
				return null;
			}
			if (num6 != num5)
			{
				return null;
			}
			int num7 = (int)(msize / 2f);
			num += num7;
			num3 += num7;
			int num8 = num3 + (int)((float)(num5 - 1) * msize) - (num4 - 1);
			if (num8 > 0)
			{
				if (num8 > num7)
				{
					return null;
				}
				num3 -= num8;
			}
			int num9 = num + (int)((float)(num6 - 1) * msize) - (num2 - 1);
			if (num9 > 0)
			{
				if (num9 > num7)
				{
					return null;
				}
				num -= num9;
			}
			BitMatrix bitMatrix = new BitMatrix(num5, num6);
			for (int i = 0; i < num6; i++)
			{
				int y = num + (int)((float)i * msize);
				for (int j = 0; j < num5; j++)
				{
					if (image[num3 + (int)((float)j * msize), y])
					{
						bitMatrix[j, i] = true;
					}
				}
			}
			return bitMatrix;
		}

		private static bool moduleSize(int[] leftTopBlack, BitMatrix image, out float msize)
		{
			int height = image.Height;
			int width = image.Width;
			int num = leftTopBlack[0];
			int num2 = leftTopBlack[1];
			bool flag = true;
			int num3 = 0;
			while (num < width && num2 < height)
			{
				if (flag != image[num, num2])
				{
					if (++num3 == 5)
					{
						break;
					}
					flag = !flag;
				}
				num++;
				num2++;
			}
			if (num == width || num2 == height)
			{
				msize = 0f;
				return false;
			}
			msize = (float)(num - leftTopBlack[0]) / 7f;
			return true;
		}
	}
}
