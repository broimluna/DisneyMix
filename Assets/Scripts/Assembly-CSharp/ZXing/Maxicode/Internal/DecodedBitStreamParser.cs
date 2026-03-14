using System.Text;
using ZXing.Common;

namespace ZXing.Maxicode.Internal
{
	internal static class DecodedBitStreamParser
	{
		private const char SHIFTA = '\ufff0';

		private const char SHIFTB = '\ufff1';

		private const char SHIFTC = '\ufff2';

		private const char SHIFTD = '\ufff3';

		private const char SHIFTE = '\ufff4';

		private const char TWOSHIFTA = '\ufff5';

		private const char THREESHIFTA = '\ufff6';

		private const char LATCHA = '\ufff7';

		private const char LATCHB = '\ufff8';

		private const char LOCK = '\ufff9';

		private const char ECI = '\ufffa';

		private const char NS = '\ufffb';

		private const char PAD = '￼';

		private const char FS = '\u001c';

		private const char GS = '\u001d';

		private const char RS = '\u001e';

		private const string NINE_DIGITS = "000000000";

		private const string THREE_DIGITS = "000";

		private static string[] SETS = new string[6]
		{
			"\nABCDEFGHIJKLMNOPQRSTUVWXYZ" + '\ufffa' + '\u001c' + '\u001d' + '\u001e' + '\ufffb' + ' ' + '￼' + "\"#$%&'()*+,-./0123456789:" + '\ufff1' + '\ufff2' + '\ufff3' + '\ufff4' + '\ufff8',
			"`abcdefghijklmnopqrstuvwxyz" + '\ufffa' + '\u001c' + '\u001d' + '\u001e' + '\ufffb' + '{' + '￼' + "}~\u007f;<=>?[\\]^_ ,./:@!|" + '￼' + '\ufff5' + '\ufff6' + '￼' + '\ufff0' + '\ufff2' + '\ufff3' + '\ufff4' + '\ufff7',
			"ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚ" + '\ufffa' + '\u001c' + '\u001d' + '\u001e' + "ÛÜÝÞßª¬±²³µ¹º¼½¾\u0080\u0081\u0082\u0083\u0084\u0085\u0086\u0087\u0088\u0089" + '\ufff7' + ' ' + '\ufff9' + '\ufff3' + '\ufff4' + '\ufff8',
			"àáâãäåæçèéêëìíîïðñòóôõö÷øùú" + '\ufffa' + '\u001c' + '\u001d' + '\u001e' + '\ufffb' + "ûüýþÿ¡\u00a8«\u00af°\u00b4·\u00b8»¿\u008a\u008b\u008c\u008d\u008e\u008f\u0090\u0091\u0092\u0093\u0094" + '\ufff7' + ' ' + '\ufff2' + '\ufff9' + '\ufff4' + '\ufff8',
			"\0\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t\n\v\f\r\u000e\u000f\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a" + '\ufffa' + '￼' + '￼' + '\u001b' + '\ufffb' + '\u001c' + '\u001d' + '\u001e' + "\u001f\u009f\u00a0¢£¤¥¦§©\u00ad®¶\u0095\u0096\u0097\u0098\u0099\u009a\u009b\u009c\u009d\u009e" + '\ufff7' + ' ' + '\ufff2' + '\ufff3' + '\ufff9' + '\ufff8',
			"\0\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t\n\v\f\r\u000e\u000f\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f !\"#$%&'()*+,-./0123456789:;<=>?"
		};

