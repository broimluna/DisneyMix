using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.QrCode.Internal
{
	public class FinderPatternFinder
	{
		private sealed class FurthestFromAverageComparator : IComparer<FinderPattern>
		{
			private readonly float average;

			public FurthestFromAverageComparator(float f)
			{
				average = f;
			}

			public int Compare(FinderPattern x, FinderPattern y)
			{
				float num = Math.Abs(y.EstimatedModuleSize - average);
				float num2 = Math.Abs(x.EstimatedModuleSize - average);
				return (num < num2) ? (-1) : ((num != num2) ? 1 : 0);
			}
		}

		private sealed class CenterComparator : IComparer<FinderPattern>
		{
			private readonly float average;

			public CenterComparator(float f)
			{
				average = f;
			}

			public int Compare(FinderPattern x, FinderPattern y)
			{
				if (y.Count == x.Count)
				{
					float num = Math.Abs(y.EstimatedModuleSize - average);
					float num2 = Math.Abs(x.EstimatedModuleSize - average);
					return (num < num2) ? 1 : ((num != num2) ? (-1) : 0);
				}
				return y.Count - x.Count;
			}
		}

		private const int CENTER_QUORUM = 2;

		protected internal const int MIN_SKIP = 3;

		protected internal const int MAX_MODULES = 57;

		private const int INTEGER_MATH_SHIFT = 8;

		private readonly BitMatrix image;

		private List<FinderPattern> possibleCenters;

		private bool hasSkipped;

		private readonly int[] crossCheckStateCount;

		private readonly ResultPointCallback resultPointCallback;

		protected internal virtual BitMatrix Image
		{
			get
			{
				return image;
			}
		}

		protected internal virtual List<FinderPattern> PossibleCenters
		{
			get
			{
				return possibleCenters;
			}
		}

		private int[] CrossCheckStateCount
		{
			get
			{
				crossCheckStateCount[0] = 0;
				crossCheckStateCount[1] = 0;
				crossCheckStateCount[2] = 0;
				crossCheckStateCount[3] = 0;
				crossCheckStateCount[4] = 0;
				return crossCheckStateCount;
			}
		}

		public FinderPatternFinder(BitMatrix image)
			: this(image, null)
		{
		}

		public FinderPatternFinder(BitMatrix image, ResultPointCallback resultPointCallback)
		{
			this.image = image;
			possibleCenters = new List<FinderPattern>();
			crossCheckStateCount = new int[5];
			this.resultPointCallback = resultPointCallback;
		}

		internal virtual FinderPatternInfo find(IDictionary<DecodeHintType, object> hints)
		{
			bool flag = hints != null && hints.ContainsKey(DecodeHintType.TRY_HARDER);
			bool pureBarcode = hints != null && hints.ContainsKey(DecodeHintType.PURE_BARCODE);
			int height = image.Height;
			int width = image.Width;
			int num = 3 * height / 228;
			if (num < 3 || flag)
			{
				num = 3;
			}
			bool flag2 = false;
			int[] array = new int[5];
			for (int i = num - 1; i < height; i += num)
			{
				if (flag2)
				{
					break;
				}
				array[0] = 0;
				array[1] = 0;
				array[2] = 0;
				array[3] = 0;
				array[4] = 0;
				int num2 = 0;
				for (int j = 0; j < width; j++)
				{
					if (image[j, i])
					{
						if ((num2 & 1) == 1)
						{
							num2++;
						}
						array[num2]++;
					}
					else if ((num2 & 1) == 0)
					{
						if (num2 == 4)
						{
							if (foundPatternCross(array))
							{
								if (handlePossibleCenter(array, i, j, pureBarcode))
								{
									num = 2;
									if (hasSkipped)
									{
										flag2 = haveMultiplyConfirmedCenters();
									}
									else
									{
										int num3 = findRowSkip();
										if (num3 > array[2])
										{
											i += num3 - array[2] - num;
											j = width - 1;
										}
									}
									num2 = 0;
									array[0] = 0;
									array[1] = 0;
									array[2] = 0;
									array[3] = 0;
									array[4] = 0;
								}
								else
								{
									array[0] = array[2];
									array[1] = array[3];
									array[2] = array[4];
									array[3] = 1;
									array[4] = 0;
									num2 = 3;
								}
							}
							else
							{
								array[0] = array[2];
								array[1] = array[3];
								array[2] = array[4];
								array[3] = 1;
								array[4] = 0;
								num2 = 3;
							}
						}
						else
						{
							array[++num2]++;
						}
					}
					else
					{
						array[num2]++;
					}
				}
				if (foundPatternCross(array) && handlePossibleCenter(array, i, width, pureBarcode))
				{
					num = array[0];
					if (hasSkipped)
					{
						flag2 = haveMultiplyConfirmedCenters();
					}
				}
			}
			FinderPattern[] array2 = selectBestPatterns();
			if (array2 == null)
			{
				return null;
			}
			ResultPoint.orderBestPatterns(array2);
			return new FinderPatternInfo(array2);
		}

		private static float? centerFromEnd(int[] stateCount, int end)
		{
			float num = (float)(end - stateCount[4] - stateCount[3]) - (float)stateCount[2] / 2f;
			if (float.IsNaN(num))
			{
				return null;
			}
			return num;
		}

		protected internal static bool foundPatternCross(int[] stateCount)
		{
			int num = 0;
			for (int i = 0; i < 5; i++)
			{
				int num2 = stateCount[i];
				if (num2 == 0)
				{
					return false;
				}
				num += num2;
			}
			if (num < 7)
			{
				return false;
			}
			int num3 = (num << 8) / 7;
			int num4 = num3 / 2;
			return Math.Abs(num3 - (stateCount[0] << 8)) < num4 && Math.Abs(num3 - (stateCount[1] << 8)) < num4 && Math.Abs(3 * num3 - (stateCount[2] << 8)) < 3 * num4 && Math.Abs(num3 - (stateCount[3] << 8)) < num4 && Math.Abs(num3 - (stateCount[4] << 8)) < num4;
		}

		private bool crossCheckDiagonal(int startI, int centerJ, int maxCount, int originalStateCountTotal)
		{
			int height = image.Height;
			int width = image.Width;
			int[] array = CrossCheckStateCount;
			int i;
			for (i = 0; startI - i >= 0 && image[centerJ - i, startI - i]; i++)
			{
				array[2]++;
			}
			if (startI - i < 0 || centerJ - i < 0)
			{
				return false;
			}
			for (; startI - i >= 0 && centerJ - i >= 0 && !image[centerJ - i, startI - i]; i++)
			{
				if (array[1] > maxCount)
				{
					break;
				}
				array[1]++;
			}
			if (startI - i < 0 || centerJ - i < 0 || array[1] > maxCount)
			{
				return false;
			}
			for (; startI - i >= 0 && centerJ - i >= 0 && image[centerJ - i, startI - i]; i++)
			{
				if (array[0] > maxCount)
				{
					break;
				}
				array[0]++;
			}
			if (array[0] > maxCount)
			{
				return false;
			}
			for (i = 1; startI + i < height && centerJ + i < width && image[centerJ + i, startI + i]; i++)
			{
				array[2]++;
			}
			if (startI + i >= height || centerJ + i >= width)
			{
				return false;
			}
			for (; startI + i < height && centerJ + i < width && !image[centerJ + i, startI + i]; i++)
			{
				if (array[3] >= maxCount)
				{
					break;
				}
				array[3]++;
			}
			if (startI + i >= height || centerJ + i >= width || array[3] >= maxCount)
			{
				return false;
			}
			for (; startI + i < height && centerJ + i < width && image[centerJ + i, startI + i]; i++)
			{
				if (array[4] >= maxCount)
				{
					break;
				}
				array[4]++;
			}
			if (array[4] >= maxCount)
			{
				return false;
			}
			int num = array[0] + array[1] + array[2] + array[3] + array[4];
			return Math.Abs(num - originalStateCountTotal) < 2 * originalStateCountTotal && foundPatternCross(array);
		}

		private float? crossCheckVertical(int startI, int centerJ, int maxCount, int originalStateCountTotal)
		{
			int height = image.Height;
			int[] array = CrossCheckStateCount;
			int num = startI;
			while (num >= 0 && image[centerJ, num])
			{
				array[2]++;
				num--;
			}
			if (num < 0)
			{
				return null;
			}
			while (num >= 0 && !image[centerJ, num] && array[1] <= maxCount)
			{
				array[1]++;
				num--;
			}
			if (num < 0 || array[1] > maxCount)
			{
				return null;
			}
			while (num >= 0 && image[centerJ, num] && array[0] <= maxCount)
			{
				array[0]++;
				num--;
			}
			if (array[0] > maxCount)
			{
				return null;
			}
			for (num = startI + 1; num < height && image[centerJ, num]; num++)
			{
				array[2]++;
			}
			if (num == height)
			{
				return null;
			}
			for (; num < height && !image[centerJ, num]; num++)
			{
				if (array[3] >= maxCount)
				{
					break;
				}
				array[3]++;
			}
			if (num == height || array[3] >= maxCount)
			{
				return null;
			}
			for (; num < height && image[centerJ, num]; num++)
			{
				if (array[4] >= maxCount)
				{
					break;
				}
				array[4]++;
			}
			if (array[4] >= maxCount)
			{
				return null;
			}
			int num2 = array[0] + array[1] + array[2] + array[3] + array[4];
			if (5 * Math.Abs(num2 - originalStateCountTotal) >= 2 * originalStateCountTotal)
			{
				return null;
			}
			return (!foundPatternCross(array)) ? ((float?)null) : centerFromEnd(array, num);
		}

		private float? crossCheckHorizontal(int startJ, int centerI, int maxCount, int originalStateCountTotal)
		{
			int width = image.Width;
			int[] array = CrossCheckStateCount;
			int num = startJ;
			while (num >= 0 && image[num, centerI])
			{
				array[2]++;
				num--;
			}
			if (num < 0)
			{
				return null;
			}
			while (num >= 0 && !image[num, centerI] && array[1] <= maxCount)
			{
				array[1]++;
				num--;
			}
			if (num < 0 || array[1] > maxCount)
			{
				return null;
			}
			while (num >= 0 && image[num, centerI] && array[0] <= maxCount)
			{
				array[0]++;
				num--;
			}
			if (array[0] > maxCount)
			{
				return null;
			}
			for (num = startJ + 1; num < width && image[num, centerI]; num++)
			{
				array[2]++;
			}
			if (num == width)
			{
				return null;
			}
			for (; num < width && !image[num, centerI]; num++)
			{
				if (array[3] >= maxCount)
				{
					break;
				}
				array[3]++;
			}
			if (num == width || array[3] >= maxCount)
			{
				return null;
			}
			for (; num < width && image[num, centerI]; num++)
			{
				if (array[4] >= maxCount)
				{
					break;
				}
				array[4]++;
			}
			if (array[4] >= maxCount)
			{
				return null;
			}
			int num2 = array[0] + array[1] + array[2] + array[3] + array[4];
			if (5 * Math.Abs(num2 - originalStateCountTotal) >= originalStateCountTotal)
			{
				return null;
			}
			return (!foundPatternCross(array)) ? ((float?)null) : centerFromEnd(array, num);
		}

		protected bool handlePossibleCenter(int[] stateCount, int i, int j, bool pureBarcode)
		{
			int num = stateCount[0] + stateCount[1] + stateCount[2] + stateCount[3] + stateCount[4];
			float? num2 = centerFromEnd(stateCount, j);
			if (!num2.HasValue)
			{
				return false;
			}
			float? num3 = crossCheckVertical(i, (int)num2.Value, stateCount[2], num);
			if (num3.HasValue)
			{
				num2 = crossCheckHorizontal((int)num2.Value, (int)num3.Value, stateCount[2], num);
				if (num2.HasValue && (!pureBarcode || crossCheckDiagonal((int)num3.Value, (int)num2.Value, stateCount[2], num)))
				{
					float num4 = (float)num / 7f;
					bool flag = false;
					for (int k = 0; k < possibleCenters.Count; k++)
					{
						FinderPattern finderPattern = possibleCenters[k];
						if (finderPattern.aboutEquals(num4, num3.Value, num2.Value))
						{
							possibleCenters.RemoveAt(k);
							possibleCenters.Insert(k, finderPattern.combineEstimate(num3.Value, num2.Value, num4));
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						FinderPattern finderPattern2 = new FinderPattern(num2.Value, num3.Value, num4);
						possibleCenters.Add(finderPattern2);
						if (resultPointCallback != null)
						{
							resultPointCallback(finderPattern2);
						}
					}
					return true;
				}
			}
			return false;
		}

		private int findRowSkip()
		{
			int count = possibleCenters.Count;
			if (count <= 1)
			{
				return 0;
			}
			ResultPoint resultPoint = null;
			foreach (FinderPattern possibleCenter in possibleCenters)
			{
				if (possibleCenter.Count >= 2)
				{
					if (resultPoint != null)
					{
						hasSkipped = true;
						return (int)(Math.Abs(resultPoint.X - possibleCenter.X) - Math.Abs(resultPoint.Y - possibleCenter.Y)) / 2;
					}
					resultPoint = possibleCenter;
				}
			}
			return 0;
		}

		private bool haveMultiplyConfirmedCenters()
		{
			int num = 0;
			float num2 = 0f;
			int count = possibleCenters.Count;
			foreach (FinderPattern possibleCenter in possibleCenters)
			{
				if (possibleCenter.Count >= 2)
				{
					num++;
					num2 += possibleCenter.EstimatedModuleSize;
				}
			}
			if (num < 3)
			{
				return false;
			}
			float num3 = num2 / (float)count;
			float num4 = 0f;
			for (int i = 0; i < count; i++)
			{
				FinderPattern finderPattern = possibleCenters[i];
				num4 += Math.Abs(finderPattern.EstimatedModuleSize - num3);
			}
			return num4 <= 0.05f * num2;
		}

		private FinderPattern[] selectBestPatterns()
		{
			int count = possibleCenters.Count;
			if (count < 3)
			{
				return null;
			}
			if (count > 3)
			{
				float num = 0f;
				float num2 = 0f;
				foreach (FinderPattern possibleCenter in possibleCenters)
				{
					float estimatedModuleSize = possibleCenter.EstimatedModuleSize;
					num += estimatedModuleSize;
					num2 += estimatedModuleSize * estimatedModuleSize;
				}
				float num3 = num / (float)count;
				float val = (float)Math.Sqrt(num2 / (float)count - num3 * num3);
				possibleCenters.Sort(new FurthestFromAverageComparator(num3));
				float num4 = Math.Max(0.2f * num3, val);
				for (int i = 0; i < possibleCenters.Count; i++)
				{
					if (possibleCenters.Count <= 3)
					{
						break;
					}
					FinderPattern finderPattern = possibleCenters[i];
					if (Math.Abs(finderPattern.EstimatedModuleSize - num3) > num4)
					{
						possibleCenters.RemoveAt(i);
						i--;
					}
				}
			}
			if (possibleCenters.Count > 3)
			{
				float num5 = 0f;
				foreach (FinderPattern possibleCenter2 in possibleCenters)
				{
					num5 += possibleCenter2.EstimatedModuleSize;
				}
				float f = num5 / (float)possibleCenters.Count;
				possibleCenters.Sort(new CenterComparator(f));
				possibleCenters = possibleCenters.GetRange(0, 3);
			}
			return new FinderPattern[3]
			{
				possibleCenters[0],
				possibleCenters[1],
				possibleCenters[2]
			};
		}
	}
}
