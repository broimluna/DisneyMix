using ZXing.OneD;

namespace ZXing.Client.Result
{
	internal sealed class ProductResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			BarcodeFormat barcodeFormat = result.BarcodeFormat;
			if (barcodeFormat != BarcodeFormat.UPC_A && barcodeFormat != BarcodeFormat.UPC_E && barcodeFormat != BarcodeFormat.EAN_8 && barcodeFormat != BarcodeFormat.EAN_13)
			{
				return null;
			}
			string text = result.Text;
			if (text == null)
			{
				return null;
			}
			if (!ResultParser.isStringOfDigits(text, text.Length))
			{
				return null;
			}
			string normalizedProductID = ((barcodeFormat != BarcodeFormat.UPC_E || text.Length != 8) ? text : UPCEReader.convertUPCEtoUPCA(text));
			return new ProductParsedResult(text, normalizedProductID);
		}
	}
}
