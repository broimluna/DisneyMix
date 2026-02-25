using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing.OneD.RSS.Expanded.Decoders;

namespace ZXing.OneD.RSS.Expanded
{
	public sealed class RSSExpandedReader : AbstractRSSReader
	{
		private const int FINDER_PAT_A = 0;

		private const int FINDER_PAT_B = 1;

		private const int FINDER_PAT_C = 2;

		private const int FINDER_PAT_D = 3;

		private const int FINDER_PAT_E = 4;

		private const int FINDER_PAT_F = 5;

		private const int MAX_PAIRS = 11;

		private static readonly int[] SYMBOL_WIDEST = new int[5] { 7, 5, 4, 3, 1 };

		private static readonly int[] EVEN_TOTAL_SUBSET = new int[5] { 4, 20, 52, 104, 204 };

		private static readonly int[] GSUM = new int[5] { 0, 348, 1388, 2948, 3988 };

		private static readonly int[][] FINDER_PATTERNS = new int[6][]
		{
			new int[4] { 1, 8, 4, 1 },
			new int[4] { 3, 6, 4, 1 },
			new int[4] { 3, 4, 6, 1 },
			new int[4] { 3, 2, 8, 1 },
			new int[4] { 2, 6, 5, 1 },
			new int[4] { 2, 2, 9, 1 }
		};

		private static readonly int[][] WEIGHTS = new int[23][]
		{
			new int[8] { 1, 3, 9, 27, 81, 32, 96, 77 },
			new int[8] { 20, 60, 180, 118, 143, 7, 21, 63 },
			new int[8] { 189, 145, 13, 39, 117, 140, 209, 205 },
			new int[8] { 193, 157, 49, 147, 19, 57, 171, 91 },
			new int[8] { 62, 186, 136, 197, 169, 85, 44, 132 },
			new int[8] { 185, 133, 188, 142, 4, 12, 36, 108 },
			new int[8] { 113, 128, 173, 97, 80, 29, 87, 50 },
			new int[8] { 150, 28, 84, 41, 123, 158, 52, 156 },
			new int[8] { 46, 138, 203, 187, 139, 206, 196, 166 },
			new int[8] { 76, 17, 51, 153, 37, 111, 122, 155 },
			new int[8] { 43, 129, 176, 106, 107, 110, 119, 146 },
			new int[8] { 16, 48, 144, 10, 30, 90, 59, 177 },
			new int[8] { 109, 116, 137, 200, 178, 112, 125, 164 },
			new int[8] { 70, 210, 208, 202, 184, 130, 179, 115 },
			new int[8] { 134, 191, 151, 31, 93, 68, 204, 190 },
			new int[8] { 148, 22, 66, 198, 172, 94, 71, 2 },
			new int[8] { 6, 18, 54, 162, 64, 192, 154, 40 },
			new int[8] { 120, 149, 25, 75, 14, 42, 126, 167 },
			new int[8] { 79, 26, 78, 23, 69, 207, 199, 175 },
			new int[8] { 103, 98, 83, 38, 114, 131, 182, 124 },
			new int[8] { 161, 61, 183, 127, 170, 88, 53, 159 },
			new int[8] { 55, 165, 73, 8, 24, 72, 5, 15 },
			new int[8] { 45, 135, 194, 160, 58, 174, 100, 89 }
		};

		private static readonly int[][] FINDER_PATTERN_SEQUENCES = new int[10][]
		{
			new int[2],
			new int[3] { 0, 1, 1 },
			new int[4] { 0, 2, 1, 3 },
			new int[5] { 0, 4, 1, 3, 2 },
			new int[6] { 0, 4, 1, 3, 3, 5 },
			new int[7] { 0, 4, 1, 3, 4, 5, 5 },
			new int[8] { 0, 0, 1, 1, 2, 2, 3, 3 },
			new int[9] { 0, 0, 1, 1, 2, 2, 3, 4, 4 },
			new int[10] { 0, 0, 1, 1, 2, 2, 3, 4, 5, 5 },
			new int[11]
			{
				0, 0, 1, 1, 2, 3, 3, 4, 4, 5,
				5
			}
		};

		private readonly List<ExpandedPair> pairs = new List<ExpandedPair>(11);

		private readonly List<ExpandedRow> rows = new List<ExpandedRow>();

		private readonly int[] startEnd = new int[2];

		private bool startFromEven;

		internal List<ExpandedPair> Pairs
		{
			get
			{
				return pairs;
			}
		}

		internal List<ExpandedRow> Rows
		{
			get
			{
				return rows;
			}
		}

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			pairs.Clear();
			startFromEven = false;
			if (decodeRow2pairs(rowNumber, row))
			{
				return constructResult(pairs);
			}
			pairs.Clear();
			startFromEven = true;
			if (decodeRow2pairs(rowNumber, row))
			{
				return constructResult(pairs);
			}
			return null;
		}

