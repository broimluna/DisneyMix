using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD.RSS
{
	public sealed class RSS14Reader : AbstractRSSReader
	{
		private static readonly int[] OUTSIDE_EVEN_TOTAL_SUBSET = new int[5] { 1, 10, 34, 70, 126 };

		private static readonly int[] INSIDE_ODD_TOTAL_SUBSET = new int[4] { 4, 20, 48, 81 };

		private static readonly int[] OUTSIDE_GSUM = new int[5] { 0, 161, 961, 2015, 2715 };

		private static readonly int[] INSIDE_GSUM = new int[4] { 0, 336, 1036, 1516 };

		private static readonly int[] OUTSIDE_ODD_WIDEST = new int[5] { 8, 6, 4, 3, 1 };

		private static readonly int[] INSIDE_ODD_WIDEST = new int[4] { 2, 4, 6, 8 };

		private static readonly int[][] FINDER_PATTERNS = new int[9][]
		{
			new int[4] { 3, 8, 2, 1 },
			new int[4] { 3, 5, 5, 1 },
			new int[4] { 3, 3, 7, 1 },
			new int[4] { 3, 1, 9, 1 },
			new int[4] { 2, 7, 4, 1 },
			new int[4] { 2, 5, 6, 1 },
			new int[4] { 2, 3, 8, 1 },
			new int[4] { 1, 5, 7, 1 },
			new int[4] { 1, 3, 9, 1 }
		};

		private readonly List<Pair> possibleLeftPairs;

		private readonly List<Pair> possibleRightPairs;

		public RSS14Reader()
		{
			possibleLeftPairs = new List<Pair>();
			possibleRightPairs = new List<Pair>();
		}

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			Pair pair = decodePair(row, false, rowNumber, hints);
			addOrTally(possibleLeftPairs, pair);
			row.reverse();
			Pair pair2 = decodePair(row, true, rowNumber, hints);
			addOrTally(possibleRightPairs, pair2);
			row.reverse();
			int num = possibleLeftPairs.Count;
			for (int i = 0; i < num; i++)
			{
				Pair pair3 = possibleLeftPairs[i];
				if (pair3.Count <= 1)
				{
					continue;
				}
				int num2 = possibleRightPairs.Count;
				for (int j = 0; j < num2; j++)
				{
					Pair pair4 = possibleRightPairs[j];
					if (pair4.Count > 1 && checkChecksum(pair3, pair4))
					{
						return constructResult(pair3, pair4);
					}
				}
			}
			return null;
		}

		private static void addOrTally(IList<Pair> possiblePairs, Pair pair)
		{
			if (pair == null)
			{
				return;
			}
			bool flag = false;
			foreach (Pair possiblePair in possiblePairs)
			{
				if (possiblePair.Value == pair.Value)
				{
					possiblePair.incrementCount();
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				possiblePairs.Add(pair);
			}
		}

		public override void reset()
		{
			possibleLeftPairs.Clear();
			possibleRightPairs.Clear();
		}

		private static Result constructResult(Pair leftPair, Pair rightPair)
		{
			string text = (4537077L * (long)leftPair.Value + rightPair.Value).ToString();
			StringBuilder stringBuilder = new StringBuilder(14);
			for (int num = 13 - text.Length; num > 0; num--)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(text);
			int num2 = 0;
			for (int i = 0; i < 13; i++)
			{
				int num3 = stringBuilder[i] - 48;
				num2 += (((i & 1) != 0) ? num3 : (3 * num3));
			}
			num2 = 10 - num2 % 10;
			if (num2 == 10)
			{
				num2 = 0;
			}
			stringBuilder.Append(num2);
			ResultPoint[] resultPoints = leftPair.FinderPattern.ResultPoints;
			ResultPoint[] resultPoints2 = rightPair.FinderPattern.ResultPoints;
			return new Result(stringBuilder.ToString(), null, new ResultPoint[4]
			{
				resultPoints[0],
				resultPoints[1],
				resultPoints2[0],
				resultPoints2[1]
			}, BarcodeFormat.RSS_14);
		}

		private static bool checkChecksum(Pair leftPair, Pair rightPair)
		{
			int num = (leftPair.ChecksumPortion + 16 * rightPair.ChecksumPortion) % 79;
			int num2 = 9 * leftPair.FinderPattern.Value + rightPair.FinderPattern.Value;
			if (num2 > 72)
			{
				num2--;
			}
			if (num2 > 8)
			{
				num2--;
			}
			return num == num2;
		}

		private Pair decodePair(BitArray row, bool right, int rowNumber, IDictionary<DecodeHintType, object> hints)
		{
			int[] array = findFinderPattern(row, 0, right);
			if (array == null)
			{
				return null;
			}
			FinderPattern finderPattern = parseFoundFinderPattern(row, rowNumber, right, array);
			if (finderPattern == null)
			{
				return null;
			}
			ResultPointCallback resultPointCallback = ((hints != null && hints.ContainsKey(DecodeHintType.NEED_RESULT_POINT_CALLBACK)) ? ((ResultPointCallback)hints[DecodeHintType.NEED_RESULT_POINT_CALLBACK]) : null);
			if (resultPointCallback != null)
			{
				float num = (float)(array[0] + array[1]) / 2f;
				if (right)
				{
					num = (float)(row.Size - 1) - num;
				}
				resultPointCallback(new ResultPoint(num, rowNumber));
			}
			DataCharacter dataCharacter = decodeDataCharacter(row, finderPattern, true);
			if (dataCharacter == null)
			{
				return null;
			}
			DataCharacter dataCharacter2 = decodeDataCharacter(row, finderPattern, false);
			if (dataCharacter2 == null)
			{
				return null;
			}
			return new Pair(1597 * dataCharacter.Value + dataCharacter2.Value, dataCharacter.ChecksumPortion + 4 * dataCharacter2.ChecksumPortion, finderPattern);
		}

		private DataCharacter decodeDataCharacter(BitArray row, FinderPattern pattern, bool outsideChar)
		{
			int[] array = getDataCharacterCounters();
			array[0] = 0;
			array[1] = 0;
			array[2] = 0;
			array[3] = 0;
			array[4] = 0;
			array[5] = 0;
			array[6] = 0;
			array[7] = 0;
			if (outsideChar)
			{
				OneDReader.recordPatternInReverse(row, pattern.StartEnd[0], array);
			}
			else
			{
				OneDReader.recordPattern(row, pattern.StartEnd[1] + 1, array);
				int num = 0;
				int num2 = array.Length - 1;
				while (num < num2)
				{
					int num3 = array[num];
					array[num] = array[num2];
					array[num2] = num3;
					num++;
					num2--;
				}
			}
			int num4 = ((!outsideChar) ? 15 : 16);
			float num5 = (float)AbstractRSSReader.count(array) / (float)num4;
			int[] array2 = getOddCounts();
			int[] array3 = getEvenCounts();
			float[] array4 = getOddRoundingErrors();
			float[] array5 = getEvenRoundingErrors();
			for (int i = 0; i < array.Length; i++)
			{
				float num6 = (float)array[i] / num5;
				int num7 = (int)(num6 + 0.5f);
				if (num7 < 1)
				{
					num7 = 1;
				}
				else if (num7 > 8)
				{
					num7 = 8;
				}
				int num8 = i >> 1;
				if ((i & 1) == 0)
				{
					array2[num8] = num7;
					array4[num8] = num6 - (float)num7;
				}
				else
				{
					array3[num8] = num7;
					array5[num8] = num6 - (float)num7;
				}
			}
			if (!adjustOddEvenCounts(outsideChar, num4))
			{
				return null;
			}
			int num9 = 0;
			int num10 = 0;
			for (int num11 = array2.Length - 1; num11 >= 0; num11--)
			{
				num10 *= 9;
				num10 += array2[num11];
				num9 += array2[num11];
			}
			int num12 = 0;
			int num13 = 0;
			for (int num14 = array3.Length - 1; num14 >= 0; num14--)
			{
				num12 *= 9;
				num12 += array3[num14];
				num13 += array3[num14];
			}
			int checksumPortion = num10 + 3 * num12;
			if (outsideChar)
			{
				if ((num9 & 1) != 0 || num9 > 12 || num9 < 4)
				{
					return null;
				}
				int num15 = (12 - num9) / 2;
				int num16 = OUTSIDE_ODD_WIDEST[num15];
				int maxWidth = 9 - num16;
				int rSSvalue = RSSUtils.getRSSvalue(array2, num16, false);
				int rSSvalue2 = RSSUtils.getRSSvalue(array3, maxWidth, true);
				int num17 = OUTSIDE_EVEN_TOTAL_SUBSET[num15];
				int num18 = OUTSIDE_GSUM[num15];
				return new DataCharacter(rSSvalue * num17 + rSSvalue2 + num18, checksumPortion);
			}
			if ((num13 & 1) != 0 || num13 > 10 || num13 < 4)
			{
				return null;
			}
			int num19 = (10 - num13) / 2;
			int num20 = INSIDE_ODD_WIDEST[num19];
			int maxWidth2 = 9 - num20;
			int rSSvalue3 = RSSUtils.getRSSvalue(array2, num20, true);
			int rSSvalue4 = RSSUtils.getRSSvalue(array3, maxWidth2, false);
			int num21 = INSIDE_ODD_TOTAL_SUBSET[num19];
			int num22 = INSIDE_GSUM[num19];
			return new DataCharacter(rSSvalue4 * num21 + rSSvalue3 + num22, checksumPortion);
		}

		private int[] findFinderPattern(BitArray row, int rowOffset, bool rightFinderPattern)
		{
			int[] array = getDecodeFinderCounters();
			array[0] = 0;
			array[1] = 0;
			array[2] = 0;
			array[3] = 0;
			int size = row.Size;
			bool flag = false;
			while (rowOffset < size)
			{
				flag = !row[rowOffset];
				if (rightFinderPattern == flag)
				{
					break;
				}
				rowOffset++;
			}
			int num = 0;
			int num2 = rowOffset;
			for (int i = rowOffset; i < size; i++)
			{
				if (row[i] ^ flag)
				{
					array[num]++;
					continue;
				}
				if (num == 3)
				{
					if (AbstractRSSReader.isFinderPattern(array))
					{
						return new int[2] { num2, i };
					}
					num2 += array[0] + array[1];
					array[0] = array[2];
					array[1] = array[3];
					array[2] = 0;
					array[3] = 0;
					num--;
				}
				else
				{
					num++;
				}
				array[num] = 1;
				flag = !flag;
			}
			return null;
		}

		private FinderPattern parseFoundFinderPattern(BitArray row, int rowNumber, bool right, int[] startEnd)
		{
			bool flag = row[startEnd[0]];
			int num = startEnd[0] - 1;
			while (num >= 0 && (flag ^ row[num]))
			{
				num--;
			}
			num++;
			int num2 = startEnd[0] - num;
			int[] array = getDecodeFinderCounters();
			Array.Copy(array, 0, array, 1, array.Length - 1);
			array[0] = num2;
			int value;
			if (!AbstractRSSReader.parseFinderValue(array, FINDER_PATTERNS, out value))
			{
				return null;
			}
			int num3 = num;
			int num4 = startEnd[1];
			if (right)
			{
				num3 = row.Size - 1 - num3;
				num4 = row.Size - 1 - num4;
			}
			return new FinderPattern(value, new int[2]
			{
				num,
				startEnd[1]
			}, num3, num4, rowNumber);
		}

		private bool adjustOddEvenCounts(bool outsideChar, int numModules)
		{
			int num = AbstractRSSReader.count(getOddCounts());
			int num2 = AbstractRSSReader.count(getEvenCounts());
			int num3 = num + num2 - numModules;
			bool flag = (num & 1) == (outsideChar ? 1 : 0);
			bool flag2 = (num2 & 1) == 1;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			if (outsideChar)
			{
				if (num > 12)
				{
					flag4 = true;
				}
				else if (num < 4)
				{
					flag3 = true;
				}
				if (num2 > 12)
				{
					flag6 = true;
				}
				else if (num2 < 4)
				{
					flag5 = true;
				}
			}
			else
			{
				if (num > 11)
				{
					flag4 = true;
				}
				else if (num < 5)
				{
					flag3 = true;
				}
				if (num2 > 10)
				{
					flag6 = true;
				}
				else if (num2 < 4)
				{
					flag5 = true;
				}
			}
			switch (num3)
			{
			case 1:
				if (flag)
				{
					if (flag2)
					{
						return false;
					}
					flag4 = true;
				}
				else
				{
					if (!flag2)
					{
						return false;
					}
					flag6 = true;
				}
				break;
			case -1:
				if (flag)
				{
					if (flag2)
					{
						return false;
					}
					flag3 = true;
				}
				else
				{
					if (!flag2)
					{
						return false;
					}
					flag5 = true;
				}
				break;
			case 0:
				if (flag)
				{
					if (!flag2)
					{
						return false;
					}
					if (num < num2)
					{
						flag3 = true;
						flag6 = true;
					}
					else
					{
						flag4 = true;
						flag5 = true;
					}
				}
				else if (flag2)
				{
					return false;
				}
				break;
			default:
				return false;
			}
			if (flag3)
			{
				if (flag4)
				{
					return false;
				}
				AbstractRSSReader.increment(getOddCounts(), getOddRoundingErrors());
			}
			if (flag4)
			{
				AbstractRSSReader.decrement(getOddCounts(), getOddRoundingErrors());
			}
			if (flag5)
			{
				if (flag6)
				{
					return false;
				}
				AbstractRSSReader.increment(getEvenCounts(), getOddRoundingErrors());
			}
			if (flag6)
			{
				AbstractRSSReader.decrement(getEvenCounts(), getEvenRoundingErrors());
			}
			return true;
		}
	}
}
