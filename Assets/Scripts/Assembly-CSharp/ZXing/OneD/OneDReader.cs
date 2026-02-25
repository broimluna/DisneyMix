using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD
{
	public abstract class OneDReader : Reader
	{
		protected static int INTEGER_MATH_SHIFT = 8;

		protected static int PATTERN_MATCH_RESULT_SCALE_FACTOR = 1 << INTEGER_MATH_SHIFT;

		public Result decode(BinaryBitmap image)
		{
			return decode(image, null);
		}

		public virtual Result decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			Result result = doDecode(image, hints);
			if (result == null)
			{
				bool flag = hints != null && hints.ContainsKey(DecodeHintType.TRY_HARDER);
				bool flag2 = hints != null && hints.ContainsKey(DecodeHintType.TRY_HARDER_WITHOUT_ROTATION);
				if (flag && !flag2 && image.RotateSupported)
				{
					BinaryBitmap binaryBitmap = image.rotateCounterClockwise();
					result = doDecode(binaryBitmap, hints);
					if (result == null)
					{
						return null;
					}
					IDictionary<ResultMetadataType, object> resultMetadata = result.ResultMetadata;
					int num = 270;
					if (resultMetadata != null && resultMetadata.ContainsKey(ResultMetadataType.ORIENTATION))
					{
						num = (num + (int)resultMetadata[ResultMetadataType.ORIENTATION]) % 360;
					}
					result.putMetadata(ResultMetadataType.ORIENTATION, num);
					ResultPoint[] resultPoints = result.ResultPoints;
					if (resultPoints != null)
					{
						int height = binaryBitmap.Height;
						for (int i = 0; i < resultPoints.Length; i++)
						{
							resultPoints[i] = new ResultPoint((float)height - resultPoints[i].Y - 1f, resultPoints[i].X);
						}
					}
				}
			}
			return result;
		}

		public virtual void reset()
		{
		}

		private Result doDecode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			int width = image.Width;
			int height = image.Height;
			BitArray bitArray = new BitArray(width);
			int num = height >> 1;
			bool flag = hints != null && hints.ContainsKey(DecodeHintType.TRY_HARDER);
			int num2 = Math.Max(1, height >> ((!flag) ? 5 : 8));
			int num3 = ((!flag) ? 15 : height);
			for (int i = 0; i < num3; i++)
			{
				int num4 = i + 1 >> 1;
				bool flag2 = (i & 1) == 0;
				int num5 = num + num2 * ((!flag2) ? (-num4) : num4);
				if (num5 < 0 || num5 >= height)
				{
					break;
				}
				bitArray = image.getBlackRow(num5, bitArray);
				if (bitArray == null)
				{
					continue;
				}
				for (int j = 0; j < 2; j++)
				{
					if (j == 1)
					{
						bitArray.reverse();
						if (hints != null && hints.ContainsKey(DecodeHintType.NEED_RESULT_POINT_CALLBACK))
						{
							IDictionary<DecodeHintType, object> dictionary = new Dictionary<DecodeHintType, object>();
							foreach (KeyValuePair<DecodeHintType, object> hint in hints)
							{
								if (hint.Key != DecodeHintType.NEED_RESULT_POINT_CALLBACK)
								{
									dictionary.Add(hint.Key, hint.Value);
								}
							}
							hints = dictionary;
						}
					}
					Result result = decodeRow(num5, bitArray, hints);
					if (result == null)
					{
						continue;
					}
					if (j == 1)
					{
						result.putMetadata(ResultMetadataType.ORIENTATION, 180);
						ResultPoint[] resultPoints = result.ResultPoints;
						if (resultPoints != null)
						{
							resultPoints[0] = new ResultPoint((float)width - resultPoints[0].X - 1f, resultPoints[0].Y);
							resultPoints[1] = new ResultPoint((float)width - resultPoints[1].X - 1f, resultPoints[1].Y);
						}
					}
					return result;
				}
			}
			return null;
		}

		protected static bool recordPattern(BitArray row, int start, int[] counters)
		{
			return recordPattern(row, start, counters, counters.Length);
		}

		protected static bool recordPattern(BitArray row, int start, int[] counters, int numCounters)
		{
			for (int i = 0; i < numCounters; i++)
			{
				counters[i] = 0;
			}
			int size = row.Size;
			if (start >= size)
			{
				return false;
			}
			bool flag = !row[start];
			int num = 0;
			int j;
			for (j = start; j < size; j++)
			{
				if (row[j] ^ flag)
				{
					counters[num]++;
					continue;
				}
				num++;
				if (num == numCounters)
				{
					break;
				}
				counters[num] = 1;
				flag = !flag;
			}
			return num == numCounters || (num == numCounters - 1 && j == size);
		}

		protected static bool recordPatternInReverse(BitArray row, int start, int[] counters)
		{
			int num = counters.Length;
			bool flag = row[start];
			while (start > 0 && num >= 0)
			{
				if (row[--start] != flag)
				{
					num--;
					flag = !flag;
				}
			}
			if (num >= 0)
			{
				return false;
			}
			return recordPattern(row, start + 1, counters);
		}

		protected static int patternMatchVariance(int[] counters, int[] pattern, int maxIndividualVariance)
		{
			int num = counters.Length;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < num; i++)
			{
				num2 += counters[i];
				num3 += pattern[i];
			}
			if (num2 < num3)
			{
				return int.MaxValue;
			}
			int num4 = (num2 << INTEGER_MATH_SHIFT) / num3;
			maxIndividualVariance = maxIndividualVariance * num4 >> INTEGER_MATH_SHIFT;
			int num5 = 0;
			for (int j = 0; j < num; j++)
			{
				int num6 = counters[j] << INTEGER_MATH_SHIFT;
				int num7 = pattern[j] * num4;
				int num8 = ((num6 <= num7) ? (num7 - num6) : (num6 - num7));
				if (num8 > maxIndividualVariance)
				{
					return int.MaxValue;
				}
				num5 += num8;
			}
			return num5 / num2;
		}

		public abstract Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints);
	}
}
