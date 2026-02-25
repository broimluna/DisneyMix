namespace ZXing.Client.Result
{
	public class SMSTOMMSTOResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (!text.StartsWith("smsto:") && !text.StartsWith("SMSTO:") && !text.StartsWith("mmsto:") && !text.StartsWith("MMSTO:"))
			{
				return null;
			}
			string text2 = text.Substring(6);
			string body = null;
			int num = text2.IndexOf(':');
			if (num >= 0)
			{
				body = text2.Substring(num + 1);
				text2 = text2.Substring(0, num);
			}
			return new SMSParsedResult(text2, null, null, body);
		}
	}
}
