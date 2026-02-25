namespace Mix.Games.Data
{
	public class MemeGeneratorData : MixGameData
	{
		public override string GameProcessor
		{
			get
			{
				return "memegenerator";
			}
		}

		public string BottomCaption { get; set; }

		public string TopCaption { get; set; }

		public string ImageUrl { get; set; }
	}
}
