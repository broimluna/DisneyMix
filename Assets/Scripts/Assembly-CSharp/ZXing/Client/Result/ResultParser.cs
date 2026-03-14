using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ZXing.Client.Result
{
	public abstract class ResultParser
	{
		private static readonly ResultParser[] PARSERS = new ResultParser[20]
		{
			new BookmarkDoCoMoResultParser(),
			new AddressBookDoCoMoResultParser(),
			new EmailDoCoMoResultParser(),
			new AddressBookAUResultParser(),
			new VCardResultParser(),
			new BizcardResultParser(),
			new VEventResultParser(),
			new EmailAddressResultParser(),
			new SMTPResultParser(),
			new TelResultParser(),
			new SMSMMSResultParser(),
			new SMSTOMMSTOResultParser(),
			new GeoResultParser(),
			new WifiResultParser(),
			new URLTOResultParser(),
			new URIResultParser(),
			new ISBNResultParser(),
			new ProductResultParser(),
			new ExpandedProductResultParser(),
			new VINResultParser()
		};

		private static readonly Regex DIGITS = new Regex("\\A(?:\\d+)\\z");

		private static readonly Regex AMPERSAND = new Regex("&");

		private static readonly Regex EQUALS = new Regex("=");

		public abstract ParsedResult parse(ZXing.Result theResult);

		public static ParsedResult parseResult(ZXing.Result theResult)
		{
			ResultParser[] pARSERS = PARSERS;
			foreach (ResultParser resultParser in pARSERS)
			{
				ParsedResult parsedResult = resultParser.parse(theResult);
				if (parsedResult != null)
				{
					return parsedResult;
				}
			}
			return new TextParsedResult(theResult.Text, null);
		}

		protected static void maybeAppend(string value, StringBuilder result)
		{
			if (value != null)
			{
				result.Append('\n');
				result.Append(value);
			}
		}

		protected static void maybeAppend(string[] value, StringBuilder result)
		{
			if (value != null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					result.Append('\n');
					result.Append(value[i]);
				}
			}
		}

		protected static string[] maybeWrap(string value_Renamed)
		{
			return (value_Renamed == null) ? null : new string[1] { value_Renamed };
		}

		protected static string unescapeBackslash(string escaped)
		{
			if (escaped != null)
			{
				int num = escaped.IndexOf('\\');
				if (num >= 0)
				{
					int length = escaped.Length;
					StringBuilder stringBuilder = new StringBuilder(length - 1);
					stringBuilder.Append(escaped.ToCharArray(), 0, num);
					bool flag = false;
					for (int i = num; i < length; i++)
					{
						char c = escaped[i];
						if (flag || c != '\\')
						{
							stringBuilder.Append(c);
							flag = false;
						}
						else
						{
							flag = true;
						}
					}
					return stringBuilder.ToString();
				}
			}
			return escaped;
		}

		protected static int parseHexDigit(char c)
		{
			switch (c)
			{
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
				return 10 + (c - 97);
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
				return 10 + (c - 65);
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				return c - 48;
			default:
				return -1;
			}
		}

		internal static bool isStringOfDigits(string value, int length)
		{
			return value != null && length > 0 && length == value.Length && DIGITS.Match(value).Success;
		}

		internal static bool isSubstringOfDigits(string value, int offset, int length)
		{
			if (value == null || length <= 0)
			{
				return false;
			}
			int num = offset + length;
			return value.Length >= num && DIGITS.Match(value, offset, length).Success;
		}

		internal static IDictionary<string, string> parseNameValuePairs(string uri)
		{
			int num = uri.IndexOf('?');
			if (num < 0)
			{
				return null;
			}
			Dictionary<string, string> result = new Dictionary<string, string>(3);
			string[] array = AMPERSAND.Split(uri.Substring(num + 1));
			foreach (string keyValue in array)
			{
				appendKeyValue(keyValue, result);
			}
			return result;
		}

		private static void appendKeyValue(string keyValue, IDictionary<string, string> result)
		{
			string[] array = EQUALS.Split(keyValue, 2);
			if (array.Length == 2)
			{
				string key = array[0];
				string escaped = array[1];
				try
				{
					escaped = (result[key] = urlDecode(escaped));
				}
				catch (Exception innerException)
				{
					throw new InvalidOperationException("url decoding failed", innerException);
				}
				result[key] = escaped;
			}
		}

		internal static string[] matchPrefixedField(string prefix, string rawText, char endChar, bool trim)
		{
			IList<string> list = null;
			int num = 0;
			int length = rawText.Length;
			while (num < length)
			{
				num = rawText.IndexOf(prefix, num);
				if (num < 0)
				{
					break;
				}
				num += prefix.Length;
				int num2 = num;
				bool flag = false;
				while (!flag)
				{
					num = rawText.IndexOf(endChar, num);
					if (num < 0)
					{
						num = rawText.Length;
						flag = true;
						continue;
					}
					if (rawText[num - 1] == '\\')
					{
						num++;
						continue;
					}
					if (list == null)
					{
						list = new List<string>();
					}
					string text = unescapeBackslash(rawText.Substring(num2, num - num2));
					if (trim)
					{
						text = text.Trim();
					}
					if (!string.IsNullOrEmpty(text))
					{
						list.Add(text);
					}
					num++;
					flag = true;
				}
			}
			if (list == null || list.Count == 0)
			{
				return null;
			}
			return SupportClass.toStringArray(list);
		}

		internal static string matchSinglePrefixedField(string prefix, string rawText, char endChar, bool trim)
		{
			string[] array = matchPrefixedField(prefix, rawText, endChar, trim);
			return (array != null) ? array[0] : null;
		}

		protected static string urlDecode(string escaped)
		{
			if (escaped == null)
			{
				return null;
			}
			char[] array = escaped.ToCharArray();
			int num = findFirstEscape(array);
			if (num < 0)
			{
				return escaped;
			}
			int num2 = array.Length;
			StringBuilder stringBuilder = new StringBuilder(num2 - 2);
			stringBuilder.Append(array, 0, num);
			for (int i = num; i < num2; i++)
			{
				char c = array[i];
				switch (c)
				{
				case '+':
					stringBuilder.Append(' ');
					break;
				case '%':
				{
					if (i >= num2 - 2)
					{
						stringBuilder.Append('%');
						break;
					}
					int num3 = parseHexDigit(array[++i]);
					int num4 = parseHexDigit(array[++i]);
					if (num3 < 0 || num4 < 0)
					{
						stringBuilder.Append('%');
						stringBuilder.Append(array[i - 1]);
						stringBuilder.Append(array[i]);
					}
					stringBuilder.Append((char)((num3 << 4) + num4));
					break;
				}
				default:
					stringBuilder.Append(c);
					break;
				}
			}
			return stringBuilder.ToString();
		}

		private static int findFirstEscape(char[] escapedArray)
		{
			int num = escapedArray.Length;
			for (int i = 0; i < num; i++)
			{
				char c = escapedArray[i];
				if (c == '+' || c == '%')
				{
					return i;
				}
			}
			return -1;
		}
	}
}
