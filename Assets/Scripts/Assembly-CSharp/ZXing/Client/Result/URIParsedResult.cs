using System.Text;
using System.Text.RegularExpressions;

namespace ZXing.Client.Result
{
	public sealed class URIParsedResult : ParsedResult
	{
		private static readonly Regex USER_IN_HOST = new Regex(":/*([^/@]+)@[^/]+");

		public string URI { get; private set; }

		public string Title { get; private set; }

		public bool PossiblyMaliciousURI { get; private set; }

		public URIParsedResult(string uri, string title)
			: base(ParsedResultType.URI)
		{
			URI = massageURI(uri);
			Title = title;
			PossiblyMaliciousURI = USER_IN_HOST.Match(URI).Success;
			StringBuilder stringBuilder = new StringBuilder(30);
			ParsedResult.maybeAppend(Title, stringBuilder);
			ParsedResult.maybeAppend(URI, stringBuilder);
			displayResultValue = stringBuilder.ToString();
		}

		private static string massageURI(string uri)
		{
			int num = uri.IndexOf(':');
			if (num < 0)
			{
				uri = "http://" + uri;
			}
			else if (isColonFollowedByPortNumber(uri, num))
			{
				uri = "http://" + uri;
			}
			return uri;
		}

		private static bool isColonFollowedByPortNumber(string uri, int protocolEnd)
		{
			int num = protocolEnd + 1;
			int num2 = uri.IndexOf('/', num);
			if (num2 < 0)
			{
				num2 = uri.Length;
			}
			return ResultParser.isSubstringOfDigits(uri, num, num2 - num);
		}
	}
}
