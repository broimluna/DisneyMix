using System;
using System.Collections.Generic;
using System.Text;

namespace ZXing.Common
{
	public static class StringUtils
	{
		private const string EUC_JP = "EUC-JP";

		private const string UTF8 = "UTF-8";

		private const string ISO88591 = "ISO-8859-1";

		private static string PLATFORM_DEFAULT_ENCODING = Encoding.Default.WebName;

		public static string SHIFT_JIS = "SJIS";

		public static string GB2312 = "GB2312";

		private static readonly bool ASSUME_SHIFT_JIS = string.Compare(SHIFT_JIS, PLATFORM_DEFAULT_ENCODING, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare("EUC-JP", PLATFORM_DEFAULT_ENCODING, StringComparison.OrdinalIgnoreCase) == 0;

		public static string guessEncoding(byte[] bytes, IDictionary<DecodeHintType, object> hints)
		{
			if (hints != null && hints.ContainsKey(DecodeHintType.CHARACTER_SET))
			{
				string text = (string)hints[DecodeHintType.CHARACTER_SET];
				if (text != null)
				{
					return text;
				}
			}
			int num = bytes.Length;
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			int num10 = 0;
			int num11 = 0;
			int num12 = 0;
			bool flag4 = bytes.Length > 3 && bytes[0] == 239 && bytes[1] == 187 && bytes[2] == 191;
			for (int i = 0; i < num; i++)
			{
				if (!flag && !flag2 && !flag3)
				{
					break;
				}
				int num13 = bytes[i] & 0xFF;
				if (flag3)
				{
					if (num2 > 0)
					{
						if ((num13 & 0x80) == 0)
						{
							flag3 = false;
						}
						else
						{
							num2--;
						}
					}
					else if ((num13 & 0x80) != 0)
					{
						if ((num13 & 0x40) == 0)
						{
							flag3 = false;
						}
						else
						{
							num2++;
							if ((num13 & 0x20) == 0)
							{
								num3++;
							}
							else
							{
								num2++;
								if ((num13 & 0x10) == 0)
								{
									num4++;
								}
								else
								{
									num2++;
									if ((num13 & 8) == 0)
									{
										num5++;
									}
									else
									{
										flag3 = false;
									}
								}
							}
						}
					}
				}
				if (flag)
				{
					if (num13 > 127 && num13 < 160)
					{
						flag = false;
					}
					else
					{
						switch (num13)
						{
						case 160:
						case 161:
						case 162:
						case 163:
						case 164:
						case 165:
						case 166:
						case 167:
						case 168:
						case 169:
						case 170:
						case 171:
						case 172:
						case 173:
						case 174:
						case 175:
						case 176:
						case 177:
						case 178:
						case 179:
						case 180:
						case 181:
						case 182:
						case 183:
						case 184:
						case 185:
						case 186:
						case 187:
						case 188:
						case 189:
						case 190:
						case 191:
						case 215:
						case 247:
							num12++;
							break;
						}
					}
				}
				if (!flag2)
				{
					continue;
				}
				if (num6 > 0)
				{
					if (num13 < 64 || num13 == 127 || num13 > 252)
					{
						flag2 = false;
					}
					else
					{
						num6--;
					}
				}
				else if (num13 == 128 || num13 == 160 || num13 > 239)
				{
					flag2 = false;
				}
				else if (num13 > 160 && num13 < 224)
				{
					num7++;
					num9 = 0;
					num8++;
					if (num8 > num10)
					{
						num10 = num8;
					}
				}
				else if (num13 > 127)
				{
					num6++;
					num8 = 0;
					num9++;
					if (num9 > num11)
					{
						num11 = num9;
					}
				}
				else
				{
					num8 = 0;
					num9 = 0;
				}
			}
			if (flag3 && num2 > 0)
			{
				flag3 = false;
			}
			if (flag2 && num6 > 0)
			{
				flag2 = false;
			}
			if (flag3 && (flag4 || num3 + num4 + num5 > 0))
			{
				return "UTF-8";
			}
			if (flag2 && (ASSUME_SHIFT_JIS || num10 >= 3 || num11 >= 3))
			{
				return SHIFT_JIS;
			}
			if (flag && flag2)
			{
				return ((num10 != 2 || num7 != 2) && num12 * 10 < num) ? "ISO-8859-1" : SHIFT_JIS;
			}
			if (flag)
			{
				return "ISO-8859-1";
			}
			if (flag2)
			{
				return SHIFT_JIS;
			}
			if (flag3)
			{
				return "UTF-8";
			}
			return PLATFORM_DEFAULT_ENCODING;
		}
	}
}
