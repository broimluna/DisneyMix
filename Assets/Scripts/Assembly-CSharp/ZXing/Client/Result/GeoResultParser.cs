using System.Globalization;
using System.Text.RegularExpressions;

namespace ZXing.Client.Result
{
	internal sealed class GeoResultParser : ResultParser
	{
		private static readonly Regex GEO_URL_PATTERN = new Regex("\\A(?:geo:([\\-0-9.]+),([\\-0-9.]+)(?:,([\\-0-9.]+))?(?:\\?(.*))?)\\z", RegexOptions.IgnoreCase);

		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (text == null)
			{
				return null;
			}
			Match match = GEO_URL_PATTERN.Match(text);
			if (!match.Success)
			{
				return null;
			}
			string text2 = match.Groups[4].Value;
			if (string.IsNullOrEmpty(text2))
			{
				text2 = null;
			}
			double result2 = 0.0;
			double result3;
			if (!double.TryParse(match.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out result3))
			{
				return null;
			}
			if (result3 > 90.0 || result3 < -90.0)
			{
				return null;
			}
			double result4;
			if (!double.TryParse(match.Groups[2].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
			{
				return null;
			}
			if (result4 > 180.0 || result4 < -180.0)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(match.Groups[3].Value))
			{
				if (!double.TryParse(match.Groups[3].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out result2))
				{
					return null;
				}
				if (result2 < 0.0)
				{
					return null;
				}
			}
			return new GeoParsedResult(result3, result4, result2, text2);
		}
	}
}
