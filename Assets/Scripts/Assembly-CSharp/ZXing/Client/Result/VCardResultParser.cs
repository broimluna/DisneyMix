using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ZXing.Client.Result
{
	internal sealed class VCardResultParser : ResultParser
	{
		private static readonly Regex BEGIN_VCARD = new Regex("BEGIN:VCARD", RegexOptions.IgnoreCase);

		private static readonly Regex VCARD_LIKE_DATE = new Regex("\\A(?:\\d{4}-?\\d{2}-?\\d{2})\\z");

		private static readonly Regex CR_LF_SPACE_TAB = new Regex("\r\n[ \t]");

		private static readonly Regex NEWLINE_ESCAPE = new Regex("\\\\[nN]");

		private static readonly Regex VCARD_ESCAPES = new Regex("\\\\([,;\\\\])");

		private static readonly Regex EQUALS = new Regex("=");

		private static readonly Regex SEMICOLON = new Regex(";");

		private static readonly Regex UNESCAPED_SEMICOLONS = new Regex("(?<!\\\\);+");

		private static readonly Regex COMMA = new Regex(",");

		private static readonly Regex SEMICOLON_OR_COMMA = new Regex("[;,]");

		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			Match match = BEGIN_VCARD.Match(text);
			if (!match.Success || match.Index != 0)
			{
				return null;
			}
			List<List<string>> list = matchVCardPrefixedField("FN", text, true, false);
			if (list == null)
			{
				list = matchVCardPrefixedField("N", text, true, false);
				formatNames(list);
			}
			List<string> list2 = matchSingleVCardPrefixedField("NICKNAME", text, true, false);
			string[] nicknames = ((list2 != null) ? COMMA.Split(list2[0]) : null);
			List<List<string>> lists = matchVCardPrefixedField("TEL", text, true, false);
			List<List<string>> lists2 = matchVCardPrefixedField("EMAIL", text, true, false);
			List<string> list3 = matchSingleVCardPrefixedField("NOTE", text, false, false);
			List<List<string>> lists3 = matchVCardPrefixedField("ADR", text, true, true);
			List<string> list4 = matchSingleVCardPrefixedField("ORG", text, true, true);
			List<string> list5 = matchSingleVCardPrefixedField("BDAY", text, true, false);
			if (list5 != null && !isLikeVCardDate(list5[0]))
			{
				list5 = null;
			}
			List<string> list6 = matchSingleVCardPrefixedField("TITLE", text, true, false);
			List<List<string>> lists4 = matchVCardPrefixedField("URL", text, true, false);
			List<string> list7 = matchSingleVCardPrefixedField("IMPP", text, true, false);
			List<string> list8 = matchSingleVCardPrefixedField("GEO", text, true, false);
			string[] array = ((list8 != null) ? SEMICOLON_OR_COMMA.Split(list8[0]) : null);
			if (array != null && array.Length != 2)
			{
				array = null;
			}
			return new AddressBookParsedResult(toPrimaryValues(list), nicknames, null, toPrimaryValues(lists), toTypes(lists), toPrimaryValues(lists2), toTypes(lists2), toPrimaryValue(list7), toPrimaryValue(list3), toPrimaryValues(lists3), toTypes(lists3), toPrimaryValue(list4), toPrimaryValue(list5), toPrimaryValue(list6), toPrimaryValues(lists4), array);
		}

		public static List<List<string>> matchVCardPrefixedField(string prefix, string rawText, bool trim, bool parseFieldDivider)
		{
			List<List<string>> list = null;
			int num = 0;
			int length = rawText.Length;
			while (num < length)
			{
				Regex regex = new Regex("(?:^|\n)" + prefix + "(?:;([^:]*))?:", RegexOptions.IgnoreCase);
				if (num > 0)
				{
					num--;
				}
				Match match = regex.Match(rawText, num);
				if (!match.Success)
				{
					break;
				}
				num = match.Index + match.Length;
				string value = match.Groups[1].Value;
				List<string> list2 = null;
				bool flag = false;
				string charset = null;
				if (value != null)
				{
					string[] array = SEMICOLON.Split(value);
					foreach (string text in array)
					{
						if (list2 == null)
						{
							list2 = new List<string>(1);
						}
						list2.Add(text);
						string[] array2 = EQUALS.Split(text, 2);
						if (array2.Length > 1)
						{
							string strB = array2[0];
							string text2 = array2[1];
							if (string.Compare("ENCODING", strB, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare("QUOTED-PRINTABLE", text2, StringComparison.OrdinalIgnoreCase) == 0)
							{
								flag = true;
							}
							else if (string.Compare("CHARSET", strB, StringComparison.OrdinalIgnoreCase) == 0)
							{
								charset = text2;
							}
						}
					}
				}
				int num2 = num;
				while ((num = rawText.IndexOf('\n', num)) >= 0)
				{
					if (num < rawText.Length - 1 && (rawText[num + 1] == ' ' || rawText[num + 1] == '\t'))
					{
						num += 2;
						continue;
					}
					if (flag && ((num >= 1 && rawText[num - 1] == '=') || (num >= 2 && rawText[num - 2] == '=')))
					{
						num++;
						continue;
					}
					break;
				}
				if (num < 0)
				{
					num = length;
				}
				else if (num > num2)
				{
					if (list == null)
					{
						list = new List<List<string>>(1);
					}
					if (num >= 1 && rawText[num - 1] == '\r')
					{
						num--;
					}
					string text3 = rawText.Substring(num2, num - num2);
					if (trim)
					{
						text3 = text3.Trim();
					}
					if (flag)
					{
						text3 = decodeQuotedPrintable(text3, charset);
						if (parseFieldDivider)
						{
							text3 = UNESCAPED_SEMICOLONS.Replace(text3, "\n").Trim();
						}
					}
					else
					{
						if (parseFieldDivider)
						{
							text3 = UNESCAPED_SEMICOLONS.Replace(text3, "\n").Trim();
						}
						text3 = CR_LF_SPACE_TAB.Replace(text3, string.Empty);
						text3 = NEWLINE_ESCAPE.Replace(text3, "\n");
						text3 = VCARD_ESCAPES.Replace(text3, "$1");
					}
					if (list2 == null)
					{
						List<string> list3 = new List<string>(1);
						list3.Add(text3);
						list.Add(list3);
					}
					else
					{
						list2.Insert(0, text3);
						list.Add(list2);
					}
					num++;
				}
				else
				{
					num++;
				}
			}
			return list;
		}

		private static string decodeQuotedPrintable(string value, string charset)
		{
			int length = value.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			MemoryStream memoryStream = new MemoryStream();
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				switch (c)
				{
				case '=':
				{
					if (i >= length - 2)
					{
						break;
					}
					char c2 = value[i + 1];
					if (c2 != '\r' && c2 != '\n')
					{
						char c3 = value[i + 2];
						int num = ResultParser.parseHexDigit(c2);
						int num2 = ResultParser.parseHexDigit(c3);
						if (num >= 0 && num2 >= 0)
						{
							memoryStream.WriteByte((byte)((num << 4) | num2));
						}
						i += 2;
					}
					break;
				}
				default:
					maybeAppendFragment(memoryStream, charset, stringBuilder);
					stringBuilder.Append(c);
					break;
				case '\n':
				case '\r':
					break;
				}
			}
			maybeAppendFragment(memoryStream, charset, stringBuilder);
			return stringBuilder.ToString();
		}

		private static void maybeAppendFragment(MemoryStream fragmentBuffer, string charset, StringBuilder result)
		{
			if (fragmentBuffer.Length <= 0)
			{
				return;
			}
			byte[] array = fragmentBuffer.ToArray();
			string value;
			if (charset == null)
			{
				value = Encoding.UTF8.GetString(array, 0, array.Length);
			}
			else
			{
				try
				{
					value = Encoding.GetEncoding(charset).GetString(array, 0, array.Length);
				}
				catch (Exception)
				{
					value = Encoding.UTF8.GetString(array, 0, array.Length);
				}
			}
			fragmentBuffer.Seek(0L, SeekOrigin.Begin);
			fragmentBuffer.SetLength(0L);
			result.Append(value);
		}

		internal static List<string> matchSingleVCardPrefixedField(string prefix, string rawText, bool trim, bool parseFieldDivider)
		{
			List<List<string>> list = matchVCardPrefixedField(prefix, rawText, trim, parseFieldDivider);
			return (list != null && list.Count != 0) ? list[0] : null;
		}

		private static string toPrimaryValue(List<string> list)
		{
			return (list != null && list.Count != 0) ? list[0] : null;
		}

		private static string[] toPrimaryValues(ICollection<List<string>> lists)
		{
			if (lists == null || lists.Count == 0)
			{
				return null;
			}
			List<string> list = new List<string>(lists.Count);
			foreach (List<string> list2 in lists)
			{
				string text = list2[0];
				if (!string.IsNullOrEmpty(text))
				{
					list.Add(text);
				}
			}
			return SupportClass.toStringArray(list);
		}

		private static string[] toTypes(ICollection<List<string>> lists)
		{
			if (lists == null || lists.Count == 0)
			{
				return null;
			}
			List<string> list = new List<string>(lists.Count);
			foreach (List<string> list2 in lists)
			{
				string item = null;
				for (int i = 1; i < list2.Count; i++)
				{
					string text = list2[i];
					int num = text.IndexOf('=');
					if (num < 0)
					{
						item = text;
						break;
					}
					if (string.Compare("TYPE", text.Substring(0, num), StringComparison.OrdinalIgnoreCase) == 0)
					{
						item = text.Substring(num + 1);
						break;
					}
				}
				list.Add(item);
			}
			return SupportClass.toStringArray(list);
		}

		private static bool isLikeVCardDate(string value)
		{
			return value == null || VCARD_LIKE_DATE.Match(value).Success;
		}

		private static void formatNames(IEnumerable<List<string>> names)
		{
			if (names == null)
			{
				return;
			}
			foreach (List<string> name in names)
			{
				string text = name[0];
				string[] array = new string[5];
				int num = 0;
				int num2 = 0;
				int num3;
				while (num2 < array.Length - 1 && (num3 = text.IndexOf(';', num)) >= 0)
				{
					array[num2] = text.Substring(num, num3 - num);
					num2++;
					num = num3 + 1;
				}
				array[num2] = text.Substring(num);
				StringBuilder stringBuilder = new StringBuilder(100);
				maybeAppendComponent(array, 3, stringBuilder);
				maybeAppendComponent(array, 1, stringBuilder);
				maybeAppendComponent(array, 2, stringBuilder);
				maybeAppendComponent(array, 0, stringBuilder);
				maybeAppendComponent(array, 4, stringBuilder);
				name.Insert(0, stringBuilder.ToString().Trim());
			}
		}

		private static void maybeAppendComponent(string[] components, int i, StringBuilder newName)
		{
			if (!string.IsNullOrEmpty(components[i]))
			{
				if (newName.Length > 0)
				{
					newName.Append(' ');
				}
				newName.Append(components[i]);
			}
		}
	}
}
