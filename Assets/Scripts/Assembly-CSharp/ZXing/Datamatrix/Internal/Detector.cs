using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing.Common.Detector;

namespace ZXing.Datamatrix.Internal
{
	public sealed class Detector
	{
		private sealed class ResultPointsAndTransitions
		{
			public ResultPoint From { get; private set; }

			public ResultPoint To { get; private set; }

			public int Transitions { get; private set; }

			public ResultPointsAndTransitions(ResultPoint from, ResultPoint to, int transitions)
			{
				From = from;
				To = to;
				Transitions = transitions;
			}

			public override string ToString()
			{
				return string.Concat(From, "/", To, '/', Transitions);
			}
		}

		private sealed class ResultPointsAndTransitionsComparator : IComparer<ResultPointsAndTransitions>
		{
			public int Compare(ResultPointsAndTransitions o1, ResultPointsAndTransitions o2)
			{
				return o1.Transitions - o2.Transitions;
			}
		}

		private readonly BitMatrix image;

		private readonly WhiteRectangleDetector rectangleDetector;

		public Detector(BitMatrix image)
		{
			this.image = image;
			rectangleDetector = WhiteRectangleDetector.Create(image);
		}

		public DetectorResult detect()
		{
			if (rectangleDetector == null)
			{
				return null;
			}
			ResultPoint[] array = rectangleDetector.detect();
			if (array == null)
			{
				return null;
			}
			ResultPoint resultPoint = array[0];
			ResultPoint resultPoint2 = array[1];
			ResultPoint resultPoint3 = array[2];
			ResultPoint resultPoint4 = array[3];
			List<ResultPointsAndTransitions> list = new List<ResultPointsAndTransitions>(4);
			list.Add(transitionsBetween(resultPoint, resultPoint2));
			list.Add(transitionsBetween(resultPoint, resultPoint3));
			list.Add(transitionsBetween(resultPoint2, resultPoint4));
			list.Add(transitionsBetween(resultPoint3, resultPoint4));
			list.Sort(new ResultPointsAndTransitionsComparator());
			ResultPointsAndTransitions resultPointsAndTransitions = list[0];
			ResultPointsAndTransitions resultPointsAndTransitions2 = list[1];
			Dictionary<ResultPoint, int> dictionary = new Dictionary<ResultPoint, int>();
			increment(dictionary, resultPointsAndTransitions.From);
			increment(dictionary, resultPointsAndTransitions.To);
			increment(dictionary, resultPointsAndTransitions2.From);
			increment(dictionary, resultPointsAndTransitions2.To);
			ResultPoint resultPoint5 = null;
			ResultPoint resultPoint6 = null;
			ResultPoint resultPoint7 = null;
			foreach (KeyValuePair<ResultPoint, int> item in dictionary)
			{
				ResultPoint key = item.Key;
				int value = item.Value;
				if (value == 2)
				{
					resultPoint6 = key;
				}
				else if (resultPoint5 == null)
				{
					resultPoint5 = key;
				}
				else
				{
					resultPoint7 = key;
				}
			}
			if (resultPoint5 == null || resultPoint6 == null || resultPoint7 == null)
			{
				return null;
			}
			ResultPoint[] array2 = new ResultPoint[3] { resultPoint5, resultPoint6, resultPoint7 };
			ResultPoint.orderBestPatterns(array2);
			ResultPoint resultPoint8 = array2[0];
			resultPoint6 = array2[1];
			ResultPoint resultPoint9 = array2[2];
			ResultPoint resultPoint10 = ((!dictionary.ContainsKey(resultPoint)) ? resultPoint : ((!dictionary.ContainsKey(resultPoint2)) ? resultPoint2 : (dictionary.ContainsKey(resultPoint3) ? resultPoint4 : resultPoint3)));
			int num = transitionsBetween(resultPoint9, resultPoint10).Transitions;
			int num2 = transitionsBetween(resultPoint8, resultPoint10).Transitions;
			if ((num & 1) == 1)
			{
				num++;
			}
			num += 2;
			if ((num2 & 1) == 1)
			{
				num2++;
			}
			num2 += 2;
			ResultPoint resultPoint11;
			BitMatrix bitMatrix;
			if (4 * num >= 7 * num2 || 4 * num2 >= 7 * num)
			{
				resultPoint11 = correctTopRightRectangular(resultPoint6, resultPoint8, resultPoint9, resultPoint10, num, num2);
				if (resultPoint11 == null)
				{
					resultPoint11 = resultPoint10;
				}
				num = transitionsBetween(resultPoint9, resultPoint11).Transitions;
				num2 = transitionsBetween(resultPoint8, resultPoint11).Transitions;
				if ((num & 1) == 1)
				{
					num++;
				}
				if ((num2 & 1) == 1)
				{
					num2++;
				}
				bitMatrix = sampleGrid(image, resultPoint9, resultPoint6, resultPoint8, resultPoint11, num, num2);
			}
			else
			{
				int dimension = Math.Min(num2, num);
				resultPoint11 = correctTopRight(resultPoint6, resultPoint8, resultPoint9, resultPoint10, dimension);
				if (resultPoint11 == null)
				{
					resultPoint11 = resultPoint10;
				}
				int num3 = Math.Max(transitionsBetween(resultPoint9, resultPoint11).Transitions, transitionsBetween(resultPoint8, resultPoint11).Transitions);
				num3++;
				if ((num3 & 1) == 1)
				{
					num3++;
				}
				bitMatrix = sampleGrid(image, resultPoint9, resultPoint6, resultPoint8, resultPoint11, num3, num3);
			}
			if (bitMatrix == null)
			{
				return null;
			}
			return new DetectorResult(bitMatrix, new ResultPoint[4] { resultPoint9, resultPoint6, resultPoint8, resultPoint11 });
		}

