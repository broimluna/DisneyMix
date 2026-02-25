using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing.Multi.QrCode.Internal;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace ZXing.Multi.QrCode
{
	public sealed class QRCodeMultiReader : QRCodeReader, MultipleBarcodeReader
	{
		private static readonly ResultPoint[] NO_POINTS = new ResultPoint[0];

		public Result[] decodeMultiple(BinaryBitmap image)
		{
			return decodeMultiple(image, null);
		}

		public Result[] decodeMultiple(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			List<Result> list = new List<Result>();
			DetectorResult[] array = new MultiDetector(image.BlackMatrix).detectMulti(hints);
			DetectorResult[] array2 = array;
			foreach (DetectorResult detectorResult in array2)
			{
				DecoderResult decoderResult = getDecoder().decode(detectorResult.Bits, hints);
				if (decoderResult != null)
				{
					ResultPoint[] points = detectorResult.Points;
					QRCodeDecoderMetaData qRCodeDecoderMetaData = decoderResult.Other as QRCodeDecoderMetaData;
					if (qRCodeDecoderMetaData != null)
					{
						qRCodeDecoderMetaData.applyMirroredCorrection(points);
					}
					Result result = new Result(decoderResult.Text, decoderResult.RawBytes, points, BarcodeFormat.QR_CODE);
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
					list.Add(result);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			list = ProcessStructuredAppend(list);
			return list.ToArray();
		}

		private List<Result> ProcessStructuredAppend(List<Result> results)
		{
			bool flag = false;
			foreach (Result result2 in results)
			{
				if (result2.ResultMetadata.ContainsKey(ResultMetadataType.STRUCTURED_APPEND_SEQUENCE))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return results;
			}
			List<Result> list = new List<Result>();
			List<Result> list2 = new List<Result>();
			foreach (Result result3 in results)
			{
				list.Add(result3);
				if (result3.ResultMetadata.ContainsKey(ResultMetadataType.STRUCTURED_APPEND_SEQUENCE))
				{
					list2.Add(result3);
				}
			}
			list2.Sort(SaSequenceSort);
			string text = string.Empty;
			int num = 0;
			int num2 = 0;
			foreach (Result item in list2)
			{
				text += item.Text;
				num += item.RawBytes.Length;
				if (!item.ResultMetadata.ContainsKey(ResultMetadataType.BYTE_SEGMENTS))
				{
					continue;
				}
				foreach (byte[] item2 in (IEnumerable<byte[]>)item.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS])
				{
					num2 += item2.Length;
				}
			}
			byte[] array = new byte[num];
			byte[] array2 = new byte[num2];
			int num3 = 0;
			int num4 = 0;
			foreach (Result item3 in list2)
			{
				Array.Copy(item3.RawBytes, 0, array, num3, item3.RawBytes.Length);
				num3 += item3.RawBytes.Length;
				if (!item3.ResultMetadata.ContainsKey(ResultMetadataType.BYTE_SEGMENTS))
				{
					continue;
				}
				foreach (byte[] item4 in (IEnumerable<byte[]>)item3.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS])
				{
					Array.Copy(item4, 0, array2, num4, item4.Length);
					num4 += item4.Length;
				}
			}
			Result result = new Result(text, array, NO_POINTS, BarcodeFormat.QR_CODE);
			if (num2 > 0)
			{
				List<byte[]> list3 = new List<byte[]>();
				list3.Add(array2);
				result.putMetadata(ResultMetadataType.BYTE_SEGMENTS, list3);
			}
			list.Add(result);
			return list;
		}

		private int SaSequenceSort(Result a, Result b)
		{
			int num = (int)a.ResultMetadata[ResultMetadataType.STRUCTURED_APPEND_SEQUENCE];
			int num2 = (int)b.ResultMetadata[ResultMetadataType.STRUCTURED_APPEND_SEQUENCE];
			return num - num2;
		}
	}
}
