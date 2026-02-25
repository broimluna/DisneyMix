using System.Collections.Generic;

namespace ZXing.Client.Result
{
	internal sealed class AddressBookAUResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (text == null || text.IndexOf("MEMORY") < 0 || text.IndexOf("\r\n") < 0)
			{
				return null;
			}
			string value_Renamed = ResultParser.matchSinglePrefixedField("NAME1:", text, '\r', true);
			string pronunciation = ResultParser.matchSinglePrefixedField("NAME2:", text, '\r', true);
			string[] phoneNumbers = matchMultipleValuePrefix("TEL", 3, text, true);
			string[] emails = matchMultipleValuePrefix("MAIL", 3, text, true);
			string note = ResultParser.matchSinglePrefixedField("MEMORY:", text, '\r', false);
			string text2 = ResultParser.matchSinglePrefixedField("ADD:", text, '\r', true);
			string[] addresses = ((text2 == null) ? null : new string[1] { text2 });
			return new AddressBookParsedResult(ResultParser.maybeWrap(value_Renamed), null, pronunciation, phoneNumbers, null, emails, null, null, note, addresses, null, null, null, null, null, null);
		}

		private static string[] matchMultipleValuePrefix(string prefix, int max, string rawText, bool trim)
		{
			IList<string> list = null;
			for (int i = 1; i <= max; i++)
			{
				string text = ResultParser.matchSinglePrefixedField(prefix + i + ':', rawText, '\r', trim);
				if (text == null)
				{
					break;
				}
				if (list == null)
				{
					list = new List<string>();
				}
				list.Add(text);
			}
			if (list == null)
			{
				return null;
			}
			return SupportClass.toStringArray(list);
		}
	}
}
