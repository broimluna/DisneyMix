namespace ZXing.Client.Result
{
	public class SMTPResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (!text.StartsWith("smtp:") && !text.StartsWith("SMTP:"))
			{
				return null;
			}
			string text2 = text.Substring(5);
			string text3 = null;
			string body = null;
			int num = text2.IndexOf(':');
			if (num >= 0)
			{
				text3 = text2.Substring(num + 1);
				text2 = text2.Substring(0, num);
				num = text3.IndexOf(':');
				if (num >= 0)
				{
					body = text3.Substring(num + 1);
					text3 = text3.Substring(0, num);
				}
			}
			string mailtoURI = "mailto:" + text2;
			return new EmailAddressParsedResult(text2, text3, body, mailtoURI);
		}
	}
}
