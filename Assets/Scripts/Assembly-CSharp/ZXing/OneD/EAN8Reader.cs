using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class EAN8Reader : UPCEANReader
	{
		private readonly int[] decodeMiddleCounters;

		internal override BarcodeFormat BarcodeFormat
		{
			get
			{
				return BarcodeFormat.EAN_8;
			}
		}

		public EAN8Reader()
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
			for (int i = 0; i < 4; i++)
			{
				if (num >= size)
				{
					break;
				}
				int digit;
				if (!UPCEANReader.decodeDigit(row, array, num, UPCEANReader.L_PATTERNS, out digit))
				{
					return -1;
				}
				result.Append((char)(48 + digit));
				int[] array2 = array;
				foreach (int num2 in array2)
				{
					num += num2;
				}
			}
			int[] array3 = UPCEANReader.findGuardPattern(row, num, true, UPCEANReader.MIDDLE_PATTERN);
			if (array3 == null)
			{
				return -1;
			}
			num = array3[1];
			for (int k = 0; k < 4; k++)
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
				result.Append((char)(48 + digit2));
				int[] array4 = array;
				foreach (int num3 in array4)
				{
					num += num3;
				}
			}
			return num;
		}
	}
}