		private ResultPoint correctTopRightRectangular(ResultPoint bottomLeft, ResultPoint bottomRight, ResultPoint topLeft, ResultPoint topRight, int dimensionTop, int dimensionRight)
		{
			float num = (float)distance(bottomLeft, bottomRight) / (float)dimensionTop;
			int num2 = distance(topLeft, topRight);
			if (num2 == 0)
			{
				return null;
			}
			float num3 = (topRight.X - topLeft.X) / (float)num2;
			float num4 = (topRight.Y - topLeft.Y) / (float)num2;
			ResultPoint resultPoint = new ResultPoint(topRight.X + num * num3, topRight.Y + num * num4);
			num = (float)distance(bottomLeft, topLeft) / (float)dimensionRight;
			num2 = distance(bottomRight, topRight);
			if (num2 == 0)
			{
				return null;
			}
			num3 = (topRight.X - bottomRight.X) / (float)num2;
			num4 = (topRight.Y - bottomRight.Y) / (float)num2;
			ResultPoint resultPoint2 = new ResultPoint(topRight.X + num * num3, topRight.Y + num * num4);
			if (!isValid(resultPoint))
			{
				if (isValid(resultPoint2))
				{
					return resultPoint2;
				}
				return null;
			}
			if (!isValid(resultPoint2))
			{
				return resultPoint;
			}
			int num5 = Math.Abs(dimensionTop - transitionsBetween(topLeft, resultPoint).Transitions) + Math.Abs(dimensionRight - transitionsBetween(bottomRight, resultPoint).Transitions);
			int num6 = Math.Abs(dimensionTop - transitionsBetween(topLeft, resultPoint2).Transitions) + Math.Abs(dimensionRight - transitionsBetween(bottomRight, resultPoint2).Transitions);
			if (num5 <= num6)
			{
				return resultPoint;
			}
			return resultPoint2;
		}

