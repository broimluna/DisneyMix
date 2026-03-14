using System.Collections.Generic;
using System.Text;

namespace ZXing.Client.Result
{
	public class ExpandedProductResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			BarcodeFormat barcodeFormat = result.BarcodeFormat;
			if (barcodeFormat != BarcodeFormat.RSS_EXPANDED)
			{
				return null;
			}
			string text = result.Text;
			string productID = null;
			string sscc = null;
			string lotNumber = null;
			string productionDate = null;
			string packagingDate = null;
			string bestBeforeDate = null;
			string expirationDate = null;
			string weight = null;
			string weightType = null;
			string weightIncrement = null;
			string price = null;
			string priceIncrement = null;
			string priceCurrency = null;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int num = 0;
			while (num < text.Length)
			{
				string text2 = findAIvalue(num, text);
				if (text2 == null)
				{
					return null;
				}
				num += text2.Length + 2;
				string text3 = findValue(num, text);
				num += text3.Length;
				if ("00".Equals(text2))
				{
					sscc = text3;
				}
				else if ("01".Equals(text2))
				{
					productID = text3;
				}
				else if ("10".Equals(text2))
				{
					lotNumber = text3;
				}
				else if ("11".Equals(text2))
				{
					productionDate = text3;
				}
				else if ("13".Equals(text2))
				{
					packagingDate = text3;
				}
				else if ("15".Equals(text2))
				{
					bestBeforeDate = text3;
				}
				else if ("17".Equals(text2))
				{
					expirationDate = text3;
				}
				else if ("3100".Equals(text2) || "3101".Equals(text2) || "3102".Equals(text2) || "3103".Equals(text2) || "3104".Equals(text2) || "3105".Equals(text2) || "3106".Equals(text2) || "3107".Equals(text2) || "3108".Equals(text2) || "3109".Equals(text2))
				{
					weight = text3;
					weightType = ExpandedProductParsedResult.KILOGRAM;
					weightIncrement = text2.Substring(3);
				}
				else if ("3200".Equals(text2) || "3201".Equals(text2) || "3202".Equals(text2) || "3203".Equals(text2) || "3204".Equals(text2) || "3205".Equals(text2) || "3206".Equals(text2) || "3207".Equals(text2) || "3208".Equals(text2) || "3209".Equals(text2))
				{
					weight = text3;
					weightType = ExpandedProductParsedResult.POUND;
					weightIncrement = text2.Substring(3);
				}
				else if ("3920".Equals(text2) || "3921".Equals(text2) || "3922".Equals(text2) || "3923".Equals(text2))
				{
					price = text3;
					priceIncrement = text2.Substring(3);
				}
				else if ("3930".Equals(text2) || "3931".Equals(text2) || "3932".Equals(text2) || "3933".Equals(text2))
				{
					if (text3.Length < 4)
					{
						return null;
					}
					price = text3.Substring(3);
					priceCurrency = text3.Substring(0, 3);
					priceIncrement = text2.Substring(3);
				}
				else
				{
					dictionary[text2] = text3;
				}
			}
			return new ExpandedProductParsedResult(text, productID, sscc, lotNumber, productionDate, packagingDate, bestBeforeDate, expirationDate, weight, weightType, weightIncrement, price, priceIncrement, priceCurrency, dictionary);
		}

		private static string findAIvalue(int i, string rawText)
		{
			char c = rawText[i];
			if (c != '(')
			{
				return null;
			}
			string text = rawText.Substring(i + 1);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c2 in text)
			{
				switch (c2)
				{
				case ')':
					return stringBuilder.ToString();
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					break;
				default:
					return null;
				}
				stringBuilder.Append(c2);
			}
			return stringBuilder.ToString();
		}

		private static string findValue(int i, string rawText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = rawText.Substring(i);
			for (int j = 0; j < text.Length; j++)
			{
				char c = text[j];
				if (c == '(')
				{
					if (findAIvalue(j, text) != null)
					{
						break;
					}
					stringBuilder.Append('(');
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
