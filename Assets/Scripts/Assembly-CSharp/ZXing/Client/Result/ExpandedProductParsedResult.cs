using System.Collections.Generic;

namespace ZXing.Client.Result
{
	public class ExpandedProductParsedResult : ParsedResult
	{
		public static string KILOGRAM = "KG";

		public static string POUND = "LB";

		private readonly string rawText;

		private readonly string productID;

		private readonly string sscc;

		private readonly string lotNumber;

		private readonly string productionDate;

		private readonly string packagingDate;

		private readonly string bestBeforeDate;

		private readonly string expirationDate;

		private readonly string weight;

		private readonly string weightType;

		private readonly string weightIncrement;

		private readonly string price;

		private readonly string priceIncrement;

		private readonly string priceCurrency;

		private readonly IDictionary<string, string> uncommonAIs;

		public string RawText
		{
			get
			{
				return rawText;
			}
		}

		public string ProductID
		{
			get
			{
				return productID;
			}
		}

		public string Sscc
		{
			get
			{
				return sscc;
			}
		}

		public string LotNumber
		{
			get
			{
				return lotNumber;
			}
		}

		public string ProductionDate
		{
			get
			{
				return productionDate;
			}
		}

		public string PackagingDate
		{
			get
			{
				return packagingDate;
			}
		}

		public string BestBeforeDate
		{
			get
			{
				return bestBeforeDate;
			}
		}

		public string ExpirationDate
		{
			get
			{
				return expirationDate;
			}
		}

		public string Weight
		{
			get
			{
				return weight;
			}
		}

		public string WeightType
		{
			get
			{
				return weightType;
			}
		}

		public string WeightIncrement
		{
			get
			{
				return weightIncrement;
			}
		}

		public string Price
		{
			get
			{
				return price;
			}
		}

		public string PriceIncrement
		{
			get
			{
				return priceIncrement;
			}
		}

		public string PriceCurrency
		{
			get
			{
				return priceCurrency;
			}
		}

		public IDictionary<string, string> UncommonAIs
		{
			get
			{
				return uncommonAIs;
			}
		}

		public override string DisplayResult
		{
			get
			{
				return rawText;
			}
		}

		public ExpandedProductParsedResult(string rawText, string productID, string sscc, string lotNumber, string productionDate, string packagingDate, string bestBeforeDate, string expirationDate, string weight, string weightType, string weightIncrement, string price, string priceIncrement, string priceCurrency, IDictionary<string, string> uncommonAIs)
			: base(ParsedResultType.PRODUCT)
		{
			this.rawText = rawText;
			this.productID = productID;
			this.sscc = sscc;
			this.lotNumber = lotNumber;
			this.productionDate = productionDate;
			this.packagingDate = packagingDate;
			this.bestBeforeDate = bestBeforeDate;
			this.expirationDate = expirationDate;
			this.weight = weight;
			this.weightType = weightType;
			this.weightIncrement = weightIncrement;
			this.price = price;
			this.priceIncrement = priceIncrement;
			this.priceCurrency = priceCurrency;
			this.uncommonAIs = uncommonAIs;
			displayResultValue = productID;
		}

		public override bool Equals(object o)
		{
			if (!(o is ExpandedProductParsedResult))
			{
				return false;
			}
			ExpandedProductParsedResult expandedProductParsedResult = (ExpandedProductParsedResult)o;
			return equalsOrNull(productID, expandedProductParsedResult.productID) && equalsOrNull(sscc, expandedProductParsedResult.sscc) && equalsOrNull(lotNumber, expandedProductParsedResult.lotNumber) && equalsOrNull(productionDate, expandedProductParsedResult.productionDate) && equalsOrNull(bestBeforeDate, expandedProductParsedResult.bestBeforeDate) && equalsOrNull(expirationDate, expandedProductParsedResult.expirationDate) && equalsOrNull(weight, expandedProductParsedResult.weight) && equalsOrNull(weightType, expandedProductParsedResult.weightType) && equalsOrNull(weightIncrement, expandedProductParsedResult.weightIncrement) && equalsOrNull(price, expandedProductParsedResult.price) && equalsOrNull(priceIncrement, expandedProductParsedResult.priceIncrement) && equalsOrNull(priceCurrency, expandedProductParsedResult.priceCurrency) && equalsOrNull(uncommonAIs, expandedProductParsedResult.uncommonAIs);
		}

		private static bool equalsOrNull(object o1, object o2)
		{
			return (o1 != null) ? o1.Equals(o2) : (o2 == null);
		}

		private static bool equalsOrNull(IDictionary<string, string> o1, IDictionary<string, string> o2)
		{
			if (o1 == null)
			{
				return o2 == null;
			}
			if (o1.Count != o2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, string> item in o1)
			{
				if (!o2.ContainsKey(item.Key))
				{
					return false;
				}
				if (!item.Value.Equals(o2[item.Key]))
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			int num = 0;
			num ^= hashNotNull(productID);
			num ^= hashNotNull(sscc);
			num ^= hashNotNull(lotNumber);
			num ^= hashNotNull(productionDate);
			num ^= hashNotNull(bestBeforeDate);
			num ^= hashNotNull(expirationDate);
			num ^= hashNotNull(weight);
			num ^= hashNotNull(weightType);
			num ^= hashNotNull(weightIncrement);
			num ^= hashNotNull(price);
			num ^= hashNotNull(priceIncrement);
			num ^= hashNotNull(priceCurrency);
			return num ^ hashNotNull(uncommonAIs);
		}

		private static int hashNotNull(object o)
		{
			return (o != null) ? o.GetHashCode() : 0;
		}
	}
}
