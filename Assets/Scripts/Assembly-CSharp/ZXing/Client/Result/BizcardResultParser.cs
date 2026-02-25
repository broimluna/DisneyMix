using System.Collections.Generic;

namespace ZXing.Client.Result
{
	internal sealed class BizcardResultParser : AbstractDoCoMoResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (text == null || !text.StartsWith("BIZCARD:"))
			{
				return null;
			}
			string firstName = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("N:", text, true);
			string lastName = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("X:", text, true);
			string value_Renamed = buildName(firstName, lastName);
			string title = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("T:", text, true);
			string org = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("C:", text, true);
			string[] addresses = AbstractDoCoMoResultParser.matchDoCoMoPrefixedField("A:", text, true);
			string number = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("B:", text, true);
			string number2 = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("M:", text, true);
			string number3 = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("F:", text, true);
			string value_Renamed2 = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("E:", text, true);
			return new AddressBookParsedResult(ResultParser.maybeWrap(value_Renamed), null, null, buildPhoneNumbers(number, number2, number3), null, ResultParser.maybeWrap(value_Renamed2), null, null, null, addresses, null, org, null, title, null, null);
		}

		private static string[] buildPhoneNumbers(string number1, string number2, string number3)
		{
			List<string> list = new List<string>();
			if (number1 != null)
			{
				list.Add(number1);
			}
			if (number2 != null)
			{
				list.Add(number2);
			}
			if (number3 != null)
			{
				list.Add(number3);
			}
			if (list.Count == 0)
			{
				return null;
			}
			return SupportClass.toStringArray(list);
		}

		private static string buildName(string firstName, string lastName)
		{
			if (firstName == null)
			{
				return lastName;
			}
			return (lastName != null) ? (firstName + ' ' + lastName) : firstName;
		}
	}
}
