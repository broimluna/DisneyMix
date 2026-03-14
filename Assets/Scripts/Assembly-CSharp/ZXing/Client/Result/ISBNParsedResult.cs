namespace ZXing.Client.Result
{
	public sealed class ISBNParsedResult : ParsedResult
	{
		public string ISBN { get; private set; }

		internal ISBNParsedResult(string isbn)
			: base(ParsedResultType.ISBN)
		{
			ISBN = isbn;
			displayResultValue = isbn;
		}
	}
}
