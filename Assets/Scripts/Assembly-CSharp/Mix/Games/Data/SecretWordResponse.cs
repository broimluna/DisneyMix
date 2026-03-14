namespace Mix.Games.Data
{
	public class SecretWordResponse : MixGameResponse
	{
		private const int MAX_LIVES = 7;

		public int LivesRemaining { get; set; }

		public bool Success { get; set; }

		public SecretWordResponse()
		{
			LivesRemaining = 7;
		}
	}
}
