using System;
using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class Code128Writer : OneDimensionalCodeWriter
	{
		private const int CODE_START_B = 104;

		private const int CODE_START_C = 105;

		private const int CODE_CODE_B = 100;

		private const int CODE_CODE_C = 99;

		private const int CODE_STOP = 106;

		private const char ESCAPE_FNC_1 = 'ñ';

		private const char ESCAPE_FNC_2 = 'ò';

		private const char ESCAPE_FNC_3 = 'ó';

		private const char ESCAPE_FNC_4 = 'ô';

		private const int CODE_FNC_1 = 102;

		private const int CODE_FNC_2 = 97;

		private const int CODE_FNC_3 = 96;

		private const int CODE_FNC_4_B = 100;

		private bool forceCodesetB;

		public override BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (format != BarcodeFormat.CODE_128)
			{
				throw new ArgumentException("Can only encode CODE_128, but got " + format);
			}
			forceCodesetB = hints != null && hints.ContainsKey(EncodeHintType.CODE128_FORCE_CODESET_B) && (bool)hints[EncodeHintType.CODE128_FORCE_CODESET_B];
			return base.encode(contents, format, width, height, hints);
		}

		public override bool[] encode(string contents)
		{
			int length = contents.Length;
			if (length < 1 || length > 80)
			{
				throw new ArgumentException("Contents length should be between 1 and 80 characters, but got " + length);
			}
			for (int i = 0; i < length; i++)
			{
				char c = contents[i];
				if (c < ' ' || c > '~')
				{
					switch (c)
					{
					case 'ñ':
					case 'ò':
					case 'ó':
					case 'ô':
						continue;
					}
					throw new ArgumentException("Bad character in input: " + c);
				}
			}
			List<int[]> list = new List<int[]>();
			int num = 0;
			int num2 = 1;
			int num3 = 0;
			int num4 = 0;
			while (num4 < length)
			{
				int length2 = ((num3 != 99) ? 4 : 2);
				int num5 = ((!isDigits(contents, num4, length2)) ? 100 : ((!forceCodesetB) ? 99 : 100));
				int num6;
				if (num5 == num3)
				{
					switch (contents[num4])
					{
					case 'ñ':
						num6 = 102;
						break;
					case 'ò':
						num6 = 97;
						break;
					case 'ó':
						num6 = 96;
						break;
					case 'ô':
						num6 = 100;
						break;
					default:
						if (num3 == 100)
						{
							num6 = contents[num4] - 32;
							break;
						}
						num6 = int.Parse(contents.Substring(num4, 2));
						num4++;
						break;
					}
					num4++;
				}
				else
				{
					num6 = ((num3 != 0) ? num5 : ((num5 != 100) ? 105 : 104));
					num3 = num5;
				}
				list.Add(Code128Reader.CODE_PATTERNS[num6]);
				num += num6 * num2;
				if (num4 != 0)
				{
					num2++;
				}
			}
			num %= 103;
			list.Add(Code128Reader.CODE_PATTERNS[num]);
			list.Add(Code128Reader.CODE_PATTERNS[106]);
			int num7 = 0;
			foreach (int[] item in list)
			{
				int[] array = item;
				foreach (int num8 in array)
				{
					num7 += num8;
				}
			}
			bool[] array2 = new bool[num7];
			int num9 = 0;
			foreach (int[] item2 in list)
			{
				num9 += OneDimensionalCodeWriter.appendPattern(array2, num9, item2, true);
			}
			return array2;
		}

		private static bool isDigits(string value, int start, int length)
		{
			int num = start + length;
			int length2 = value.Length;
			for (int i = start; i < num && i < length2; i++)
			{
				char c = value[i];
				if (c < '0' || c > '9')
				{
					if (c != 'ñ')
					{
						return false;
					}
					num++;
				}
			}
			return num <= length2;
		}
	}
}
