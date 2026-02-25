using System.Text.RegularExpressions;

namespace ZXing.Client.Result
{
	internal sealed class EmailDoCoMoResultParser : AbstractDoCoMoResultParser
	{
		private static readonly Regex ATEXT_ALPHANUMERIC = new Regex("\\A(?:[a-zA-Z0-9@.!#$%&'*+\\-/=?^_`{|}~]+)\\z");

		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (!text.StartsWith("MATMSG:"))
			{
				return null;
			}
			string[] array = AbstractDoCoMoResultParser.matchDoCoMoPrefixedField("TO:", text, true);
			if (array == null)
			{
				return null;
			}
			string text2 = array[0];
			if (!isBasicallyValidEmailAddress(text2))
			{
				return null;
			}
			string subject = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("SUB:", text, false);
			string body = AbstractDoCoMoResultParser.matchSingleDoCoMoPrefixedField("BODY:", text, false);
			return new EmailAddressParsedResult(text2, subject, body, "mailto:" + text2);
		}

		internal static bool isBasicallyValidEmailAddress(string email)
		{
			return email != null && ATEXT_ALPHANUMERIC.Match(email).Success && email.IndexOf('@') >= 0;
		}
	}
}
