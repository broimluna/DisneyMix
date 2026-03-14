namespace Mix.Games.Data
{
	public class WouldYouRatherResponse : MixGameResponse
	{
		public enum WouldYouRatherResponseType
		{
			ChoiceA = 0,
			ChoiceB = 1
		}

		public WouldYouRatherResponseType Choice { get; set; }
	}
}
