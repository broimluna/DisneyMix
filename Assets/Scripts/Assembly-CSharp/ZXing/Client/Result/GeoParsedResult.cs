using System.Globalization;
using System.Text;

namespace ZXing.Client.Result
{
	public sealed class GeoParsedResult : ParsedResult
	{
		public double Latitude { get; private set; }

		public double Longitude { get; private set; }

		public double Altitude { get; private set; }

		public string Query { get; private set; }

		public string GeoURI { get; private set; }

		public string GoogleMapsURI { get; private set; }

		internal GeoParsedResult(double latitude, double longitude, double altitude, string query)
			: base(ParsedResultType.GEO)
		{
			Latitude = latitude;
			Longitude = longitude;
			Altitude = altitude;
			Query = query;
			GeoURI = getGeoURI();
			GoogleMapsURI = getGoogleMapsURI();
			displayResultValue = getDisplayResult();
		}

		private string getDisplayResult()
		{
			StringBuilder stringBuilder = new StringBuilder(20);
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:0.0###########}", Latitude);
			stringBuilder.Append(", ");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:0.0###########}", Longitude);
			if (Altitude > 0.0)
			{
				stringBuilder.Append(", ");
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:0.0###########}", Altitude);
				stringBuilder.Append('m');
			}
			if (Query != null)
			{
				stringBuilder.Append(" (");
				stringBuilder.Append(Query);
				stringBuilder.Append(')');
			}
			return stringBuilder.ToString();
		}

		private string getGeoURI()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("geo:");
			stringBuilder.Append(Latitude);
			stringBuilder.Append(',');
			stringBuilder.Append(Longitude);
			if (Altitude > 0.0)
			{
				stringBuilder.Append(',');
				stringBuilder.Append(Altitude);
			}
			if (Query != null)
			{
				stringBuilder.Append('?');
				stringBuilder.Append(Query);
			}
			return stringBuilder.ToString();
		}

		private string getGoogleMapsURI()
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("http://maps.google.com/?ll=");
			stringBuilder.Append(Latitude);
			stringBuilder.Append(',');
			stringBuilder.Append(Longitude);
			if (Altitude > 0.0)
			{
				double num = Altitude * 3.28;
				int num2 = (int)(num / 1000.0);
				int num3 = 0;
				while (num2 > 1 && num3 < 18)
				{
					num2 >>= 1;
					num3++;
				}
				int value = 19 - num3;
				stringBuilder.Append("&z=");
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}
	}
}
