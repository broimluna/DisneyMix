using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing.Multi;
using ZXing.PDF417.Internal;

namespace ZXing.PDF417
{
	public sealed class PDF417Reader : Reader, MultipleBarcodeReader
	{
		public Result decode(BinaryBitmap image)
		{
			return decode(image, null);
		}

		public Result decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			Result[] array = decode(image, hints, false);
			if (array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		public Result[] decodeMultiple(BinaryBitmap image)
		{
			return decodeMultiple(image, null);
		}

		public Result[] decodeMultiple(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			return decode(image, hints, true);
		}

		private static Result[] decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints, bool multiple)
		{
			List<Result> list = new List<Result>();
			PDF417DetectorResult pDF417DetectorResult = Detector.detect(image, hints, multiple);
			foreach (ResultPoint[] point in pDF417DetectorResult.Points)
			{
				DecoderResult decoderResult = PDF417ScanningDecoder.decode(pDF417DetectorResult.Bits, point[4], point[5], point[6], point[7], getMinCodewordWidth(point), getMaxCodewordWidth(point));
				if (decoderResult != null)
				{
					Result result = new Result(decoderResult.Text, decoderResult.RawBytes, point, BarcodeFormat.PDF_417);
					result.putMetadata(ResultMetadataType.ERROR_CORRECTION_LEVEL, decoderResult.ECLevel);
					PDF417ResultMetadata pDF417ResultMetadata = (PDF417ResultMetadata)decoderResult.Other;
					if (pDF417ResultMetadata != null)
					{
						result.putMetadata(ResultMetadataType.PDF417_EXTRA_METADATA, pDF417ResultMetadata);
					}
					list.Add(result);
				}
			}
			return list.ToArray();
		}

		private static int getMaxWidth(ResultPoint p1, ResultPoint p2)
		{
			if (p1 == null || p2 == null)
			{
				return 0;
			}
			return (int)Math.Abs(p1.X - p2.X);
		}

		private static int getMinWidth(ResultPoint p1, ResultPoint p2)
		{
			if (p1 == null || p2 == null)
			{
				return int.MaxValue;
			}
			return (int)Math.Abs(p1.X - p2.X);
		}

		private static int getMaxCodewordWidth(ResultPoint[] p)
		{
			return Math.Max(Math.Max(getMaxWidth(p[0], p[4]), getMaxWidth(p[6], p[2]) * PDF417Common.MODULES_IN_CODEWORD / PDF417Common.MODULES_IN_STOP_PATTERN), Math.Max(getMaxWidth(p[1], p[5]), getMaxWidth(p[7], p[3]) * PDF417Common.MODULES_IN_CODEWORD / PDF417Common.MODULES_IN_STOP_PATTERN));
		}

		private static int getMinCodewordWidth(ResultPoint[] p)
		{
			return Math.Min(Math.Min(getMinWidth(p[0], p[4]), getMinWidth(p[6], p[2]) * PDF417Common.MODULES_IN_CODEWORD / PDF417Common.MODULES_IN_STOP_PATTERN), Math.Min(getMinWidth(p[1], p[5]), getMinWidth(p[7], p[3]) * PDF417Common.MODULES_IN_CODEWORD / PDF417Common.MODULES_IN_STOP_PATTERN));
		}

		public void reset()
		{
		}
	}
}
