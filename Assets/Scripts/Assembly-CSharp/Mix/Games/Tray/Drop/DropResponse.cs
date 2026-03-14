using Mix.Games.Data;

namespace Mix.Games.Tray.Drop
{
	public class DropResponse : MixGameResponse
	{
		public int Score { get; set; }

		public int[] BonusJumps { get; set; }

		public DropDirection[] JumpsOffPath { get; set; }

		public DropResponse()
		{
			Score = 0;
			BonusJumps = new int[0];
			JumpsOffPath = new DropDirection[0];
		}
	}
}
