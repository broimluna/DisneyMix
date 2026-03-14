using System.Collections.Generic;

namespace ZXing.Client.Result
{
	internal sealed class EmailAddressResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (text == null)
			{
				return null;
			}
			string text2;
			if (text.ToLower().StartsWith("mailto:"))
			{
				text2 = text.Substring(7);
				int num = text2.IndexOf('?');
				if (num >= 0)
				{
					text2 = text2.Substring(0, num);
				}
				text2 = ResultParser.urlDecode(text2);
				IDictionary<string, string> dictionary = ResultParser.parseNameValuePairs(text);
				string subject = null;
				string body = null;
				if (dictionary != null)
				{
					if (string.IsNullOrEmpty(text2))
					{
						text2 = dictionary["to"];
					}
					subject = dictionary["subject"];
					body = dictionary["body"];
				}
				return new EmailAddressParsedResult(text2, subject, body, text);
			}
			if (!EmailDoCoMoResultParser.isBasicallyValidEmailAddress(text))
			{
				return null;
			}
			text2 = text;
			return new EmailAddressParsedResult(text2, null, null, "mailto:" + text2);
		}
	}
}
