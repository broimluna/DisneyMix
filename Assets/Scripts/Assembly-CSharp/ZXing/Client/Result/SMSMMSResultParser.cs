using System.Collections.Generic;

namespace ZXing.Client.Result
{
	internal sealed class SMSMMSResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (text == null || (!text.StartsWith("sms:") && !text.StartsWith("SMS:") && !text.StartsWith("mms:") && !text.StartsWith("MMS:")))
			{
				return null;
			}
			IDictionary<string, string> dictionary = ResultParser.parseNameValuePairs(text);
			string subject = null;
			string body = null;
			bool flag = false;
			if (dictionary != null && dictionary.Count != 0)
			{
				subject = dictionary["subject"];
				body = dictionary["body"];
				flag = true;
			}
			int num = text.IndexOf('?', 4);
			string text2 = ((num >= 0 && flag) ? text.Substring(4, num - 4) : text.Substring(4));
			int num2 = -1;
			List<string> list = new List<string>(1);
			List<string> list2 = new List<string>(1);
			int num3;
			while ((num3 = text2.IndexOf(',', num2 + 1)) > num2)
			{
				string numberPart = text2.Substring(num2 + 1, num3);
				addNumberVia(list, list2, numberPart);
				num2 = num3;
			}
			addNumberVia(list, list2, text2.Substring(num2 + 1));
			return new SMSParsedResult(SupportClass.toStringArray(list), SupportClass.toStringArray(list2), subject, body);
		}

		private static void addNumberVia(ICollection<string> numbers, ICollection<string> vias, string numberPart)
		{
			int num = numberPart.IndexOf(';');
			if (num < 0)
			{
				numbers.Add(numberPart);
				vias.Add(null);
				return;
			}
			numbers.Add(numberPart.Substring(0, num));
			string text = numberPart.Substring(num + 1);
			string item = ((!text.StartsWith("via=")) ? null : text.Substring(4));
			vias.Add(item);
		}
	}
}
