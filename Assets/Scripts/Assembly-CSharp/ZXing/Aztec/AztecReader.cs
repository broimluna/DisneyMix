using System.Collections.Generic;
using ZXing.Aztec.Internal;
using ZXing.Common;

namespace ZXing.Aztec
{
	public class AztecReader : Reader
	{
		public Result decode(BinaryBitmap image)
		{
			return decode(image, null);
		}

		public Result decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			BitMatrix blackMatrix = image.BlackMatrix;
			if (blackMatrix == null)
			{
				return null;
			}
			Detector detector = new Detector(blackMatrix);
			ResultPoint[] array = null;
			DecoderResult decoderResult = null;
			AztecDetectorResult aztecDetectorResult = detector.detect(false);
			if (aztecDetectorResult != null)
			{
				array = aztecDetectorResult.Points;
				decoderResult = new Decoder().decode(aztecDetectorResult);
			}
			if (decoderResult == null)
			{
				aztecDetectorResult = detector.detect(true);
				if (aztecDetectorResult == null)
				{
					return null;
				}
				array = aztecDetectorResult.Points;
				decoderResult = new Decoder().decode(aztecDetectorResult);
				if (decoderResult == null)
				{
					return null;
				}
			}
			if (hints != null && hints.ContainsKey(DecodeHintType.NEED_RESULT_POINT_CALLBACK))
			{
				ResultPointCallback resultPointCallback = (ResultPointCallback)hints[DecodeHintType.NEED_RESULT_POINT_CALLBACK];
				if (resultPointCallback != null)
				{
					ResultPoint[] array2 = array;
					foreach (ResultPoint point in array2)
					{
						resultPointCallback(point);
					}
				}
			}
			Result result = new Result(decoderResult.Text, decoderResult.RawBytes, array, BarcodeFormat.AZTEC);
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
			result.putMetadata(ResultMetadataType.AZTEC_EXTRA_METADATA, new AztecResultMetadata(aztecDetectorResult.Compact, aztecDetectorResult.NbDatablocks, aztecDetectorResult.NbLayers));
			return result;
		}

		public void reset()
		{
		}
	}
}
