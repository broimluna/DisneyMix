using System.IO;
using LitJson;

namespace Mix.Games.Localization
{
	public class MixStandaloneLocalizer : MonoSingleton<MixStandaloneLocalizer>
	{
		public static string NO_TOKEN = "No token!";

		private static string LOC_FILE_PATH = "CDNAssets/localization/en_US.1_3.json";

		private JsonData data;

		public void Init()
		{
			string text = File.ReadAllText(LOC_FILE_PATH);
			if (text != null)
			{
				data = JsonMapper.ToObject(text);
			}
		}

		public string GetLocalizedContent(string token)
		{
			if (data == null)
			{
				Init();
			}
			if (data.Contains(token))
			{
				return data[token].ToString();
			}
			return NO_TOKEN;
		}
	}
}
