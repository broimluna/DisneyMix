namespace ZXing.Client.Result
{
	public sealed class ProductParsedResult : ParsedResult
	{
		public string ProductID { get; private set; }

		public string NormalizedProductID { get; private set; }

		internal ProductParsedResult(string productID)
			: this(productID, productID)
		{
		}

		internal ProductParsedResult(string productID, string normalizedProductID)
			: base(ParsedResultType.PRODUCT)
		{
			ProductID = productID;
			NormalizedProductID = normalizedProductID;
			displayResultValue = productID;
		}
	}
}
