using Mix.Games.Data;

namespace Mix.Games.Tray.MemeGenerator
{
	public class Meme_Generator_Data : MixGameContentData
	{
		public string url { get; set; }

		public string thumb { get; set; }

		public override string worksheetName
		{
			get
			{
				return "meme";
			}
		}
	}
}
