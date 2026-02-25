using System.Text;

namespace ZXing.Client.Result
{
	public class WifiParsedResult : ParsedResult
	{
		public string Ssid { get; private set; }

		public string NetworkEncryption { get; private set; }

		public string Password { get; private set; }

		public bool Hidden { get; private set; }

		public WifiParsedResult(string networkEncryption, string ssid, string password)
			: this(networkEncryption, ssid, password, false)
		{
		}

		public WifiParsedResult(string networkEncryption, string ssid, string password, bool hidden)
			: base(ParsedResultType.WIFI)
		{
			Ssid = ssid;
			NetworkEncryption = networkEncryption;
			Password = password;
			Hidden = hidden;
			StringBuilder stringBuilder = new StringBuilder(80);
			ParsedResult.maybeAppend(Ssid, stringBuilder);
			ParsedResult.maybeAppend(NetworkEncryption, stringBuilder);
			ParsedResult.maybeAppend(Password, stringBuilder);
			ParsedResult.maybeAppend(hidden.ToString(), stringBuilder);
			displayResultValue = stringBuilder.ToString();
		}
	}
}