		public override void reset()
		{
			pairs.Clear();
			rows.Clear();
		}

		internal bool decodeRow2pairs(int rowNumber, BitArray row)
		{
			while (true)
			{
				ExpandedPair expandedPair = retrieveNextPair(row, pairs, rowNumber);
				if (expandedPair == null)
				{
					break;
				}
				pairs.Add(expandedPair);
			}
			if (pairs.Count == 0)
			{
				return false;
			}
			if (checkChecksum())
			{
				return true;
			}
			bool flag = rows.Count != 0;
			bool wasReversed = false;
			storeRow(rowNumber, wasReversed);
			if (flag)
			{
				List<ExpandedPair> list = checkRows(false);
				if (list != null)
				{
					return true;
				}
				list = checkRows(true);
				if (list != null)
				{
					return true;
				}
			}
			return false;
		}

		private List<ExpandedPair> checkRows(bool reverse)
		{
			if (rows.Count > 25)
			{
				rows.Clear();
				return null;
			}
			pairs.Clear();
			if (reverse)
			{
				rows.Reverse();
			}
			List<ExpandedPair> result = checkRows(new List<ExpandedRow>(), 0);
			if (reverse)
			{
				rows.Reverse();
			}
			return result;
		}

		private List<ExpandedPair> checkRows(List<ExpandedRow> collectedRows, int currentRow)
		{
			for (int i = currentRow; i < rows.Count; i++)
			{
				ExpandedRow expandedRow = rows[i];
				pairs.Clear();
				int num = collectedRows.Count;
				for (int j = 0; j < num; j++)
				{
					pairs.AddRange(collectedRows[j].Pairs);
				}
				pairs.AddRange(expandedRow.Pairs);
				if (isValidSequence(pairs))
				{
					if (checkChecksum())
					{
						return pairs;
					}
					List<ExpandedRow> list = new List<ExpandedRow>();
					list.AddRange(collectedRows);
					list.Add(expandedRow);
					List<ExpandedPair> list2 = checkRows(list, i + 1);
					if (list2 != null)
					{
						return list2;
					}
				}
			}
			return null;
		}

