using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	internal sealed class UPCEANExtension5Support
	{
		private static readonly int[] CHECK_DIGIT_ENCODINGS = new int[10] { 24, 20, 18, 17, 12, 6, 3, 10, 9, 5 };

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
			for (int i = 0; i < 5; i++)
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
					num2 |= 1 << ((4 - i) & 0x1F);
				}
				if (i != 4)
				{
					num = row.getNextSet(num);
					num = row.getNextUnset(num);
				}
			}
			if (resultString.Length != 5)
			{
				return -1;
			}
			int checkDigit;
			if (!determineCheckDigit(num2, out checkDigit))
			{
				return -1;
			}
			if (extensionChecksum(resultString.ToString()) != checkDigit)
			{
				return -1;
			}
			return num;
		}

		private static int extensionChecksum(string s)
		{
			int length = s.Length;
			int num = 0;
			for (int num2 = length - 2; num2 >= 0; num2 -= 2)
			{
				num += s[num2] - 48;
			}
			num *= 3;
			for (int num3 = length - 1; num3 >= 0; num3 -= 2)
			{
				num += s[num3] - 48;
			}
			num *= 3;
			return num % 10;
		}

		private static bool determineCheckDigit(int lgPatternFound, out int checkDigit)
		{
			for (checkDigit = 0; checkDigit < 10; checkDigit++)
			{
				if (lgPatternFound == CHECK_DIGIT_ENCODINGS[checkDigit])
				{
					return true;
				}
			}
			return false;
		}

		private static IDictionary<ResultMetadataType, object> parseExtensionString(string raw)
		{
			if (raw.Length != 5)
			{
				return null;
			}
			object obj = parseExtension5String(raw);
			if (obj == null)
			{
				return null;
			}
			IDictionary<ResultMetadataType, object> dictionary = new Dictionary<ResultMetadataType, object>();
			dictionary[ResultMetadataType.SUGGESTED_PRICE] = obj;
			return dictionary;
		}

		private static string parseExtension5String(string raw)
		{
			string text;
			switch (raw[0])
			{
			case '0':
				text = "£";
				break;
			case '5':
				text = "$";
				break;
			case '9':
				if ("90000".Equals(raw))
				{
					return null;
				}
				if ("99991".Equals(raw))
				{
					return "0.00";
				}
				if ("99990".Equals(raw))
				{
					return "Used";
				}
				text = string.Empty;
				break;
			default:
				text = string.Empty;
				break;
			}
			int num = int.Parse(raw.Substring(1));
			string text2 = (num / 100).ToString();
			int num2 = num % 100;
			string text3 = ((num2 >= 10) ? num2.ToString() : ("0" + num2));
			return text + text2 + '.' + text3;
		}
	}
}
