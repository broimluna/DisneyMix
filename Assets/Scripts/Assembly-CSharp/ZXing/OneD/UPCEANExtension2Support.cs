using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	internal sealed class UPCEANExtension2Support
	{
		private readonly int[] decodeMiddleCounters = new int[4];

		private readonly StringBuilder decodeRowStringBuffer = new StringBuilder();

		internal Result decodeRow(int rowNumber, BitArray row, int[] extensionStartRange)
		{
			StringBuilder stringBuilder = decodeRowStringBuffer;
			stringBuilder.Length = 0;
			int num = decodeMiddle(row, extensionStartRange, stringBuilder);
			if (num < 0)
			{
				return null;
			}
			string text = stringBuilder.ToString();
			IDictionary<ResultMetadataType, object> dictionary = parseExtensionString(text);
			Result result = new Result(text, null, new ResultPoint[2]
			{
				new ResultPoint((float)(extensionStartRange[0] + extensionStartRange[1]) / 2f, rowNumber),
				new ResultPoint(num, rowNumber)
			}, BarcodeFormat.UPC_EAN_EXTENSION);
			if (dictionary != null)
			{
				result.putAllMetadata(dictionary);
			}
			return result;
		}

		private int decodeMiddle(BitArray row, int[] startRange, StringBuilder resultString)
		{
			int[] array = decodeMiddleCounters;
			array[0] = 0;
			array[1] = 0;
			array[2] = 0;
			array[3] = 0;
			int size = row.Size;
			int num = startRange[1];
			int num2 = 0;
			for (int i = 0; i < 2; i++)
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
					num2 |= 1 << ((1 - i) & 0x1F);
				}
				if (i != 1)
				{
					num = row.getNextSet(num);
					num = row.getNextUnset(num);
				}
			}
			if (resultString.Length != 2)
			{
				return -1;
			}
			if (int.Parse(resultString.ToString()) % 4 != num2)
			{
				return -1;
			}
			return num;
		}

		private static IDictionary<ResultMetadataType, object> parseExtensionString(string raw)
		{
			if (raw.Length != 2)
			{
				return null;
			}
			IDictionary<ResultMetadataType, object> dictionary = new Dictionary<ResultMetadataType, object>();
			dictionary[ResultMetadataType.ISSUE_NUMBER] = Convert.ToInt32(raw);
			return dictionary;
		}
	}
}