		private static bool isValidSequence(List<ExpandedPair> pairs)
		{
			int[][] fINDER_PATTERN_SEQUENCES = FINDER_PATTERN_SEQUENCES;
			foreach (int[] array in fINDER_PATTERN_SEQUENCES)
			{
				if (pairs.Count > array.Length)
				{
					continue;
				}
				bool flag = true;
				for (int j = 0; j < pairs.Count; j++)
				{
					if (pairs[j].FinderPattern.Value != array[j])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		private void storeRow(int rowNumber, bool wasReversed)
		{
			int i = 0;
			bool flag = false;
			bool flag2 = false;
			for (; i < rows.Count; i++)
			{
				ExpandedRow expandedRow = rows[i];
				if (expandedRow.RowNumber > rowNumber)
				{
					flag2 = expandedRow.IsEquivalent(pairs);
					break;
				}
				flag = expandedRow.IsEquivalent(pairs);
			}
			if (!flag2 && !flag && !isPartialRow(pairs, rows))
			{
				rows.Insert(i, new ExpandedRow(pairs, rowNumber, wasReversed));
				removePartialRows(pairs, rows);
			}
		}

		private static void removePartialRows(List<ExpandedPair> pairs, List<ExpandedRow> rows)
		{
			for (int i = 0; i < rows.Count; i++)
			{
				ExpandedRow expandedRow = rows[i];
				if (expandedRow.Pairs.Count == pairs.Count)
				{
					continue;
				}
				bool flag = true;
				foreach (ExpandedPair pair in expandedRow.Pairs)
				{
					bool flag2 = false;
					foreach (ExpandedPair pair2 in pairs)
					{
						if (pair.Equals(pair2))
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					rows.RemoveAt(i);
				}
			}
		}

		private static bool isPartialRow(IEnumerable<ExpandedPair> pairs, IEnumerable<ExpandedRow> rows)
		{
			foreach (ExpandedRow row in rows)
			{
				bool flag = true;
				foreach (ExpandedPair pair in pairs)
				{
					bool flag2 = false;
					foreach (ExpandedPair pair2 in row.Pairs)
					{
						if (pair.Equals(pair2))
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		internal static Result constructResult(List<ExpandedPair> pairs)
		{
			BitArray information = BitArrayBuilder.buildBitArray(pairs);
			AbstractExpandedDecoder abstractExpandedDecoder = AbstractExpandedDecoder.createDecoder(information);
			string text = abstractExpandedDecoder.parseInformation();
			if (text == null)
			{
				return null;
			}
			ResultPoint[] resultPoints = pairs[0].FinderPattern.ResultPoints;
			ResultPoint[] resultPoints2 = pairs[pairs.Count - 1].FinderPattern.ResultPoints;
			return new Result(text, null, new ResultPoint[4]
			{
				resultPoints[0],
				resultPoints[1],
				resultPoints2[0],
				resultPoints2[1]
			}, BarcodeFormat.RSS_EXPANDED);
		}

		private bool checkChecksum()
		{
			ExpandedPair expandedPair = pairs[0];
			DataCharacter leftChar = expandedPair.LeftChar;
			DataCharacter rightChar = expandedPair.RightChar;
			if (rightChar == null)
			{
				return false;
			}
			int num = rightChar.ChecksumPortion;
			int num2 = 2;
			for (int i = 1; i < pairs.Count; i++)
			{
				ExpandedPair expandedPair2 = pairs[i];
				num += expandedPair2.LeftChar.ChecksumPortion;
				num2++;
				DataCharacter rightChar2 = expandedPair2.RightChar;
				if (rightChar2 != null)
				{
					num += rightChar2.ChecksumPortion;
					num2++;
				}
			}
			num %= 211;
			int num3 = 211 * (num2 - 4) + num;
			return num3 == leftChar.Value;
		}

		private static int getNextSecondBar(BitArray row, int initialPos)
		{
			int nextUnset;
			if (row[initialPos])
			{
				nextUnset = row.getNextUnset(initialPos);
				return row.getNextSet(nextUnset);
			}
			nextUnset = row.getNextSet(initialPos);
			return row.getNextUnset(nextUnset);
		}

		internal ExpandedPair retrieveNextPair(BitArray row, List<ExpandedPair> previousPairs, int rowNumber)
		{
			bool flag = previousPairs.Count % 2 == 0;
			if (startFromEven)
			{
				flag = !flag;
			}
			bool flag2 = true;
			int forcedOffset = -1;
			FinderPattern finderPattern;
			do
			{
				if (!findNextPair(row, previousPairs, forcedOffset))
				{
					return null;
				}
				finderPattern = parseFoundFinderPattern(row, rowNumber, flag);
				if (finderPattern == null)
				{
					forcedOffset = getNextSecondBar(row, startEnd[0]);
				}
				else
				{
					flag2 = false;
				}
			}
			while (flag2);
			DataCharacter dataCharacter = decodeDataCharacter(row, finderPattern, flag, true);
			if (dataCharacter == null)
			{
				return null;
			}
			if (previousPairs.Count != 0 && previousPairs[previousPairs.Count - 1].MustBeLast)
			{
				return null;
			}
			DataCharacter rightChar = decodeDataCharacter(row, finderPattern, flag, false);
			return new ExpandedPair(dataCharacter, rightChar, finderPattern, true);
		}

		private bool findNextPair(BitArray row, List<ExpandedPair> previousPairs, int forcedOffset)
		{
			int[] array = getDecodeFinderCounters();
			array[0] = 0;
			array[1] = 0;
			array[2] = 0;
			array[3] = 0;
			int size = row.Size;
			int i;
			if (forcedOffset >= 0)
			{
				i = forcedOffset;
			}
			else if (previousPairs.Count == 0)
			{
				i = 0;
			}
			else
			{
				ExpandedPair expandedPair = previousPairs[previousPairs.Count - 1];
				i = expandedPair.FinderPattern.StartEnd[1];
			}
			bool flag = previousPairs.Count % 2 != 0;
			if (startFromEven)
			{
				flag = !flag;
			}
			bool flag2 = false;
			for (; i < size; i++)
			{
				flag2 = !row[i];
				if (!flag2)
				{
					break;
				}
			}
			int num = 0;
			int num2 = i;
			for (int j = i; j < size; j++)
			{
				if (row[j] ^ flag2)
				{
					array[num]++;
					continue;
				}
				if (num == 3)
				{
					if (flag)
					{
						reverseCounters(array);
					}
					if (AbstractRSSReader.isFinderPattern(array))
					{
						startEnd[0] = num2;
						startEnd[1] = j;
						return true;
					}
					if (flag)
					{
						reverseCounters(array);
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
				flag2 = !flag2;
			}
			return false;
		}

		private static void reverseCounters(int[] counters)
		{
			int num = counters.Length;
			for (int i = 0; i < num / 2; i++)
			{
				int num2 = counters[i];
				counters[i] = counters[num - i - 1];
				counters[num - i - 1] = num2;
			}
		}

		private FinderPattern parseFoundFinderPattern(BitArray row, int rowNumber, bool oddPattern)
		{
			int num2;
			int num3;
			int num4;
			if (oddPattern)
			{
				int num = startEnd[0] - 1;
				while (num >= 0 && !row[num])
				{
					num--;
				}
				num++;
				num2 = startEnd[0] - num;
				num3 = num;
				num4 = startEnd[1];
			}
			else
			{
				num3 = startEnd[0];
				num4 = row.getNextUnset(startEnd[1] + 1);
				num2 = num4 - startEnd[1];
			}
			int[] array = getDecodeFinderCounters();
			Array.Copy(array, 0, array, 1, array.Length - 1);
			array[0] = num2;
			int value;
			if (!AbstractRSSReader.parseFinderValue(array, FINDER_PATTERNS, out value))
			{
				return null;
			}
			return new FinderPattern(value, new int[2] { num3, num4 }, num3, num4, rowNumber);
		}

		internal DataCharacter decodeDataCharacter(BitArray row, FinderPattern pattern, bool isOddPattern, bool leftChar)
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
			if (leftChar)
			{
				if (!OneDReader.recordPatternInReverse(row, pattern.StartEnd[0], array))
				{
					return null;
				}
			}
			else
			{
				if (!OneDReader.recordPattern(row, pattern.StartEnd[1], array))
				{
					return null;
				}
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
			float num4 = (float)AbstractRSSReader.count(array) / 17f;
			float num5 = (float)(pattern.StartEnd[1] - pattern.StartEnd[0]) / 15f;
			if (Math.Abs(num4 - num5) / num5 > 0.3f)
			{
				return null;
			}
			int[] array2 = getOddCounts();
			int[] array3 = getEvenCounts();
			float[] array4 = getOddRoundingErrors();
			float[] array5 = getEvenRoundingErrors();
			for (int i = 0; i < array.Length; i++)
			{
				float num6 = 1f * (float)array[i] / num4;
				int num7 = (int)(num6 + 0.5f);
				if (num7 < 1)
				{
					if (num6 < 0.3f)
					{
						return null;
					}
					num7 = 1;
				}
				else if (num7 > 8)
				{
					if (num6 > 8.7f)
					{
						return null;
					}
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
			if (!adjustOddEvenCounts(17))
			{
				return null;
			}
			int num9 = 4 * pattern.Value + ((!isOddPattern) ? 2 : 0) + ((!leftChar) ? 1 : 0) - 1;
			int num10 = 0;
			int num11 = 0;
			for (int num12 = array2.Length - 1; num12 >= 0; num12--)
			{
				if (isNotA1left(pattern, isOddPattern, leftChar))
				{
					int num13 = WEIGHTS[num9][2 * num12];
					num11 += array2[num12] * num13;
				}
				num10 += array2[num12];
			}
			int num14 = 0;
			for (int num15 = array3.Length - 1; num15 >= 0; num15--)
			{
				if (isNotA1left(pattern, isOddPattern, leftChar))
				{
					int num16 = WEIGHTS[num9][2 * num15 + 1];
					num14 += array3[num15] * num16;
				}
			}
			int checksumPortion = num11 + num14;
			if ((num10 & 1) != 0 || num10 > 13 || num10 < 4)
			{
				return null;
			}
			int num17 = (13 - num10) / 2;
			int num18 = SYMBOL_WIDEST[num17];
			int maxWidth = 9 - num18;
			int rSSvalue = RSSUtils.getRSSvalue(array2, num18, true);
			int rSSvalue2 = RSSUtils.getRSSvalue(array3, maxWidth, false);
			int num19 = EVEN_TOTAL_SUBSET[num17];
			int num20 = GSUM[num17];
			int value = rSSvalue * num19 + rSSvalue2 + num20;
			return new DataCharacter(value, checksumPortion);
		}

		private static bool isNotA1left(FinderPattern pattern, bool isOddPattern, bool leftChar)
		{
			return pattern.Value != 0 || !isOddPattern || !leftChar;
		}

		private bool adjustOddEvenCounts(int numModules)
		{
			int num = AbstractRSSReader.count(getOddCounts());
			int num2 = AbstractRSSReader.count(getEvenCounts());
			int num3 = num + num2 - numModules;
			bool flag = (num & 1) == 1;
			bool flag2 = (num2 & 1) == 0;
			bool flag3 = false;
			bool flag4 = false;
			if (num > 13)
			{
				flag4 = true;
			}
			else if (num < 4)
			{
				flag3 = true;
			}
			bool flag5 = false;
			bool flag6 = false;
			if (num2 > 13)
			{
				flag6 = true;
			}
			else if (num2 < 4)
			{
				flag5 = true;
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
