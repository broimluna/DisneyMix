using System;
using System.Collections.Generic;

namespace ZXing
{
	public sealed class Result
	{
		public string Text { get; private set; }

		public byte[] RawBytes { get; private set; }

		public ResultPoint[] ResultPoints { get; private set; }

		public BarcodeFormat BarcodeFormat { get; private set; }

		public IDictionary<ResultMetadataType, object> ResultMetadata { get; private set; }

		public long Timestamp { get; private set; }

		public Result(string text, byte[] rawBytes, ResultPoint[] resultPoints, BarcodeFormat format)
			: this(text, rawBytes, resultPoints, format, DateTime.Now.Ticks)
		{
		}

		public Result(string text, byte[] rawBytes, ResultPoint[] resultPoints, BarcodeFormat format, long timestamp)
		{
			if (text == null && rawBytes == null)
			{
				throw new ArgumentException("Text and bytes are null");
			}
			Text = text;
			RawBytes = rawBytes;
			ResultPoints = resultPoints;
			BarcodeFormat = format;
			ResultMetadata = null;
			Timestamp = timestamp;
		}

		public void putMetadata(ResultMetadataType type, object value)
		{
			if (ResultMetadata == null)
			{
				ResultMetadata = new Dictionary<ResultMetadataType, object>();
			}
			ResultMetadata[type] = value;
		}

		public void putAllMetadata(IDictionary<ResultMetadataType, object> metadata)
		{
			if (metadata == null)
			{
				return;
			}
			if (ResultMetadata == null)
			{
				ResultMetadata = metadata;
				return;
			}
			foreach (KeyValuePair<ResultMetadataType, object> item in metadata)
			{
				ResultMetadata[item.Key] = item.Value;
			}
		}

		public void addResultPoints(ResultPoint[] newPoints)
		{
			ResultPoint[] resultPoints = ResultPoints;
			if (resultPoints == null)
			{
				ResultPoints = newPoints;
			}
			else if (newPoints != null && newPoints.Length > 0)
			{
				ResultPoint[] array = new ResultPoint[resultPoints.Length + newPoints.Length];
				Array.Copy(resultPoints, 0, array, 0, resultPoints.Length);
				Array.Copy(newPoints, 0, array, resultPoints.Length, newPoints.Length);
				ResultPoints = array;
			}
		}

		public override string ToString()
		{
			if (Text == null)
			{
				return "[" + RawBytes.Length + " bytes]";
			}
			return Text;
		}
	}
}
