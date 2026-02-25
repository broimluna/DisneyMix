using System.Text;

namespace ZXing.Client.Result
{
	public sealed class TelParsedResult : ParsedResult
	{
		public string Number { get; private set; }

		public string TelURI { get; private set; }

		public string Title { get; private set; }

		public TelParsedResult(string number, string telURI, string title)
			: base(ParsedResultType.TEL)
		{
			Number = number;
			TelURI = telURI;
			Title = title;
			StringBuilder stringBuilder = new StringBuilder(20);
			ParsedResult.maybeAppend(number, stringBuilder);
			ParsedResult.maybeAppend(title, stringBuilder);
			displayResultValue = stringBuilder.ToString();
		}
	}
}
