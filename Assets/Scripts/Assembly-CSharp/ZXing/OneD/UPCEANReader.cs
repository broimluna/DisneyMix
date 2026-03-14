using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	public abstract class UPCEANReader : OneDReader
	{
		private static readonly int MAX_AVG_VARIANCE;

		private static readonly int MAX_INDIVIDUAL_VARIANCE;

		internal static int[] START_END_PATTERN;

		internal static int[] MIDDLE_PATTERN;

		internal static int[][] L_PATTERNS;

		internal static int[][] L_AND_G_PATTERNS;

		private readonly StringBuilder decodeRowStringBuffer;

		private readonly UPCEANExtensionSupport extensionReader;

		private readonly EANManufacturerOrgSupport eanManSupport;

		internal abstract BarcodeFormat BarcodeFormat { get; }

		protected UPCEANReader()
		{
			decodeRowStringBuffer = new StringBuilder(20);
			extensionReader = new UPCEANExtensionSupport();
			eanManSupport = new EANManufacturerOrgSupport();
		}

		static UPCEANReader()
		{
			MAX_AVG_VARIANCE = (int)((float)OneDReader.PATTERN_MATCH_RESULT_SCALE_FACTOR * 0.48f);
			MAX_INDIVIDUAL_VARIANCE = (int)((float)OneDReader.PATTERN_MATCH_RESULT_SCALE_FACTOR * 0.7f);
			START_END_PATTERN = new int[3] { 1, 1, 1 };
			MIDDLE_PATTERN = new int[5] { 1, 1, 1, 1, 1 };
			L_PATTERNS = new int[10][]
			{
				new int[4] { 3, 2, 1, 1 },
				new int[4] { 2, 2, 2, 1 },
				new int[4] { 2, 1, 2, 2 },
				new int[4] { 1, 4, 1, 1 },
				new int[4] { 1, 1, 3, 2 },
				new int[4] { 1, 2, 3, 1 },
				new int[4] { 1, 1, 1, 4 },
				new int[4] { 1, 3, 1, 2 },
				new int[4] { 1, 2, 1, 3 },
				new int[4] { 3, 1, 1, 2 }
			};
			L_AND_G_PATTERNS = new int[20][];
			Array.Copy(L_PATTERNS, 0, L_AND_G_PATTERNS, 0, 10);
			for (int i = 10; i < 20; i++)
			{
				int[] array = L_PATTERNS[i - 10];
				int[] array2 = new int[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = array[array.Length - j - 1];
				}
				L_AND_G_PATTERNS[i] = array2;
			}
		}

		internal static int[] findStartGuardPattern(BitArray row)
		{
			bool flag = false;
			int[] array = null;
			int num = 0;
			int[] array2 = new int[START_END_PATTERN.Length];
			while (!flag)
			{
				for (int i = 0; i < START_END_PATTERN.Length; i++)
				{
					array2[i] = 0;
				}
				array = findGuardPattern(row, num, false, START_END_PATTERN, array2);
				if (array == null)
				{
					return null;
				}
				int num2 = array[0];
				num = array[1];
				int num3 = num2 - (num - num2);
				if (num3 >= 0)
				{
					flag = row.isRange(num3, num2, false);
				}
			}
			return array;
		}

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			return decodeRow(rowNumber, row, findStartGuardPattern(row), hints);
		}

		public virtual Result decodeRow(int rowNumber, BitArray row, int[] startGuardRange, IDictionary<DecodeHintType, object> hints)
		{
			ResultPointCallback resultPointCallback = ((hints != null && hints.ContainsKey(DecodeHintType.NEED_RESULT_POINT_CALLBACK)) ? ((ResultPointCallback)hints[DecodeHintType.NEED_RESULT_POINT_CALLBACK]) : null);
			if (resultPointCallback != null)
			{
				resultPointCallback(new ResultPoint((float)(startGuardRange[0] + startGuardRange[1]) / 2f, rowNumber));
			}
			StringBuilder stringBuilder = decodeRowStringBuffer;
			stringBuilder.Length = 0;
			int num = decodeMiddle(row, startGuardRange, stringBuilder);
			if (num < 0)
			{
				return null;
			}
			if (resultPointCallback != null)
			{
				resultPointCallback(new ResultPoint(num, rowNumber));
			}
			int[] array = decodeEnd(row, num);
			if (array == null)
			{
				return null;
			}
			if (resultPointCallback != null)
			{
				resultPointCallback(new ResultPoint((float)(array[0] + array[1]) / 2f, rowNumber));
			}
			int num2 = array[1];
			int num3 = num2 + (num2 - array[0]);
			if (num3 >= row.Size || !row.isRange(num2, num3, false))
			{
				return null;
			}
			string text = stringBuilder.ToString();
			if (text.Length < 8)
			{
				return null;
			}
			if (!checkChecksum(text))
			{
				return null;
			}
			float x = (float)(startGuardRange[1] + startGuardRange[0]) / 2f;
			float x2 = (float)(array[1] + array[0]) / 2f;
			BarcodeFormat barcodeFormat = BarcodeFormat;
			Result result = new Result(text, null, new ResultPoint[2]
			{
				new ResultPoint(x, rowNumber),
				new ResultPoint(x2, rowNumber)
			}, barcodeFormat);
			Result result2 = extensionReader.decodeRow(rowNumber, row, array[1]);
			if (result2 != null)
			{
				result.putMetadata(ResultMetadataType.UPC_EAN_EXTENSION, result2.Text);
				result.putAllMetadata(result2.ResultMetadata);
				result.addResultPoints(result2.ResultPoints);
				int length = result2.Text.Length;
				int[] array2 = ((hints == null || !hints.ContainsKey(DecodeHintType.ALLOWED_EAN_EXTENSIONS)) ? null : ((int[])hints[DecodeHintType.ALLOWED_EAN_EXTENSIONS]));
				if (array2 != null)
				{
					bool flag = false;
					int[] array3 = array2;
					foreach (int num4 in array3)
					{
						if (length == num4)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return null;
					}
				}
			}
			if (barcodeFormat == BarcodeFormat.EAN_13 || barcodeFormat == BarcodeFormat.UPC_A)
			{
				string text2 = eanManSupport.lookupCountryIdentifier(text);
				if (text2 != null)
				{
					result.putMetadata(ResultMetadataType.POSSIBLE_COUNTRY, text2);
				}
			}
			return result;
		}

		protected virtual bool checkChecksum(string s)
		{
			return checkStandardUPCEANChecksum(s);
		}

		internal static bool checkStandardUPCEANChecksum(string s)
		{
			int length = s.Length;
			if (length == 0)
			{
				return false;
			}
			int num = 0;
			for (int num2 = length - 2; num2 >= 0; num2 -= 2)
			{
				int num3 = s[num2] - 48;
				if (num3 < 0 || num3 > 9)
				{
					return false;
				}
				num += num3;
			}
			num *= 3;
			for (int num4 = length - 1; num4 >= 0; num4 -= 2)
			{
				int num5 = s[num4] - 48;
				if (num5 < 0 || num5 > 9)
				{
					return false;
				}
				num += num5;
			}
			return num % 10 == 0;
		}

		protected virtual int[] decodeEnd(BitArray row, int endStart)
		{
			return findGuardPattern(row, endStart, false, START_END_PATTERN);
		}

		internal static int[] findGuardPattern(BitArray row, int rowOffset, bool whiteFirst, int[] pattern)
		{
			return findGuardPattern(row, rowOffset, whiteFirst, pattern, new int[pattern.Length]);
		}

		internal static int[] findGuardPattern(BitArray row, int rowOffset, bool whiteFirst, int[] pattern, int[] counters)
		{
			int num = pattern.Length;
			int size = row.Size;
			bool flag = whiteFirst;
			rowOffset = ((!whiteFirst) ? row.getNextSet(rowOffset) : row.getNextUnset(rowOffset));
			int num2 = 0;
			int num3 = rowOffset;
			for (int i = rowOffset; i < size; i++)
			{
				if (row[i] ^ flag)
				{
					counters[num2]++;
					continue;
				}
				if (num2 == num - 1)
				{
					if (OneDReader.patternMatchVariance(counters, pattern, MAX_INDIVIDUAL_VARIANCE) < MAX_AVG_VARIANCE)
					{
						return new int[2] { num3, i };
					}
					num3 += counters[0] + counters[1];
					Array.Copy(counters, 2, counters, 0, num - 2);
					counters[num - 2] = 0;
					counters[num - 1] = 0;
					num2--;
				}
				else
				{
					num2++;
				}
				counters[num2] = 1;
				flag = !flag;
			}
			return null;
		}

		internal static bool decodeDigit(BitArray row, int[] counters, int rowOffset, int[][] patterns, out int digit)
		{
			digit = -1;
			if (!OneDReader.recordPattern(row, rowOffset, counters))
			{
				return false;
			}
			int num = MAX_AVG_VARIANCE;
			int num2 = patterns.Length;
			for (int i = 0; i < num2; i++)
			{
				int[] pattern = patterns[i];
				int num3 = OneDReader.patternMatchVariance(counters, pattern, MAX_INDIVIDUAL_VARIANCE);
				if (num3 < num)
				{
					num = num3;
					digit = i;
				}
			}
			return digit >= 0;
		}

		protected internal abstract int decodeMiddle(BitArray row, int[] startRange, StringBuilder resultString);
	}
}