		private ResultPoint correctTopRight(ResultPoint bottomLeft, ResultPoint bottomRight, ResultPoint topLeft, ResultPoint topRight, int dimension)
		{
			float num = (float)distance(bottomLeft, bottomRight) / (float)dimension;
			int num2 = distance(topLeft, topRight);
			if (num2 == 0)
			{
				return null;
			}
			float num3 = (topRight.X - topLeft.X) / (float)num2;
			float num4 = (topRight.Y - topLeft.Y) / (float)num2;
			ResultPoint resultPoint = new ResultPoint(topRight.X + num * num3, topRight.Y + num * num4);
			num = (float)distance(bottomLeft, topLeft) / (float)dimension;
			num2 = distance(bottomRight, topRight);
			if (num2 == 0)
			{
				return null;
			}
			num3 = (topRight.X - bottomRight.X) / (float)num2;
			num4 = (topRight.Y - bottomRight.Y) / (float)num2;
			ResultPoint resultPoint2 = new ResultPoint(topRight.X + num * num3, topRight.Y + num * num4);
			if (!isValid(resultPoint))
			{
				if (isValid(resultPoint2))
				{
					return resultPoint2;
				}
				return null;
			}
			if (!isValid(resultPoint2))
			{
				return resultPoint;
			}
			int num5 = Math.Abs(transitionsBetween(topLeft, resultPoint).Transitions - transitionsBetween(bottomRight, resultPoint).Transitions);
			int num6 = Math.Abs(transitionsBetween(topLeft, resultPoint2).Transitions - transitionsBetween(bottomRight, resultPoint2).Transitions);
			return (num5 > num6) ? resultPoint2 : resultPoint;
		}

		private bool isValid(ResultPoint p)
		{
			return p.X >= 0f && p.X < (float)image.Width && p.Y > 0f && p.Y < (float)image.Height;
		}

		private static int distance(ResultPoint a, ResultPoint b)
		{
			return MathUtils.round(ResultPoint.distance(a, b));
		}

		private static void increment(IDictionary<ResultPoint, int> table, ResultPoint key)
		{
			if (table.ContainsKey(key))
			{
				table[key]++;
			}
			else
			{
				table[key] = 1;
			}
		}

		private static BitMatrix sampleGrid(BitMatrix image, ResultPoint topLeft, ResultPoint bottomLeft, ResultPoint bottomRight, ResultPoint topRight, int dimensionX, int dimensionY)
		{
			GridSampler instance = GridSampler.Instance;
			return instance.sampleGrid(image, dimensionX, dimensionY, 0.5f, 0.5f, (float)dimensionX - 0.5f, 0.5f, (float)dimensionX - 0.5f, (float)dimensionY - 0.5f, 0.5f, (float)dimensionY - 0.5f, topLeft.X, topLeft.Y, topRight.X, topRight.Y, bottomRight.X, bottomRight.Y, bottomLeft.X, bottomLeft.Y);
		}

		private ResultPointsAndTransitions transitionsBetween(ResultPoint from, ResultPoint to)
		{
			int num = (int)from.X;
			int num2 = (int)from.Y;
			int num3 = (int)to.X;
			int num4 = (int)to.Y;
			bool flag = Math.Abs(num4 - num2) > Math.Abs(num3 - num);
			if (flag)
			{
				int num5 = num;
				num = num2;
				num2 = num5;
				num5 = num3;
				num3 = num4;
				num4 = num5;
			}
			int num6 = Math.Abs(num3 - num);
			int num7 = Math.Abs(num4 - num2);
			int num8 = -num6 >> 1;
			int num9 = ((num2 < num4) ? 1 : (-1));
			int num10 = ((num < num3) ? 1 : (-1));
			int num11 = 0;
			bool flag2 = image[(!flag) ? num : num2, (!flag) ? num2 : num];
			int i = num;
			int num12 = num2;
			for (; i != num3; i += num10)
			{
				bool flag3 = image[(!flag) ? i : num12, (!flag) ? num12 : i];
				if (flag3 != flag2)
				{
					num11++;
					flag2 = flag3;
				}
				num8 += num7;
				if (num8 > 0)
				{
					if (num12 == num4)
					{
						break;
					}
					num12 += num9;
					num8 -= num6;
				}
			}
			return new ResultPointsAndTransitions(from, to, num11);
		}
	}
}
