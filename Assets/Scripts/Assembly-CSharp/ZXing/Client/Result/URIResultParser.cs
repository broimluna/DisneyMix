using System.Text.RegularExpressions;

namespace ZXing.Client.Result
{
	internal sealed class URIResultParser : ResultParser
	{
		private static readonly Regex URL_WITH_PROTOCOL_PATTERN = new Regex("[a-zA-Z0-9]{2,}:");

		private static readonly Regex URL_WITHOUT_PROTOCOL_PATTERN = new Regex("([a-zA-Z0-9\\-]+\\.)+[a-zA-Z]{2,}(:\\d{1,5})?(/|\\?|$)");

		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (text.StartsWith("URL:") || text.StartsWith("URI:"))
			{
				return new URIParsedResult(text.Substring(4).Trim(), null);
			}
			text = text.Trim();
			return (!isBasicallyValidURI(text)) ? null : new URIParsedResult(text, null);
		}

		internal static bool isBasicallyValidURI(string uri)
		{
			if (uri.IndexOf(" ") >= 0)
			{
				return false;
			}
			Match match = URL_WITH_PROTOCOL_PATTERN.Match(uri);
			if (match.Success && match.Index == 0)
			{
				return true;
			}
			match = URL_WITHOUT_PROTOCOL_PATTERN.Match(uri);
			return match.Success && match.Index == 0;
		}
	}
}
