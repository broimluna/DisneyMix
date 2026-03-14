using System;
using System.Text.RegularExpressions;

namespace ZXing.Client.Result
{
	public class VINResultParser : ResultParser
	{
		private static readonly Regex IOQ = new Regex("[IOQ]");

		private static readonly Regex AZ09 = new Regex("\\A(?:[A-Z0-9]{17})\\z");

		public override ParsedResult parse(ZXing.Result result)
		{
			try
			{
				if (result.BarcodeFormat != BarcodeFormat.CODE_39)
				{
					return null;
				}
				string text = result.Text;
				text = IOQ.Replace(text, string.Empty).Trim();
				Match match = AZ09.Match(text);
				if (!match.Success)
				{
					return null;
				}
				if (!checkChecksum(text))
				{
					return null;
				}
				string text2 = text.Substring(0, 3);
				return new VINParsedResult(text, text2, text.Substring(3, 6), text.Substring(9, 8), countryCode(text2), text.Substring(3, 5), modelYear(text[9]), text[10], text.Substring(11));
			}
			catch
			{
				return null;
			}
		}

		private static bool checkChecksum(string vin)
		{
			int num = 0;
			for (int i = 0; i < vin.Length; i++)
			{
				num += vinPositionWeight(i + 1) * vinCharValue(vin[i]);
			}
			char c = vin[8];
			char c2 = checkChar(num % 11);
			return c == c2;
		}

		private static int vinCharValue(char c)
		{
			if (c >= 'A' && c <= 'I')
			{
				return c - 65 + 1;
			}
			if (c >= 'J' && c <= 'R')
			{
				return c - 74 + 1;
			}
			if (c >= 'S' && c <= 'Z')
			{
				return c - 83 + 2;
			}
			if (c >= '0' && c <= '9')
			{
				return c - 48;
			}
			throw new ArgumentException(c.ToString());
		}

		private static int vinPositionWeight(int position)
		{
			if (position >= 1 && position <= 7)
			{
				return 9 - position;
			}
			switch (position)
			{
			case 8:
				return 10;
			case 9:
				return 0;
			case 10:
			case 11:
			case 12:
			case 13:
			case 14:
			case 15:
			case 16:
			case 17:
				return 19 - position;
			default:
				throw new ArgumentException();
			}
		}

		private static char checkChar(int remainder)
		{
			if (remainder < 10)
			{
				return (char)(48 + remainder);
			}
			if (remainder == 10)
			{
				return 'X';
			}
			throw new ArgumentException();
		}

		private static int modelYear(char c)
		{
			if (c >= 'E' && c <= 'H')
			{
				return c - 69 + 1984;
			}
			if (c >= 'J' && c <= 'N')
			{
				return c - 74 + 1988;
			}
			switch (c)
			{
			case 'P':
				return 1993;
			case 'R':
			case 'S':
			case 'T':
				return c - 82 + 1994;
			default:
				if (c >= 'V' && c <= 'Y')
				{
					return c - 86 + 1997;
				}
				if (c >= '1' && c <= '9')
				{
					return c - 49 + 2001;
				}
				if (c >= 'A' && c <= 'D')
				{
					return c - 65 + 2010;
				}
				throw new ArgumentException(c.ToString());
			}
		}

		private static string countryCode(string wmi)
		{
			char c = wmi[0];
			char c2 = wmi[1];
			switch (c)
			{
			case '1':
			case '4':
			case '5':
				return "US";
			case '2':
				return "CA";
			case '3':
				if (c2 >= 'A' && c2 <= 'W')
				{
					return "MX";
				}
				break;
			case '9':
				if ((c2 >= 'A' && c2 <= 'E') || (c2 >= '3' && c2 <= '9'))
				{
					return "BR";
				}
				break;
			case 'J':
				if (c2 >= 'A' && c2 <= 'T')
				{
					return "JP";
				}
				break;
			case 'K':
				if (c2 >= 'L' && c2 <= 'R')
				{
					return "KO";
				}
				break;
			case 'L':
				return "CN";
			case 'M':
				if (c2 >= 'A' && c2 <= 'E')
				{
					return "IN";
				}
				break;
			case 'S':
				if (c2 >= 'A' && c2 <= 'M')
				{
					return "UK";
				}
				if (c2 >= 'N' && c2 <= 'T')
				{
					return "DE";
				}
				break;
			case 'V':
				if (c2 >= 'F' && c2 <= 'R')
				{
					return "FR";
				}
				if (c2 >= 'S' && c2 <= 'W')
				{
					return "ES";
				}
				break;
			case 'W':
				return "DE";
			case 'X':
				switch (c2)
				{
				case '0':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return "RU";
				}
				break;
			case 'Z':
				if (c2 >= 'A' && c2 <= 'R')
				{
					return "IT";
				}
				break;
			}
			return null;
		}
	}
}
