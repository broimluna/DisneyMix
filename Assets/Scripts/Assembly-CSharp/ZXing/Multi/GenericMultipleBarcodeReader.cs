using System.Collections.Generic;

namespace ZXing.Multi
{
	public sealed class GenericMultipleBarcodeReader : Reader, MultipleBarcodeReader
	{
		private const int MIN_DIMENSION_TO_RECUR = 30;

		private const int MAX_DEPTH = 4;

		private readonly Reader _delegate;

		public GenericMultipleBarcodeReader(Reader @delegate)
		{
			_delegate = @delegate;
		}

		public Result[] decodeMultiple(BinaryBitmap image)
		{
			return decodeMultiple(image, null);
		}

		public Result[] decodeMultiple(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			List<Result> list = new List<Result>();
			doDecodeMultiple(image, hints, list, 0, 0, 0);
			if (list.Count == 0)
			{
				return null;
			}
			int count = list.Count;
			Result[] array = new Result[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = list[i];
			}
			return array;
		}

		private void doDecodeMultiple(BinaryBitmap image, IDictionary<DecodeHintType, object> hints, IList<Result> results, int xOffset, int yOffset, int currentDepth)
		{
			if (currentDepth > 4)
			{
				return;
			}
			Result result = _delegate.decode(image, hints);
			if (result == null)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < results.Count; i++)
			{
				Result result2 = results[i];
				if (result2.Text.Equals(result.Text))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				results.Add(translateResultPoints(result, xOffset, yOffset));
			}
			ResultPoint[] resultPoints = result.ResultPoints;
			if (resultPoints == null || resultPoints.Length == 0)
			{
				return;
			}
			int width = image.Width;
			int height = image.Height;
			float num = width;
			float num2 = height;
			float num3 = 0f;
			float num4 = 0f;
			foreach (ResultPoint resultPoint in resultPoints)
			{
				float x = resultPoint.X;
				float y = resultPoint.Y;
				if (x < num)
				{
					num = x;
				}
				if (y < num2)
				{
					num2 = y;
				}
				if (x > num3)
				{
					num3 = x;
				}
				if (y > num4)
				{
					num4 = y;
				}
			}
			if (num > 30f)
			{
				doDecodeMultiple(image.crop(0, 0, (int)num, height), hints, results, xOffset, yOffset, currentDepth + 1);
			}
			if (num2 > 30f)
			{
				doDecodeMultiple(image.crop(0, 0, width, (int)num2), hints, results, xOffset, yOffset, currentDepth + 1);
			}
			if (num3 < (float)(width - 30))
			{
				doDecodeMultiple(image.crop((int)num3, 0, width - (int)num3, height), hints, results, xOffset + (int)num3, yOffset, currentDepth + 1);
			}
			if (num4 < (float)(height - 30))
			{
				doDecodeMultiple(image.crop(0, (int)num4, width, height - (int)num4), hints, results, xOffset, yOffset + (int)num4, currentDepth + 1);
			}
		}

		private static Result translateResultPoints(Result result, int xOffset, int yOffset)
		{
			ResultPoint[] resultPoints = result.ResultPoints;
			ResultPoint[] array = new ResultPoint[resultPoints.Length];
			for (int i = 0; i < resultPoints.Length; i++)
			{
				ResultPoint resultPoint = resultPoints[i];
				array[i] = new ResultPoint(resultPoint.X + (float)xOffset, resultPoint.Y + (float)yOffset);
			}
			Result result2 = new Result(result.Text, result.RawBytes, array, result.BarcodeFormat);
			result2.putAllMetadata(result.ResultMetadata);
			return result2;
		}

		public Result decode(BinaryBitmap image)
		{
			return _delegate.decode(image);
		}

		public Result decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			return _delegate.decode(image, hints);
		}

		public void reset()
		{
			_delegate.reset();
		}
	}
}
