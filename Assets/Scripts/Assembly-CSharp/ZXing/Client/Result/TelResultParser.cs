namespace ZXing.Client.Result
{
	internal sealed class TelResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (text == null || (!text.StartsWith("tel:") && !text.StartsWith("TEL:")))
			{
				return null;
			}
			string telURI = ((!text.StartsWith("TEL:")) ? text : ("tel:" + text.Substring(4)));
			int num = text.IndexOf('?', 4);
			string number = ((num >= 0) ? text.Substring(4, num - 4) : text.Substring(4));
			return new TelParsedResult(number, telURI, null);
		}
	}
}
