using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class Code128Reader : OneDReader
	{
		private const int CODE_SHIFT = 98;

		private const int CODE_CODE_C = 99;

		private const int CODE_CODE_B = 100;

		private const int CODE_CODE_A = 101;

		private const int CODE_FNC_1 = 102;

		private const int CODE_FNC_2 = 97;

		private const int CODE_FNC_3 = 96;

		private const int CODE_FNC_4_A = 101;

		private const int CODE_FNC_4_B = 100;

		private const int CODE_START_A = 103;

		private const int CODE_START_B = 104;

		private const int CODE_START_C = 105;

		private const int CODE_STOP = 106;

		internal static int[][] CODE_PATTERNS = new int[107][]
		{
			new int[6] { 2, 1, 2, 2, 2, 2 },
			new int[6] { 2, 2, 2, 1, 2, 2 },
			new int[6] { 2, 2, 2, 2, 2, 1 },
			new int[6] { 1, 2, 1, 2, 2, 3 },
			new int[6] { 1, 2, 1, 3, 2, 2 },
			new int[6] { 1, 3, 1, 2, 2, 2 },
			new int[6] { 1, 2, 2, 2, 1, 3 },
			new int[6] { 1, 2, 2, 3, 1, 2 },
			new int[6] { 1, 3, 2, 2, 1, 2 },
			new int[6] { 2, 2, 1, 2, 1, 3 },
			new int[6] { 2, 2, 1, 3, 1, 2 },
			new int[6] { 2, 3, 1, 2, 1, 2 },
			new int[6] { 1, 1, 2, 2, 3, 2 },
			new int[6] { 1, 2, 2, 1, 3, 2 },
			new int[6] { 1, 2, 2, 2, 3, 1 },
			new int[6] { 1, 1, 3, 2, 2, 2 },
			new int[6] { 1, 2, 3, 1, 2, 2 },
			new int[6] { 1, 2, 3, 2, 2, 1 },
			new int[6] { 2, 2, 3, 2, 1, 1 },
			new int[6] { 2, 2, 1, 1, 3, 2 },
			new int[6] { 2, 2, 1, 2, 3, 1 },
			new int[6] { 2, 1, 3, 2, 1, 2 },
			new int[6] { 2, 2, 3, 1, 1, 2 },
			new int[6] { 3, 1, 2, 1, 3, 1 },
			new int[6] { 3, 1, 1, 2, 2, 2 },
			new int[6] { 3, 2, 1, 1, 2, 2 },
			new int[6] { 3, 2, 1, 2, 2, 1 },
			new int[6] { 3, 1, 2, 2, 1, 2 },
			new int[6] { 3, 2, 2, 1, 1, 2 },
			new int[6] { 3, 2, 2, 2, 1, 1 },
			new int[6] { 2, 1, 2, 1, 2, 3 },
			new int[6] { 2, 1, 2, 3, 2, 1 },
			new int[6] { 2, 3, 2, 1, 2, 1 },
			new int[6] { 1, 1, 1, 3, 2, 3 },
			new int[6] { 1, 3, 1, 1, 2, 3 },
			new int[6] { 1, 3, 1, 3, 2, 1 },
			new int[6] { 1, 1, 2, 3, 1, 3 },
			new int[6] { 1, 3, 2, 1, 1, 3 },
			new int[6] { 1, 3, 2, 3, 1, 1 },
			new int[6] { 2, 1, 1, 3, 1, 3 },
			new int[6] { 2, 3, 1, 1, 1, 3 },
			new int[6] { 2, 3, 1, 3, 1, 1 },
			new int[6] { 1, 1, 2, 1, 3, 3 },
			new int[6] { 1, 1, 2, 3, 3, 1 },
			new int[6] { 1, 3, 2, 1, 3, 1 },
			new int[6] { 1, 1, 3, 1, 2, 3 },
			new int[6] { 1, 1, 3, 3, 2, 1 },
			new int[6] { 1, 3, 3, 1, 2, 1 },
			new int[6] { 3, 1, 3, 1, 2, 1 },
			new int[6] { 2, 1, 1, 3, 3, 1 },
			new int[6] { 2, 3, 1, 1, 3, 1 },
			new int[6] { 2, 1, 3, 1, 1, 3 },
			new int[6] { 2, 1, 3, 3, 1, 1 },
			new int[6] { 2, 1, 3, 1, 3, 1 },
			new int[6] { 3, 1, 1, 1, 2, 3 },
			new int[6] { 3, 1, 1, 3, 2, 1 },
			new int[6] { 3, 3, 1, 1, 2, 1 },
			new int[6] { 3, 1, 2, 1, 1, 3 },
			new int[6] { 3, 1, 2, 3, 1, 1 },
			new int[6] { 3, 3, 2, 1, 1, 1 },
			new int[6] { 3, 1, 4, 1, 1, 1 },
			new int[6] { 2, 2, 1, 4, 1, 1 },
			new int[6] { 4, 3, 1, 1, 1, 1 },
			new int[6] { 1, 1, 1, 2, 2, 4 },
			new int[6] { 1, 1, 1, 4, 2, 2 },
			new int[6] { 1, 2, 1, 1, 2, 4 },
			new int[6] { 1, 2, 1, 4, 2, 1 },
			new int[6] { 1, 4, 1, 1, 2, 2 },
			new int[6] { 1, 4, 1, 2, 2, 1 },
			new int[6] { 1, 1, 2, 2, 1, 4 },
			new int[6] { 1, 1, 2, 4, 1, 2 },
			new int[6] { 1, 2, 2, 1, 1, 4 },
			new int[6] { 1, 2, 2, 4, 1, 1 },
			new int[6] { 1, 4, 2, 1, 1, 2 },
			new int[6] { 1, 4, 2, 2, 1, 1 },
			new int[6] { 2, 4, 1, 2, 1, 1 },
			new int[6] { 2, 2, 1, 1, 1, 4 },
			new int[6] { 4, 1, 3, 1, 1, 1 },
			new int[6] { 2, 4, 1, 1, 1, 2 },
			new int[6] { 1, 3, 4, 1, 1, 1 },
			new int[6] { 1, 1, 1, 2, 4, 2 },
			new int[6] { 1, 2, 1, 1, 4, 2 },
			new int[6] { 1, 2, 1, 2, 4, 1 },
			new int[6] { 1, 1, 4, 2, 1, 2 },
			new int[6] { 1, 2, 4, 1, 1, 2 },
			new int[6] { 1, 2, 4, 2, 1, 1 },
			new int[6] { 4, 1, 1, 2, 1, 2 },
			new int[6] { 4, 2, 1, 1, 1, 2 },
			new int[6] { 4, 2, 1, 2, 1, 1 },
			new int[6] { 2, 1, 2, 1, 4, 1 },
			new int[6] { 2, 1, 4, 1, 2, 1 },
			new int[6] { 4, 1, 2, 1, 2, 1 },
			new int[6] { 1, 1, 1, 1, 4, 3 },
			new int[6] { 1, 1, 1, 3, 4, 1 },
			new int[6] { 1, 3, 1, 1, 4, 1 },
			new int[6] { 1, 1, 4, 1, 1, 3 },
			new int[6] { 1, 1, 4, 3, 1, 1 },
			new int[6] { 4, 1, 1, 1, 1, 3 },
			new int[6] { 4, 1, 1, 3, 1, 1 },
			new int[6] { 1, 1, 3, 1, 4, 1 },
			new int[6] { 1, 1, 4, 1, 3, 1 },
			new int[6] { 3, 1, 1, 1, 4, 1 },
			new int[6] { 4, 1, 1, 1, 3, 1 },
			new int[6] { 2, 1, 1, 4, 1, 2 },
			new int[6] { 2, 1, 1, 2, 1, 4 },
			new int[6] { 2, 1, 1, 2, 3, 2 },
			new int[7] { 2, 3, 3, 1, 1, 1, 2 }
		};

		private static readonly int MAX_AVG_VARIANCE = (int)((float)OneDReader.PATTERN_MATCH_RESULT_SCALE_FACTOR * 0.25f);

		private static readonly int MAX_INDIVIDUAL_VARIANCE = (int)((float)OneDReader.PATTERN_MATCH_RESULT_SCALE_FACTOR * 0.7f);

		private static int[] findStartPattern(BitArray row)
		{
			int size = row.Size;
			int nextSet = row.getNextSet(0);
			int num = 0;
			int[] array = new int[6];
			int num2 = nextSet;
			bool flag = false;
			int num3 = array.Length;
			for (int i = nextSet; i < size; i++)
			{
				if (row[i] ^ flag)
				{
					array[num]++;
					continue;
				}
				if (num == num3 - 1)
				{
					int num4 = MAX_AVG_VARIANCE;
					int num5 = -1;
					for (int j = 103; j <= 105; j++)
					{
						int num6 = OneDReader.patternMatchVariance(array, CODE_PATTERNS[j], MAX_INDIVIDUAL_VARIANCE);
						if (num6 < num4)
						{
							num4 = num6;
							num5 = j;
						}
					}
					if (num5 >= 0 && row.isRange(Math.Max(0, num2 - (i - num2) / 2), num2, false))
					{
						return new int[3] { num2, i, num5 };
					}
					num2 += array[0] + array[1];
					Array.Copy(array, 2, array, 0, num3 - 2);
					array[num3 - 2] = 0;
					array[num3 - 1] = 0;
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

		private static bool decodeCode(BitArray row, int[] counters, int rowOffset, out int code)
		{
			code = -1;
			if (!OneDReader.recordPattern(row, rowOffset, counters))
			{
				return false;
			}
			int num = MAX_AVG_VARIANCE;
			for (int i = 0; i < CODE_PATTERNS.Length; i++)
			{
				int[] pattern = CODE_PATTERNS[i];
				int num2 = OneDReader.patternMatchVariance(counters, pattern, MAX_INDIVIDUAL_VARIANCE);
				if (num2 < num)
				{
					num = num2;
					code = i;
				}
			}
			return code >= 0;
		}

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			bool flag = hints != null && hints.ContainsKey(DecodeHintType.ASSUME_GS1);
			int[] array = findStartPattern(row);
			if (array == null)
			{
				return null;
			}
			int num = array[2];
			List<byte> list = new List<byte>(20);
			list.Add((byte)num);
			int num2;
			switch (num)
			{
			case 103:
				num2 = 101;
				break;
			case 104:
				num2 = 100;
				break;
			case 105:
				num2 = 99;
				break;
			default:
				return null;
			}
			bool flag2 = false;
			bool flag3 = false;
			StringBuilder stringBuilder = new StringBuilder(20);
			int num3 = array[0];
			int num4 = array[1];
			int[] array2 = new int[6];
			int num5 = 0;
			int code = 0;
			int num6 = num;
			int num7 = 0;
			bool flag4 = true;
			bool flag5 = false;
			bool flag6 = false;
			while (!flag2)
			{
				bool flag7 = flag3;
				flag3 = false;
				num5 = code;
				if (!decodeCode(row, array2, num4, out code))
				{
					return null;
				}
				list.Add((byte)code);
				if (code != 106)
				{
					flag4 = true;
				}
				if (code != 106)
				{
					num7++;
					num6 += num7 * code;
				}
				num3 = num4;
				int[] array3 = array2;
				foreach (int num8 in array3)
				{
					num4 += num8;
				}
				switch (code)
				{
				case 103:
				case 104:
				case 105:
					return null;
				}
				switch (num2)
				{
				case 101:
					if (code < 64)
					{
						if (flag6 == flag5)
						{
							stringBuilder.Append((char)(32 + code));
						}
						else
						{
							stringBuilder.Append((char)(32 + code + 128));
						}
						flag6 = false;
						break;
					}
					if (code < 96)
					{
						if (flag6 == flag5)
						{
							stringBuilder.Append((char)(code - 64));
						}
						else
						{
							stringBuilder.Append((char)(code + 64));
						}
						flag6 = false;
						break;
					}
					if (code != 106)
					{
						flag4 = false;
					}
					switch (code)
					{
					case 102:
						if (flag)
						{
							if (stringBuilder.Length == 0)
							{
								stringBuilder.Append("]C1");
							}
							else
							{
								stringBuilder.Append('\u001d');
							}
						}
						break;
					case 101:
						if (!flag5 && flag6)
						{
							flag5 = true;
							flag6 = false;
						}
						else if (flag5 && flag6)
						{
							flag5 = false;
							flag6 = false;
						}
						else
						{
							flag6 = true;
						}
						break;
					case 98:
						flag3 = true;
						num2 = 100;
						break;
					case 100:
						num2 = 100;
						break;
					case 99:
						num2 = 99;
						break;
					case 106:
						flag2 = true;
						break;
					}
					break;
				case 100:
					if (code < 96)
					{
						if (flag6 == flag5)
						{
							stringBuilder.Append((char)(32 + code));
						}
						else
						{
							stringBuilder.Append((char)(32 + code + 128));
						}
						flag6 = false;
						break;
					}
					if (code != 106)
					{
						flag4 = false;
					}
					switch (code)
					{
					case 102:
						if (flag)
						{
							if (stringBuilder.Length == 0)
							{
								stringBuilder.Append("]C1");
							}
							else
							{
								stringBuilder.Append('\u001d');
							}
						}
						break;
					case 100:
						if (!flag5 && flag6)
						{
							flag5 = true;
							flag6 = false;
						}
						else if (flag5 && flag6)
						{
							flag5 = false;
							flag6 = false;
						}
						else
						{
							flag6 = true;
						}
						break;
					case 98:
						flag3 = true;
						num2 = 101;
						break;
					case 101:
						num2 = 101;
						break;
					case 99:
						num2 = 99;
						break;
					case 106:
						flag2 = true;
						break;
					}
					break;
				case 99:
					if (code < 100)
					{
						if (code < 10)
						{
							stringBuilder.Append('0');
						}
						stringBuilder.Append(code);
						break;
					}
					if (code != 106)
					{
						flag4 = false;
					}
					switch (code)
					{
					case 102:
						if (flag)
						{
							if (stringBuilder.Length == 0)
							{
								stringBuilder.Append("]C1");
							}
							else
							{
								stringBuilder.Append('\u001d');
							}
						}
						break;
					case 101:
						num2 = 101;
						break;
					case 100:
						num2 = 100;
						break;
					case 106:
						flag2 = true;
						break;
					}
					break;
				}
				if (flag7)
				{
					num2 = ((num2 != 101) ? 101 : 100);
				}
			}
			int num9 = num4 - num3;
			num4 = row.getNextUnset(num4);
			if (!row.isRange(num4, Math.Min(row.Size, num4 + (num4 - num3) / 2), false))
			{
				return null;
			}
			num6 -= num7 * num5;
			if (num6 % 103 != num5)
			{
				return null;
			}
			int length = stringBuilder.Length;
			if (length == 0)
			{
				return null;
			}
			if (length > 0 && flag4)
			{
				if (num2 == 99)
				{
					stringBuilder.Remove(length - 2, 2);
				}
				else
				{
					stringBuilder.Remove(length - 1, 1);
				}
			}
			float x = (float)(array[1] + array[0]) / 2f;
			float x2 = (float)num3 + (float)num9 / 2f;
			ResultPointCallback resultPointCallback = ((hints != null && hints.ContainsKey(DecodeHintType.NEED_RESULT_POINT_CALLBACK)) ? ((ResultPointCallback)hints[DecodeHintType.NEED_RESULT_POINT_CALLBACK]) : null);
			if (resultPointCallback != null)
			{
				resultPointCallback(new ResultPoint(x, rowNumber));
				resultPointCallback(new ResultPoint(x2, rowNumber));
			}
			int count = list.Count;
			byte[] array4 = new byte[count];
			for (int j = 0; j < count; j++)
			{
				array4[j] = list[j];
			}
			return new Result(stringBuilder.ToString(), array4, new ResultPoint[2]
			{
				new ResultPoint(x, rowNumber),
				new ResultPoint(x2, rowNumber)
			}, BarcodeFormat.CODE_128);
		}
	}
}
