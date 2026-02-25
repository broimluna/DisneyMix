using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class UPCEReader : UPCEANReader
	{
		private static readonly int[] MIDDLE_END_PATTERN = new int[6] { 1, 1, 1, 1, 1, 1 };

		private static readonly int[][] NUMSYS_AND_CHECK_DIGIT_PATTERNS = new int[2][]
		{
			new int[10] { 56, 52, 50, 49, 44, 38, 35, 42, 41, 37 },
			new int[10] { 7, 11, 13, 14, 19, 25, 28, 21, 22, 26 }
		};

		private readonly int[] decodeMiddleCounters;

		internal override BarcodeFormat BarcodeFormat
		{
			get
			{
				return BarcodeFormat.UPC_E;
			}
		}

		public UPCEReader()
		{
			decodeMiddleCounters = new int[4];
		}

		protected internal override int decodeMiddle(BitArray row, int[] startRange, StringBuilder result)
		{
			int[] array = decodeMiddleCounters;
			array[0] = 0;
			array[1] = 0;
			array[2] = 0;
			array[3] = 0;
			int size = row.Size;
			int num = startRange[1];
			int num2 = 0;
			for (int i = 0; i < 6; i++)
			{
				if (num >= size)
				{
					break;
				}
				int digit;
				if (!UPCEANReader.decodeDigit(row, array, num, UPCEANReader.L_AND_G_PATTERNS, out digit))
				{
					return -1;
				}
				result.Append((char)(48 + digit % 10));
				int[] array2 = array;
				foreach (int num3 in array2)
				{
					num += num3;
				}
				if (digit >= 10)
				{
					num2 |= 1 << ((5 - i) & 0x1F);
				}
			}
			if (!determineNumSysAndCheckDigit(result, num2))
			{
				return -1;
			}
			return num;
		}

		protected override int[] decodeEnd(BitArray row, int endStart)
		{
			return UPCEANReader.findGuardPattern(row, endStart, true, MIDDLE_END_PATTERN);
		}

		protected override bool checkChecksum(string s)
		{
			return base.checkChecksum(convertUPCEtoUPCA(s));
		}

		private static bool determineNumSysAndCheckDigit(StringBuilder resultString, int lgPatternFound)
		{
			for (int i = 0; i <= 1; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (lgPatternFound == NUMSYS_AND_CHECK_DIGIT_PATTERNS[i][j])
					{
						resultString.Insert(0, new char[1] { (char)(48 + i) });
						resultString.Append((char)(48 + j));
						return true;
					}
				}
			}
			return false;
		}

		public static string convertUPCEtoUPCA(string upce)
		{
			string text = upce.Substring(1, 6);
			StringBuilder stringBuilder = new StringBuilder(12);
			stringBuilder.Append(upce[0]);
			char c = text[5];
			switch (c)
			{
			case '0':
			case '1':
			case '2':
				stringBuilder.Append(text, 0, 2);
				stringBuilder.Append(c);
				stringBuilder.Append("0000");
				stringBuilder.Append(text, 2, 3);
				break;
			case '3':
				stringBuilder.Append(text, 0, 3);
				stringBuilder.Append("00000");
				stringBuilder.Append(text, 3, 2);
				break;
			case '4':
				stringBuilder.Append(text, 0, 4);
				stringBuilder.Append("00000");
				stringBuilder.Append(text[4]);
				break;
			default:
				stringBuilder.Append(text, 0, 5);
				stringBuilder.Append("0000");
				stringBuilder.Append(c);
				break;
			}
			stringBuilder.Append(upce[7]);
			return stringBuilder.ToString();
		}
	}
}
