using System.Text;

namespace ZXing.Client.Result
{
	public sealed class EmailAddressParsedResult : ParsedResult
	{
		public string EmailAddress { get; private set; }

		public string Subject { get; private set; }

		public string Body { get; private set; }

		public string MailtoURI { get; private set; }

		internal EmailAddressParsedResult(string emailAddress, string subject, string body, string mailtoURI)
			: base(ParsedResultType.EMAIL_ADDRESS)
		{
			EmailAddress = emailAddress;
			Subject = subject;
			Body = body;
			MailtoURI = mailtoURI;
			StringBuilder stringBuilder = new StringBuilder(30);
			ParsedResult.maybeAppend(EmailAddress, stringBuilder);
			ParsedResult.maybeAppend(Subject, stringBuilder);
			ParsedResult.maybeAppend(Body, stringBuilder);
			displayResultValue = stringBuilder.ToString();
		}
	}
}
