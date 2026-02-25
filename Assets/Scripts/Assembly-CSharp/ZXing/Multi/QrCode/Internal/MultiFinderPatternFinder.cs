using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing.QrCode.Internal;

namespace ZXing.Multi.QrCode.Internal
{
	internal sealed class MultiFinderPatternFinder : FinderPatternFinder
	{
		private sealed class ModuleSizeComparator : IComparer<FinderPattern>
		{
			public int Compare(FinderPattern center1, FinderPattern center2)
			{
				float num = center2.EstimatedModuleSize - center1.EstimatedModuleSize;
				return ((double)num < 0.0) ? (-1) : (((double)num > 0.0) ? 1 : 0);
			}
		}

		private const float DIFF_MODSIZE_CUTOFF = 0.5f;

		private static FinderPatternInfo[] EMPTY_RESULT_ARRAY = new FinderPatternInfo[0];

		private static float MAX_MODULE_COUNT_PER_EDGE = 180f;

		private static float MIN_MODULE_COUNT_PER_EDGE = 9f;

		private static float DIFF_MODSIZE_CUTOFF_PERCENT = 0.05f;

		internal MultiFinderPatternFinder(BitMatrix image)
			: base(image)
		{
		}

		internal MultiFinderPatternFinder(BitMatrix image, ResultPointCallback resultPointCallback)
			: base(image, resultPointCallback)
		{
		}

		private FinderPattern[][] selectMutipleBestPatterns()
		{
			List<FinderPattern> list = PossibleCenters;
			int count = list.Count;
			if (count < 3)
			{
				return null;
			}
			if (count == 3)
			{
				return new FinderPattern[1][] { new FinderPattern[3]
				{
					list[0],
					list[1],
					list[2]
				} };
			}
			list.Sort(new ModuleSizeComparator());
			List<FinderPattern[]> list2 = new List<FinderPattern[]>();
			for (int i = 0; i < count - 2; i++)
			{
				FinderPattern finderPattern = list[i];
				if (finderPattern == null)
				{
					continue;
				}
				for (int j = i + 1; j < count - 1; j++)
				{
					FinderPattern finderPattern2 = list[j];
					if (finderPattern2 == null)
					{
						continue;
					}
					float num = (finderPattern.EstimatedModuleSize - finderPattern2.EstimatedModuleSize) / Math.Min(finderPattern.EstimatedModuleSize, finderPattern2.EstimatedModuleSize);
					float num2 = Math.Abs(finderPattern.EstimatedModuleSize - finderPattern2.EstimatedModuleSize);
					if (num2 > 0.5f && num >= DIFF_MODSIZE_CUTOFF_PERCENT)
					{
						break;
					}
					for (int k = j + 1; k < count; k++)
					{
						FinderPattern finderPattern3 = list[k];
						if (finderPattern3 == null)
						{
							continue;
						}
						float num3 = (finderPattern2.EstimatedModuleSize - finderPattern3.EstimatedModuleSize) / Math.Min(finderPattern2.EstimatedModuleSize, finderPattern3.EstimatedModuleSize);
						float num4 = Math.Abs(finderPattern2.EstimatedModuleSize - finderPattern3.EstimatedModuleSize);
						if (num4 > 0.5f && num3 >= DIFF_MODSIZE_CUTOFF_PERCENT)
						{
							break;
						}
						FinderPattern[] array = new FinderPattern[3] { finderPattern, finderPattern2, finderPattern3 };
						ResultPoint.orderBestPatterns(array);
						FinderPatternInfo finderPatternInfo = new FinderPatternInfo(array);
						float num5 = ResultPoint.distance(finderPatternInfo.TopLeft, finderPatternInfo.BottomLeft);
						float num6 = ResultPoint.distance(finderPatternInfo.TopRight, finderPatternInfo.BottomLeft);
						float num7 = ResultPoint.distance(finderPatternInfo.TopLeft, finderPatternInfo.TopRight);
						float num8 = (num5 + num7) / (finderPattern.EstimatedModuleSize * 2f);
						if (num8 > MAX_MODULE_COUNT_PER_EDGE || num8 < MIN_MODULE_COUNT_PER_EDGE)
						{
							continue;
						}
						float num9 = Math.Abs((num5 - num7) / Math.Min(num5, num7));
						if (!(num9 >= 0.1f))
						{
							float num10 = (float)Math.Sqrt(num5 * num5 + num7 * num7);
							float num11 = Math.Abs((num6 - num10) / Math.Min(num6, num10));
							if (!(num11 >= 0.1f))
							{
								list2.Add(array);
							}
						}
					}
				}
			}
			if (list2.Count != 0)
			{
				return list2.ToArray();
			}
			return null;
		}

		public FinderPatternInfo[] findMulti(IDictionary<DecodeHintType, object> hints)
		{
			bool flag = hints != null && hints.ContainsKey(DecodeHintType.TRY_HARDER);
			bool pureBarcode = hints != null && hints.ContainsKey(DecodeHintType.PURE_BARCODE);
			BitMatrix bitMatrix = Image;
			int height = bitMatrix.Height;
			int width = bitMatrix.Width;
			int num = (int)((float)height / 228f * 3f);
			if (num < 3 || flag)
			{
				num = 3;
			}
			int[] array = new int[5];
			for (int i = num - 1; i < height; i += num)
			{
				array[0] = 0;
				array[1] = 0;
				array[2] = 0;
				array[3] = 0;
				array[4] = 0;
				int num2 = 0;
				for (int j = 0; j < width; j++)
				{
					if (bitMatrix[j, i])
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
							if (FinderPatternFinder.foundPatternCross(array) && handlePossibleCenter(array, i, j, pureBarcode))
							{
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
							array[++num2]++;
						}
					}
					else
					{
						array[num2]++;
					}
				}
				if (FinderPatternFinder.foundPatternCross(array))
				{
					handlePossibleCenter(array, i, width, pureBarcode);
				}
			}
			FinderPattern[][] array2 = selectMutipleBestPatterns();
			if (array2 == null)
			{
				return EMPTY_RESULT_ARRAY;
			}
			List<FinderPatternInfo> list = new List<FinderPatternInfo>();
			FinderPattern[][] array3 = array2;
			foreach (FinderPattern[] array4 in array3)
			{
				ResultPoint.orderBestPatterns(array4);
				list.Add(new FinderPatternInfo(array4));
			}
			if (list.Count == 0)
			{
				return EMPTY_RESULT_ARRAY;
			}
			return list.ToArray();
		}
	}
}
