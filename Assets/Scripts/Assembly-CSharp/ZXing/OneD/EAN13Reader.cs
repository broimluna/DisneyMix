using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class EAN13Reader : UPCEANReader
	{
		internal static int[] FIRST_DIGIT_ENCODINGS = new int[10] { 0, 11, 13, 14, 19, 25, 28, 21, 22, 26 };

		private readonly int[] decodeMiddleCounters;

		internal override BarcodeFormat BarcodeFormat
		{
			get
			{
				return BarcodeFormat.EAN_13;
			}
		}

		public EAN13Reader()
		{
			decodeMiddleCounters = new int[4];
		}

		protected internal override int decodeMiddle(BitArray row, int[] startRange, StringBuilder resultString)
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
				resultString.Append((char)(48 + digit % 10));
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
			if (!determineFirstDigit(resultString, num2))
			{
				return -1;
			}
			int[] array3 = UPCEANReader.findGuardPattern(row, num, true, UPCEANReader.MIDDLE_PATTERN);
			if (array3 == null)
			{
				return -1;
			}
			num = array3[1];
			for (int k = 0; k < 6; k++)
			{
				if (num >= size)
				{
					break;
				}
				int digit2;
				if (!UPCEANReader.decodeDigit(row, array, num, UPCEANReader.L_PATTERNS, out digit2))
				{
					return -1;
				}
				resultString.Append((char)(48 + digit2));
				int[] array4 = array;
				foreach (int num4 in array4)
				{
					num += num4;
				}
			}
			return num;
		}

		private static bool determineFirstDigit(StringBuilder resultString, int lgPatternFound)
		{
			for (int i = 0; i < 10; i++)
			{
				if (lgPatternFound == FIRST_DIGIT_ENCODINGS[i])
				{
					resultString.Insert(0, new char[1] { (char)(48 + i) });
					return true;
				}
			}
			return false;
		}
	}
}
