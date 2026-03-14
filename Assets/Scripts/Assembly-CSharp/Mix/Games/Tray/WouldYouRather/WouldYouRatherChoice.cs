namespace Mix.Games.Tray.WouldYouRather
{
	public class WouldYouRatherChoice
	{
		private const string IMAGE_RELATIVE_PATH = "gogs/would_you_rather/game_assets/en_US/";

		private string mImageUrl;

		private string mBasePath;

		public string Text { get; private set; }

		public string ImageURL
		{
			get
			{
				return mBasePath + mImageUrl;
			}
			private set
			{
				mImageUrl = value;
			}
		}

		public WouldYouRatherChoice(string basePath = "gogs/would_you_rather/game_assets/en_US/")
		{
			mBasePath = basePath;
		}

		public WouldYouRatherChoice(string text, string url, string basePath = "gogs/would_you_rather/game_assets/en_US/")
		{
			Text = text;
			if (url.EndsWith(".unity3d"))
			{
				mImageUrl = url;
			}
			else
			{
				ImageURL = string.Format("{0}_hd.unity3d", url);
			}
			mBasePath = basePath;
		}
	}
}
