namespace ZXing.Client.Result
{
	public class WifiResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (!text.StartsWith("WIFI:"))
			{
				return null;
			}
			string text2 = ResultParser.matchSinglePrefixedField("S:", text, ';', false);
			if (string.IsNullOrEmpty(text2))
			{
				return null;
			}
			string password = ResultParser.matchSinglePrefixedField("P:", text, ';', false);
			string networkEncryption = ResultParser.matchSinglePrefixedField("T:", text, ';', false) ?? "nopass";
			bool result2 = false;
			bool.TryParse(ResultParser.matchSinglePrefixedField("H:", text, ';', false), out result2);
			return new WifiParsedResult(networkEncryption, text2, password, result2);
		}
	}
}
