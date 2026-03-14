namespace ZXing.Client.Result
{
	public sealed class TextParsedResult : ParsedResult
	{
		public string Text { get; private set; }

		public string Language { get; private set; }

		public TextParsedResult(string text, string language)
			: base(ParsedResultType.TEXT)
		{
			Text = text;
			Language = language;
			displayResultValue = text;
		}
	}
}
