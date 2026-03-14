namespace ZXing.Client.Result
{
	internal sealed class AddressBookDoCoMoResultParser : AbstractDoCoMoResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (text == null || !text.StartsWith("MECARD:"))
			{
				return null;
			}
			string[] array = AbstractDoCoMoResultParser.matchDoCoMoPrefixedField("N:", text, true);
			if (array == null)
			{
				return null;
			}
			string value_Renamed = parseName(array[0]);
			string pronunciation = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("SOUND:", text, true);
			string[] phoneNumbers = AbstractDoCoMoResultParser.matchDoCoMoPrefixedField("TEL:", text, true);
			string[] emails = AbstractDoCoMoResultParser.matchDoCoMoPrefixedField("EMAIL:", text, true);
			string note = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("NOTE:", text, false);
			string[] addresses = AbstractDoCoMoResultParser.matchDoCoMoPrefixedField("ADR:", text, true);
			string text2 = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("BDAY:", text, true);
			if (!ResultParser.isStringOfDigits(text2, 8))
			{
				text2 = null;
			}
			string[] urls = AbstractDoCoMoResultParser.matchDoCoMoPrefixedField("URL:", text, true);
			string org = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("ORG:", text, true);
			return new AddressBookParsedResult(ResultParser.maybeWrap(value_Renamed), null, pronunciation, phoneNumbers, null, emails, null, null, note, addresses, null, org, text2, null, urls, null);
		}

		private static string parseName(string name)
		{
			int num = name.IndexOf(',');
			if (num >= 0)
			{
				return name.Substring(num + 1) + ' ' + name.Substring(0, num);
			}
			return name;
		}
	}
}
