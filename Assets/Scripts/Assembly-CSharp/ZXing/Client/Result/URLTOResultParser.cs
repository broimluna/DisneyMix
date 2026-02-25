namespace ZXing.Client.Result
{
	internal sealed class URLTOResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (text == null || (!text.StartsWith("urlto:") && !text.StartsWith("URLTO:")))
			{
				return null;
			}
			int num = text.IndexOf(':', 6);
			if (num < 0)
			{
				return null;
			}
			string title = ((num > 6) ? text.Substring(6, num - 6) : null);
			string uri = text.Substring(num + 1);
			return new URIParsedResult(uri, title);
		}
	}
}
