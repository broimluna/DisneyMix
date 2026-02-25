using System.Text;

namespace ZXing.Client.Result
{
	public sealed class SMSParsedResult : ParsedResult
	{
		public string[] Numbers { get; private set; }

		public string[] Vias { get; private set; }

		public string Subject { get; private set; }

		public string Body { get; private set; }

		public string SMSURI { get; private set; }

		public SMSParsedResult(string number, string via, string subject, string body)
			: this(new string[1] { number }, new string[1] { via }, subject, body)
		{
		}

		public SMSParsedResult(string[] numbers, string[] vias, string subject, string body)
			: base(ParsedResultType.SMS)
		{
			Numbers = numbers;
			Vias = vias;
			Subject = subject;
			Body = body;
			SMSURI = getSMSURI();
			StringBuilder stringBuilder = new StringBuilder(100);
			ParsedResult.maybeAppend(Numbers, stringBuilder);
			ParsedResult.maybeAppend(Subject, stringBuilder);
			ParsedResult.maybeAppend(Body, stringBuilder);
			displayResultValue = stringBuilder.ToString();
		}

		private string getSMSURI()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("sms:");
			bool flag = true;
			for (int i = 0; i < Numbers.Length; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(Numbers[i]);
				if (Vias != null && Vias[i] != null)
				{
					stringBuilder.Append(";via=");
					stringBuilder.Append(Vias[i]);
				}
			}
			bool flag2 = Body != null;
			bool flag3 = Subject != null;
			if (flag2 || flag3)
			{
				stringBuilder.Append('?');
				if (flag2)
				{
					stringBuilder.Append("body=");
					stringBuilder.Append(Body);
				}
				if (flag3)
				{
					if (flag2)
					{
						stringBuilder.Append('&');
					}
					stringBuilder.Append("subject=");
					stringBuilder.Append(Subject);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