		internal static DecoderResult decode(byte[] bytes, int mode)
		{
			StringBuilder stringBuilder = new StringBuilder(144);
			string text;
			string text3;
			string text4;
			switch (mode)
			{
			case 2:
			{
				int postCode = getPostCode2(bytes);
				string text2 = "0000000000".Substring(0, getPostCode2Length(bytes));
				text = postCode.ToString(text2);
				goto IL_0061;
			}
			case 3:
				text = getPostCode3(bytes);
				goto IL_0061;
			case 4:
				stringBuilder.Append(getMessage(bytes, 1, 93));
				break;
			case 5:
				{
					stringBuilder.Append(getMessage(bytes, 1, 77));
					break;
				}
				IL_0061:
				text3 = getCountry(bytes).ToString("000");
				text4 = getServiceClass(bytes).ToString("000");
				stringBuilder.Append(getMessage(bytes, 10, 84));
				if (stringBuilder.ToString().StartsWith("[)>" + '\u001e' + "01" + '\u001d'))
				{
					stringBuilder.Insert(9, text + '\u001d' + text3 + '\u001d' + text4 + '\u001d');
				}
				else
				{
					stringBuilder.Insert(0, text + '\u001d' + text3 + '\u001d' + text4 + '\u001d');
				}
				break;
			}
			return new DecoderResult(bytes, stringBuilder.ToString(), null, mode.ToString());
		}

		private static int getBit(int bit, byte[] bytes)
		{
			bit--;
			return ((bytes[bit / 6] & (1 << 5 - bit % 6)) != 0) ? 1 : 0;
		}

		private static int getInt(byte[] bytes, byte[] x)
		{
			int num = 0;
			for (int i = 0; i < x.Length; i++)
			{
				num += getBit(x[i], bytes) << x.Length - i - 1;
			}
			return num;
		}

		private static int getCountry(byte[] bytes)
		{
			return getInt(bytes, new byte[10] { 53, 54, 43, 44, 45, 46, 47, 48, 37, 38 });
		}

		private static int getServiceClass(byte[] bytes)
		{
			return getInt(bytes, new byte[10] { 55, 56, 57, 58, 59, 60, 49, 50, 51, 52 });
		}

		private static int getPostCode2Length(byte[] bytes)
		{
			return getInt(bytes, new byte[6] { 39, 40, 41, 42, 31, 32 });
		}

		private static int getPostCode2(byte[] bytes)
		{
			return getInt(bytes, new byte[30]
			{
				33, 34, 35, 36, 25, 26, 27, 28, 29, 30,
				19, 20, 21, 22, 23, 24, 13, 14, 15, 16,
				17, 18, 7, 8, 9, 10, 11, 12, 1, 2
			});
		}

		private static string getPostCode3(byte[] bytes)
		{
			return new string(new char[6]
			{
				SETS[0][getInt(bytes, new byte[6] { 39, 40, 41, 42, 31, 32 })],
				SETS[0][getInt(bytes, new byte[6] { 33, 34, 35, 36, 25, 26 })],
				SETS[0][getInt(bytes, new byte[6] { 27, 28, 29, 30, 19, 20 })],
				SETS[0][getInt(bytes, new byte[6] { 21, 22, 23, 24, 13, 14 })],
				SETS[0][getInt(bytes, new byte[6] { 15, 16, 17, 18, 7, 8 })],
				SETS[0][getInt(bytes, new byte[6] { 9, 10, 11, 12, 1, 2 })]
			});
		}

		private static string getMessage(byte[] bytes, int start, int len)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = -1;
			int num2 = 0;
			int num3 = 0;
			for (int i = start; i < start + len; i++)
			{
				char c = SETS[num2][bytes[i]];
				switch (c)
				{
				case '\ufff7':
					num2 = 0;
					num = -1;
					break;
				case '\ufff8':
					num2 = 1;
					num = -1;
					break;
				case '\ufff0':
				case '\ufff1':
				case '\ufff2':
				case '\ufff3':
				case '\ufff4':
					num3 = num2;
					num2 = c - 65520;
					num = 1;
					break;
				case '\ufff5':
					num3 = num2;
					num2 = 0;
					num = 2;
					break;
				case '\ufff6':
					num3 = num2;
					num2 = 0;
					num = 3;
					break;
				case '\ufffb':
					stringBuilder.Append(((bytes[++i] << 24) + (bytes[++i] << 18) + (bytes[++i] << 12) + (bytes[++i] << 6) + bytes[++i]).ToString("000000000"));
					break;
				case '\ufff9':
					num = -1;
					break;
				default:
					stringBuilder.Append(c);
					break;
				}
				if (num-- == 0)
				{
					num2 = num3;
				}
			}
			while (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '￼')
			{
				stringBuilder.Length--;
			}
			return stringBuilder.ToString();
		}
	}
}
