namespace ZXing.Client.Result
{
	public class ISBNResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			BarcodeFormat barcodeFormat = result.BarcodeFormat;
			if (barcodeFormat != BarcodeFormat.EAN_13)
			{
				return null;
			}
			string text = result.Text;
			int length = text.Length;
			if (length != 13)
			{
				return null;
			}
			if (!text.StartsWith("978") && !text.StartsWith("979"))
			{
				return null;
			}
			return new ISBNParsedResult(text);
		}
	}
}
