using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.QrCode.Internal
{
	internal sealed class AlignmentPatternFinder
	{
		private readonly BitMatrix image;

		private readonly IList<AlignmentPattern> possibleCenters;

		private readonly int startX;

		private readonly int startY;

		private readonly int width;

		private readonly int height;

		private readonly float moduleSize;

		private readonly int[] crossCheckStateCount;

		private readonly ResultPointCallback resultPointCallback;

		internal AlignmentPatternFinder(BitMatrix image, int startX, int startY, int width, int height, float moduleSize, ResultPointCallback resultPointCallback)
		{
			this.image = image;
			possibleCenters = new List<AlignmentPattern>(5);
			this.startX = startX;
			this.startY = startY;
			this.width = width;
			this.height = height;
			this.moduleSize = moduleSize;
			crossCheckStateCount = new int[3];
			this.resultPointCallback = resultPointCallback;
		}

		internal AlignmentPattern find()
		{
			int num = startX;
			int num2 = height;
			int num3 = num + width;
			int num4 = startY + (num2 >> 1);
			int[] array = new int[3];
			for (int i = 0; i < num2; i++)
			{
				int num5 = num4 + (((i & 1) != 0) ? (-(i + 1 >> 1)) : (i + 1 >> 1));
				array[0] = 0;
				array[1] = 0;
				array[2] = 0;
				int j;
				for (j = num; j < num3 && !image[j, num5]; j++)
				{
				}
				int num6 = 0;
				for (; j < num3; j++)
				{
					if (image[j, num5])
					{
						switch (num6)
						{
						case 1:
							array[num6]++;
							break;
						case 2:
							if (foundPatternCross(array))
							{
								AlignmentPattern alignmentPattern = handlePossibleCenter(array, num5, j);
								if (alignmentPattern != null)
								{
									return alignmentPattern;
								}
							}
							array[0] = array[2];
							array[1] = 1;
							array[2] = 0;
							num6 = 1;
							break;
						default:
							array[++num6]++;
							break;
						}
					}
					else
					{
						if (num6 == 1)
						{
							num6++;
						}
						array[num6]++;
					}
				}
				if (foundPatternCross(array))
				{
					AlignmentPattern alignmentPattern2 = handlePossibleCenter(array, num5, num3);
					if (alignmentPattern2 != null)
					{
						return alignmentPattern2;
					}
				}
			}
			if (possibleCenters.Count != 0)
			{
				return possibleCenters[0];
			}
			return null;
		}

		private static float? centerFromEnd(int[] stateCount, int end)
		{
			float num = (float)(end - stateCount[2]) - (float)stateCount[1] / 2f;
			if (float.IsNaN(num))
			{
				return null;
			}
			return num;
		}

		private bool foundPatternCross(int[] stateCount)
		{
			float num = moduleSize / 2f;
			for (int i = 0; i < 3; i++)
			{
				if (Math.Abs(moduleSize - (float)stateCount[i]) >= num)
				{
					return false;
				}
			}
			return true;
		}

		private float? crossCheckVertical(int startI, int centerJ, int maxCount, int originalStateCountTotal)
		{
			int num = image.Height;
			int[] array = crossCheckStateCount;
			array[0] = 0;
			array[1] = 0;
			array[2] = 0;
			int num2 = startI;
			while (num2 >= 0 && image[centerJ, num2] && array[1] <= maxCount)
			{
				array[1]++;
				num2--;
			}
			if (num2 < 0 || array[1] > maxCount)
			{
				return null;
			}
			while (num2 >= 0 && !image[centerJ, num2] && array[0] <= maxCount)
			{
				array[0]++;
				num2--;
			}
			if (array[0] > maxCount)
			{
				return null;
			}
			for (num2 = startI + 1; num2 < num && image[centerJ, num2]; num2++)
			{
				if (array[1] > maxCount)
				{
					break;
				}
				array[1]++;
			}
			if (num2 == num || array[1] > maxCount)
			{
				return null;
			}
			for (; num2 < num && !image[centerJ, num2]; num2++)
			{
				if (array[2] > maxCount)
				{
					break;
				}
				array[2]++;
			}
			if (array[2] > maxCount)
			{
				return null;
			}
			int num3 = array[0] + array[1] + array[2];
			if (5 * Math.Abs(num3 - originalStateCountTotal) >= 2 * originalStateCountTotal)
			{
				return null;
			}
			return (!foundPatternCross(array)) ? ((float?)null) : centerFromEnd(array, num2);
		}

		private AlignmentPattern handlePossibleCenter(int[] stateCount, int i, int j)
		{
			int originalStateCountTotal = stateCount[0] + stateCount[1] + stateCount[2];
			float? num = centerFromEnd(stateCount, j);
			if (!num.HasValue)
			{
				return null;
			}
			float? num2 = crossCheckVertical(i, (int)num.Value, 2 * stateCount[1], originalStateCountTotal);
			if (num2.HasValue)
			{
				float num3 = (float)(stateCount[0] + stateCount[1] + stateCount[2]) / 3f;
				foreach (AlignmentPattern possibleCenter in possibleCenters)
				{
					if (possibleCenter.aboutEquals(num3, num2.Value, num.Value))
					{
						return possibleCenter.combineEstimate(num2.Value, num.Value, num3);
					}
				}
				AlignmentPattern alignmentPattern = new AlignmentPattern(num.Value, num2.Value, num3);
				possibleCenters.Add(alignmentPattern);
				if (resultPointCallback != null)
				{
					resultPointCallback(alignmentPattern);
				}
			}
			return null;
		}
	}
}
